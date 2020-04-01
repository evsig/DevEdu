using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels
{
    public class TimeTableWithCourseNameOutputModel
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public int RoomNumber { get; set; }
        public DateTime Day { get; set; } // => string
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
    }
}
