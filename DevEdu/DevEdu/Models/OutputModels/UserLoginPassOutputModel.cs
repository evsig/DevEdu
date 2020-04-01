using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels
{
    public class UserLoginPassOutputModel
    {
        public int Id;
        public string Login;
        public string Password;
        public List<RoleOutputModel> Roles { get; set; }
    }
}
