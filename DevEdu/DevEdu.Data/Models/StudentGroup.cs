using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class StudentGroup
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int? Rating { get; set; }

        //public double? Rating { get; set; }

        public override bool Equals(object obj)
        {
            return obj is StudentGroup group &&
                   Id == group.Id &&
                   UserId == group.UserId &&
                   GroupId == group.GroupId &&
                   Rating == group.Rating;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, UserId, GroupId, Rating);
        }
    }
}
