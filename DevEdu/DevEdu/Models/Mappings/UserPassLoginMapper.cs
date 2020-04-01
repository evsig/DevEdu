using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class UserPassLoginMapper
    {

        public static User FromInputModel(UserLoginPassInputModel loginPassword)
        {            
            return new User
            {
                Id = loginPassword.Id,
                Password = loginPassword.Password,
                Login = loginPassword.Login,
            };                       
        }

        public static UserLoginPassOutputModel ToOutputModel(User currentUser)
        {
            return new UserLoginPassOutputModel
            {
                Id = (int)currentUser.Id,
                Login = currentUser.Login,
                Roles = RoleMapper.ToOutputModels(currentUser.Roles)
            };
        }
    }
}
