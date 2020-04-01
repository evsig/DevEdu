using System;

namespace DevEdu.Data.Models
{
    public class CourseProgram : ICloneable
    {
        public int? Id { get; set; }
        public int CourseId { get; set; }
        public bool IsActual { get; set; }
        public string Title { get; set; }

        //public List<ProgramDetails> ProgramDetails { get; set; }

        public object Clone()
        {
            return new CourseProgram { CourseId = this.CourseId, IsActual= this.IsActual, Title= this.Title };
        }

    }
}
