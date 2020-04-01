using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevEdu.Data.Models;
using DevEdu.Data.Models.InputModels;
using DevEdu.Data.Models.OutputModels;
using DevEdu.Models.InputModels;

namespace DevEdu.Models.Mappings
{
    public class CourseProgramMapper
    {
        public static List<CourseProgramOutputModel> ToListCourseProgramOutputModel(List<CourseProgram> programs)
        {
            var result = new List<CourseProgramOutputModel>();
            foreach (var varible in programs)
            {
                result.Add(new CourseProgramOutputModel
                {
                    Id = varible.Id,
                    CourseId = varible.CourseId,
                    IsActual = varible.IsActual
                });
            }

            return result;
        }

        public static CourseProgramOutputModel ToCourseProgramOutputModel(CourseProgram program)
        {
            return new CourseProgramOutputModel
            {
                Id = program.Id,
                CourseId = program.CourseId,
                IsActual = program.IsActual
            };
        }

        public static CourseProgram ToCourseProgram(CourseProgramInputModel program)
        {
            return new CourseProgram()
            {
                Id = program.Id,
                CourseId = program.CourseId,
                IsActual = program.IsActual
            };
        }

        public static List<CourseProgram> ToCourseProgramAndProgramDetails(ProgramInputModel program)
        {
            List<CourseProgram> data = new List<CourseProgram>();
            List<ProgramDetails> details = new List<ProgramDetails>();
            foreach (var varible in program.ProgramDetails)
            {
                details.Add(new ProgramDetails
                {
                    LessonNumber = varible.LessonNumber,
                    LessonTheme = varible.LessonTheme
                });
            }
            data.Add(new CourseProgram
            {
                Id = program.CourseProgramId,
                CourseId = program.CourseId,
                IsActual = program.IsActual,
            });


            return data;
        }
    }
}
