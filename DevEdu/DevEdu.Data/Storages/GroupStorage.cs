using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using DevEdu.Data.Models;
using System.Data;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Data.Storages
{
    public class GroupStorage
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction dbTransaction;
        public GroupStorage(string dbСon)
        {
            this.connection = new SqlConnection(dbСon);
        }
        public GroupStorage(IDbConnection dbСon, IDbTransaction dbTran)
        {
            this.connection = dbСon;
            this.dbTransaction = dbTran;
        }

        internal static class SpName
        {
            public const string StudentGroupSelectAll = "Student_Group_SelectAll";
            public const string StudentGroupInsert = "Student_Group_Insert";
            public const string StudentGroupDeleteById = "Student_Group_DeleteById";
            public const string StudentGroupDelete = "Student_Group_Delete";
            public const string StudentGroupSelectById = "Student_Group_SelectById";
            public const string StudentGroupSelectByUserId = "Student_Group_SelectByUserID";

            public const string TeacherGroupSelectAll = "Teacher_Group_SelectAll";
            public const string TeacherGroupInsert = "Teacher_Group_Insert";
            public const string TeacherGroupDeleteById = "Teacher_Group_DeleteById";
            public const string TeacherGroupDelete = "Teacher_Group_Delete";
            public const string TeacherGroupSelectById = "Teacher_Group_SelectById";
            public const string StudentRatingUpdateByUserId = "StudentRating_UpdateByUserId";
            public const string GetTeacherGroupsByTeacherId = "GetTeacherGroups";

            public const string GroupSelectAll = "Group_SelectAll";
            public const string GroupSelectById = "Group_SelectById";
            public const string GroupInsert = "Group_Insert";
            public const string GroupUpdate = "Group_Update";
            public const string GroupDelete = "Group_DeleteById";

            public const string GroupList = "Get_GroupList";
        }

        public async Task<List<Group>> GroupGetAll()
        {
            try
            {
                var result = await connection.QueryAsync<Group>(
                    SpName.GroupSelectAll,
                    null,
                    dbTransaction,
                    commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<int> GroupAddOrUpdate(Group model)
        {
            DynamicParameters groupModel = new DynamicParameters();
            groupModel.Add("StartDate", model.StartDate);
            groupModel.Add("EndDate", model.EndDate);
            groupModel.Add("TimeStart", model.TimeStart);
            groupModel.Add("Duration", model.Duration);
            groupModel.Add("CourseProgramId", model.CourseProgramId);

            try
            {
                if (model.Id == null)
                {
                    var result = await connection.QueryAsync<decimal>(
                               SpName.GroupInsert,
                               groupModel,
                               dbTransaction,
                               commandType: CommandType.StoredProcedure);
                    return decimal.ToInt32(result.FirstOrDefault());
                }
                else
                {
                    groupModel.Add("Id", model.Id);
                    var result = await connection.QueryAsync<decimal>(
                            SpName.GroupUpdate,
                            groupModel,
                            dbTransaction,
                            commandType: CommandType.StoredProcedure);
                    return decimal.ToInt32(result.FirstOrDefault());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }


        public async Task GroupDelete(int id)
        {
            try
            {
                var result = await connection.QueryAsync<int>(
                    SpName.GroupDelete,
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

        public async Task<Group> GroupGetById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<Group>(
                    SpName.GroupSelectById,
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


        #region StudentGroup
        public async Task<int> StudentGroupInsert(StudentGroup studentGroup)
        {
            string procName = SpName.StudentGroupInsert;

            {
                try
                {
                    var result = await connection.QueryAsync<decimal>(
                            procName,
                            new
                            {
                                studentGroup.GroupId,
                                studentGroup.UserId,
                                studentGroup.Rating
                            },
                            dbTransaction,
                            commandType: CommandType.StoredProcedure
                    );
                    return Convert.ToInt32(result.First());
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public async Task StudentGroupDelete(StudentGroup studentGroup)
        {
            string procName = SpName.StudentGroupDeleteById;

            try
            {
                var result = await connection.QueryAsync<decimal>(
                    procName,
                    new
                    {
                        studentGroup.UserId,
                        studentGroup.GroupId
                    },
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

        #region StudentGroupGetByUserId 
        public async Task<StudentGroup> StudentGroupGetByUserId(int userId)
        {
            try
            {
                var result = await connection.QueryAsync<StudentGroup>(
                    SpName.StudentGroupSelectByUserId,
                    new { userId },
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

        public async Task<StudentGroup> StudentGroupGetById(int? id)
        {
            try
            {
                var result = await connection.QueryAsync<StudentGroup>(
                    SpName.StudentGroupSelectById,
                    new { id },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );
                return result.First();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<List<StudentGroup>> StudentGroupGetAll()
        {
            {
                try
                {
                    var result = await connection.QueryAsync<StudentGroup>(
                    SpName.StudentGroupSelectAll,
                    null,
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                    );
                    return result.ToList();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        #endregion


        #region UpdateUserRating
        public async void UpdateUserRating(StudentGroup model)
        {
            try
            {
                var result = await connection.QueryAsync(
                    SpName.StudentRatingUpdateByUserId,
                    new
                    {
                        model.UserId,
                        model.GroupId,
                        model.Rating
                    },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetListOfGroupsRelatedToTeacherByUserId  + GetBelongnessOfGroupToTheTeacher
        public async Task<IEnumerable<Group>> GetTeacherGroupsById(int userId)
        {
            try
            {
                var result = await connection.QueryAsync<Group>(
                    SpName.GetTeacherGroupsByTeacherId,
                    new { userId },
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
        public async Task<bool> GetBelongnessOfGroupToTheTeacher(int groupId, int teacherId)
        {
            var getTeacherGroupsById = await GetTeacherGroupsById(teacherId);
            return getTeacherGroupsById.Select(l => l.Id).Contains(groupId);
        }





        #endregion

        public async Task<List<GroupList>> GetGroupList(GroupList group)
        {
            try
            {
                var result = await connection.QueryAsync<GroupList>(
                    SpName.GroupList,
                    group.GroupId,
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        #region TeacherGroup
        public async Task<int> TeacherGroupInsert(TeacherGroup teacherGroup)
        {
            string procName = SpName.TeacherGroupInsert;
            
            try
            {
                var result = await connection.QueryAsync<decimal>(
                        procName,
                        new
                        {
                            teacherGroup.GroupId,
                            teacherGroup.UserId
                        },
                        dbTransaction,
                            commandType: CommandType.StoredProcedure
                            );
                return Convert.ToInt32(result.First());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task TeacherGroupDelete(int groupId, int userId)
        {
            string procName = SpName.TeacherGroupDelete;
            
            {
                try
                {
                    var result = await connection.QueryAsync(
                        procName,
                        new
                        {
                            userId,
                            groupId
                        },
                        dbTransaction,
                        commandType: CommandType.StoredProcedure
                        );
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<TeacherGroup> TeacherGroupGetById(int? id)
        {
            try
            {
                var result = await connection.QueryAsync<TeacherGroup>(
                    SpName.TeacherGroupSelectById,
                    new { id },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );
                return result.First();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<List<TeacherGroup>> TeacherGroupGetAll()
        {
            {
                try
                {
                    var result = await connection.QueryAsync<TeacherGroup>(
                    SpName.TeacherGroupSelectAll,
                    null,
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                    );
                    return result.ToList();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

    }
}


