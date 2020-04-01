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
    public class ThemeDetailsTests
    {
        CourseStorage courseStorage;
        List<ThemeDetails> themeDetailsList = ThemeDetailsMock.listOfThemes;
        List<ProgramDetails> programDetailsList = ProgramDetailsMock.listProgramDetails;

        [Test]

        public void GeneralTest()
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

                foreach (ThemeDetails ThemeDetailsToDelete in themeDetailsList)
                {
                    TestEntityDelete(ThemeDetailsToDelete);
                }

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
            for (int i = 0; i < themeDetailsList.Count; i++)
            {
                int programDetailId = await courseStorage.ProgramDetailsAddOrUpdate(programDetailsList[i]);
                programDetailsList[i].Id = programDetailId;
                themeDetailsList[i].ProgramDetailId = programDetailId;
                int id = await courseStorage.ThemeDetailsAddOrUpdate(themeDetailsList[i]);
                themeDetailsList[i].Id = id;
            }
        }

        private async void TestSelects()
        {
            List<ThemeDetails> actualThemes = await courseStorage.ThemeDetailsGetAll();
            for (int i = 0; i < themeDetailsList.Count; i++)
            {
                ThemeDetails theme34 = themeDetailsList[i];

                Assert.Contains(theme34, actualThemes);
            }
            ThemeDetails theme = await courseStorage.ThemeDetailsGetById((int)themeDetailsList[0].Id);

            Assert.AreEqual(themeDetailsList[0], theme);
        }
        private async void TestEntityDelete(ThemeDetails ThemeDetailsToDelete)
        {
            List<ThemeDetails> actualThemes = await courseStorage.ThemeDetailsGetAll();
            await courseStorage.ThemeDetailsDeleteById((int)ThemeDetailsToDelete.Id);
            List<ThemeDetails> actualThemesAfterDelete = await courseStorage.ThemeDetailsGetAll();

            Assert.AreEqual(actualThemes.Count - 1, actualThemesAfterDelete.Count);
        }

        private async void TestUpdate()
        {
            themeDetailsList[0].ProgramDetailId = 3;
            themeDetailsList[0].Topic = "Kjgeir";

            await courseStorage.ThemeDetailsAddOrUpdate(themeDetailsList[0]);
            ThemeDetails theme1 = await courseStorage.ThemeDetailsGetById((int)themeDetailsList[0].Id);
            Assert.AreEqual(themeDetailsList[0], theme1);

            ThemeDetails theme2 = await courseStorage.ThemeDetailsGetById((int)themeDetailsList[3].Id);
            Assert.AreEqual(themeDetailsList[3], theme2);
        }
    }
}
