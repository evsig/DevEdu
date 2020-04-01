using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevEdu.Common;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DevEdu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly CourseStorage courseStorage;
        private readonly GroupStorage groupStorage;

        public CommonController(IConfiguration Configuration)
        {
            string dbCon = Configuration.GetConnectionString("DefaultConnection");
            userStorage = new UserStorage(dbCon);
            courseStorage = new CourseStorage(dbCon);
            groupStorage = new GroupStorage(dbCon);
        }
        #region student-group

        [Authorize(Roles = ExistingRoles.HR)]
        [HttpPost]
        [Route("student-group")]
        //Добавить студента в группу
        public async Task InsertStudentGroup([FromBody]StudentGroup model)
        {
            await groupStorage.StudentGroupInsert(model);
        }

        [Authorize]
        [HttpDelete]
        [Route("student-group")]
        public async Task DeleteStudentGroup([FromBody]StudentGroup student_Group)
        {
            await groupStorage.StudentGroupDelete(student_Group);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentGroup>> GetStudentGroupById(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is empty");
            return await groupStorage.StudentGroupGetById(id);
        }

        
        [Authorize(Roles = ExistingRoles.HR)]
        [HttpPost]
        [Route("teacher-group")]
        //Добавить учителя для группы
        public async Task TeacherGroupInsert([FromBody]TeacherGroup model)
        {
            await groupStorage.TeacherGroupInsert(model);
        }

        [Authorize]
        [HttpDelete]
        [Route("teacher-group")]
        public async Task TeacherGroupDelete([FromBody]TeacherGroup teacher_Group)
        {
            await groupStorage.TeacherGroupDelete(teacher_Group.GroupId, teacher_Group.UserId);
        }

        [Authorize]
        [HttpGet]
        [Route("teacher-group/{id}")]
        public async Task<ActionResult<TeacherGroup>> TeacherGroupGetById(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is empty");
            return await groupStorage.TeacherGroupGetById(id);
        }

        #endregion

        #region user-skill
        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPost]
        [Route("user-skill/{userId}")]  //Добавить Skill пользователю
        public async Task UserSkillAdd([FromBody] User_Skill model)
        {
            await userStorage.UserSkillAdd(model);
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpDelete]
        [Route("user-skill/{userId}")] //Удалить Skill пользователю
        public async Task UserSkillDelete([FromBody] User_Skill model)
        {
            await userStorage.UserSkillDelete(model);
        }

        [Authorize(Roles = ExistingRoles.Admin)]
        [HttpDelete]
        [Route("user-all-skill/{userId}/{skillId}")]  //Удалить Skill всем пользователям
        public async Task<ActionResult> AllUserSkillDelete(int skillId)
        {
            if (skillId.Equals(null)) return BadRequest("Id is empty");
            return Ok(await userStorage.UsersSkillDeleteAll(skillId));
        }

        #endregion

                #region course-program-skill
        [Authorize]
        [HttpGet]
        [Route("course-program-skill")]
        public async Task<IEnumerable<CourseProgramSkill>> GetAllCourseProgramSkills()
        {
            return await courseStorage.CourseProgramSkillsGetAll();
        }

        [Authorize]
        [HttpGet]
        [Route("course-program-skill{id}")]
        public async Task<ActionResult<CourseProgramSkill>> GetCourseProgramSkillById(int Id)
        {
            if (Id.Equals(null)) return BadRequest("Id is empty");
            return Ok(await courseStorage.CourseProgramSkillGetById(Id));
        }

        [Authorize]
        [HttpPost]
        [Route("course-program-skill")]
        //Добавить или изменить CourseProgramSkill 
        public async Task<int?> CourseProgramSkillAddOrUpdate([FromBody] CourseProgramSkill model)
        {
            return await courseStorage.CourseProgramSkillAddOrUpdate(model);
        }

        [Authorize]
        [HttpDelete]
        [Route("course-program-skill")]
        //Удалить CourseProgramSkill
        public async Task<int?> CourseProgramSkillDelete([FromBody] CourseProgramSkill model)
        {
            return await courseStorage.CourseProgramSkillDelete(model);
        }
        #endregion
    }
}