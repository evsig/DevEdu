using DevEdu.Data.Models;
using DevEdu.Models.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels.Mappings
{
    public class LessonWithJournalAndTopicMapper
    {
        public static IEnumerable<LessonWithJournalAndTopicOutputModel> ToOutputModels(IEnumerable<Lesson> lessons)
        {
            List<LessonWithJournalAndTopicOutputModel> result = new List<LessonWithJournalAndTopicOutputModel>();

            foreach (Lesson lesson in lessons)
            {
                result.Add(ToOutputModel(lesson));
            }

            return result;
        }

        public static LessonWithJournalAndTopicOutputModel ToOutputModel(Lesson lesson)
        {
            var result =  new LessonWithJournalAndTopicOutputModel
            {
                
                Hometask = lesson.Hometask,
                LessonDate = lesson.Date.ToString(@"yyyy/MM/dd"),
                LessonId = (int)lesson.Id,
                ToRead = lesson.ToRead,
                Videos = lesson.Videos,
                Topics = LessonTopicMapper.ToOutputModels(lesson.LessonTopicsDetails),
                Journals = JournalMapper.ToOutputModels(lesson.Journals)
            };
            foreach (var journal in result.Journals)
            {
                if (journal.IsAbsent == false)
                {
                    journal.AbsentReason = null;
                }

            }
            return result;
        }
    }
}
