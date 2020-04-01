using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels
{
    public class StudentWithCityOutputModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }      
        public string Bio { get; set; }
        public string? Photo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public CityOutputModel City { get; set; }
        public int? Rating { get; set; }
    }
}
