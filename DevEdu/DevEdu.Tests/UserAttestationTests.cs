using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DevEdu.Tests
{
    class UserAttestationTests
    {
        UserAttestationStorage UAStorage;
        List<UserAttestation> listAttestations = UserAttestationMock.listAttestation;

        [Test]
        public void CourseTest()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            UAStorage = new UserAttestationStorage(connection, transaction);

            try
            {
                Setup();
                TestSelects();
                TestUpdate();
                TestDelete(listAttestations);

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
            for (int i = 0; i < listAttestations.Count; i++)
            {
                int? id = await UAStorage.UserAttestationAddOrUpdate(listAttestations[i]);
                listAttestations[i].Id = id;
            }
        }

        private async void TestSelects()
        {
            List<UserAttestation> attestationActual = await UAStorage.UsersAttestationGetAll();
            for (int i = 0; i < listAttestations.Count; i++)
            {
                Assert.Contains(listAttestations[i], attestationActual);
            }

            UserAttestation actualAttestation = await UAStorage.UserAttestationGetByID((int)listAttestations[0].Id);
            Assert.AreEqual(listAttestations[0], actualAttestation);
        }

        private async void TestUpdate()
        {
            UserAttestation updateAttestation = listAttestations[0];
            updateAttestation.TheoryPassed = false;
            await UAStorage.UserAttestationAddOrUpdate(updateAttestation);
            UserAttestation updateAttestation1 = await UAStorage.UserAttestationGetByID((int)updateAttestation.Id);

            Assert.AreEqual(updateAttestation, updateAttestation1);

            UserAttestation updateAttestation2 = await UAStorage.UserAttestationGetByID((int)listAttestations[1].Id);
            Assert.AreEqual(listAttestations[1], updateAttestation2);
        }

        private async void TestDelete(List<UserAttestation> listAttestations)
        {
            foreach (UserAttestation listAttestations1 in listAttestations)
            {
                List<UserAttestation> list = await UAStorage.UsersAttestationGetAll();
                UAStorage.UserAttestationDelete((int)listAttestations1.Id);
                List<UserAttestation> list1 = await UAStorage.UsersAttestationGetAll();
                Assert.AreEqual(list.Count - 1, list1.Count);

                Assert.False(list1.Contains(listAttestations1));
            }
        }
    }
}
