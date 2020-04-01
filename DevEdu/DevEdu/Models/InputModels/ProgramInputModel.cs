using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.InputModels
{
    public class ProgramInputModel
    {
        public int? CourseProgramId { get; set; }
        public int CourseId { get; set; }
        public bool IsActual { get; set; }
        public List<ProgramDetailsInputModel> ProgramDetails { get; set; }        
    }
}
