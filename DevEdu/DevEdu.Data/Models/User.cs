using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class User : ICloneable
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Bio { get; set; }
        public int CityId { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Photo { get; set; }
        public City City { get; set; }
        public List<Role> Roles { get; set; }
        public StudentGroup StudentGroup { get; set; }
        public List<CourseProgramSkill> Skills { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Id == user.Id &&
                   FirstName == user.FirstName &&
                   LastName == user.LastName &&
                   Patronymic == user.Patronymic &&
                   BirthDate.ToString("yyyy-MM-dd") ==
                    user.BirthDate.ToString("yyyy-MM-dd") &&
                   Email == user.Email &&
                   Phone == user.Phone &&
                   Password == user.Password &&
                   Login == user.Login &&
                   Bio == user.Bio && 
                   CityId == user.CityId &&
                   RegistrationDate == user.RegistrationDate;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Id);
            hash.Add(FirstName);
            hash.Add(LastName);
            hash.Add(Patronymic);
            hash.Add(BirthDate);
            hash.Add(Email);
            hash.Add(Phone);
            hash.Add(Password);
            hash.Add(Login);
            hash.Add(Bio);
            hash.Add(CityId);
            hash.Add(RegistrationDate);
            hash.Add(Photo);
            return hash.ToHashCode();
        }

        public override string ToString()
        {
            string user = "{";
            user = user + "\n\tId: " + Id;
            user = user + "\n\tFirstName: " + FirstName;
            user = user + "\n\tLastName: " + LastName;
            user = user + "\n\tPatronymic: " + Patronymic; ;
            user = user + "\n\tBirthDate: " + BirthDate;
            user = user + "\n\tEmail: " + Email;
            user = user + "\n\tPhone: " + Phone;
            user = user + "\n\tPassword: " + Password;
            user = user + "\n\tLogin: " + Login;
            user = user + "\n\tBio: " + Bio;
            user = user + "\n\tCityId: " + CityId;
            user = user + "\n\tRegistrationDate: " + RegistrationDate;
            user = user + "\n\tPhoto: " + Photo;
            user = user + "\n}";
            return user;
        }

        public object Clone()
        {
            return new User
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                Patronymic = this.Patronymic,
                BirthDate = this.BirthDate,
                Email = this.Email,
                Phone = this.Phone,
                Password = this.Password,
                Login = this.Login,
                Bio = this.Bio,
                CityId = this.CityId,
                RegistrationDate = this.RegistrationDate,
                Photo = this.Photo,
                City = this.City,
                //Role = this.Role,
                StudentGroup = this.StudentGroup,
                Skills = this.Skills
            };
        }
    }
}
