using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DevEdu.Tests
{
    
    public class CourseTests
    {
        CourseStorage courseStorage;
        List<Course> listCourse;

        [Test]
        public async Task CourseTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            courseStorage = new CourseStorage(connection, transaction);
            
            try
            {
                await Setup();
                await TestSelects();
                await TestUpdate();
                await TestDeleteCourse(listCourse);
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

        private async Task Setup()
        {
            listCourse = new List<Course>();
            foreach (var item in CourseMock.listCourse)
            {
                listCourse.Add((Course)item.Clone());
            }
            for (int i = 0; i < listCourse.Count; i++)
            {
                int id = await courseStorage.CourseAddOrUpdate(listCourse[i]);
                listCourse[i].Id = id;
            }
        }

        private async Task TestSelects()
        {
            List<Course> courseActual = await courseStorage.CourseGetAll();
            for (int i = 0; i < listCourse.Count; i++)
            {
                Assert.Contains(listCourse[i], courseActual);
            }

            Course actualCourse = await courseStorage.CourseGetById((int)listCourse[0].Id);
            Assert.AreEqual(listCourse[0], actualCourse);
        }

        private async Task TestUpdate()
        {
            Course updateCourse = listCourse[0];
            updateCourse.Name = "Вышивание крестиком";
            await courseStorage.CourseAddOrUpdate(updateCourse);
            Course actualCours1 = await courseStorage.CourseGetById((int)updateCourse.Id);

            Assert.AreEqual(updateCourse, actualCours1);

            Course updateCourse1 = await courseStorage.CourseGetById((int)listCourse[1].Id);
            Assert.AreEqual(listCourse[1], updateCourse1);
        }

        private async Task TestDeleteCourse(List<Course> listCourse)
        {
            foreach (Course listCourse1 in listCourse)
            {
                List<Course> list = await courseStorage.CourseGetAll();
                await courseStorage.CourseDelete((int)listCourse1.Id);
                List<Course> list1 = await courseStorage.CourseGetAll();
                Assert.AreEqual(list.Count - 1, list1.Count);

                Assert.False(list1.Contains(listCourse1));
            }
        }
    }
}