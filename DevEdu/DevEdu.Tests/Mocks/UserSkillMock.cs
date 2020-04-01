using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Tests.Mocks
{
    class UserSkillMock
    {
        /* public static User_Skill CreateUserSkill(int userId, int skillId)
         {
             User_Skill userSkill = new User_Skill
             {
                 UserId = userId,
                 SkillId = skillId
             };
             return userSkill;
         }*/

        public static List<User_Skill> listUserSkill = new List<User_Skill>
            {
                new User_Skill()
                {
                    UserId=0,
                    SkillId=0
                },
                new User_Skill()
                {
                    UserId=0,
                    SkillId=0
                }
            };
    }
}
