using NUnit.Framework;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
using System.Collections.Generic;
using DevEdu.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;

namespace DevEdu.Tests
{
    [TestFixture]
    public class JournalTests
    {
        List<Journal> listJournal = new List<Journal>();
        List<City> listCity = new List<City>();
        List<User> listUser = new List<User>();
        List<CourseProgram> listCourseProgram = new List<CourseProgram>();
        List<Group> listGroup = new List<Group>();
        List<Lesson> listLesson = new List<Lesson>();
        List<Course> listCourse = new List<Course>();

        CourseStorage courseStorage;
        GroupStorage groupStorage;
        DictionaryStorage dictionaryStorage;
        UserStorage userStorage;
        LessonStorage lessonStorage;

        [Test]
        public async Task JournalTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();

            courseStorage = new CourseStorage(connection, transaction);
            groupStorage = new GroupStorage(connection, transaction);
            dictionaryStorage = new DictionaryStorage(connection, transaction);
            userStorage = new UserStorage(connection, transaction);
            lessonStorage = new LessonStorage(connection, transaction);

            try
            {
                await Setup();
                await TestSelects();
                await TestUpdate();
                foreach (Journal journalToDelete in listJournal)
                {
                    await TestEntityDelete(journalToDelete);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                transaction.Rollback();
            }

        }

        #region Setup
        private async Task Setup()
        {
            listJournal.AddRange(JournalMock.journalMocks);

            listCity.AddRange(DictionaryMocks.cityMock.Select(m => (City)m.Clone()));
            listUser.AddRange(UserMock.listUsers.Select(u => (User)u.Clone()));
            listCourseProgram.AddRange(CourseProgramMock.listCourseProgram.Select(m => (CourseProgram)m.Clone()));
            listGroup.AddRange(GroupMock.groupMocks.Select(m => (Group)m.Clone()));
            listLesson.AddRange(LessonMock.lessonMocks.Select(m => (Lesson)m.Clone()));
            listCourse.AddRange(CourseMock.listCourse.Select(m => (Course)m.Clone()));

            for (int i = 0; i < listJournal.Count; i++)
            {
                listCity[i].Id = await dictionaryStorage.CityAddOrUpdate(listCity[i]);
                listUser[i].CityId = (int)listCity[i].Id;
                listUser[i].Id = await userStorage.UserAddOrUpdate(listUser[i]);

                listCourse[i].Id = await courseStorage.CourseAddOrUpdate(listCourse[i]);
                listCourseProgram[i].CourseId = (int)listCourse[i].Id;
                listCourseProgram[i].Id = await courseStorage.CourseProgramAddOrUpdate(listCourseProgram[i]);
                listGroup[i].CourseProgramId = (int)listCourseProgram[i].Id;
                listGroup[i].Id = await groupStorage.GroupAddOrUpdate(listGroup[i]);
                listLesson[i].GroupId = (int)listGroup[i].Id;

                listLesson[i].Id = await lessonStorage.AddOrUpdateLesson(listLesson[i]);
                listJournal[i].LessonID = (int)listLesson[i].Id;
                listJournal[i].UserId = (int)listUser[i].Id;
                listJournal[i].Id = await lessonStorage.AddOrUpdateJournal(listJournal[i]);
            }
        }

        #endregion
        private async Task TestSelects()
        {
            List<Journal> actualJournal = await lessonStorage.GetAllJournal();
            for (int i = 0; i < listJournal.Count; i++)
            {
                Assert.Contains(listJournal[i], actualJournal);
            }
            Journal actual = await lessonStorage.GetJournalById((int)listJournal[0].Id);
            Assert.AreEqual(listJournal[0], actual);
        }

        private async Task TestUpdate()
        {
            Journal journal = listJournal[0];
            journal.Id = listJournal[0].Id;
            journal.Feadback = "abyrvalg";
            journal.IsAbsent = false;
            journal.AbsentReason = "aseae";
            await lessonStorage.AddOrUpdateJournal(journal);
            Journal actualJournal = await lessonStorage.GetJournalById((int)journal.Id);
            Assert.AreEqual(journal, actualJournal);

            Journal secondAactualJournal = await lessonStorage.GetJournalById((int)listJournal[1].Id);
            Assert.AreEqual(listJournal[1], secondAactualJournal);
        }

        private async Task TestEntityDelete(Journal journalToDelete)
        {
            List<Journal> actualJournals = await lessonStorage.GetAllJournal();
            await lessonStorage.DeleteJournal((int)journalToDelete.Id);
            List<Journal> actualJournals1 = await lessonStorage.GetAllJournal();
            Assert.AreEqual(actualJournals.Count - 1, actualJournals1.Count);

            for (int i = 0; i < actualJournals1.Count; i++)
            {
                Assert.AreNotEqual(actualJournals1[i].Id, (int)journalToDelete.Id);
            }
        }
    }
}