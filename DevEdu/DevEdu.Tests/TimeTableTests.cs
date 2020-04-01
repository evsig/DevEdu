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
    public class TimeTableTests
    {
        List<TimeTable> listTimetable = new List<TimeTable>();

        GroupStorage groupStorage;
        LessonStorage lessonStorage;
        CourseStorage courseStorage;

        [Test]
        public void TimeTableTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            groupStorage = new GroupStorage(connection, transaction);
            lessonStorage = new LessonStorage(connection, transaction);
            courseStorage = new CourseStorage(connection, transaction);

            try
            {
                Setup();
                TestSelects();
                TestUpdate();

                foreach (TimeTable timeTableToDelete in listTimetable)
                {
                    TestEntityDelete(timeTableToDelete);
                }

                ClearAllMocks();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw new Exception();
            }
                     
        }

        #region SetUp
        public async void Setup()
        {
            listTimetable.AddRange(TimeTableMock.listTimetable);

            for (int i = 0; i < listTimetable.Count; i++)
            {
                CourseMock.listCourse[i].Id = await courseStorage.CourseAddOrUpdate(CourseMock.listCourse[i]);
                CourseProgramMock.listCourseProgram[i].CourseId = (int)CourseMock.listCourse[i].Id;
                CourseProgramMock.listCourseProgram[i].Id = await courseStorage.CourseProgramAddOrUpdate(CourseProgramMock.listCourseProgram[i]);
                GroupMock.groupMocks[i].CourseProgramId = (int)CourseProgramMock.listCourseProgram[i].Id;
                GroupMock.groupMocks[i].Id = groupStorage.GroupAddOrUpdate(GroupMock.groupMocks[i]).Result;
                listTimetable[i].GroupId = (int)GroupMock.groupMocks[i].Id;
            //    int? id = lessonStorage.AddOrUpdateTimeTable(listTimetable[i]);
          //      listTimetable[i].Id = id;
            }
        }
        #endregion

        private void TestSelects()
        {
            for (int i = 0; i < listTimetable.Count; i++)
            {
              //  TimeTable actualTimetable = lessonStorage.GetTimeTableById((int)listTimetable[i].Id);
            //    Assert.AreEqual(listTimetable[i], actualTimetable);
            }
        }

        private void TestUpdate()
        {
            listTimetable[0].RoomNumber = 555;
            listTimetable[0].Day = new DateTime(2017, 1, 1);
            listTimetable[0].TimeStart = new TimeSpan(11, 11, 11);
            listTimetable[0].TimeEnd = new TimeSpan(22, 22, 22);
            listTimetable[0].GroupId = (int)GroupMock.groupMocks[0].Id;

            lessonStorage.AddOrUpdateTimeTable(listTimetable[0]);

        //    TimeTable actual = lessonStorage.GetTimeTableById((int)listTimetable[0].Id);
         //   Assert.AreEqual(listTimetable[0], actual);
        }
        private void TestEntityDelete(TimeTable timeTableToDelete)
        {
            for (int i = 0; i < listTimetable.Count; i++)
            {
                lessonStorage.DeleteTimeTable((int)listTimetable[i].Id);
                Assert.AreEqual(null, lessonStorage.GetTimeTableById((int)listTimetable[i].Id));
            }
        }

        private async void ClearAllMocks()
        {
            for (int i = 0; i < listTimetable.Count; i++)
            {
                groupStorage.GroupDelete((int)GroupMock.groupMocks[i].Id);
                await courseStorage.CourseProgramDelete((int)CourseProgramMock.listCourseProgram[i].Id);
                await courseStorage.CourseDelete((int)CourseMock.listCourse[i].Id);
            }
        }
    }
}
