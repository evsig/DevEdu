using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class Journal
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int LessonID { get; set; }
        public bool IsAbsent { get; set; }
        public string Feadback { get; set; }
        public string AbsentReason { get; set; }        public Lesson Lesson { get; set; }
        public override bool Equals(object obj)
        {
            return obj is Journal journal &&
                Id == Id &&
                UserId == UserId &&
                Lesson.Id == Lesson.Id &&
                IsAbsent == IsAbsent &&
                Feadback == Feadback &&
                AbsentReason == AbsentReason;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Id, UserId, Lesson.Id, IsAbsent, Feadback, AbsentReason);
        }
    }
}
