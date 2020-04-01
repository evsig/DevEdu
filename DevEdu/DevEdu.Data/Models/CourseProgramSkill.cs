using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class CourseProgramSkill : ICloneable
    {
        public int? Id { get; set; }
        public int CourseProgramId { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CourseProgramSkill skill &&
                   Id == skill.Id &&
                   CourseProgramId == skill.CourseProgramId &&
                   Name == skill.Name;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CourseProgramId, Name);
        }

        public object Clone()
        {
            return new CourseProgramSkill { CourseProgramId=this.CourseProgramId, Name = this.Name };
        }

        
    }
}
