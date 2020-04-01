using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class TimeTable
    {
        public int? Id { get; set; }
        public int GroupId { get; set; }
        public int RoomNumber { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public Course Course { get; set; }




        public override bool Equals(object obj)
        {
            return obj is TimeTable table &&
                   Id == table.Id &&
                   GroupId == table.GroupId &&
                   RoomNumber == table.RoomNumber &&
                   Day == table.Day &&
                   TimeStart == table.TimeStart &&
                   TimeEnd == table.TimeEnd;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, GroupId, RoomNumber, Day, TimeStart, TimeEnd);
        }
    }
}
