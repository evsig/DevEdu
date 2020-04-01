using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Tests.Mocks
{
    public class ThemeDetailsMock
    {
        public static List<ThemeDetails> listOfThemes = new List<ThemeDetails>
            {
                new ThemeDetails()
                {
                    ProgramDetailId = 0,
                    Topic = "WPF"
                },
                new ThemeDetails()
            {
                    ProgramDetailId = 0,
                    Topic = "Paint"
            },
                new ThemeDetails()
            {
                    ProgramDetailId = 0,
                    Topic = "ArrayList"
            },
                new ThemeDetails()
            {
                    ProgramDetailId = 0,
                    Topic = "EntityFrameWork"
            }
            };        
    }
}
