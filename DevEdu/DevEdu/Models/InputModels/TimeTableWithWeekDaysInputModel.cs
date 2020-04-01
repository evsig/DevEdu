using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.InputModels
{
    public class TimeTableWithWeekDaysInputModel
    {
        public int RoomNumber { get; set; }
        public List<int> WeekDays { get; set; } 
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GroupId { get; set; }

    }
}

