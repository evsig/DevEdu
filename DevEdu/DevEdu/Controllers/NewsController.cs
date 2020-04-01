using DevEdu.Data.Storages;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevEdu.Models.InputModels;
using DevEdu.Models.OutputModels;
using DevEdu.Models.Mappings;
using DevEdu.Data.Models;
using DevEdu.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace DevEdu.Controllers
{
    [ApiController]
    [Route("api/[controller]")] /// localhost:44345/api/news/456
    public class NewsController : ControllerBase
    {
        private readonly NewsStorage newsStorage;
        private readonly UserStorage userStorage;
        private readonly GroupStorage groupStorage;

        private int teacherRoleId = 251;
        private int HRRoleId = 252;

        public NewsController(IConfiguration Configuration)
        {
            string dbCon = Configuration.GetConnectionString("DefaultConnection");
            newsStorage = new NewsStorage(dbCon);
            userStorage = new UserStorage(dbCon);
            groupStorage = new GroupStorage(dbCon);

        }
        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<NewsOutputModel>>> GetAllNews()
        {
            var news = NewsMapper.ToOutputModels(await newsStorage.NewsGetAll());
            if (news == null) return NotFound("Objects not fount");

            return Ok(news);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsOutputModel>> GetNewsById(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is empty");

            var news = NewsMapper.ToOutputModel(await newsStorage.NewsGetById(id));
            if (news == null) return NotFound("Object not found");
            return Ok(news);
        }

        [Authorize (Roles = ExistingRoles.Teacher)]
        [HttpGet("for-teacher/{teacherId}")]
        public async Task<ActionResult<IEnumerable<NewsOutputModel>>> GetNewsByTeacherId(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is empty");

            var news = NewsMapper.ToOutputModels(await newsStorage.GetNewsForTeacherFromHR(id));
            if (news == null) return NotFound("Object not found");
            return Ok(news);
        }

        [Authorize(Roles = ExistingRoles.HrOrTeacher)]
        [HttpPost]
        public async Task<ActionResult<int>> AddOrUpdateNews([FromBody] NewsInputModel model)
        {
            if (model == null) return BadRequest("Model is empty");
            var newsId = await newsStorage.NewsAddOrUpdate(NewsMapper.ToDataModel(model));

            if (newsId.Equals(null)) return BadRequest("Failed to add object");

            return Ok(newsId);
        }

        [Authorize(Roles = ExistingRoles.HrOrTeacher)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteNews(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is empty");
            var deletedRow = await newsStorage.NewsDelete(id);
            if (deletedRow.Equals(null)) return BadRequest("Failed to delete object");
            return Ok(deletedRow);
        }

        // ДОБАВЛЕНИЕ НОВОСТЕЙ 

        // для пользователя

        [Authorize]
        [HttpPost("for-user/{userId}")]
        public async Task<ActionResult<int>> AddNewsForUser([FromBody] NewsInputModel model)
        {
            if (model == null) return BadRequest("Model is empty");
            if (model.GroupID != null) { model.GroupID = null; }
            if (model.RecipientID == null) { return BadRequest("The inputModel doesn't contain RecipientID"); }
            var newsId = 0;
            bool isCreationAllowed;
       
            isCreationAllowed = await CreationIsAllowed(model.AuthorId,
                    ((int)model.RecipientID));            

            if (isCreationAllowed && await CanUserPost(model.AuthorId))
            {
                newsId = await newsStorage.NewsAddOrUpdate(NewsMapper.ToDataModel(model));
            }

            if (newsId.Equals(null)) return BadRequest("Failed to add object");
            return Ok(newsId);
        }

        // для группы

        [Authorize]
        [HttpPost("for-group/{groupId}")]
        public async Task<ActionResult<int>> AddNewsForGpoup([FromBody] NewsInputModel model)
        {
            if (model == null) return BadRequest("Model is empty");
            if (model.RecipientID != null) { model.RecipientID = null; }
            if (model.GroupID == null) { return BadRequest("The inputModel doesn't contain GroupID"); }
                        var newsId = 0;
            bool isCreationAllowed;
            isCreationAllowed = await groupStorage.GetBelongnessOfGroupToTheTeacher((int)model.GroupID,
                    model.AuthorId);            
            if (isCreationAllowed && await CanUserPost(model.AuthorId))
            {
                newsId = await newsStorage.NewsAddOrUpdate(NewsMapper.ToDataModel(model));
            }
            if (newsId.Equals(null)) return BadRequest("Failed to add object");
            return Ok(newsId);
        }

        // для всех преподов

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPost("for-teachers")]
        public async Task<ActionResult<List<int>>> AddNewsForTeachers([FromBody] NewsInputModel model)
        {
            List<int> newsIds = new List<int>();

            if (model == null) return BadRequest("Model is empty");
            if (model.GroupID != null) { model.GroupID = null; }
           
                List<User> teachers = await userStorage.GetUserByRoleId(teacherRoleId);
                foreach (var user in teachers)
                {
                    model.RecipientID = user.Id;
                    newsIds.Add(await newsStorage.NewsAddOrUpdate(NewsMapper.ToDataModel(model)));
                }            
            if(newsIds.Equals(null)) return BadRequest("Failed to add object");
            return Ok(newsIds);
        }

        // для всех

        [Authorize]
        [HttpPost("for-all")]
        public async Task<ActionResult<int>> AddNewsForAll([FromBody] NewsInputModel model)
        {
            var newsId = 0;

            if (model == null) return BadRequest("Model is empty");
            if (model.RecipientID != null)
            {
                model.RecipientID = null;
            }
            if (model.GroupID != null)
            {
                model.GroupID = null;
            }
           
            newsId = await newsStorage.NewsAddOrUpdate(NewsMapper.ToDataModel(model));            
            if (newsId.Equals(null)) return BadRequest("Failed to add object");
            return Ok(newsId);
        }


        // ПОЛУЧИТЬ НОВОСТИ
        // для студента
        [Authorize (Roles = ExistingRoles.Student)]
        [HttpGet("for-user/{userId}")]  //1220
        public async Task<ActionResult<IEnumerable<NewsOutputModel>>> GetAllNewsForUser(int userId)
        {
            var news = NewsMapper.ToOutputModels(await newsStorage.NewsGetAllByUserRegistrationDate(userId));
            if (news == null) return NotFound("Objects not fount");

            return Ok(news);
        }

        private async Task<bool> CanUserPost(int id)
        {
            // get list roles
            List<User_Role> authotRoles = await userStorage.GetUser_RoleByUserId(id);
            foreach (var role in authotRoles)
            {
                if (role.Id == teacherRoleId || role.Id == HRRoleId)
                {
                    return true;
                }
            }
            return false;
        }
        
        private async Task<bool> CreationIsAllowed(int teacherId, int studentId)
        {
            StudentGroup group = await groupStorage.StudentGroupGetByUserId(studentId);
            return await groupStorage.GetBelongnessOfGroupToTheTeacher(Convert.ToInt32(group.GroupId), teacherId);
        }
    }
}



