using DevEdu.Data.Models;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class UsersWithSomeRoleMapper
    {
        public static List<UsersWithSomeRoleOutputModel> ToOutputModels(List<User> users)
        {
            List<UsersWithSomeRoleOutputModel> result = new List<UsersWithSomeRoleOutputModel>();

            foreach (User user in users)
            {
                result.Add(ToOutputModel(user));
            }

            return result;
        }

        public static UsersWithSomeRoleOutputModel ToOutputModel(User user)
        {
            var result = new UsersWithSomeRoleOutputModel
            {
                Id = (int)user.Id,
                FirstName = user.FirstName,
                Patronymic = user.Patronymic,
                BirthDate = user.BirthDate.ToString(),
                Email = user.Email,
                Phone = user.Phone,
                Bio = user.Bio,
                City = CityMapper.ToOutputModel(user.City).Name,
                RegistrationDate = user.RegistrationDate.ToString(),
                Roles = RoleMapper.ToOutputModels(user.Roles)
            };
            return result;
        }
    }
}
