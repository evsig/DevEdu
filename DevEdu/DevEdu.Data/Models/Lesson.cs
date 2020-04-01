using System;
using System.Collections.Generic;

namespace DevEdu.Data.Models
{
    public class Lesson : ICloneable
    {
        public int? Id { get; set; }
        public int GroupId { get; set; }
        public DateTime Date { get; set; }
        public string Hometask { get; set; }
        public string Videos { get; set; }
        public string ToRead { get; set; }
        public List<ThemeDetails> LessonTopicsDetails { get; set; }
        public List<Journal> Journals { get; set; }

        public object Clone()
        {
            return new Lesson
            {
                GroupId = this.GroupId,
                Date = this.Date,
                Hometask = this.Hometask,
                Videos = this.Videos,
                ToRead = this.ToRead
            };
        }

        public override bool Equals(object obj)
        {
            return obj is Lesson lesson &&
                Id == Id &&
                GroupId == GroupId &&
                Date == Date &&
                Hometask == Hometask &&
                Videos == Videos &&
                ToRead == ToRead;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, GroupId, Date, Hometask, Videos, ToRead);
        }

        //public override string? ToString()
        //{
        //    string lesson = "{";
        //    lesson = lesson + "\n\tId: " + Id;
        //    lesson = lesson + "\n\tGroupId: " + GroupId;
        //    lesson = lesson + "\n\tDate: " + Date;
        //    lesson = lesson + "\n\tHometask: " + Hometask;
        //    lesson = lesson + "\n\tVideos: " + Videos;
        //    lesson = lesson + "\n\tToRead: " + ToRead;
        //    lesson = lesson + "\n}";
        //    return lesson;
        //}
    }
}