using DevEdu.Data.Models;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class GroupWithTopicAndQuantityMapper
    {
        public static List<GroupWithTopicAndQuantityOutputModel> ToOutputModels(List<GroupWithTopicAndQuantity> groups)
        {
            List<GroupWithTopicAndQuantityOutputModel> result = new List<GroupWithTopicAndQuantityOutputModel>();

            foreach (GroupWithTopicAndQuantity group in groups)
            {
                result.Add(ToOutputModel(group));
            }

            return result;
        }

        public static GroupWithTopicAndQuantityOutputModel ToOutputModel(GroupWithTopicAndQuantity group)
        {
            return new GroupWithTopicAndQuantityOutputModel
            {
                GroupId = group.GroupId,
                Topic = group.Topic,
                Quantity = group.Quantity,
                StartDate = group.StartDate.ToString(),
                EndDate = group.EndDate.ToString()
            };
        }
    }
}
