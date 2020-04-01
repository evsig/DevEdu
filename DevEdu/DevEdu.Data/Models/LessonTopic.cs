using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class LessonTopic : ICloneable
    {  
        public int? Id { get; set; }

        public int LessonId { get; set; }
        public int ThemeDetailsId { get; set; }
        public override bool Equals(object obj)
        {
            return obj is LessonTopic lessonTopic &&
                Id == Id &&
                LessonId == LessonId &&
                ThemeDetailsId == ThemeDetailsId;
        }
        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Id);
            hash.Add(LessonId);
            hash.Add(ThemeDetailsId);
            return hash.ToHashCode();
         
        }

        public override string ToString()
        {
            string lessonTopic = "{";
            lessonTopic = lessonTopic + "\n\tId: " + Id;
            lessonTopic = lessonTopic + "\n\tLessonTopicId: " + LessonId;
            lessonTopic = lessonTopic + "\n\tThemeDetailsId: " + ThemeDetailsId;

            lessonTopic = lessonTopic + "\n}";
            return lessonTopic;
        }

        public object Clone()
        {
            return new LessonTopic { LessonId = LessonId, ThemeDetailsId = ThemeDetailsId };
        }
    }
}
