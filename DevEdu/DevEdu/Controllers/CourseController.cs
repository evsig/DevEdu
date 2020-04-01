using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DevEdu.Common;
using DevEdu.Data;
using DevEdu.Data.Models;
using DevEdu.Data.Models.InputModels;
using DevEdu.Data.Models.OutputModels;
using DevEdu.Data.Storages;
using DevEdu.Models.InputModels;
using DevEdu.Models.Mappings;
using DevEdu.Models.OutputModels;
using DevEdu.Models.OutputModels.Mappings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DevEdu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly CourseStorage courseStorage;

        public CourseController(IConfiguration Configuration)
        {
            string dbCon = Configuration.GetConnectionString("DefaultConnection");
            courseStorage = new CourseStorage(dbCon);
        }

        #region Course 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> Get()
        {
            var result = await courseStorage.CourseGetAll();

            if (result == null) return NotFound("Objects not found");

            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id}")] //api/course/id  
        public async Task<ActionResult<Course>> Get(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is null");

            var result = await courseStorage.CourseGetAll();

            if (result == null) return NotFound("Object not found");

            return Ok(result);
        }

        [Authorize]
        [HttpPost("course")]
        public ActionResult<int> InsertNewCourse([FromBody] Course newCourse)
        {
            if (newCourse == null) return BadRequest("Model is empty");
            var newCourseId = courseStorage.CourseAddOrUpdate(newCourse);

            if (newCourseId.Equals(null)) return BadRequest("Failed to add object");

            return Ok(newCourseId);
        }

        [Authorize]
        [HttpPut("course")]
        public ActionResult<int> UpdateCourse([FromBody] Course сourse)
        {
            if (сourse == null) return BadRequest("Model is empty");

            var сourseId = courseStorage.CourseAddOrUpdate(сourse);

            if (сourse.Equals(null)) return BadRequest("Failed to update object");

            return Ok(сourseId);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse([FromBody] Course сourse)
        {
            //Тут нужна проверка типа такой if (id.Equals(null)) return BadRequest("Id is null"); 
            await courseStorage.CourseDelete((int)сourse.Id);

            return Ok();
        }

        #endregion

        #region CourseProgram 
        [Authorize]
        [HttpGet("course-program")]
        public async Task<ActionResult<List<CourseProgramOutputModel>>> GetObj()
        {
            var coursePrograms = CourseProgramMapper.ToListCourseProgramOutputModel(await courseStorage.CourseProgramGetAll());
            if (coursePrograms == null)
            {
                return NotFound();
            }
            return Ok(coursePrograms);
        }

        [Authorize]
        [HttpGet("course-program/{id}")]
        public async Task<ActionResult<CourseProgramOutputModel>> GetObjById(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is empty");

            var courseProgram = CourseProgramMapper.ToCourseProgramOutputModel(await courseStorage.CourseProgramGetById(id));

            if (courseProgram == null) return NotFound("Object not found");

            return Ok(courseProgram);
        }

        [Authorize]
        [HttpGet("course-program/by-course-id/{courseId}")]
        public async Task<ActionResult<CourseProgramOutputModel>> GetObjByCourseId(int courseId)
        {
            if (courseId.Equals(null)) return BadRequest("Course Id is empty");

            var courseProgram = CourseProgramMapper.ToCourseProgramOutputModel(await courseStorage.CourseProgramGetByCourseId(courseId));

            if (courseProgram == null) return NotFound("Object not found");

            return Ok(courseProgram);
        }

        [Authorize]
        [HttpPost("course-program")]
        public async Task<ActionResult<int>> InsertNewCourseProgram([FromBody]CourseProgramInputModel newProgram)
        {
            if (newProgram == null) return BadRequest("Model is empty");
            var courseProgramId = await courseStorage.CourseProgramAddOrUpdate(CourseProgramMapper.ToCourseProgram(newProgram));

            if (courseProgramId.Equals(null)) return BadRequest("Failed to add object");

            return Ok(courseProgramId);
        }

        [Authorize]
        [HttpPut("course-program")]
        public async Task<ActionResult<int>> UpdateCourseProgram([FromBody] CourseProgramInputModel updateProgram)
        {
            if (updateProgram == null) return BadRequest("Model is empty");

            var courseProgramId = await courseStorage.CourseProgramAddOrUpdate(CourseProgramMapper.ToCourseProgram(updateProgram));

            if (courseProgramId.Equals(null)) return BadRequest("Failed to update object");

            return Ok(courseProgramId);
        }

        [Authorize]
        [HttpDelete("course-program/{id}")]
        public async Task<ActionResult> DeleteCourseProgram(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is empty");

            await courseStorage.CourseProgramDelete(id);

            return Ok();
        }

        [Authorize]
        [HttpPost("course-program/new-version/{courseProgramId}")]
        public async Task<ActionResult<int>> AddNewVersionCourseProgram(int courseProgramId)
        {
            if (courseProgramId.Equals(null)) return BadRequest("Id is empty");

            courseProgramId = await courseStorage.CourseProgramAddNewVersion(courseProgramId);

            if (courseProgramId.Equals(null)) return BadRequest("Failed to add new version");

            return Ok(courseProgramId);
        }
        #endregion

        #region ThemeDetails 
        [Authorize]
        [HttpGet("theme-details")]

        public async Task<ActionResult<List<ThemeDetails>>> GetThDetails()
        {
            var result = await courseStorage.ThemeDetailsGetAll();
            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("theme-details/{id}")]
        public async Task<ActionResult<ThemeDetails>> GetThDetailsById(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is null");

            var result = await courseStorage.ThemeDetailsGetById(id);

            if (result == null) return NotFound();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("theme-details/{programDetailId}")]
        public async Task<ActionResult<ThemeDetails>> GetThDetailsByPrDetailId(int programDetailId)
        {
            if (programDetailId.Equals(null)) return BadRequest("Id is null");
            var result = await courseStorage.ThemeDetailsGetByProgramDetailId(programDetailId);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [Authorize]
        [HttpPost("theme-details")]
        public async Task<ActionResult<int>> AddThemeDetails([FromBody]ThemeDetails themeDetails)
        {
            if (themeDetails == null) return BadRequest("Model is null");

            var result = await courseStorage.ThemeDetailsAddOrUpdate(themeDetails);

            if (result.Equals(null)) return BadRequest("Failed");

            return Ok(result);
        }

        [Authorize]
        [HttpPut("theme-details")]
        public async Task<ActionResult<int>> UpdateThemeDetails([FromBody] ThemeDetails themeDetails)
        {
            if (themeDetails == null) return BadRequest("Model is null");

            var result = await courseStorage.ThemeDetailsAddOrUpdate(themeDetails);

            if (result.Equals(null)) return BadRequest("Failed");

            return Ok(result);
        }

        [Authorize]
        [HttpDelete("theme-details")]
        public async Task<ActionResult> DeleteThemeDetails(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is null");

            await courseStorage.ThemeDetailsDeleteById(id);
            return Ok();
        }

        [Authorize]
        [HttpGet("theme-details/{GroupId}")]
        public async Task<ActionResult<List<ThemeDetails>>> GetThemeDetailsByGroupId(int GroupId)
        {
            if (GroupId.Equals(null)) return BadRequest("Id is null");

            var result = await courseStorage.ThemeDetailsGetByGroupId(GroupId);

            if (result == null) return NotFound();

            return Ok(result);
        }

        // Получить список непройденных тем в разрезе группы
        [Authorize (Roles = ExistingRoles.Teacher)]
        [HttpGet("topics-to-teach/{groupId}")]
        public async Task<ActionResult<IEnumerable<TopicToTeachOutputModel>>> GetTopicsToTeachByGroupId(int groupId)
        {
            if (groupId.Equals(null)) return BadRequest("Id is null");

            var result = TopicToTeachMapper.ToOutputModels(await courseStorage.GetTopicsToTeachByGroupId(groupId));

            if (result == null) return NotFound();

            return Ok(result);
        }

        #endregion

        #region ProgramDetails
        [Authorize]
        [HttpGet("program-details")]
        public async Task<ActionResult<List<ProgramDetails>>> ProgramDetailsGetAll()
        {

            var result = await courseStorage.ProgramDetailsGetAll();
            if (result == null) return NotFound();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("program-details/{id}")]
        public async Task<ActionResult<ProgramDetails>> ProgramDetailsGetById(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is null");
            var result = await courseStorage.ProgramDetailsGetById(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [Authorize]
        [HttpGet("program-details/{courseProgramId}")]
        public async Task<ActionResult<ProgramDetails>> ProgramDetailsGetByCourseProgramId(int courseProgramId)
        {
            if (courseProgramId.Equals(null)) return BadRequest("Id is null");
            var result = await courseStorage.ProgramDetailsGetByCourseProgramId(courseProgramId);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [Authorize]
        [HttpPost("program-details")]
        public async Task<ActionResult<int>> InsertNewProgramDetails([FromBody]ProgramDetails programDetails)
        {
            if (programDetails == null) return BadRequest("Model is empty");
            var result = await courseStorage.ProgramDetailsAddOrUpdate(programDetails);

            if (result.Equals(null)) return NotFound();
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("program-details/{int id}")]
        public async Task<ActionResult> DeleteProgramDetails(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is null");
            await courseStorage.ProgramDetailsDelete(id);
            return Ok();
        }

        [Authorize]
        [HttpPut("program-details")]
        public async Task<ActionResult<int>> UpdateProgramDetails([FromBody]ProgramDetails newProgramDetails)
        {
            if (newProgramDetails == null) return BadRequest("Model is empty");
            var result = await courseStorage.ProgramDetailsAddOrUpdate(newProgramDetails);
            if (result.Equals(null)) return BadRequest();

            return Ok(result);
        }
        #endregion
    }
}