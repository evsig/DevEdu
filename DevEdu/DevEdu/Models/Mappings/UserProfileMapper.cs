using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using DevEdu.Models.Mappings;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;

namespace DevEdu.Models.Mappings
{
    public class UserProfileMapper
    {
        public static UserProfileOutputModel ToOutputModel(User user, IEnumerable<User> teachers)
        {
            var result = new UserProfileOutputModel
            {
                UserId = (int)user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Patronymic = user.Patronymic,
                BirthDate = user.BirthDate.ToString(@"yyyy/MM/dd"),
                Email = user.Email,
                Phone = user.Phone,
                Photo = user.Photo,
                Skills = UserSkillsMapper.ToOutputModels(user.Skills),
                Teachers = UserNameMapper.ToOutputModels(teachers)
            };
            return result;
        }
        public static User ToDataModel(UserProfileInputModel inputModel)
        {
            User user = new User
            {
                Id = inputModel.Id,
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Patronymic = inputModel.Patronymic,
                BirthDate = Convert.ToDateTime(inputModel.BirthDate),
                Email = inputModel.Email,
                Phone = inputModel.Phone,
                Photo = inputModel.Photo
            };
            return user;
        }
    }

}
