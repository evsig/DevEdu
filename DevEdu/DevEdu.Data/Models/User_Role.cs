using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class User_Role : ICloneable
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User_Role user_role &&
                   Id == user_role.Id &&
                   RoleId == user_role.RoleId &&
                   UserId == user_role.UserId;
        }
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Id);
            hash.Add(RoleId);
            hash.Add(UserId);
            return hash.ToHashCode();
        }

        public object Clone()
        {
            return new User_Role
            {
                UserId = this.UserId,
                RoleId = this.RoleId
            };
        }
    }
}
