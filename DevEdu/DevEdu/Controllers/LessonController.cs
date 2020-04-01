using System;
using System.Collections.Generic;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Models.InputModels;
using DevEdu.Models.Mappings;
using DevEdu.Models.OutputModels;
using DevEdu.Models.OutputModels.Mappings;
using Microsoft.AspNetCore.Mvc;
using DevEdu.Models;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using DevEdu.Common;
using Microsoft.AspNetCore.Authorization;
using DevEdu.Common;

namespace DevEdu.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly LessonStorage lessonStorage;
        private readonly GroupStorage groupStorage;
        private readonly CourseStorage courseStorage;

        public LessonController(IConfiguration Configuration)
        {
            string dbCon = Configuration.GetConnectionString("DefaultConnection");
            lessonStorage = new LessonStorage(dbCon);
            groupStorage = new GroupStorage(dbCon);
            courseStorage = new CourseStorage(dbCon);
        }

        [Authorize]
        [HttpGet("by-student/{userId}")] //1287
        public async Task<ActionResult<List<LessonWithJournalAndTopicOutputModel>>> GetAllLessonByUser(int userId)
        {
            IEnumerable<Lesson> lessons = await lessonStorage.GetLessonsWithJournalsAndTopics(userId);
            return Ok(LessonWithJournalAndTopicMapper.ToOutputModels(lessons));
        }

        [Authorize(Roles = ExistingRoles.Student)]
        [HttpPut("hometask")]
        public async Task<ActionResult<int>> SetHometask([FromBody] LessonHometaskInputModel inputModel)
        {
            bool isUserInGroup = inputModel.GroupId == (await groupStorage.StudentGroupGetByUserId((int)inputModel.UserId)).GroupId;
            if (isUserInGroup)
            {
                return await lessonStorage.AddOrUpdateLesson(LessonHometaskMapper.ToDataModel(inputModel));
            }
            else
            {
                return -1;
            }
        }


        [Authorize]
        [HttpGet]
        [Route("student/timetable/{teacherId}")]
        public async Task<List<TimeTableWithCourseNameOutputModel>> GetTimetableForStudent(int teacherId)
        {
            return TimeTableWithCourseNameMapper.ToOutputModels(await lessonStorage.GetTeacherTimetable(teacherId));
        }
        #region Journal

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPost]
        [Route("journal")]
        public async Task<ActionResult<int>> AddOrUpdateJournal([FromBody] JournalFullInputModel inputModel)
        {
            Journal journal = JournalFullMapper.ToDataModel(inputModel);
            if (journal == null) return BadRequest("Model is empty");
            return Ok(await lessonStorage.AddOrUpdateJournal(journal));
        }

        [Authorize]
        [HttpGet("journal/{id}")]
        public async Task<ActionResult<JournalOutputModel>> JournalGetById(int id)
        {
            Journal journal = await lessonStorage.GetJournalById(id);
            if (journal == null) return BadRequest("Model is empty");
            return Ok(JournalMapper.ToOutputModel(journal));
        }

        [Authorize]
        [HttpGet]
        [Route("journal")]
        public async Task<ActionResult<List<JournalOutputModel>>> JournalGetAll()
        {
            List<Journal> journals = await lessonStorage.GetAllJournal();
            return Ok(JournalMapper.ToOutputModels(journals));
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpDelete("journal/{id}")]
        public async Task<ActionResult> DeleteByIdJournal(int id)
        {
            await lessonStorage.DeleteJournal(id);
            return Ok();
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPut]
        [Route("fill-who-were-absent")]
        public async Task<ActionResult> FillWhoWereAbsent(JournalInputModel inputModel)
        {
            if (inputModel == null) { return BadRequest("Model is empty"); }
            List<Journal> journals = JournalMapper.ToModel(inputModel);
            bool belong = await groupStorage.GetBelongnessOfGroupToTheTeacher(inputModel.GroupId, inputModel.TeacherId);
            if (belong) { lessonStorage.FillWhoWhereAbsent(journals); return Ok(); }
            else { return BadRequest(("Failed to fill")); }
        }
        #endregion

        #region Lesson 
        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPost]    // api/lesson
        public async Task<ActionResult<int>> AddOrUpdateLesson([FromBody] LessonInputModel inputModel)
        {
            Lesson lesson = LessonMapper.ToDataModel(inputModel);
            if (lesson == null) return BadRequest("Model is empty");
            return Ok(await lessonStorage.AddOrUpdateLesson(lesson));
        }

        [Authorize]
        [HttpGet("{id}")]  // api/lesson/735
        public async Task<ActionResult<LessonOutputModel>> LessonGetById(int id)
        {
            Lesson lesson = await lessonStorage.GetLessonById(id);
            if (lesson == null) return BadRequest($"Lesson {id} does not exist");
            return Ok(LessonMapper.ToOutputModel(lesson));
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<LessonOutputModel>>> LessonGetAll()
        {
            List<Lesson> lessons = await lessonStorage.GetAllLesson();
            return Ok(LessonMapper.ToOutputModels(lessons));
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpDelete("{id}")] // api/lesson/735
        public async Task<ActionResult> DeleteLessonById(int id)
        {
            await lessonStorage.DeleteLesson(id);
            return Ok();
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPost("with-journals")] //api/lesson/with-journals
        public async Task<ActionResult<int?>> AddLessonWithJournals([FromBody] LessonInputModel inputModel)
        {
            if (inputModel == null) return BadRequest("Model is empty");

            Lesson dataModel = LessonMapper.ToDataModel(inputModel);

            if (dataModel == null) return BadRequest("Failed to add an object");

            return Ok(await lessonStorage.AddJournals(dataModel));
        }

        #endregion

        #region LessonTopic  outputmodel ?

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPost("lesson-topic")] // api/lesson/lesson-topic
        public async Task<ActionResult<int>> AddLessonTopic([FromBody] LessonTopicInputModel inputModel)
        {
            LessonTopic lessonTopic = LessonTopicMapper.ToDataModel(inputModel);
            if (lessonTopic == null) return BadRequest("Model is empty");
            return Ok(await lessonStorage.AddLessonTopic(lessonTopic));
        }

        [Authorize]
        [HttpGet("lesson-topic/{id}")]
        public async Task<ActionResult<LessonTopic>> LessonTopicGetById(int id) // outputmodel ?
        {
            return Ok(await lessonStorage.GetLessonTopicById(id));
        }

        [Authorize]
        [HttpGet("lesson-topic")]
        public async Task<ActionResult<List<LessonTopic>>> LessonTopicGetAll()
        {
            return Ok(await lessonStorage.GetAllLessonTopic());
        }

        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpDelete("lesson-topic/{id}")]
        public async Task<ActionResult> DeleteByIdLessonTopic(int id)
        {
            await lessonStorage.DeleteLessonTopic(id);
            return Ok();
        }
        #endregion

        #region themes 

        //указакать какие темы были пройдены на занятии   
        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPost("themes")]  //  /api/lesson/themes 
        public async Task AddTopicsToLesson([FromBody]LessonWithTopicsInputModel lessonWithTopicsInputModel)
        {
            IEnumerable<ThemeDetails> allThemes = await courseStorage.ThemeDetailsGetByGroupId(lessonWithTopicsInputModel.GroupId);
            IEnumerable<int> themesToLearn = allThemes.Select(g => (int)g.Id).ToList();
            IEnumerable<Group> teacherGroups = await groupStorage.GetTeacherGroupsById(lessonWithTopicsInputModel.TeacherId);
            if (teacherGroups.Select(g => g.Id).Contains(lessonWithTopicsInputModel.GroupId))
            {
                foreach (var item in lessonWithTopicsInputModel.ThemeDetailsIds.Distinct())
                {
                    if (themesToLearn.Contains(item))
                    {
                        await lessonStorage.AddLessonTopic(new LessonTopic { LessonId = lessonWithTopicsInputModel.LessonId, ThemeDetailsId = item });
                    }
                }
            }
            else
            {
                Console.WriteLine("Учитель не может добавлять темы в уроки этих групп");
            }
        }
        #endregion

        #region TimeTable  
        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpPost("timetable")]
        public async Task<ActionResult<int>> AddOrUpdateTimeTable([FromBody] TimeTableInputModel inputModel)
        {
            TimeTable timeTable = TimeTableMapper.ToDataModel(inputModel);
            if (timeTable == null) return BadRequest("Model is empty");
            return await lessonStorage.AddOrUpdateTimeTable(timeTable);
        }

        [Authorize]
        [HttpGet("timetable/{id}")]
        public async Task<ActionResult<TimeTable>> TimeTableGetById(int id)
        {
            return Ok(await lessonStorage.GetTimeTableById(id));
        }


        [Authorize(Roles = ExistingRoles.Teacher)]
        [HttpDelete("timetable/{id}")]
        public async Task<ActionResult> TimeTableDeleteById(int id)
        {
            await lessonStorage.DeleteTimeTable(id);
            return Ok();
        }

        //проверить перед добавлением не пересекаются ли занятия в разных группах
        //если нет, то добавить, если да, то вывести пересечения
        [Authorize(Roles = ExistingRoles.HR)]
        [HttpPost("timetable-checked")]
        public async Task<ActionResult<TimeTableWithCourseNameOutputModel>> GetTimeTableConflicts([FromBody] TimeTableWithWeekDaysInputModel model)
        {
            if (model == null) return BadRequest("Model is empty");
            List<TimeTable> newTimeTable = TimeTableWithWeekDaysMapper.ToDataModels(model);
            List<TimeTable> result = await lessonStorage.GetTimeTableConflicts(newTimeTable);

            if (result.Count != 0)
            {
                return Ok(TimeTableWithCourseNameMapper.ToOutputModels(result));
            }

            foreach (var timetable in newTimeTable)
            {
                await lessonStorage.AddOrUpdateTimeTable(timetable);
            }
            return Ok();
        }
        #endregion

        #region  Личный кабинет препода Увидеть расписание  

        [Authorize]
        [HttpGet("teacher/timetable/{userId}")]
        public async Task<ActionResult<List<TimeTableWithCourseNameOutputModel>>> GetTeacherTimetable(int userId)
        {
            List<TimeTable> timeTables = await lessonStorage.GetTeacherTimetable(userId);
            return Ok(TimeTableWithCourseNameMapper.ToOutputModels(timeTables));
        }
        #endregion
    }
}

