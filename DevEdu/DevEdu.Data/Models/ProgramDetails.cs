using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{

    public class ProgramDetails
    {
        public int? Id { get; set; }
        public int CourseProgram { get; set; }
        public int LessonNumber { get; set; }
        public string LessonTheme { get; set; }
        public List<ThemeDetails> ThemeDetails { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ProgramDetails details &&
                   Id == details.Id &&
                   CourseProgram == details.CourseProgram &&
                   LessonNumber == details.LessonNumber &&
                   LessonTheme == details.LessonTheme;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CourseProgram, LessonNumber, LessonTheme);
        }
    }
}
