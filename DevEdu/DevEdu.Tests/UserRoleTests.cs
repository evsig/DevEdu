using DevEdu.Data;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DevEdu.Tests
{
    class UserRoleTests
    {
        UserStorage userStorage;
        DictionaryStorage dictionaryStorage;

        List<User_Role> listUser_role = new List<User_Role>();
        List<User> listUser = new List<User>();
        List<Role> listRole = new List<Role>();
        List<City> listCity = new List<City>();

        [TestCase()]
        public async Task User_RoleTestAsync()
        {
            using IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            userStorage = new UserStorage(connection, transaction);
            dictionaryStorage = new DictionaryStorage(connection, transaction);

            try
            {
                Setup();
                User_RoleTestSelects();
                await User_RoleTestDelete();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw new Exception();
            }
        }

        private void Setup()
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

            foreach (User item in UserMock.listUsers)
            {
                User newUser = (User)item.Clone();
                newUser.CityId = (int)listCity[0].Id;
                newUser.Id = null;
                listUser.Add(newUser);
            }
            for (int i = 0; i < listUser.Count; i++)
            {
                int id = (int)userStorage.UserAddOrUpdate(listUser[i]).Result;
                listUser[i].Id = id;
            }


            foreach (User_Role item in User_RoleMock.user_RoleMocks)
            {
                User_Role newUser_Role = (User_Role)item.Clone();
                newUser_Role.Id = null;
                listUser_role.Add(newUser_Role);
            }
            for (int i = 0; i < listUser_role.Count; i++)
            {
                listUser_role[i].RoleId = (int)listRole[i].Id;
                listUser_role[i].UserId = (int)listUser[i].Id;
                int id = (int)userStorage.User_RoleAdd(listUser_role[i]).Result;
                listUser_role[i].Id = id;
            }
        }

        private void User_RoleTestSelects()
        {
            List<User_Role> user_RoleActual = userStorage.GetAllUser_Role().Result;
            for (int i = 0; i < listUser_role.Count; i++)
            {
                Assert.Contains(listUser_role[i], user_RoleActual);
            }
        }

        private async Task User_RoleTestDelete()
        {
            foreach (User_Role listUser_Role1 in listUser_role)
            {
                List<User_Role> list = await userStorage.GetAllUser_Role();
                userStorage.User_RoleDeleteById((int)listUser_Role1.Id);
                List<User_Role> list1 = userStorage.GetAllUser_Role().Result;
                Assert.AreEqual(list.Count - 1, list1.Count);
                Assert.False(list1.Contains(listUser_Role1));
            }
        }

        //[Test]
        //public void UserRoleCRUDTest()
        //{

        //    Setup();
        //    for (int i = 0; i < listUser_role.Count; i++)
        //    {
        //        listUser_role[i].Id = AddUserRoleTest(listUser_role[i]);
        //    }

        //    TestSelectAll();
        //    TestSelectByRoleId();

        //    foreach (User_Role userRoleToDelete in listUser_role)
        //    {
        //        TestEntityDelete(userRoleToDelete);
        //    }
        //    ClearTest();

        //}


        //private int? AddUserRoleTest(User_Role userRoleToAdd)
        //{
        //    List<User_Role> UserRoleActual = userStorage.GetAllUser_Role();
        //    int? Id = userStorage.User_RoleAdd(userRoleToAdd);
        //    List<User_Role> actualUser_RoleAfterAdd = userStorage.GetAllUser_Role();
        //    Assert.AreEqual(UserRoleActual.Count + 1, actualUser_RoleAfterAdd.Count);
        //    return Id;
        //}

        //private void TestSelectAll()
        //{
        //    List<User_Role> UserRoleActual = userStorage.GetAllUser_Role();
        //    List<User_Role> result = new List<User_Role>();
        //    foreach (User_Role userRole in UserRoleActual)
        //    {
        //        for (int i = 0; i < listUser_role.Count; i++)
        //        {
        //            if (listUser_role[i].Equals(userRole))
        //            {
        //                Assert.True(listUser_role[i].Equals(userRole));
        //                result.Add(userRole);

        //            }
        //        }
        //    }
        //    Assert.IsNotEmpty(result);
        //}

        //private void TestSelectByRoleId()
        //{
        //    List<User_Role> UserRoleActual = userStorage.User_RoleSelectByRoleId((int)listUser_role[0].RoleId);
        //    foreach (User_Role userRole in UserRoleActual)
        //    {
        //        Assert.AreEqual(userRole.RoleId, (int)listUser_role[0].RoleId);
        //    }
        //    Assert.IsNotEmpty(UserRoleActual);
        //}

        //private void TestSelectByUserId()
        //{
        //    var UserRoleActual = userStorage.UserRolesSelectByUserId(listUser_role[0].UserId).Result;
        //    foreach (Role userRole in UserRoleActual)
        //    {
        //        Assert.AreEqual(userRole.Id, (int)listUser_role[0].Id);
        //    }
        //    Assert.IsNotEmpty(UserRoleActual);

        //}

        //private void TestEntityDelete(User_Role userRoleToDelete)
        //{
        //    List<User_Role> UserRoleActual = userStorage.GetAllUser_Role();
        //    userStorage.User_RoleDelete(userRoleToDelete);
        //    List<User_Role> actualUser_RoleAfterDelete = userStorage.GetAllUser_Role();
        //    Assert.AreEqual(UserRoleActual.Count - 1, actualUser_RoleAfterDelete.Count);
        //}

        //private void ClearTest()
        //{
        //    for (int i = 0; i < listUser_role.Count; i++)
        //    {
        //        userStorage.UserDeleteById((int)listUser[i].Id);
        //        dictionaryStorage.CityDelete(listCity[i].Id);
        //        dictionaryStorage.RoleDelete(listRole[i].Id);
        //    }
        //}
    }
}