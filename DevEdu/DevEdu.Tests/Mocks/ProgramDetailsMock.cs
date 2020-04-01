using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Tests.Mocks
{
    public class ProgramDetailsMock
    {
        public static List<ProgramDetails> listProgramDetails = new List<ProgramDetails>
            {
                new ProgramDetails()
                {
                    CourseProgram = 2,
                    LessonNumber = 1,
                    LessonTheme = "консольная приложуля из говна и палок"
                },
                new ProgramDetails()
                {
                    CourseProgram = 3,
                    LessonNumber = 15,
                    LessonTheme = "адронный коллайдер из табурета и паяльника"
                },
            };
    }
}
