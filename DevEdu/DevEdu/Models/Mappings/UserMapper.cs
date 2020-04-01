using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class UserMapper
    {
        public static IEnumerable<UserOutputModel> ToOutputModels(IEnumerable<User> users)
        {
            List<UserOutputModel> result = new List<UserOutputModel>();

            foreach (User user in users)
            {
                result.Add(ToOutputModel(user));
            }

            return result;
        }

        public static UserOutputModel ToOutputModel(User user)
        {
            return new UserOutputModel
            {
                Id = (int)user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                BirthDate = user.BirthDate.ToString(@"yyyy/MM/dd"),
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password,
                Login = user.Login,
                Bio = user.Bio,
                CityId = user.CityId,
                RegistrationDate = user.RegistrationDate.ToString(),
                Photo = user.Photo
            };
        }

        public static IEnumerable<User> ToDataModels(List<UserInputModel> users)
        {
            List<User> result = new List<User>();

            foreach (UserInputModel user in users)
            {
                result.Add(ToDataModel(user));
            }

            return result;
        }

        public static User ToDataModel(UserInputModel user)
        {
            return new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                BirthDate = Convert.ToDateTime(user.BirthDate),
                Email = user.Email,
                Phone = user.Phone,
                Password = user.Password,
                Login = user.Login,
                Bio = user.Bio,
                CityId = user.CityId,
                RegistrationDate = Convert.ToDateTime(user.RegistrationDate),
                Photo = user.Photo
            };
        }
    }
}
