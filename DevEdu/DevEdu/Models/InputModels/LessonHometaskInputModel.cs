using System;


namespace DevEdu.Models
{
    public class LessonHometaskInputModel
    {
        public int? Id { get; set; }
        public int GroupId { get; set; }
        public string Date { get; set; }
        public string Hometask { get; set; }
        public string Videos { get; set; }
        public string ToRead { get; set; }
        public int UserId { get; set; }
    }
}
