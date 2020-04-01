using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.InputModels
{
    public class UserLoginPassInputModel
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
    }
}
