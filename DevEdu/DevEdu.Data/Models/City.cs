using System;

namespace DevEdu.Data
{
    public class City : ICloneable
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is City city &&
                   Id == city.Id &&
                   Name == city.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }
        public object Clone()
        {
            return new City { Name = this.Name };
        }
    }
}
