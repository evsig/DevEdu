using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models
{
    public class ReportDetailedGroupOutputModel
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int StudentCount { get; set; }
        public int TeacherCount { get; set; }
    }
}
