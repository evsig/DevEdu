using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class LessonHometaskMapper
    {
        public static Lesson ToDataModel(LessonHometaskInputModel lesson)
        {
            return new Lesson
            {
                Id = lesson.Id,
                GroupId = lesson.GroupId,
                Date = Convert.ToDateTime(lesson.Date),
                Hometask = lesson.Hometask,
                Videos = lesson.Videos,
                ToRead = lesson.ToRead

            };
        }
    }
}
