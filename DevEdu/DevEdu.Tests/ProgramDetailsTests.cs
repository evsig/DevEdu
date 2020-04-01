using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
using NUnit.Framework;

namespace DevEdu.Tests
{
    [TestFixture]
    public class ProgramDetailsTests
    {
        List<ProgramDetails> listProgramDetails = ProgramDetailsMock.listProgramDetails;
        CourseStorage courseStorage;

        [Test]
        public void ProgramDetailsTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            courseStorage = new CourseStorage(connection, transaction);

            try
            {
                Setup();
                TestSelects();
                TestUpdate();
                TestDeleteProgramDetails(listProgramDetails);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw new System.Exception();
            }
            
        }

        private async void Setup()
        {
            for (int i = 0; i < listProgramDetails.Count; i++)
            {
                int id = await courseStorage.ProgramDetailsAddOrUpdate(listProgramDetails[i]);
                listProgramDetails[i].Id = id;
            }
        }

        private async void TestSelects()
        {
            List<ProgramDetails> courseActual = await courseStorage.ProgramDetailsGetAll();
            for (int i = 0; i < listProgramDetails.Count; i++)
            {
                Assert.Contains(listProgramDetails[i], courseActual);
            }

            ProgramDetails actualCourse = await courseStorage.ProgramDetailsGetById((int)listProgramDetails[0].Id);
            Assert.AreEqual(listProgramDetails[0], actualCourse);
        }

        private async void TestUpdate()
        {
            ProgramDetails updateProgramDetails = listProgramDetails[0];
            updateProgramDetails.LessonTheme = "Быть или не быть";
            courseStorage.ProgramDetailsAddOrUpdate(updateProgramDetails);
            ProgramDetails actualCours1 = await courseStorage.ProgramDetailsGetById((int)updateProgramDetails.Id);

            Assert.AreEqual(updateProgramDetails, actualCours1);

            ProgramDetails updateCourse1 = await courseStorage.ProgramDetailsGetById((int)listProgramDetails[1].Id); ;
            Assert.AreEqual(listProgramDetails[1], updateCourse1);
        }

        private async void TestDeleteProgramDetails(List<ProgramDetails> listProgramDetails)
        {
            foreach (ProgramDetails listCourse1 in listProgramDetails)
            {
                List<ProgramDetails> list = await courseStorage.ProgramDetailsGetAll();
                courseStorage.CourseDelete((int)listCourse1.Id);
                List<ProgramDetails> list1 = await courseStorage.ProgramDetailsGetAll();
                Assert.AreEqual(list.Count - 1, list1.Count);

                Assert.False(list1.Contains(listCourse1));
            }
        }
    }

}
