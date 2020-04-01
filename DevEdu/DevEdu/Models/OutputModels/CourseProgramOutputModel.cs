using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models.OutputModels
{
    public class CourseProgramOutputModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public bool IsActual { get; set; }
        public List<ProgramDetails> ProgramDetails { get; set; }
    }
}
