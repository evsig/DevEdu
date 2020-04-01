using DevEdu.Data;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DevEdu.Tests
{
    [TestFixture]
    public class StudentGroupTests
    {
        List<StudentGroup> listStudentGroup = new List<StudentGroup>();
        
        CourseStorage courseStorage;
        DictionaryStorage dictionaryStorage;
        UserStorage userStorage;
        GroupStorage groupStorage;

        public async void Setup()
        {
           listStudentGroup.AddRange(StudentGroupMock.listStudentGroup);

            for (int i = 0; i < listStudentGroup.Count; i++)
            {
                DictionaryMocks.cityMock[i].Id = dictionaryStorage.CityAddOrUpdate(DictionaryMocks.cityMock[i]).Result;
                UserMock.listUsers[i].CityId = (int)DictionaryMocks.cityMock[i].Id;
                UserMock.listUsers[i].Id = await userStorage.UserAddOrUpdate(UserMock.listUsers[i]);
                CourseMock.listCourse[i].Id = await courseStorage.CourseAddOrUpdate(CourseMock.listCourse[i]);
                CourseProgramMock.listCourseProgram[i].CourseId = (int)CourseMock.listCourse[i].Id;
                CourseProgramMock.listCourseProgram[i].Id = await courseStorage.CourseProgramAddOrUpdate(CourseProgramMock.listCourseProgram[i]);
                GroupMock.groupMocks[i].CourseProgramId = (int)CourseProgramMock.listCourseProgram[i].Id;
                GroupMock.groupMocks[i].Id = groupStorage.GroupAddOrUpdate(GroupMock.groupMocks[i]).Result;
                StudentGroupMock.listStudentGroup[i].UserId = (int)UserMock.listUsers[i].Id;
                StudentGroupMock.listStudentGroup[i].GroupId = (int)GroupMock.groupMocks[i].Id;
                StudentGroupMock.listStudentGroup[i].Id = groupStorage.StudentGroupInsert(StudentGroupMock.listStudentGroup[i]).Id;
            }
        }
        
        [Test]
        public void StudentGroupTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            courseStorage = new CourseStorage(connection, transaction);
            dictionaryStorage = new DictionaryStorage(connection, transaction);
            userStorage = new UserStorage(connection, transaction);
            groupStorage = new GroupStorage(connection, transaction);

            try
            {
                Setup();
                TestSelects();
                foreach (StudentGroup studentGroupToDelete in listStudentGroup)
                {
                    TestEntityDelete(studentGroupToDelete);
                }
                ClearTest();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw new Exception();
            }
        }


        private async void TestSelects()
        {
            List<StudentGroup> studentGroupActual = await groupStorage.StudentGroupGetAll();
            for (int i = 0; i < listStudentGroup.Count; i++)
            {
                Assert.Contains(listStudentGroup[i], studentGroupActual);
            }
            StudentGroup actualstudentGroup = await groupStorage.StudentGroupGetById((int)listStudentGroup[0].Id);
            Assert.AreEqual(listStudentGroup[0], actualstudentGroup);
        }

        private async void TestEntityDelete(StudentGroup studentGroupToDelete)
        {
            List<StudentGroup> studentGroupActual = await groupStorage.StudentGroupGetAll();
            await groupStorage.StudentGroupDelete(studentGroupToDelete);
            List<StudentGroup> actualStudentGroupAfterDelete = await groupStorage.StudentGroupGetAll();
            Assert.AreEqual(studentGroupActual.Count - 1, actualStudentGroupAfterDelete.Count);
            Assert.False(actualStudentGroupAfterDelete.Contains(studentGroupToDelete));
        }

        private void ClearTest()
        {
            userStorage.UserDeleteById((int)UserMock.listUsers[0].Id);
            dictionaryStorage.CityDelete(DictionaryMocks.cityMock[0].Id);
            groupStorage.GroupDelete((int)GroupMock.groupMocks[0].Id);
            courseStorage.CourseProgramDelete((int)CourseProgramMock.listCourseProgram[0].Id);
        }
    }
}


