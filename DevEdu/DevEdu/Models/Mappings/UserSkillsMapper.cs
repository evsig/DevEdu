using DevEdu.Data.Models;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;

namespace DevEdu.Models.Mappings
{
    public class UserSkillsMapper
    {
        public static List<UserSkillsOutputModel> ToOutputModels(List<CourseProgramSkill> skills)
        {
            List<UserSkillsOutputModel> result = new List<UserSkillsOutputModel>();

            foreach (CourseProgramSkill skill in skills)
            {
                result.Add(ToOutputModel(skill));
            }

            return result;
        }

        public static UserSkillsOutputModel ToOutputModel(CourseProgramSkill skill)
        {
            return new UserSkillsOutputModel
            {
                SkillId = (int)skill.Id,
                Name = skill.Name
            };
        }
    }
}
