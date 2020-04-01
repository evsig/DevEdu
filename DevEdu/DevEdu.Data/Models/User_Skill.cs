using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class User_Skill
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int SkillId { get; set; }
    }
}
