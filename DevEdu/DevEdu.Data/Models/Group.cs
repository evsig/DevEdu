using System;

namespace DevEdu.Data.Models
{
    public class Group : ICloneable
    {
        public int? Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan TimeStart { get; set; }
        public int Duration { get; set; }
        public int CourseProgramId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Group journal &&
                Id == Id &&
                StartDate == StartDate &&
                EndDate == EndDate &&
                TimeStart == TimeStart &&
                Duration == Duration &&
                CourseProgramId == CourseProgramId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, StartDate, EndDate, TimeStart, Duration, CourseProgramId);
        }

        public object Clone()
        {
            return new Group
            {
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                TimeStart = this.TimeStart,
                Duration = this.Duration,
                CourseProgramId = this.CourseProgramId,
            };
        }
    }
}