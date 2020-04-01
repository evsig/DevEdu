using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class TeacherGroup
	{
            public int? Id { get; set; }
            public int UserId { get; set; }
            public int GroupId { get; set; }

            public override bool Equals(object obj)
            {
                return obj is TeacherGroup group &&
                       Id == group.Id &&
                       UserId == group.UserId &&
                       GroupId == group.GroupId;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Id, UserId, GroupId);
            }
    }
}
