using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class AttestationTheme : ICloneable
    {
        public int? Id { get; set; }
        public int CourseId { get; set; }
        public string Theme { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AttestationTheme theme &&
                   Id == theme.Id &&
                   CourseId == theme.CourseId &&
                   Theme == theme.Theme;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CourseId, Theme);
        }
        public object Clone()
        {
            return new AttestationTheme { CourseId = this.CourseId, Theme = this.Theme };
        }
    }
}
