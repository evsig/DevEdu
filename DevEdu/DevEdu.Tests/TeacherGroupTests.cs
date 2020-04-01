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
    [TestFixture]
    public class TeacherGroupTests
    {

        List<City> listCity = new List<City>();
        List<User> listUser = new List<User>();
        List<CourseProgram> listCourseProgram = new List<CourseProgram>();
        List<Group> listGroup = new List<Group>();
        List<Course> listCourses = new List<Course>();
        List<TeacherGroup> listTeacherGroup = new List<TeacherGroup>();

        CourseStorage courseStorage;
        DictionaryStorage dictionaryStorage;
        UserStorage userStorage ;
        GroupStorage groupStorage;

        public async Task Setup()
        {
            listTeacherGroup.AddRange(TeacherGroupMock.listTeacherGroup);


            listCity = DictionaryMocks.cityMock;
            listUser = UserMock.listUsers;
            listCourseProgram = CourseProgramMock.listCourseProgram;
            listGroup = GroupMock.groupMocks;
            listCourses = CourseMock.listCourse;



            for (int i = 0; i < listTeacherGroup.Count; i++)
            {
                listCity[i].Id = await dictionaryStorage.CityAddOrUpdate(listCity[i]);
                listUser[i].CityId = (int)listCity[i].Id;
                listUser[i].Id = await userStorage.UserAddOrUpdate(listUser[i]);

                listCourses[i].Id = await courseStorage.CourseAddOrUpdate(listCourses[i]);
                listCourseProgram[i].CourseId = (int)listCourses[i].Id;
                listCourseProgram[i].Id = await courseStorage.CourseProgramAddOrUpdate(listCourseProgram[i]);
                listGroup[i].CourseProgramId = (int)listCourseProgram[i].Id;
                listGroup[i].Id = await groupStorage.GroupAddOrUpdate(listGroup[i]);
                listTeacherGroup[i].UserId = (int)listCity[i].Id;
                listTeacherGroup[i].GroupId= (int)listGroup[i].Id;
                listTeacherGroup[i].Id = await groupStorage.TeacherGroupInsert(listTeacherGroup[i]);

            }

        }

        [Test]
        public async Task TeacherGroupTest()
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
                await Setup();
                await TestSelects();
                foreach (TeacherGroup teacherGroupToDelete in listTeacherGroup)
                {
                    await TestEntityDelete(teacherGroupToDelete);
                }

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
            List<TeacherGroup> teacherGroupActual = await groupStorage.TeacherGroupGetAll();
            for (int i = 0; i < listTeacherGroup.Count; i++)
            {
                Assert.Contains(listTeacherGroup[i], teacherGroupActual);
            }
            TeacherGroup actualteacherGroup = await groupStorage.TeacherGroupGetById((int)listTeacherGroup[0].Id);
            Assert.AreEqual(listTeacherGroup[0], actualteacherGroup);

        }

        private async Task TestEntityDelete(TeacherGroup teacherGroupToDelete)
        {
            List<TeacherGroup> teacherGroupActual = await groupStorage.TeacherGroupGetAll();
            await groupStorage.TeacherGroupDelete(TeacherGroupMock.listTeacherGroup[0].GroupId, TeacherGroupMock.listTeacherGroup[0].UserId);
            List<TeacherGroup> actualTeacherGroupAfterDelete = await groupStorage.TeacherGroupGetAll();
            Assert.AreEqual(teacherGroupActual.Count - 1, actualTeacherGroupAfterDelete.Count);
        }

       
    }
}