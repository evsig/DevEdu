using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels.Mappings
{
    public class LessonTopicMapper
    {
        public static List<LessonTopicOutputModel> ToOutputModels(List<ThemeDetails> lessonTopics)
        {
            List<LessonTopicOutputModel> result = new List<LessonTopicOutputModel>();

            foreach (ThemeDetails lessonTopic in lessonTopics)
            {
                result.Add(ToOutputModel(lessonTopic));
            }

            return result;
        }

        public static LessonTopicOutputModel ToOutputModel(ThemeDetails lessonTopic)
        {
            return new LessonTopicOutputModel
            {
                Id = (int)lessonTopic.Id,
                Name = lessonTopic.Topic
            };
        }


        public static LessonTopic ToDataModel(LessonTopicInputModel inputModel)
        {
            var result = new LessonTopic
            {
                Id = inputModel.Id,
                LessonId = inputModel.LessonId,
                ThemeDetailsId = inputModel.ThemeDetailsId
            };
            return result;
        }
    }
}