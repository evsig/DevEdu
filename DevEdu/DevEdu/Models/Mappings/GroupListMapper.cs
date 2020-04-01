using DevEdu.Data.Models;
using DevEdu.Models.InputModel;
using DevEdu.Models.InputModels;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace DevEdu.Models.Mappings
{
    public class GroupListMapper
    {
        public static List<GroupListOutputModel> ToOutputModels(List<GroupList> groupList)
        {
            List<GroupListOutputModel> result = new List<GroupListOutputModel>();

            foreach (GroupList member in groupList)
            {
                result.Add(ToOutputModel(member));
            }

            return result;
        }

        public static GroupListOutputModel ToOutputModel(GroupList groupList)
        {
            return new GroupListOutputModel
            {
                RoleName = groupList.RoleName,
                FirstName = groupList.FirstName,
                LastName = groupList.LastName
            };
        }

        public static List<GroupList> FromInputModels(List<GroupListInputModel> groupLists)
        {
            List<GroupList> result = new List<GroupList>();

            foreach (GroupListInputModel groupList in groupLists)
            {
                result.Add(FromInputModel(groupList));
            }

            return result;
        }

        public static GroupList FromInputModel(GroupListInputModel groupList)
        {
            if (groupList.GroupId != null)
            {
                return new GroupList
                {
                    GroupId = groupList.GroupId
                };
            }
            return null;
        }
    }
}
