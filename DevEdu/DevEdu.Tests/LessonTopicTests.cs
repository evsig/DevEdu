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
    public class LessonTopicTests
    {
        List<LessonTopic> listLessonTopic = new List<LessonTopic>();
        List<Course> listCourse = new List<Course>();
        List<CourseProgram> listCourseProgram = new List<CourseProgram>();
        List<Group> listGroup = new List<Group>();
        List<Lesson> listLesson = new List<Lesson>();

        CourseStorage courseStorage;
        GroupStorage groupStorage;
        LessonStorage lessonStorage;

        [Test]
        public async Task LessonTopicTest()
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

                foreach (LessonTopic lessonTopicToDelete in listLessonTopic)
                {
                    await TestEntityDelete(lessonTopicToDelete);
                }
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                transaction.Rollback();
            }
        }

        #region Setup

        public async Task Setup()
        {
            listLessonTopic.AddRange(LessonTopicMock.lessonTopicMocks.Select(x => (LessonTopic)x.Clone()));
            listCourse.AddRange(CourseMock.listCourse.Select(x => (Course)x.Clone()));
            listCourseProgram.AddRange(CourseProgramMock.listCourseProgram.Select(x => (CourseProgram)x.Clone()));
            listGroup.AddRange(GroupMock.groupMocks.Select(x => (Group)x.Clone()));
            listLesson.AddRange(LessonMock.lessonMocks.Select(x => (Lesson)x.Clone()));

            for (int i = 0; i < listLessonTopic.Count; i++)
            {
                listCourse[i].Id = await courseStorage.CourseAddOrUpdate(listCourse[i]);
                listCourseProgram[i].CourseId = (int)listCourse[i].Id;
                listCourseProgram[i].Id = await courseStorage.CourseProgramAddOrUpdate(listCourseProgram[i]);
                listGroup[i].CourseProgramId = (int)listCourseProgram[i].Id;
                listGroup[i].Id = await groupStorage.GroupAddOrUpdate(listGroup[i]);
                listLesson[i].GroupId = (int)listGroup[i].Id;
                listLesson[i].Id = await lessonStorage.AddOrUpdateLesson(listLesson[i]);
                listLessonTopic[i].LessonId = (int)listLesson[i].Id;
                listLessonTopic[i].Id = await lessonStorage.AddLessonTopic(listLessonTopic[i]);

            }
        }
        #endregion

        private async Task TestSelects()
        {
            List<LessonTopic> actualLessonTopic = await lessonStorage.GetAllLessonTopic();
            for (int i = 0; i < listLessonTopic.Count; i++)
            {
                Assert.Contains(listLessonTopic[i], actualLessonTopic);
            }

            LessonTopic actual = await lessonStorage.GetLessonTopicById((int)listLessonTopic[0].Id);
            Assert.AreEqual(listLessonTopic[0], actual);
        }

        private async Task TestEntityDelete(LessonTopic lessonTopicToDelete)
        {
            List<LessonTopic> actualLessonTopics = await lessonStorage.GetAllLessonTopic();
            await lessonStorage.DeleteLessonTopic((int)lessonTopicToDelete.Id);
            List<LessonTopic> actualLessonTopic1 = await lessonStorage.GetAllLessonTopic();
            Assert.AreEqual(actualLessonTopics.Count - 1, actualLessonTopic1.Count);

            for (int i = 0; i < actualLessonTopic1.Count; i++)
            {
                Assert.AreNotEqual(actualLessonTopic1[i].Id, (int)lessonTopicToDelete.Id);
            }
        }

    }
}
