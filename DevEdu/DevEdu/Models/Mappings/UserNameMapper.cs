using DevEdu.Data.Models;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;

namespace DevEdu.Models.Mappings
{
    public class UserNameMapper
    {
        public static List<UserNamePhotoOutputModel> ToOutputModels(IEnumerable<User> users)
        {
            List<UserNamePhotoOutputModel> result = new List<UserNamePhotoOutputModel>();

            foreach (User user in users)
            {
                result.Add(ToOutputModel(user));
            }

            return result;
        }

        public static UserNamePhotoOutputModel ToOutputModel(User user)
        {
            UserNamePhotoOutputModel result = new UserNamePhotoOutputModel
            {
                UserId = (int)user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return result;
        }
    }
}
