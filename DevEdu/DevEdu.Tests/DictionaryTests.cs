using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using DevEdu.Data;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
//using DocumentFormat.OpenXml.Wordprocessing;
using NUnit.Framework;

namespace DevEdu.Tests
{
    [TestFixture]
    class DictionaryTests
    {
        DictionaryStorage dictionaryStorage;


        #region City
        List<City> listCity = new List<City>();

        [TestCase()]

        public async Task CityTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            dictionaryStorage = new DictionaryStorage(connection, transaction);

            try
            {
                CitySetup();
                CityTestSelects();
                await CityTestUpdate();
                await CityTestDelete(listCity);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw new Exception();
            }
            
        }

        private void CitySetup()
        {
            foreach (City item in DictionaryMocks.cityMock)
            {
                City newCity = (City)item.Clone();
                newCity.Id = null;
                listCity.Add(newCity);
            }
            for (int i = 0; i < listCity.Count; i++)
            {
                int id = (int)dictionaryStorage.CityAddOrUpdate(listCity[i]).Result;
                listCity[i].Id = id;
            }

        }
        private void CityTestSelects()
        {
            List<City> cityActual = dictionaryStorage.CitiesGetAll().Result;
            for (int i = 0; i < listCity.Count; i++)
            {
                Assert.Contains(listCity[i], cityActual);
            }

            City actualCity = dictionaryStorage.CityGetByID((int)listCity[0].Id).Result;
            Assert.AreEqual(listCity[0], actualCity);
        }

        private async Task CityTestUpdate()
        {
            City updateCity = listCity[0];
            updateCity.Name = "hohoho";
            await dictionaryStorage.CityAddOrUpdate(updateCity);
            City actualCity1 = dictionaryStorage.CityGetByID((int)updateCity.Id).Result;

            Assert.AreEqual(updateCity, actualCity1);

            City updateCity1 = dictionaryStorage.CityGetByID((int)listCity[1].Id).Result;
            Assert.AreEqual(listCity[1], updateCity1);
        }

        private async Task CityTestDelete(List<City> listCity)
        {
            foreach (City listCity1 in listCity)
            {
                List<City> list = dictionaryStorage.CitiesGetAll().Result;
                await dictionaryStorage.CityDelete((int)listCity1.Id);
                List<City> list1 = dictionaryStorage.CitiesGetAll().Result;
                Assert.AreEqual(list.Count - 1, list1.Count);

                Assert.False(list1.Contains(listCity1));
            }
        }


        #endregion

        #region Role


        List<Role> listRole= new List<Role>();

        [TestCase()]

        public async Task RoleTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            dictionaryStorage = new DictionaryStorage(connection, transaction);

            try
            {
                RoleSetup();
                RoleTestSelects();
                await RoleTestUpdate();
                await RoleTestDelete(listRole);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw new Exception();
            }
            
        }

        private void RoleSetup()
        {
            foreach (Role item in DictionaryMocks.roleMocks)
            {
                Role newRole = (Role)item.Clone();
                newRole.Id = null;
                listRole.Add(newRole);
            }
            for (int i = 0; i < listRole.Count; i++)
            {
                int id = (int)dictionaryStorage.RoleAddOrUpdate(listRole[i]).Result;
                listRole[i].Id = id;
            }

        }
        private void RoleTestSelects()
        {
            List<Role> roleActual = dictionaryStorage.RoleGetAll().Result;
            for (int i = 0; i < listRole.Count; i++)
            {
                Assert.Contains(listRole[i], roleActual);
            }

            Role actualRole = dictionaryStorage.RoleGetByID((int)listRole[0].Id).Result;
            Assert.AreEqual(listRole[0], actualRole);
        }

        private async Task RoleTestUpdate()
        {
            Role updateRole = listRole[0];
            updateRole.Name = "hohoho";
            await dictionaryStorage.RoleAddOrUpdate(updateRole);
            Role actualRole1 = dictionaryStorage.RoleGetByID((int)updateRole.Id).Result;

            Assert.AreEqual(updateRole, actualRole1);

            Role updateRole1 = dictionaryStorage.RoleGetByID((int)listRole[1].Id).Result;
            Assert.AreEqual(listRole[1], updateRole1);
        }

        private async Task RoleTestDelete(List<Role> listRole)
        {
            foreach (Role listRole1 in listRole)
            {
                List<Role> list = dictionaryStorage.RoleGetAll().Result;
                await dictionaryStorage.RoleDelete((int)listRole1.Id);
                List<Role> list1 = dictionaryStorage.RoleGetAll().Result;
                Assert.AreEqual(list.Count - 1, list1.Count);

                Assert.False(list1.Contains(listRole1));
            }
        }

      
        #endregion

        #region AttestationTheme

        List<AttestationTheme> listAttestationTheme = new List<AttestationTheme>();

        [Test]
        public async Task AttestationThemeTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            dictionaryStorage = new DictionaryStorage(connection, transaction);

            try
            {
                AttestationThemeSetup();
                AttestationThemeTestSelects();
                await AttestationThemeTestUpdate();
                await AttestationThemeTestDelete(listAttestationTheme);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw new System.Exception();
            }
            
        }

        private void AttestationThemeSetup()
        {
            foreach (AttestationTheme item in DictionaryMocks.AttestationThemeMocks)
            {
                AttestationTheme newAttestationTheme = (AttestationTheme)item.Clone();
                newAttestationTheme.Id = null;
                listAttestationTheme.Add(newAttestationTheme);
            }
            for (int i = 0; i < listAttestationTheme.Count; i++)
            {
                int id = (int)dictionaryStorage.AttestationThemeAddOrUpdate(listAttestationTheme[i]).Result;
                listAttestationTheme[i].Id = id;
            }
        }

        private void AttestationThemeTestSelects()
        {
            List<AttestationTheme> listActual = dictionaryStorage.AttestationThemeGetAll().Result;
            for (int i = 0; i < listAttestationTheme.Count; i++)
            {
                Assert.Contains(listAttestationTheme[i], listActual);
            }

            AttestationTheme actualAttestationTheme = dictionaryStorage.AttestationThemeGetByID((int)listAttestationTheme[0].Id).Result;
            Assert.AreEqual(listAttestationTheme[0], actualAttestationTheme);
        }

        private async Task AttestationThemeTestUpdate()
        {
            AttestationTheme attestationTheme = listAttestationTheme[0];
            attestationTheme.Theme = "Вышивание крестиком";
            await dictionaryStorage.AttestationThemeAddOrUpdate(attestationTheme);
            AttestationTheme attestationTheme1 = dictionaryStorage.AttestationThemeGetByID((int)attestationTheme.Id).Result;

            Assert.AreEqual(attestationTheme, attestationTheme1);

            AttestationTheme attestationTheme2 = dictionaryStorage.AttestationThemeGetByID((int)listAttestationTheme[1].Id).Result;
            Assert.AreEqual(listAttestationTheme[1], attestationTheme2);
        }

        private async Task AttestationThemeTestDelete(List<AttestationTheme> listAttestationTheme)
        {
            foreach (AttestationTheme attestationTheme in listAttestationTheme)
            {
                List<AttestationTheme> list = dictionaryStorage.AttestationThemeGetAll().Result;
                await dictionaryStorage.AttestationThemeDelete((int)attestationTheme.Id);
                List<AttestationTheme> list1 = dictionaryStorage.AttestationThemeGetAll().Result;
                Assert.AreEqual(list.Count - 1, list1.Count);

                Assert.False(list1.Contains(attestationTheme));
            }
        }

        #endregion
    }
}