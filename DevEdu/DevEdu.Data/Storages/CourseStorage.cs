using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DevEdu.Data.Models;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Data.Storages
{
    public class CourseStorage
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction dbTransaction;
        public CourseStorage(string dbСon)
        {
            this.connection = new SqlConnection(dbСon);
        }
        public CourseStorage(IDbConnection dbСon, IDbTransaction dbTran)
        {
            this.connection = dbСon;
            this.dbTransaction = dbTran;
        }
        
        internal static class SpName
        {
            public const string CourseSelectAll = "Course_SelectAll";
            public const string CourseSelectId = "Course_SelectId";
            public const string CourseInsert = "Course_Insert";
            public const string CourseUpdate = "Course_Update";
            public const string CourseDeleteId = "Course_DeleteId";

            public const string CourseProgramSelectAll = "CourseProgram_SelectAll";
            public const string CourseProgramSelectById = "CourseProgram_SelectById";
            public const string CourseProgramSelectByCourseId = "CourseProgram_SelectByCourseId";
            public const string CourseProgramDelete = "CourseProgram_Delete";
            public const string CourseProgramInsert = "CourseProgram_Insert";
            public const string CourseProgramUpdate = "CourseProgram_Update";
            public const string CourseProgramInsertNewVersion = "CourseProgram_InsertNewVersion";

            public const string CourseProgramSkillSelectAll = "CourseProgramSkill_SelectAll";
            public const string CourseProgramSkillSelectById = "CourseProgramSkill_SelectById";
            public const string CourseProgramSkillDelete = "CourseProgramSkill_Delete";
            public const string CourseProgramSkillInsert = "CourseProgramSkill_Insert";
            public const string CourseProgramSkillUpdate = "CourseProgramSkill_Update";


            public const string ThemeDetailsSelectAll = "ThemeDetails_SelectAll";
            public const string ThemeDetailsSelectById = "ThemeDetails_SelectById";
            public const string ThemeDetailsByProgramDetailId = "ThemeDetails_SelectByProgramDetailId";
            public const string ThemeDetailsGetByGroupId = "ThemeDetails_SelectByGroupId";

            public const string ThemeDetailsInsert = "ThemeDetails_Insert";
            public const string ThemeDetailsUpdate = "ThemeDetails_UpdateById";
            public const string ThemeDetailsDelete = "ThemeDetails_DeleteById";

            public const string ProgramDetailsSelectAll = "ProgramDetails_SelectAll";
            public const string ProgramDetailsSelectById = "ProgramDetails_SelectById";
            public const string ProgramDetailsGetByCourseProgramId = "ProgramDetails_SelectByCourseProgram";
            public const string ProgramDetailsInsert = "ProgramDetails_Insert";
            public const string ProgramDetailsUpdate = "ProgramDetails_Update";
            public const string ProgramDetailsDelete = "ProgramDetails_Delete";

            public const string ReportTopicsToTeachByGroupId = "Report_TopicsToTeachByGroupId";
        }

        #region Course
        public async Task<List<Course>> CourseGetAll()
        {
            
            try
            {
                var result = await connection.QueryAsync<Course>(
                    SpName.CourseSelectAll,
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

        public async Task<Course> CourseGetById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<Course>(
                    SpName.CourseSelectId,
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

        public async Task<int> CourseAddOrUpdate(Course course)
        {
            try
            {
                if (course.Id.HasValue)
                {
                    var result = await connection.QueryAsync<decimal>(
                            SpName.CourseUpdate,
                            new
                            {
                                course.Id,
                                course.Name,
                                course.Description,
                                course.Price
                            },
                            dbTransaction,
                            commandType: CommandType.StoredProcedure
                    );
                    return decimal.ToInt32(result.FirstOrDefault());
                }
                else
                {
                    var result = await connection.QueryAsync<decimal>(
                            SpName.CourseInsert,
                            new
                            {
                                course.Name,
                                course.Description,
                                course.Price
                            },
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

        public async Task CourseDelete(int id)
        {
            try
            {
                var result = await connection.QueryAsync<Course>(
                    SpName.CourseDeleteId,
                    new { id },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );

                result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region CourseProgram

        public async Task<List<CourseProgram>> CourseProgramGetAll()
        {
            try
            {
                var result = await connection.QueryAsync<CourseProgram>(
                    SpName.CourseProgramSelectAll,
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

        public async Task<CourseProgram> CourseProgramGetById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<CourseProgram>(
                    SpName.CourseProgramSelectById,
                    new { id = id },
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

        public async Task<CourseProgram> CourseProgramGetByCourseId(int courseId)
        {
            try
            {
                var result = await connection.QueryAsync<CourseProgram>(
                    SpName.CourseProgramSelectByCourseId,
                    new { CourseId = courseId },
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

        public async Task CourseProgramDelete(int courseProgramId)
        {
            try
            {
                var result = await connection.QueryAsync<Course>(
                    SpName.CourseProgramDelete,
                    new { Id = courseProgramId },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );
                result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<int> CourseProgramAddOrUpdate(CourseProgram newProgram)
        {
            string procName = SpName.CourseProgramInsert;

            try
            {
                if (procName == SpName.CourseProgramInsert)
                {
                    var result = await connection.QueryAsync<decimal>(
                        procName,
                         new
                         {
                             newProgram.CourseId,
                             newProgram.IsActual,
                             newProgram.Title
                         },
                         dbTransaction,
                        commandType: CommandType.StoredProcedure
                    );
                    return decimal.ToInt32(result.FirstOrDefault());
                }
                else
                {
                    var result = await connection.QueryAsync<decimal>(
                        procName,
                        new
                        {
                            id = newProgram.Id,
                            isActual = newProgram.IsActual,
                            title = newProgram.Title
                        },
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

        public async Task<int> CourseProgramAddNewVersion(int courseProgramId)
        {
            try
            {
                var result = await connection.QueryAsync<decimal>(
                    SpName.CourseProgramInsertNewVersion,
                    new
                    {
                        id = courseProgramId
                    },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure);
                return decimal.ToInt32(result.FirstOrDefault());
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region ThemeDetailsStorage


        public async Task<List<ThemeDetails>> ThemeDetailsGetAll()
        {
            try
            {
                try
                {
                    var result = await connection.QueryAsync<ThemeDetails>(
                        SpName.ThemeDetailsSelectAll,
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
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<ThemeDetails> ThemeDetailsGetById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<ThemeDetails>(
                    SpName.ThemeDetailsSelectById,
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
        public async Task<ThemeDetails> ThemeDetailsGetByProgramDetailId(int programDetailId)
        {
            try
            {
                var result = await connection.QueryAsync<ThemeDetails>(
                    SpName.ThemeDetailsSelectById,
                    new { programDetailId },
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

        public async Task<int> ThemeDetailsAddOrUpdate(ThemeDetails newThemeDetails)
        {
            string proc = SpName.ThemeDetailsInsert;

            if (newThemeDetails.Id.HasValue)
            {
                proc = SpName.ThemeDetailsUpdate;
            }
            
            try
            {
                var result = await connection.QueryAsync<decimal>(
                    proc,
                    new
                    {
                        newThemeDetails.ProgramDetailId,
                        newThemeDetails.Topic
                    },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                    );
                return decimal.ToInt32(result.FirstOrDefault());
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public async Task<int> ThemeDetailsDeleteById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<int>(
                     SpName.ThemeDetailsDelete,
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

        public async Task<IEnumerable<ThemeDetails>> ThemeDetailsGetByGroupId(int GroupId)
        {
            try
            {
                var result = await connection.QueryAsync<ThemeDetails>(
                    SpName.ThemeDetailsGetByGroupId,
                    new { GroupId },
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

        public async Task<IEnumerable<ThemeDetails>> GetTopicsToTeachByGroupId(int groupId)
        {
            try
            {
                var result = await connection.QueryAsync<ThemeDetails>(
                    SpName.ReportTopicsToTeachByGroupId,
                    new { groupId },
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

        #region CourseProgramSkill

        public async Task<List<CourseProgramSkill>> CourseProgramSkillsGetAll()
        {
            {
                try
                {
                    var result = await connection.QueryAsync<CourseProgramSkill>(
                            SpName.CourseProgramSkillSelectAll,
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

        }

        public async Task<CourseProgramSkill> CourseProgramSkillGetById(int Id)
        {
            try
            {
                var result = await connection.QueryAsync<CourseProgramSkill>(
                        SpName.CourseProgramSkillSelectById,
                        new { Id },
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

        public async Task<int?> CourseProgramSkillDelete(CourseProgramSkill skill)
        {
            try
            {
                var result = await connection.QueryAsync<decimal>(
                        SpName.CourseProgramSkillDelete,
                        new
                        {
                            skill.CourseProgramId,
                            skill.Name
                        },
                        dbTransaction,
                        commandType: CommandType.StoredProcedure
                        );
                return Convert.ToInt32(result.FirstOrDefault());
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<int> CourseProgramSkillAddOrUpdate(CourseProgramSkill newSkill)
        {
            try
            {
                if (newSkill.Id.HasValue)
                {
                    var result = await connection.QueryAsync<decimal>(
                        SpName.CourseProgramSkillUpdate,
                        new
                        {
                            newSkill.Id,
                            newSkill.CourseProgramId,
                            newSkill.Name
                        },
                        dbTransaction,
                        commandType: CommandType.StoredProcedure
                        );
                    return Convert.ToInt32(result.FirstOrDefault());
                }
                else
                {
                    var result = await connection.QueryAsync<decimal>(
                        SpName.CourseProgramSkillInsert,
                        new
                        {
                            newSkill.CourseProgramId,
                            newSkill.Name
                        },
                        dbTransaction,
                        commandType: CommandType.StoredProcedure
                        );
                    return Convert.ToInt32(result.FirstOrDefault());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        #endregion

        #region ProgramDetails

        public async Task<List<ProgramDetails>> ProgramDetailsGetAll()
        {
            try
            {
                var result = await connection.QueryAsync<ProgramDetails>(
                    SpName.ProgramDetailsSelectAll,
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
        public async Task<ProgramDetails> ProgramDetailsGetById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<ProgramDetails>(
                    SpName.ProgramDetailsSelectById,
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
        public async Task<ProgramDetails> ProgramDetailsGetByCourseProgramId(int courseProgramId)
        {
            try
            {
                var result = await connection.QueryAsync<ProgramDetails>
                    (
                    SpName.ProgramDetailsSelectById,
                    new { courseProgramId },
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
        public async Task<int> ProgramDetailsAddOrUpdate(ProgramDetails programDetails)
        {
            try
            {
                if (programDetails.Id.HasValue)
                {
                    var result = await connection.QueryAsync<decimal>(
                            SpName.ProgramDetailsUpdate,
                            new
                            {
                                programDetails.Id,
                                programDetails.CourseProgram,
                                programDetails.LessonNumber,
                                programDetails.LessonTheme
                            },
                            dbTransaction,
                            commandType: CommandType.StoredProcedure
                    );
                    return decimal.ToInt32(result.FirstOrDefault());
                }
                else
                {
                    var result = await connection.QueryAsync<decimal>(
                            SpName.ProgramDetailsInsert,
                            new
                            {
                                programDetails.CourseProgram,
                                programDetails.LessonNumber,
                                programDetails.LessonTheme
                            },
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
        public async Task ProgramDetailsDelete(int id)
        {
            try
            {
                var result = await connection.QueryAsync<ProgramDetails>
                    (
                    SpName.ProgramDetailsDelete,
                    new { id },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                    );
                result.FirstOrDefault();

            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        #endregion
    }
}