using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class Role : ICloneable
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Role role &&
                   Id == role.Id &&
                   Name == role.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
        public object Clone()
        {
            return new Role { Name = this.Name };
        }
    }
    
}
