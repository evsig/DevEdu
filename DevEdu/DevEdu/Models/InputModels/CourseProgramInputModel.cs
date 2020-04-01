using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models.InputModels
{
    public class CourseProgramInputModel
    {
        public int? Id { get; set; }
        public int CourseId { get; set; }
        public bool IsActual { get; set; }
    }
}
