using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevEdu.Auth;
using DevEdu.Common;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Models.InputModel;
using DevEdu.Models.InputModels;
using DevEdu.Models.Mappings;
using DevEdu.Models.OutputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DevEdu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserStorage userStorage;
        private readonly GroupStorage groupStorage;
        private readonly UserAttestationStorage userAttestationStorage;
        public UserController(IConfiguration Configuration)
        {
            string dbCon = Configuration.GetConnectionString("DefaultConnection");
            userStorage = new UserStorage(dbCon);
            groupStorage = new GroupStorage(dbCon);
            userAttestationStorage = new UserAttestationStorage(dbCon);
        }
        #region User

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserOutputModel>>> GetAllUsers()
        {
            IEnumerable<User> users = await userStorage.UserSelectAll();
            return Ok(UserMapper.ToOutputModels(users));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserOutputModel>> GetUserById(int id)
        {
            if (id < 0) return BadRequest();
            User userById = await userStorage.UserSelectById(id);
            return Ok(UserMapper.ToOutputModel(userById));
        }

        [Authorize]
        [HttpGet("get-by-login")]
        public async Task<ActionResult<UserOutputModel>> GetUserByLogin(UserInputModel model)
        {
            if (model == null) return NotFound("Object not found");
            User userByLoginPass = await userStorage.UserGetByLogin(UserMapper.ToDataModel(model));
            return Ok(UserMapper.ToOutputModel(userByLoginPass));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<int?>> AddUser([FromBody] UserInputModel model)        
        {
            if (model == null || model.Id < 1) return BadRequest();

            if (model.Login != null && model.Password != null)
            {
                model.Login = null;
                model.Password = null;
            }
            User userModel = UserMapper.ToDataModel(model);
            int id = await userStorage.UserAddOrUpdate(userModel);
            return Ok(id);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<int?>> UpdateUser([FromBody] UserInputModel model)
        {
            if (model == null || model.Id < 1) return BadRequest();
            model.Password = Hashing.HashUserPassword(model.Password);
            User userModel = UserMapper.ToDataModel(model);
            int id = await userStorage.UserAddOrUpdate(userModel);

            return Ok(id);
        }

        [Authorize]
        [HttpDelete("{userId}")]
        public ActionResult DeleteUser(int userId)
        {
            if (userId < 1) return BadRequest();
            userStorage.UserDeleteById(userId);
            return Ok();
        }

        [Authorize(Roles = ExistingRoles.HR)]
        [HttpPut] // добавить преподавателя в базу
        [Route("user-as-teacher")]
        public async Task<ActionResult> AddUserAsTeacher([FromBody] UserLoginPassInputModel model)
        {
            if (model == null || model.Id < 1) return BadRequest("Uncorrect input data");            
            model.Password = Hashing.HashUserPassword(model.Password);
            await userStorage.AddPasswordLoginToUser(UserPassLoginMapper.FromInputModel(model));
            User_RoleInputModel userRole = new User_RoleInputModel();
            userRole.UserId = model.Id;
            userRole.RoleId = 250;
            await userStorage.User_RoleAdd(User_RoleMapper.ToDataModel(userRole));
            return Ok();
        }

        [Authorize(Roles = ExistingRoles.HR)]
        [HttpPut] // добавить студента в базу
        [Route("user-as-student")]
        public async Task<ActionResult> AddUserAsStudent([FromBody] UserLoginPassInputModel model)
        {
            if (model == null || model.Id < 1) return BadRequest("Uncorrect input data");            
            model.Password = Hashing.HashUserPassword(model.Password);
            await userStorage.AddPasswordLoginToUser(UserPassLoginMapper.FromInputModel(model));
            User_RoleInputModel userRole = new User_RoleInputModel();
            userRole.UserId = model.Id;
            userRole.RoleId = 251;
            await userStorage.User_RoleAdd(User_RoleMapper.ToDataModel(userRole));

            return Ok();
        }

        [Authorize]
        [HttpPut] 
        [Route("change-password")]
        public async Task<ActionResult> ChangePassword([FromBody] UserLoginPassInputModel model)
        {            if (model == null || model.Id < 1) return BadRequest();

            model.Password = Hashing.HashUserPassword(model.Password);
            await userStorage.ChangePassword(UserPassLoginMapper.FromInputModel(model));
            return Ok();
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPut("bio")]
        public async Task<ActionResult> UpdateBioUser([FromBody] UserInputModel model)
        {
            if (model == null || model.Id < 1) return BadRequest();

            await userStorage.UpdateStudentBioByUserId(UserMapper.ToDataModel(model));
            return Ok();
        }
        #endregion

        #region User_Role

        [Authorize(Roles = ExistingRoles.HR)]
        [HttpPost]
        [Route("user-role")]
        public async Task<ActionResult> AddUser_Role([FromBody] User_RoleInputModel model)
        {
            if (model == null || model.Id < 1) return BadRequest();

            return Ok(await userStorage.User_RoleAdd(User_RoleMapper.ToDataModel(model)));      
        }

        [Authorize(Roles = ExistingRoles.HR)]
        [HttpDelete]
        [Route("user-role")]
        public async Task<ActionResult> DeleteUser_Role([FromBody] User_RoleInputModel model)
        {
            if (model == null || model.Id < 1) return BadRequest();

            await userStorage.User_RoleDelete(User_RoleMapper.ToDataModel(model));
            return Ok();
        }

        [Authorize(Roles = ExistingRoles.HR)]
        [HttpGet("user-roles-by-roleid/{roleId}")]
        public async Task<ActionResult<List<User_Role>>> GetUser_RoleByRoleId(int roleId)
        {
            if (roleId < 1) return BadRequest();
            return Ok(await userStorage.User_RoleSelectByRoleId(roleId));
        }

        [Authorize]
        [HttpGet("user-roles-by-userid/{userId}")]
        public async Task<ActionResult<List<Role>>> GetUser_RoleByUserId(int userId)
        {
            if (userId < 1) return BadRequest();

            var result = await userStorage.UserRolesSelectByUserId(userId);
            return Ok(result.ToList());
        }

        [Authorize]
        [HttpGet("user-role")]
        public async Task<ActionResult<List<User_Role>>> GetAllUser_Role()
        {
            return Ok(await userStorage.GetAllUser_Role());
        }

        #endregion        

        //Личный кабинет препода
        //3. Узнать кто учится в группе
        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpGet]
        [Route("teacher/student-in-group/{groupId}")] // 615    
        public async Task<ActionResult<IEnumerable<StudentWithCityOutputModel>>> GetStudentsInGroup(int groupId)
        {
            if (groupId < 0) return BadRequest();
            var students = StudentWithCityMapper.ToOutputModels(await userStorage.GetStudentsInGroup(groupId));
            return Ok(students);
        }

        #region Get Journal Blank Fields of User
        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpGet("get-blank-fields/{userId}")]
        public async Task<ActionResult<List<UserBlankFieldsOutputModel>>> GetBlankFields(int userId)
        {
            if (userId < 0) return BadRequest();
            var blankFields = UserBlankFieldsMapper.ToOutputModels(await userStorage.GetUserBlankFields(userId));
            return Ok(blankFields);
        }

        #endregion

        #region User Profile

        [Authorize(Roles = ExistingRoles.Teacher + "," + ExistingRoles.Student)]
        [HttpGet("profile/{userId}")] //1220
        public async Task<ActionResult<UserProfileOutputModel>> GetAllLessonByUser(int userId)
        {
            if (userId < 1) return BadRequest();

            Task<User> getStudentWithSkills = Task.Run(() => userStorage.GetStudentWithSkillsByUserId(userId));
            Task<IEnumerable<User>> getTeachers = Task.Run(() => userStorage.GetCurrentTeachersByStudentId(userId));

            await Task.WhenAll(getStudentWithSkills, getTeachers);

            User userWithSkills = await getStudentWithSkills;
            IEnumerable<User> teachers = await getTeachers;

            return Ok(UserProfileMapper.ToOutputModel(userWithSkills, teachers));
        }


        [HttpPut("profile/{userId}")] //1220
        public async Task<ActionResult>  UpdateStudentProfileByUserId([FromBody] UserProfileInputModel inputModel, int userId)
        {
            if (inputModel.Id == userId)
            {
               await userStorage.UpdateStudentProfile(UserProfileMapper.ToDataModel(inputModel));
            }
            else
            {
                return BadRequest("Not enough rights!");
            }
            return Ok();
        }


        #endregion

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpGet("user-attestation")]
        public async Task<ActionResult<List<UserAttestationOutputModel>>> GetAllUserAttestation()
        {
            return Ok(UserAttestationMapper.ToOutputModels(await userAttestationStorage.UsersAttestationGetAll()));
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPut("user-attestation")]
        public async Task<ActionResult<int?>> UpdateUserAttestation([FromBody] UserAttestationInputModel model)
        {
            if (model == null || model.UserId < 1) return BadRequest();

            return await userAttestationStorage.UserAttestationAddOrUpdate(UserAttestationMapper.ToDataModel(model));
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpGet("user-attestation/{id}")]
        public async Task<ActionResult<UserAttestationOutputModel>> GetUserAttestation(int id)
        {
            if (id < 1) return BadRequest();
            return Ok(UserAttestationMapper.ToOutputModel(await userAttestationStorage.UserAttestationGetByID(id)));
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPost("user-attestation")]
        public async Task<ActionResult<int?>> AddUserAttestation([FromBody] UserAttestationInputModel model)
        {
            if (model == null || model.UserId < 1) return BadRequest();

            return Ok(await userAttestationStorage.UserAttestationAddOrUpdate(UserAttestationMapper.ToDataModel(model)));
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpDelete("user-attestation/{id}")]
        public async Task<ActionResult> DeleteUserAttestation(int id)
        {
            if (id < 1) return BadRequest();
            await userAttestationStorage.UserAttestationDelete(id);
            return Ok();
        }

        [Authorize (Roles = ExistingRoles.Teacher)]
        [HttpPut("user-attestation-edit")]
        public async Task<ActionResult> EditUserAttestation([FromBody] UserAttestationWideInputModel model)
        {
            if (model == null || model.StudentId < 1) return BadRequest();
            IEnumerable<Group> groups = await groupStorage.GetTeacherGroupsById(model.TeacherId);
            StudentGroup tempStudentGroup = await groupStorage.StudentGroupGetByUserId(model.StudentId);
            Group group = await groupStorage.GroupGetById(tempStudentGroup.GroupId);
            if (groups.Contains(group))
            {
                await userAttestationStorage.UserAttestationEdit(UserAttestationWideMapper.ToDataModels(model));
            }
            return Ok();
        }
    }
}
