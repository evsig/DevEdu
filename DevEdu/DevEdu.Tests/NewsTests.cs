using DevEdu.Data;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Tests
{
    class NewsTests
    {
        List<News> listNews;
        User user1;
        User user2;
        User user3;
        Group group;
        Course course;
        CourseProgram courseProgram;
        City city;

        NewsStorage newsStorage;
        UserStorage userStorage;
        GroupStorage groupStorage;
        DictionaryStorage dictionaryStorage;
        CourseStorage courseStorage;

        [Test]
        public async Task NewsTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            newsStorage = new NewsStorage(connection, transaction);
            userStorage = new UserStorage(connection, transaction);
            groupStorage = new GroupStorage(connection, transaction);
            dictionaryStorage = new DictionaryStorage(connection, transaction);
            courseStorage = new CourseStorage(connection, transaction);

            try
            {
                await Setup();
                await TestSelects();
                await TestUpdate();

                foreach (News newsToDelete in listNews)
                {
                    await TestDeleteNews(newsToDelete);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally { 
                transaction.Rollback();
            }
            
        }

        private async Task Setup()
        {
            listNews = new List<News>(NewsMock.listNews.Select(x => (News)x.Clone()));
            user1 = (User)UserMock.listUsers[0].Clone();
            user2 = (User)UserMock.listUsers[1].Clone();
            group = (Group)GroupMock.groupMocks[0].Clone();
            course = (Course)CourseMock.listCourse[0].Clone();
            courseProgram = (CourseProgram)CourseProgramMock.listCourseProgram[0].Clone();
            city = (City)DictionaryMocks.cityMock[0].Clone();

            var createdCityId = await dictionaryStorage.CityAddOrUpdate(city);
            user1.CityId = (int)createdCityId;
            user2.CityId = (int)createdCityId;

            listNews[0].AuthorId = await userStorage.UserAddOrUpdate(user1);
            listNews[0].RecipientID = await userStorage.UserAddOrUpdate(user2);

            listNews[1].AuthorId = await userStorage.UserAddOrUpdate(user2);
            courseProgram.CourseId = await courseStorage.CourseAddOrUpdate(course);
            group.CourseProgramId = await courseStorage.CourseProgramAddOrUpdate(courseProgram);
            listNews[1].GroupID = await groupStorage.GroupAddOrUpdate(group);

            for (int i = 0; i < listNews.Count; i++)
            {
                News inputmodel = new News()
                {
                    AuthorId = listNews[i].AuthorId,
                    Content = listNews[i].Content,
                    GroupID = listNews[i].GroupID,
                    Id = listNews[i].Id,
                    PublicationDate = listNews[i].PublicationDate,
                    RecipientID = listNews[i].RecipientID,
                    Title = listNews[i].Title
                };
                int id = await newsStorage.NewsAddOrUpdate(inputmodel);
                listNews[i].Id = id;
                listNews[i].PublicationDate = (await newsStorage.NewsGetById(id)).PublicationDate;
            }
        }

        private async Task TestSelects()
        {
            List<News> newsActual = await newsStorage.NewsGetAll();
            for (int i = 0; i < listNews.Count; i++)
            {
                Assert.Contains(listNews[i], newsActual);
            }

            News actualNews = await newsStorage.NewsGetById((int)listNews[0].Id);
            Assert.AreEqual(listNews[0], actualNews);
        }

        private async Task TestUpdate()
        {
            News updateNews = listNews[0];
            updateNews.Content = "Гендельф умер";
            News inputmodel = new News(){
                AuthorId = updateNews.AuthorId,
                Content = updateNews.Content,
                GroupID = updateNews.GroupID,
                Id = updateNews.Id,
                PublicationDate = updateNews.PublicationDate,
                RecipientID = updateNews.RecipientID,
                Title = updateNews.Title
            };
            await newsStorage.NewsAddOrUpdate(inputmodel);
            News updateList1 = await newsStorage.NewsGetById((int)updateNews.Id);

            Assert.AreEqual(updateNews, updateList1);

            News updateNews1 = await newsStorage.NewsGetById((int)listNews[1].Id); ;
            Assert.AreEqual(listNews[1], updateNews1);
        }

        private async Task TestDeleteNews(News newsToDelete)
        {

            List<News> list = await newsStorage.NewsGetAll();
            await newsStorage.NewsDelete((int)newsToDelete.Id);
            List<News> list1 = await newsStorage.NewsGetAll();
            Assert.AreEqual(list.Count - 1, list1.Count);

            Assert.False(list1.Contains(newsToDelete));

        }
    }
}
