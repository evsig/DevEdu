using Dapper;
using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;

namespace DevEdu.Data.Storages
{
    public class LessonStorage
    {
        private readonly UserStorage userStorage;
        private readonly IDbConnection connection;
        private readonly IDbTransaction dbTransaction;
        public LessonStorage(string dbСon)
        {
            this.connection = new SqlConnection(dbСon);
            userStorage = new UserStorage();
        }
        public LessonStorage(IDbConnection dbСon, IDbTransaction dbTran)
        {
            this.connection = dbСon;
            this.dbTransaction = dbTran;
        }

        internal static class SpName
        {
            public const string JournalDeleteById = "Journal_DeleteByID";
            public const string JournalInsert = "Journal_Insert";
            public const string JournalUpdate = "Journal_Update";
            public const string JournalSelectAll = "Journal_SelectAll";
            public const string JournalSelectById = "Journal_SelectByID";

            public const string LessonDeleteById = "Lesson_DeleteByID";
            public const string LessonInsert = "Lesson_Insert";
            public const string LessonUpdate = "Lesson_Update";
            public const string LessonSelectAll = "Lesson_SelectAll";
            public const string LessonSelectById = "Lesson_SelectByID";
            public const string LessonSelectByUserID = "Lessons_SelectByUserID";

            public const string LessonTopicDeleteById = "LessonTopic_DeleteByID";
            public const string LessonTopicInsert = "LessonTopic_Insert";
            public const string LessonTopicSelectAll = "LessonTopic_SelectAll";
            public const string LessonTopicSelectById = "LessonTopic_SelectByID";

            public const string TimeTableDeleteById = "TimeTable_DeleteById";
            public const string TimeTableInsert = "TimeTable_Insert";
            public const string TimeTableUpdate = "TimeTable_Update";
            public const string TimeTableSelectById = "TimeTable_SelectById";
            public const string TimeTableSelectByGroupId = "TimeTable_SelectByGroupId";
            public const string TimetableGetByTeacherId = "Timetable_GetByTeacherId";
            public const string TimetableGetConflicts = "Timetable_GetConflicts";

            public const string LessonGetLessonIdByGroupIdAndDate = "Lesson_GetLessonIdByGroupIdAndDate";
            public const string FillIsAbsentById = "FillIsAbsentById";

        }

        #region GetLessonsWithJournalsAndTopics
        public async Task<List<Lesson>> GetLessonsWithJournalsAndTopics(int userId)
        {
            Thread.Sleep(5000);
            try
            {
                var lessonsDictionary = new Dictionary<int, Lesson>();

                var result = await connection.QueryAsync<Lesson, ThemeDetails, Journal, Lesson>(
                        SpName.LessonSelectByUserID,
                        (lesson, themeDetails, journal) =>
                        {
                            Lesson lessonEntry;

                            if (!lessonsDictionary.TryGetValue((int)lesson.Id, out lessonEntry))
                            {
                                lessonEntry = lesson;
                                lessonEntry.LessonTopicsDetails = new List<ThemeDetails>();
                                lessonEntry.Journals = new List<Journal>();
                                lessonsDictionary.Add((int)lessonEntry.Id, lessonEntry);
                            }

                            lessonEntry.Journals.Add(journal);
                            lessonEntry.LessonTopicsDetails.Add(themeDetails);
                            return lessonEntry;
                        },
                        param: new { userId },
                        dbTransaction,
                        commandType: CommandType.StoredProcedure,
                        splitOn: "Id");
                return result.Distinct().ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region AddOrUpdateJournal
        public async Task<int> AddOrUpdateJournal(Journal model)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters(new
                {
                    model.UserId,
                    model.LessonID,
                    model.IsAbsent,
                    model.Feadback,
                    model.AbsentReason
                    //  model.Lesson
                });

                if (model.Id.HasValue)
                {
                    parameter.Add("@ID", model.Id);
                    var result = await connection.QueryAsync<decimal>(
                        SpName.JournalUpdate,
                        parameter,
                        dbTransaction,
                        commandType: CommandType.StoredProcedure
                    );
                    return decimal.ToInt32(result.FirstOrDefault());
                }
                else
                {
                    var result = await connection.QueryAsync<decimal>(
                            SpName.JournalInsert,
                            parameter,
                            dbTransaction,
                            commandType: CommandType.StoredProcedure
                        );
                    return decimal.ToInt32(result.FirstOrDefault());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion

        #region GetAllJournal
        public async Task<List<Journal>> GetAllJournal()
        {
            try
            {
                var result = await connection.QueryAsync<Journal>(
                    SpName.JournalSelectAll,
                    null,
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );
                return result.ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion

        #region GetJournalById
        public async Task<Journal> GetJournalById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<Journal>(
                    SpName.JournalSelectById,
                    new { id },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );
                return result.First();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion

        #region DeleteJournal
        public async Task DeleteJournal(int id)
        {
            try
            {
                await connection.QueryAsync<Journal>(
                SpName.JournalDeleteById,
                new { id },
                dbTransaction,
                commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion


        #region DeleteLesson
        public async Task DeleteLesson(int id)
        {
            try
            {
                await connection.QueryAsync<Journal>(
                SpName.LessonDeleteById,
                new { id },
                dbTransaction,
                commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion

        #region AddOrUpdateLesson
        public async Task<int> AddOrUpdateLesson(Lesson model)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters(new
                {
                    model.GroupId,
                    model.Date,
                    model.Hometask,
                    model.Videos,
                    model.ToRead
                });

                if (model.Id.HasValue)
                {
                    parameter.Add("@ID", model.Id);
                    var result = await connection.QueryAsync<decimal>(
                        SpName.LessonUpdate,
                        parameter,
                        dbTransaction,
                        commandType: CommandType.StoredProcedure
                    );
                    return decimal.ToInt32(result.FirstOrDefault());
                }
                else
                {
                    var result = await connection.QueryAsync<decimal>(
                        SpName.LessonInsert,
                        parameter,
                        dbTransaction,
                        commandType: CommandType.StoredProcedure
                    );
                    return decimal.ToInt32(result.FirstOrDefault());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion

        #region GetAllLesson
        public async Task<List<Lesson>> GetAllLesson()
        {
            {
                try
                {
                    var result = await connection.QueryAsync<Lesson>(
                        SpName.LessonSelectAll,
                        null,
                        dbTransaction,
                        commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            //List<Lesson> result = new List<Lesson>();
            //using SqlConnection connection = new SqlConnection(dbСonnectionString);
            //try
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(SpName.LessonSelectAll, connection);
            //    command.CommandType = System.Data.CommandType.StoredProcedure;
            //    var readerResult = command.ExecuteReader();
            //    while (readerResult.Read())
            //    {
            //        result.Add(new Lesson()
            //        {
            //            Id = readerResult.GetInt32(0),
            //            GroupId = readerResult.GetInt32(1),
            //            Date = readerResult.GetDateTime(2),
            //            Hometask = readerResult.GetString(3),
            //            Videos = readerResult.GetString(4),
            //            ToRead = readerResult.GetString(5)
            //        });
            //    }
            //}
            //catch (SqlException ex)
            //{
            //    throw ex;
            //}

            // return result;
        }

        #endregion

        #region GetLessonById
        public async Task<Lesson> GetLessonById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<Lesson>(
                    SpName.LessonSelectById,
                    new { id },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );
                return result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteLessonTopic
        public async Task DeleteLessonTopic(int id)
        {
            try
            {
                await connection.QueryAsync<LessonTopic>(
                SpName.LessonTopicDeleteById,
                new { id },
                dbTransaction,
                commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion


        #region AddLessonTopic
        public async Task<int> AddLessonTopic(LessonTopic lessonTopic)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters(new
                {
                    lessonId = lessonTopic.LessonId,
                    themeDetailsID = lessonTopic.ThemeDetailsId
                });
                var result = await connection.QueryAsync<decimal>(
                SpName.LessonTopicInsert,
                parameter,
                dbTransaction,
                commandType: CommandType.StoredProcedure
                );
                return decimal.ToInt32(result.FirstOrDefault());
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
        #endregion

        #region GetAllLessonTopic
        public async Task<List<LessonTopic>> GetAllLessonTopic()
        {
            try
            {
                var result = await connection.QueryAsync<LessonTopic>(
                    SpName.LessonTopicSelectAll,
                    null,
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );
                return result.ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion

        #region GetLessonTopicById
        public async Task<LessonTopic> GetLessonTopicById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<LessonTopic>(
                    SpName.LessonTopicSelectById,
                    new { id },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                    );
                return result.First();
            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion


        #region DeleteTimeTable
        public async Task DeleteTimeTable(int id)
        {
            try
            {
                await connection.QueryAsync(
                    SpName.TimeTableDeleteById,
                    new { id },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region AddOrUpdateTimeTable
        public async Task<int> AddOrUpdateTimeTable(TimeTable model)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters(new
                {
                    model.GroupId,
                    model.RoomNumber,
                    model.Day,
                    model.TimeStart,
                    model.TimeEnd
                });

                if (model.Id.HasValue)
                {
                    parameter.Add("@ID", model.Id);
                    var result = await connection.QueryAsync<decimal>(
                        SpName.TimeTableUpdate,
                        parameter,
                        dbTransaction,
                        commandType: CommandType.StoredProcedure
                    );
                    return decimal.ToInt32(result.FirstOrDefault());
                }
                else
                {
                    var result = await connection.QueryAsync<decimal>(
                      SpName.TimeTableInsert,
                      parameter,
                      dbTransaction,
                      commandType: CommandType.StoredProcedure
                  );
                    return decimal.ToInt32(result.FirstOrDefault());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion

        #region GetTimeTableByGroupId
        public List<TimeTable> GetTimeTableByGroupId(int groupId)
        {
            try
            {
                return connection.Query<TimeTable>(
                    SpName.TimeTableSelectByGroupId,
                    new { groupId },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                ).ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion

        #region GetTimeTableById
        public async Task<TimeTable> GetTimeTableById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<TimeTable>(
                    SpName.TimeTableSelectById,
                    new { id },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );
                return result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion

        #region Timetable_GetByTeacherId
        public async Task<List<TimeTable>> GetTeacherTimetable(int userId)
        {
            try
            {
                string procName = SpName.TimetableGetByTeacherId;
                var result = await connection.QueryAsync<TimeTable, Course, TimeTable>(
                        procName,
                        (timeTable, course) =>
                        {
                            timeTable.Course = course;
                            return timeTable;
                        },
                        param: new { userId },
                        dbTransaction,
                        commandType: CommandType.StoredProcedure);
                return result.Distinct().ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetTimeTableConflicts
        public async Task<List<TimeTable>> GetTimeTableConflicts(List<TimeTable> models)
        {
            try
            {
                List<TimeTable> table = new List<TimeTable>();
                foreach (var model in models)
                {
                    string procName = SpName.TimetableGetConflicts;
                    var result = await connection.QueryAsync<TimeTable, Course, TimeTable>(
                            procName,
                            (timeTable, course) =>
                            {
                                timeTable.Course = course;
                                return timeTable;
                            },
                            splitOn: "ID",
                            param: new
                            {
                                model.Day,
                                model.TimeStart,
                                model.TimeEnd,
                                model.RoomNumber
                            },
                            commandType: CommandType.StoredProcedure
                            );

                    if (result.FirstOrDefault() != null)
                    {
                        table.Add(result.FirstOrDefault());
                    }
                }

                return table;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region FillWhoWereAbsent
        public void FillWhoWhereAbsent(List<Journal> journals)
        {
            int lessonId = GetLessonId(journals[0].Lesson.GroupId, Convert.ToString(journals[0].Lesson.Date));

            for (int i = 0; i < journals.Count; i++)
            {
                int userId = journals[i].UserId;
                try
                {
                    connection.Query(
                       SpName.FillIsAbsentById,
                       new
                       {
                           userId,
                           lessonId
                       },
                       dbTransaction,
                       commandType: CommandType.StoredProcedure
                       );
                }

                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }
        public int GetLessonId(int groupId, string date)
        {
            {
                try
                {
                    return connection.Query<int>(
                        SpName.LessonGetLessonIdByGroupIdAndDate,
                        new { groupId, date },
                        dbTransaction,
                        commandType: CommandType.StoredProcedure
                        ).FirstOrDefault();
                }

                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }
        #endregion

        #region AddJournals
        public async Task<int> AddJournals(Lesson model)   // ASYNC
        {
            // model.Id = AddOrUpdateLesson(model);
            List<User> users = await userStorage.GetStudentsInGroup(model.GroupId);
            List<Journal> journals = new List<Journal>();
            for (int i = 0; i < users.Count; i++)
            {
                Journal journal = new Journal();
                journal.Lesson = model;
                journal.IsAbsent = false;
                journal.UserId = (int)users[i].Id;
                journal.Id = await AddOrUpdateJournal(journal);
                journals.Add(journal);
            }
            model.Journals = journals;
            return (int)model.Id;
        }
        #endregion
    }
}
