using DevEdu.Data;
using DevEdu.Data.Models;
using System;
using System.Collections.Generic;

namespace DevEdu.Tests.Mocks
{
    public static class UserMock
    {

        public static List<User> listUsers = new List<User>
            {
                new User()
                {
                    FirstName = "Гэндальф",
                    LastName = "Серый",
                    Patronymic = "Дворфович",
                    BirthDate = new DateTime(1799, 10, 10),
                    Email = "Test0@mail.ru",
                    Phone = "111",
                    Password = "111",
                    Login = "Gend",
                    Bio = "Gray",
                    Photo = @"C:\genfalg.jpg",
                    CityId = 1,  // change it in test
                    //RegistrationDate = DateTime.Now
                },
                new User()
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Patronymic = "Иванович",
                    BirthDate = new DateTime(1799, 10, 10),
                    Email = "myEmail@mail.ru",
                    Phone = "7890",
                    Password = "test",
                    Login = "test",
                    Bio = "Gray",
                    Photo = @"C:\ivan.jpg",
                    CityId = 2,  // change it in test
                    //RegistrationDate = DateTime.Now
                },
                //new User()
                //{
                //    FirstName = "Алексей",
                //    LastName = "Иванов",
                //    Patronymic = "Петрович",
                //    BirthDate = new DateTime(1799, 10, 10),
                //    Email = "Test@mail.ru",
                //    Phone = "999",
                //    Password = "123",
                //    Login = "Ivanov",
                //    Bio = "Ivan",
                //    Photo = @"C:\alex.jpg",
                //    CityId = 3,  // change it in test
                //    //RegistrationDate = DateTime.Now
                //},
                //new User()
                //{
                //    FirstName = "Петр",
                //    LastName = "Первый",
                //    Patronymic = "Олегович",
                //    BirthDate = new DateTime(1799, 10, 10),
                //    Email = "Test2@mail.ru",
                //    Phone = "777",
                //    Password = "431",
                //    Login = "Petr",
                //    Bio = "Perviy",
                //    Photo = @"C:\petr.jpg",
                //    CityId = 4,  // change it in test
                //    //RegistrationDate = DateTime.Now
                //}
            };
    }
}
