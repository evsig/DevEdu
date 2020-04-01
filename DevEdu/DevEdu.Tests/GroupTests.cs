//using NUnit.Framework;
//using DevEdu.Data.Models;
//using DevEdu.Data.Storages;
//using DevEdu.Tests.Mocks;
//using System.Collections.Generic;
//using DevEdu.Data;
//using System;
//using System.Data;
//using System.Data.SqlClient;
//using System.Threading.Tasks;

//namespace DevEdu.Tests
//{
//    [TestFixture]
//    public class GroupTests
//    {
//        List<Course> listCourse = new List<Course>();
//        List<CourseProgram> listCourseProgram = new List<CourseProgram>();
//        List<Group> listGroup = new List<Group>();
//        GroupStorage groupStorage;

//        #region Setup
//        public void Setup()
//        {
//            int length = CourseMock.listCourse.Count;
//            for (int i = 0; i < length; i++)
//            {
//                Course newCourse = (Course)CourseMock.listCourse[i].Clone();
//                newCourse.Id = null;
//                listCourse.Add(newCourse);
//                int courseId = (int)courseStorage.CourseAddOrUpdate(listCourse[i]).Result;
//                listCourse[i].Id = courseId;


//                CourseProgram newCourseProgram = (CourseProgram)CourseProgramMock.listCourseProgram[i].Clone();
//                newCourseProgram.Id = null;
//                newCourseProgram.CourseId = (int)listCourse[i].Id;
//                listCourseProgram.Add(newCourseProgram);
//                int courseProgramId = (int)courseStorage.CourseProgramAddOrUpdate(listCourseProgram[i]).Result;
//                listCourseProgram[i].Id = courseProgramId;
           
//            }


//            foreach (Group item in GroupMock.groupMocks)
//            {
//                Group newGroup = (Group)item.Clone();
//                newGroup.Id = null;
//                listGroup.Add(newGroup);
//            }
//            for (int i = 0; i < listGroup.Count; i++)
//            {
//                listGroup[i].CourseProgramId = (int)listCourseProgram[i].Id;
//                int id = (int)groupStorage.GroupAddOrUpdate(listGroup[i]).Result;
//                listGroup[i].Id = id;
//            }
//        }
//        #endregion

//        [Test]
//        public async Task GroupTest()
//        {
//            using IDbConnection connection = new SqlConnection(DBConnection.connString);
//            connection.Open();
//            IDbTransaction transaction = connection.BeginTransaction();
//            groupStorage = new GroupStorage(connection, transaction);

//            try
//            {
//                Setup();
//                TestSelects();
//                TestUpdate();

//                foreach (Group groupToDelete in listGroup)
//                {
//                    TestEntityDelete(groupToDelete);
//                }

//                ClearAllMocks();

//                transaction.Commit();
//            }
//            catch
//            {
//                transaction.Rollback();
//                throw new Exception();
//            }
            
//        }

//        private void TestSelects()
//        {
//            List<Group> actualGroup = groupStorage.GroupGetAll().Result;
//            for (int i = 0; i < listGroup.Count; i++)
//            {
//                Assert.Contains(listGroup[i], actualGroup);
//            }

//            Group actual = groupStorage.GroupGetById((int)listGroup[0].Id).Result;
//            Assert.AreEqual(listGroup[0], actual);
//        }

//        private async Task TestUpdate()
//        {
//            Group group = listGroup[0];
//            group.Id = listGroup[0].Id;
//            group.StartDate = new DateTime(2020, 10, 10);
//            group.EndDate = new DateTime(2021, 10, 10);
//            group.TimeStart = new TimeSpan(9, 35, 0);
//            group.Duration = 10;
//            group.CourseProgramId = listGroup[0].CourseProgramId;

//            await groupStorage.GroupAddOrUpdate(group);
//            Group actualGroup = groupStorage.GroupGetById((int)group.Id).Result;
//            Assert.AreEqual(group, actualGroup);

//            Group secondAactualGroup = groupStorage.GroupGetById((int)listGroup[1].Id).Result;
//            Assert.AreEqual(listGroup[1], secondAactualGroup);
//        }

//        private void TestEntityDelete(Group groupToDelete)
//        {
//            List<Group> actualGroups = groupStorage.GroupGetAll().Result;
//            groupStorage.GroupDelete((int)groupToDelete.Id);
//            List<Group> actualGroups1 = groupStorage.GroupGetAll().Result;

//            Assert.AreEqual(actualGroups.Count - 1, actualGroups1.Count);

//            for (int i = 0; i < listGroup.Count; i++)
//            {
//                Assert.AreNotEqual(actualGroups1[i].Id, (int)groupToDelete.Id);
//            }
//        }

//        private async Task ClearAllMocks()
//        {
//            for (int i = 0; i < listGroup.Count; i++)
//            {
//                groupStorage.GroupDelete((int)listGroup[i].Id);
//                await courseStorage.CourseProgramDelete((int)listCourseProgram[i].Id);
//                await courseStorage.CourseDelete((int)listCourse[i].Id);
//            }
//        }
//    }
//}