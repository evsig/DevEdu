using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.InputModels
{
    public class User_RoleInputModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
