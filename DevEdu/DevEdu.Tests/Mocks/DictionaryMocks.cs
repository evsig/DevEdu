using DevEdu.Data;
using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Tests.Mocks
{
    public static class DictionaryMocks
    {
        #region City
        public static List<City> cityMock = new List<City>()//получить захардкоженный экземпляр
        {
            new City { Name = "Jopa" },
            new City { Name = "Zadnitsa" },
            //new City { Name = "Zhopa" },
            //new City { Name = "Zhopa2" }
        };
        #endregion

        #region Role

        public static List<Role> roleMocks = new List<Role>()
        {
            new Role { Name = "role" },
            new Role { Name = "newRole" }
        };
        #endregion

        #region AttestationTheme
        public static List<AttestationTheme> AttestationThemeMocks = new List<AttestationTheme>()
        {
            new AttestationTheme { CourseId = 1326, Theme = "ArrayList"},
            new AttestationTheme { CourseId = 1327, Theme = "LinkedList"}
        };

        #endregion

      /*  public static string NameGenerator()
        {
            DateTime thisDay = DateTime.Now;
            return thisDay.ToString();
        }*/
    }
}
