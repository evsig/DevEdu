using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Tests.Mocks
{
    public class NewsMock
    {
        public static List<News> listNews = new List<News>
            {
                new News()
                {
                    Title = "Важно",
                    Content = "Препод проспал, можете не торопиться"
                },
                new News()
                {
                    Title = "Срочно",
                    Content = "Объявлен набор на Frontend"
                }
            };
    }
}
