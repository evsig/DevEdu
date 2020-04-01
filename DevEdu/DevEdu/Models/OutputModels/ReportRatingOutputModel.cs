using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels
{
    public class ReportRatingOutputModel
    {
        public string CourseName { get; set; }
        public int CountOfStudents { get; set; }
        public int RatingByCourse { get; set; }
    }
}
