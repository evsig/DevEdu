using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class User_RoleMapper
    {
        public static User_Role ToDataModel(User_RoleInputModel model)
        {
            return new User_Role
            {
                UserId = model.UserId,
                RoleId = model.RoleId
            };
        }
    }
}
