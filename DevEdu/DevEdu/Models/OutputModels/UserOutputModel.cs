using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels
{
    public class UserOutputModel
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Bio { get; set; }
        public int CityId { get; set; }
        public string RegistrationDate { get; set; }
        public string Photo { get; set; }
    }
}
