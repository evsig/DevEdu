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
    [TestFixture]
    public class LessonTests
    {

        List<Lesson> listLesson = new List<Lesson>();
        List<Course> listCourse = new List<Course>();
        List<CourseProgram> listCourseProgram = new List<CourseProgram>();
        List<Group> listGroup = new List<Group>();


        CourseStorage courseStorage;
        GroupStorage groupStorage;
        LessonStorage lessonStorage;

        [Test]
        public async Task LessonTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            courseStorage = new CourseStorage(connection, transaction);
            groupStorage = new GroupStorage(connection, transaction);
            lessonStorage = new LessonStorage(connection, transaction);

            try
            {
                await Setup();
                await TestSelects();
                await TestUpdate();

                foreach (Lesson lessonToDelete in listLesson)
                {
                    await TestEntityDelete(lessonToDelete);
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
        public async Task Setup()
        {
            listLesson.AddRange(LessonMock.lessonMocks.Select(m => (Lesson)m.Clone()));

            listCourseProgram.AddRange(CourseProgramMock.listCourseProgram.Select(m => (CourseProgram)m.Clone()));
            listGroup.AddRange(GroupMock.groupMocks.Select(m => (Group)m.Clone()));
            listCourse.AddRange(CourseMock.listCourse.Select(m => (Course)m.Clone()));

            for (int i = 0; i < listLesson.Count; i++)
            {
                listCourse[i].Id = await courseStorage.CourseAddOrUpdate(listCourse[i]);
                listCourseProgram[i].CourseId = (int)listCourse[i].Id;
                listCourseProgram[i].Id = await courseStorage.CourseProgramAddOrUpdate(listCourseProgram[i]);
                listGroup[i].CourseProgramId = (int)listCourseProgram[i].Id;
                listGroup[i].Id = await groupStorage.GroupAddOrUpdate(listGroup[i]);
                listLesson[i].GroupId = (int)listGroup[i].Id;
                listLesson[i].Id = await lessonStorage.AddOrUpdateLesson(listLesson[i]);
            }
        }
        #endregion

        private async Task TestSelects()
        {
            List<Lesson> actualLesson = await lessonStorage.GetAllLesson();
            for (int i = 0; i < listLesson.Count; i++)
            {
                Assert.Contains(listLesson[i], actualLesson);
            }

            Lesson actual = await lessonStorage.GetLessonById((int)listLesson[0].Id);
            Assert.AreEqual(listLesson[0], actual);
        }

        private async Task TestUpdate()
        {
            Lesson Lesson = listLesson[0];
            Lesson.Id = listLesson[0].Id;
            Lesson.GroupId = listLesson[0].GroupId;
            Lesson.Date = listLesson[0].Date;
            Lesson.Hometask = "ляляля";
            Lesson.Videos = "смотреть на ляляля";
            Lesson.ToRead = "читать про ляляля";

            await lessonStorage.AddOrUpdateLesson(Lesson);
            Lesson actualLesson = await lessonStorage.GetLessonById((int)Lesson.Id);
            Assert.AreEqual(Lesson, actualLesson);

            Lesson secondAactualLesson = await lessonStorage.GetLessonById((int)listLesson[1].Id);
            Assert.AreEqual(listLesson[1], secondAactualLesson);
        }

        private async Task TestEntityDelete(Lesson lessonToDelete)
        {
            List<Lesson> actualLessons = await lessonStorage.GetAllLesson();
            lessonStorage.DeleteLesson((int)lessonToDelete.Id);
            List<Lesson> actualLessons1 = await lessonStorage.GetAllLesson();

            Assert.AreEqual(actualLessons.Count - 1, actualLessons1.Count);

            for (int i = 0; i < listLesson.Count; i++)
            {
                Assert.AreNotEqual(actualLessons1[i].Id, (int)lessonToDelete.Id);
            }
        }
    }
}

