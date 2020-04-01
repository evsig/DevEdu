using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels
{
    public class LessonOutputModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Date { get; set; }
        public string Hometask { get; set; }
        public string Videos { get; set; }
        public string ToRead { get; set; }
    }
}
