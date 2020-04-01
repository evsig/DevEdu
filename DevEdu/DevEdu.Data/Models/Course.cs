using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class Course : ICloneable
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public override bool Equals(object obj)
        {
            return obj is Course course &&
                   Id == course.Id &&
                   Name == course.Name &&
                   Description == course.Description &&
                   Price == course.Price;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, Price);
        }

        public object Clone()
        {
            return new Course { Description= this.Description, Name = this.Name, Price=this.Price };
        }
    }
}
