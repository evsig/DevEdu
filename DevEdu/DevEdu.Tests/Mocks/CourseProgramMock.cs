using System;
using System.Collections.Generic;
using System.Text;
using DevEdu.Data.Models;


namespace DevEdu.Tests.Mocks
{
    class CourseProgramMock
    {
        public static List<CourseProgram> listCourseProgram = new List<CourseProgram>
            {
                new CourseProgram()
                {
                    CourseId=0,
                    IsActual=true,
                   Title = "Program 1"
                },
                new CourseProgram()
                {
                    CourseId=0,
                    IsActual=false,
                    Title = "Program 2"
                }
            };
    }
}
