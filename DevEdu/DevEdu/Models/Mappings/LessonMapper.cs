using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class LessonMapper
    {
        public static Lesson ToDataModel(LessonInputModel inputModel)
        {
            var result = new Lesson
            {
                Id = inputModel.Id,
                GroupId = inputModel.GroupId,
                Date = Convert.ToDateTime(inputModel.Date),
                Hometask = inputModel.Hometask,
                Videos = inputModel.Videos,
                ToRead = inputModel.ToRead
            };
            return result;
        }
        public static List<LessonOutputModel> ToOutputModels(List<Lesson> lessons)
        {
            List<LessonOutputModel> result = new List<LessonOutputModel>();

            foreach (Lesson lesson in lessons)
            {
                result.Add(ToOutputModel(lesson));
            }

            return result;
        }

        public static LessonOutputModel ToOutputModel(Lesson lesson)
        {
            return new LessonOutputModel
            {
                Id = (int)lesson.Id,
                GroupId = lesson.GroupId,
                Date = lesson.Date.ToString(@"yyyy/MM/dd"),
                Hometask = lesson.Hometask,
                Videos = lesson.Videos,
                ToRead = lesson.ToRead
            };
        }

    }
}
