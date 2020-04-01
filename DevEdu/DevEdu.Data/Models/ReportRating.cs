using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class ReportRating
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CourseName { get; set; }
        public int RatingByCourse { get; set; }
        public int CountOfStudents { get; set; }
    }
}
