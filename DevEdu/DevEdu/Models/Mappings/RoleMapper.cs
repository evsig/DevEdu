using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class RoleMapper
    {
        public static List<RoleOutputModel> ToOutputModels(List<Role> roles)
        {
            List<RoleOutputModel> result = new List<RoleOutputModel>();

            foreach (Role role in roles)
            {
                result.Add(ToOutputModel(role));
            }

            return result;
        }

        public static RoleOutputModel ToOutputModel(Role role)
        {
            return new RoleOutputModel
            {
                Id = (int)role.Id,
                Name = role.Name
            };
        }

        public static Role ToDataModel(RoleInputModel model)
        {
            return new Role
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
