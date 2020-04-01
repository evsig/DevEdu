using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.InputModels
{
    public class StudentGroupInputModel
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int? Rating { get; set; }
    }
}
