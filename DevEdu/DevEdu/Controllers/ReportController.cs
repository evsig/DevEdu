using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevEdu.Common;
using DevEdu.Data.Storages;
using DevEdu.Models.InputModel;
using DevEdu.Models.Mappings;
using DevEdu.Models.OutputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DevEdu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly ReportStorage reportStorage;

        public ReportController(IConfiguration Configuration) 
        {
            string dbCon = Configuration.GetConnectionString("DefaultConnection");
            reportStorage = new ReportStorage(dbCon);
        }

        [Authorize(Roles = ExistingRoles.HrOrTeacher)]
        [HttpGet("detailed-rating")]
        public async Task<ActionResult<List<ReportRatingOutputModel>>> GetDetailRating([FromBody] ReportRatingInputModel ratings)
        {
            if (ratings == null) return BadRequest("Model is empty");
            var detailRaiting = ReportRatingMapper.ToOutputModels(await reportStorage.GetDetailRating(ReportRatingMapper.FromInputModel(ratings)));
            if (detailRaiting.Equals(null)) return NotFound("Object not found");
            return Ok(detailRaiting);
        }

        [Authorize(Roles = ExistingRoles.HrOrTeacher)]
        [HttpGet("average-rating")]
        public async Task<ActionResult<List<ReportRatingOutputModel>>> GetAverageRatingByDate([FromBody] ReportRatingInputModel ratings)
        {
            if (ratings == null) return BadRequest("Model is empty");
            var averageRating = ReportRatingMapper.ToOutputModels(await reportStorage.GetAverageRating(ReportRatingMapper.FromInputModel(ratings)));
            if (averageRating.Equals(null)) return NotFound("Object not found");
            return Ok(averageRating);
        }

        [Authorize (Roles = ExistingRoles.HrOrTeacher)]
        [HttpGet] // получаем мини-отчет (какие группы проходили определенную тему и сколько раз)
        [Route("groups/by-topics/{ThemeDetailsID}")]
        public async Task<ActionResult<List<GroupWithTopicAndQuantityOutputModel>>> GetGroupByTopic(int themeDetailsID)
        {
            if (themeDetailsID.Equals(null)) return BadRequest("Theme Details ID is empty");
            var groupByTopic = GroupWithTopicAndQuantityMapper.ToOutputModels(await reportStorage.GetQuantityOfLessonsByTopic(themeDetailsID));
            if (groupByTopic == null) return NotFound("Object not found");
            return Ok(groupByTopic);
        }
    }
}