using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels.Mappings
{
    public class TopicToTeachMapper
    {
        public static IEnumerable<TopicToTeachOutputModel> ToOutputModels(IEnumerable<ThemeDetails> topics)
        {
            List<TopicToTeachOutputModel> result = new List<TopicToTeachOutputModel>();

            foreach (ThemeDetails topic in topics)
            {
                result.Add(ToOutputModel(topic));
            }

            return result;
        }

        public static TopicToTeachOutputModel ToOutputModel(ThemeDetails topic)
        {
            return new TopicToTeachOutputModel
            {
                ProgramDetailId = (int)topic.ProgramDetailId,
                Topic = topic.Topic
            };
        }
    }
}
