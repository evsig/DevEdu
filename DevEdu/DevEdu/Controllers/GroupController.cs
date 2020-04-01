using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevEdu.Common;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Models.InputModels;
using DevEdu.Models.Mappings;
using DevEdu.Models.OutputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DevEdu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly GroupStorage groupStorage;

        public GroupController(IConfiguration Configuration)
        {
            string dbCon = Configuration.GetConnectionString("DefaultConnection");
            groupStorage = new GroupStorage(dbCon);
        }

        [Authorize(Roles = ExistingRoles.Teacher + "," + ExistingRoles.HR)]
        [HttpPost]
        [Route("add-or-update")]
        public async Task<ActionResult<int>> AddOrUpdateGroup([FromBody] Group model)
        {
            if (model == null) return BadRequest("Model is empty");
            var newsId = await groupStorage.GroupAddOrUpdate(model);

            if (newsId.Equals(null)) return BadRequest("Failed to add object");

            return Ok(newsId);
        }

        [Authorize]
        [HttpGet]
        [Route("get/{id}")]
        public async Task<ActionResult<Group>> JournalGetById(int id)
        {
            if(id.Equals(null)) return BadRequest("Id is empty");
            var journal = await groupStorage.GroupGetById(id);

            if (journal == null) return NotFound("Object not found");
            return Ok(journal);
        }

        [Authorize]
        [HttpGet]
        [Route("get-all")]
        public async Task<ActionResult<List<Group>>> GroupGetAll()
        {
            var group = await groupStorage.GroupGetAll();
            if (group == null)
            {
                return NotFound("Object not found");
            }
            return Ok(group);
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult> DeleteByIdJournal(int id)
        {
            if (id.Equals(null)) return BadRequest("Id is empty");

            await groupStorage.GroupDelete(id);

            return Ok();
            
        }

        #region Личный кабинет препода
        //1. Получить список своих активных групп

        [HttpGet]
        [Route("groups-by-teacher/{userId}")]
        public async Task<ActionResult<IEnumerable<GroupOutputModel>>> GetAllGroupByUser(int userId)
        {
            if (userId.Equals(null)) return BadRequest("Group Id is empty");
            var result = GroupMapper.ToOutputModels(await groupStorage.GetTeacherGroupsById(userId));
            if (result.Equals(null)) return BadRequest("Failed to add object");
            return Ok(result);
        }


        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPut]
        [Route("user-rating")]
        public ActionResult UpdateUserRating([FromBody] StudentGroupInputModel model)
        {
            if (model == null || model.UserId < 1 || model.GroupId < 1) return BadRequest("Object not found");
            StudentGroup modelSG = StudentGroupMapper.ToDataModel(model);
            if (modelSG.Rating > -1 && modelSG.Rating < 11)
            {
                groupStorage.UpdateUserRating(modelSG);
                return Ok();
            }
            else 
                return BadRequest("Введите рейтинг от 0 до 10");             
        }
        #endregion


    }
}
