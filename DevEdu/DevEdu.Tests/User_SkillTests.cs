using DevEdu.Data;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Threading.Tasks;

namespace DevEdu.Tests
{
    class User_SkillTests
    {
        DictionaryStorage dictionaryStorage;
        UserStorage userStorage;
        CourseStorage courseStorage;

        List<CourseProgramSkill> listCourseProgramSkill;

        #region Setup
        public async void Setup()
        {

            DictionaryMocks.cityMock[0].Id = dictionaryStorage.CityAddOrUpdate(DictionaryMocks.cityMock[0]).Result;
            UserMock.listUsers[0].CityId = (int)DictionaryMocks.cityMock[0].Id;
            UserMock.listUsers[0].Id = await userStorage.UserAddOrUpdate(UserMock.listUsers[0]);
            CourseMock.listCourse[0].Id = await courseStorage.CourseAddOrUpdate(CourseMock.listCourse[0]);
            CourseProgramMock.listCourseProgram[0].CourseId = (int)CourseMock.listCourse[0].Id;
            CourseProgramMock.listCourseProgram[0].Id = await courseStorage.CourseProgramAddOrUpdate(CourseProgramMock.listCourseProgram[0]);
            listCourseProgramSkill = CourseProgramSkillMocks.listCourseProgramSkill;
            listCourseProgramSkill[0].CourseProgramId = (int)CourseProgramMock.listCourseProgram[0].Id;
            listCourseProgramSkill[0].Id = await courseStorage.CourseProgramSkillAddOrUpdate(listCourseProgramSkill[0]);
            UserSkillMock.listUserSkill[0].UserId = (int)UserMock.listUsers[0].Id;
            UserSkillMock.listUserSkill[0].SkillId = (int)listCourseProgramSkill[0].Id;
            UserSkillMock.listUserSkill[0].Id = await userStorage.UserSkillAdd(UserSkillMock.listUserSkill[0]);

        }
        #endregion

        [Test]
        public void User_SkillTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            dictionaryStorage = new DictionaryStorage(connection, transaction);
            userStorage = new UserStorage(connection, transaction);
            courseStorage = new CourseStorage(connection, transaction);

            try
            {
                Setup();
                TestSelects();
                TestDelete();
                //TestUpdate();

                //foreach (Journal journalToDelete in listJournal)
                //{
                //    TestEntityDelete(journalToDelete);
                //}

                ClearShit();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw new Exception();
            }
            
        }

        private async Task TestSelects()
        {
            /* List<Journal> actualJournal1 = lessonStorage.GetAllJournal();
             for (int i = 0; i < listJournal.Count; i++)
             {
                 Assert.Contains(listJournal[i], actualJournal1);
             }*/

            //Journal actual = lessonStorage.GetJournalById((int)listJournal[0].Id);
            //Assert.AreEqual(listJournal[0], actual);
            Assert.IsTrue(await userStorage.IsUserSkillPresent(UserSkillMock.listUserSkill[0]));

        }

        private async Task TestDelete()
        {
            await userStorage.UserSkillDelete(UserSkillMock.listUserSkill[0]);
            Assert.IsFalse(await userStorage.IsUserSkillPresent(UserSkillMock.listUserSkill[0]));
            //List<Journal> actualJournals = lessonStorage.GetAllJournal();
            //lessonStorage.DeleteJournal((int)journalToDelete.Id);
            //List<Journal> actualJournals1 = lessonStorage.GetAllJournal();

            //Assert.AreEqual(actualJournals.Count - 1, actualJournals1.Count);

            // проверить, что в actualJournals1 нет именно той строки, которую удаляли
        }
        private void ClearShit()
        {
            //удаление данных, созданных для теста
            
            userStorage.UserDeleteById((int)UserMock.listUsers[0].Id);
            dictionaryStorage.CityDelete(DictionaryMocks.cityMock[0].Id);
            //закомментила пока не починят метод DeleteCourseProgram - пишет что соединение с базой уже открыто
            courseStorage.CourseProgramSkillDelete(listCourseProgramSkill[0]);
            courseStorage.CourseProgramDelete((int)CourseProgramMock.listCourseProgram[0].Id);
            courseStorage.CourseDelete((int)CourseMock.listCourse[0].Id);
        }


    }
}
