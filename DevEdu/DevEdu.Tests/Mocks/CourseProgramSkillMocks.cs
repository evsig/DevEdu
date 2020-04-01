using System;
using System.Collections.Generic;
using System.Text;
using DevEdu.Data.Models;


namespace DevEdu.Tests.Mocks
{
 public class CourseProgramSkillMocks
    {
        public static List<CourseProgramSkill> listCourseProgramSkill = new List<CourseProgramSkill>
        {

                new CourseProgramSkill()
                {
                    CourseProgramId = 0,
                    Name = "Умение программировать",
                },
                new CourseProgramSkill()
                {
                    CourseProgramId = 0,
                    Name = "Умение читать чужой код",
                }//,
                //new CourseProgramSkill()
                //{
                //    CourseProgramId = 0,
                //    Name = "Умение бесить Антона",
                //}

        };
    }
}
