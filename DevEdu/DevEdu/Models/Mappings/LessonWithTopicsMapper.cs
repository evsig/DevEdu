using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels.Mappings
{
    public class LessonWithTopicsMapper
    {
        public static List<LessonWithTopicsOutputModel> ToOutputModels(List<Lesson> lessons)
        {
            List<LessonWithTopicsOutputModel> result = new List<LessonWithTopicsOutputModel>();

            foreach (Lesson lesson in lessons)
            {
                result.Add(ToOutputModel(lesson));
            }

            return result;
        }

        public static LessonWithTopicsOutputModel ToOutputModel(Lesson lesson)
        {
            return new LessonWithTopicsOutputModel
            {
                Hometask = lesson.Hometask,
                LessonDate = lesson.Date,
                LessonId = (int)lesson.Id,
                ToRead = lesson.ToRead,
                Videos = lesson.Videos,
                Topics = LessonTopicMapper.ToOutputModels(lesson.LessonTopicsDetails)
            };
        }
    }
}
