using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Tests.Mocks
{
    class CourseMock
    {
        public static List<Course> listCourse = new List<Course>
            {
                new Course()
                {
                    Name = "C#",
                    Description = "C# description",
                    Price = 11250
                },
                new Course()
                {
                    Name = "Frontend",
                    Description = "Frontend description",
                    Price = 15555
                }
            };
    }
}

