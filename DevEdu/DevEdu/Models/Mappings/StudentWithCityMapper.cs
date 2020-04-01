using DevEdu.Data;
using DevEdu.Data.Models;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class StudentWithCityMapper
    {
        public static List<StudentWithCityOutputModel> ToOutputModels(List<User> users)
        {
            List<StudentWithCityOutputModel> result = new List<StudentWithCityOutputModel>();

            foreach (User user in users)
            {
                result.Add(ToOutputModel(user));
            }

            return result;
        }

        public static StudentWithCityOutputModel ToOutputModel(User user)
        {
            var tmp= new StudentWithCityOutputModel
            {
                Id = (int)user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                Email = user.Email,
                Phone = user.Phone,
                Bio = user.Bio,
                BirthDate = user.BirthDate,
                RegistrationDate = (DateTime)user.RegistrationDate,
                City = CityMapper.ToOutputModel(user.City),
                Rating = (int?)user.StudentGroup.Rating,
                Photo = user.Photo,
            };
            return tmp;
        }
    }
}
