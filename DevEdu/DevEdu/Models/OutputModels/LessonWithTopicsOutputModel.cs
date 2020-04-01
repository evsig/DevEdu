using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;

namespace DevEdu.Models
{
    public class LessonWithTopicsOutputModel
    {
        public int LessonId { get; set; }
        public DateTime LessonDate { get; set; }
        public List<LessonTopicOutputModel> Topics { get; set; }
        public string Hometask { get; set; }
        public string ToRead { get; set; }
        public string Videos { get; set; }
    }
}