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
using System.Text;
using System.Threading.Tasks;

namespace DevEdu.Tests
{
    class UserTests
    {
        List<User> listUser = new List<User>();
        List<City> listCity = new List<City>();
        UserStorage userStorage = new UserStorage(DBConnection.connString);
        DictionaryStorage dictionaryStorage = new DictionaryStorage(DBConnection.connString);

        public async Task Setup()
        {
            listUser.AddRange(UserMock.listUsers.Select(x => (User)x.Clone()));
            listCity.AddRange(DictionaryMocks.cityMock.Select(x => (City)x.Clone()));

            for (int i = 0; i < listCity.Count; i++)
            {
                City newCity = (City)DictionaryMocks.cityMock[i].Clone();
                listCity.Add(newCity);
                int cityId = (int)(await dictionaryStorage.CityAddOrUpdate(listCity[i]));
                listCity[i].Id = cityId;


                User newUser = (User)UserMock.listUsers[i].Clone();
                listUser.Add(newUser);
                listUser[i].CityId = cityId;
                int? id = await userStorage.UserAddOrUpdate(listUser[i]);
                listUser[i].Id = id;
                listUser[i].City = listCity[i];
                listUser[i].Roles = null;
                listUser[i].StudentGroup = null;
                listUser[i].Skills = null;

                User tempUser = await userStorage.UserSelectById(id);
                listUser[i].RegistrationDate = tempUser.RegistrationDate;
            }
        }

        [Test]
        public async Task UserTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            userStorage = new UserStorage(connection, transaction);
            dictionaryStorage = new DictionaryStorage(connection, transaction);
           

            try
            {
                await Setup();
                await TestSelects();
                await TestBioUpdate();
                await TestUpdate();

                foreach (User userToDelete in listUser)
                {
                    TestEntityDelete(userToDelete);
                }

                await TearDown();

                transaction.Commit();
            }

           
            catch
            {
                transaction.Rollback();
                throw new System.Exception();
            }
            
        }

        private async Task TestSelects()
        {
            List<User> userActual = await userStorage.UserSelectAll();
            for (int i = 0; i < listUser.Count; i++)
            {
                User user12 = listUser[i];

                Assert.Contains(user12, userActual);
            }
            User actualUser = await userStorage.UserSelectById((int)listUser[0].Id);

            Assert.AreEqual(listUser[0], actualUser);
        }
        private async void TestEntityDelete(User userToDelete)
        {
            List<User> actualUsers = await userStorage.UserSelectAll();
            userStorage.UserDeleteById((int)userToDelete.Id);
            List<User> actualUsersAfterDelete = await userStorage.UserSelectAll();

            Assert.AreEqual(actualUsers.Count - 1, actualUsersAfterDelete.Count);
        }

        private async Task TestUpdate()
        {
            listUser[0].FirstName = "Иван";
            listUser[0].LastName = "Иванов";
            listUser[0].Patronymic = "Иванович";
            listUser[0].BirthDate = new DateTime(1799, 10, 10);
            listUser[0].Email = "myEmail@mail.ru";
            listUser[0].Phone = "7890";
            listUser[0].Password = "test";
            listUser[0].Login = "test";
            listUser[0].Bio = "Gray";
            listUser[0].Photo = @"C:\myMewPhoto.gif";
            listUser[0].CityId = (int)DictionaryMocks.cityMock[1].Id;

            await userStorage.UserAddOrUpdate(listUser[0]);
            User actualUser2 = await userStorage.UserSelectById(listUser[0].Id);
            Assert.AreEqual(listUser[0], actualUser2);

            User actualUser3 = await userStorage.UserSelectById(listUser[2].Id);
            Assert.AreEqual(listUser[2], actualUser3);
        }
        private async Task TestBioUpdate()
        {
            listUser[0].Bio = "testUpdate";

            userStorage.UpdateStudentBioByUserId(listUser[0]);
            User actualUser2 = await userStorage.UserSelectById(listUser[0].Id);
            Assert.AreEqual(listUser[0], actualUser2);

            User actualUser3 = await userStorage.UserSelectById(listUser[2].Id);
            Assert.AreEqual(listUser[2], actualUser3);
        }
        private async Task TearDown()
        {
            foreach (City listCity1 in listCity)
            {
                List<City> list = dictionaryStorage.CitiesGetAll().Result;
                await dictionaryStorage.CityDelete((int)listCity1.Id);
                List<City> list1 = dictionaryStorage.CitiesGetAll().Result;
                Assert.AreEqual(list.Count - 1, list1.Count);

                Assert.False(list1.Contains(listCity1));
            }
        }
    }
}