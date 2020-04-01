using System.Collections.Generic;

namespace DevEdu.Models.OutputModels
{
    public class LessonWithJournalAndTopicOutputModel
    {
        public int LessonId { get; set; }
        public string LessonDate { get; set; }
        public List<LessonTopicOutputModel> Topics { get; set; }
        public string Hometask { get; set; }
        public string ToRead { get; set; }
        public string Videos { get; set; }
        public List<JournalOutputModel> Journals { get; set; }
    }
}
