using System.Collections.Generic;

namespace DevEdu.Models.OutputModels
{
    public class UserProfileOutputModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
        public List<UserSkillsOutputModel> Skills { get; set; }
        public List<UserNamePhotoOutputModel> Teachers { get; set; }
    }
}
