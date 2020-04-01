using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class ReportDetailedGroup
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StudentCount { get; set; }
        public int TeacherCount { get; set; }
    }
}
