using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.InputModels
{
    public class TimeTableInputModel
    {
        public int? Id { get; set; }
        public int GroupId { get; set; }
        public int RoomNumber { get; set; }
        public string Day { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
    }
}
