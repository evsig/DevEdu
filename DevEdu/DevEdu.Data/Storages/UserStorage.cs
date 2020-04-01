using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using DevEdu.Data.Models;
using System.Data;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace DevEdu.Data.Storages
{
    public class UserStorage
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction dbTransaction;
        public UserStorage(string dbСon)
        {
            this.connection = new SqlConnection(dbСon);
        }
        public UserStorage(IDbConnection dbСon, IDbTransaction dbTran)
        {
            this.connection = dbСon;
            this.dbTransaction = dbTran;
        }

        public UserStorage() { }

        internal static class SpName
        {
            public const string UserSkillDelete = "UserSkill_Delete";
            public const string UserSkillInsert = "UserSkill_Insert";
            public const string UserSkillSelect = "UserSkill_Select";
            public const string AllUsersSkillDelete = "AllUsers_SkillDelete";

            public const string UserSelectAll = "User_SelectAll";
            public const string UserSelectId = "User_SelectById";
            public const string UserInsert = "User_Insert";
            public const string UserUpdate = "User_Update";
            public const string UserDelete = "User_DeleteByID";
            public const string UserSelectLoginPass = "User_SelectByLoginAndPass";

            public const string UserRoleInsert = "User_Role_Insert";
            public const string UserRoleSelectRoleId = "User_Role_SelectByRoleID";
            public const string UserRoleDelete = "User_Role_Delete";
            public const string UserRoleDeleteById = "User_Role_DeleteById";
            public const string UserRoleSelectUserId = "User_Role_SelectByUserID";
            public const string UserRoleSelectAll = "User_Role_SelectAll";
            public const string UsersWithSomeRole = "Users_with_some_role";
            public const string AddPassLoginToUser = "Add_PassLogin_To_User";
            public const string UserChangePassword = "User_ChangePassword";


            public const string UserBioUpdate = "UserBio_Update";
            public const string AverageRating = "Report_AverageRating";
            public const string AverageRatingByDate = "Report_AverageRatingByTime";
            public const string DetailRating = "Report_DetailedRating";
            public const string DetailRatingByDate = "Report_DetailedRatingByTime";

            public const string GetUsersByRoleId = "Report_GetUsersByRoleId";
            public const string UserGetBlankFields = "User_GetBlankFields";
            public const string StudentWithSkillsByStudentId = "StudentWithSkills_ByStudentId";
            public const string CurrentTeachersByStudentId = "CurrentTeachers_ByStudentId";
            public const string StudentProfileUpdate = "StudentProfile_Update";
        }

        #region User 

        public async Task<List<User>> UserSelectAll()
        {
            try
            {
                var result = await connection.QueryAsync<User>(
                    SpName.UserSelectAll,
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

        public async Task<User> UserSelectById(int? id)
        {
            try
            {
                var result = await connection.QueryAsync<User>(
                    SpName.UserSelectId,
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

        public async Task<User> UserGetByLogin(User user)
        {
            try
            {
                var result = await connection.QueryAsync<User>(
                 SpName.UserSelectLoginPass,
                 new { user.Login },
                 dbTransaction,
                 commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<int> UserAddOrUpdate(User model)
        {
            DynamicParameters userModel = new DynamicParameters();
            userModel.Add("FirstName", model.FirstName);
            userModel.Add("LastName", model.LastName);
            userModel.Add("Patronymic", model.Patronymic);
            userModel.Add("BirthDate", model.BirthDate);
            userModel.Add("Email", model.Email);
            userModel.Add("Phone", model.Phone);
            userModel.Add("Password", model.Password);
            userModel.Add("Login", model.Login);
            userModel.Add("Bio", model.Bio);
            userModel.Add("Photo", model.Photo);
            userModel.Add("CityId", model.CityId);

            try
            {
                if (model.Id == null)
                {
                    var result = await connection.QueryAsync<decimal>(
                            SpName.UserInsert,
                            userModel,
                            dbTransaction,
                            commandType: CommandType.StoredProcedure);
                    return decimal.ToInt32(result.First());
                }
                else
                {
                    userModel.Add("Id", model.Id);
                    var result = await connection.QueryAsync<decimal>(
                            SpName.UserUpdate,
                            userModel,
                            dbTransaction,
                            commandType: CommandType.StoredProcedure);
                    return decimal.ToInt32(result.First());
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async void UserDeleteById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<int>(
                    SpName.UserDelete,
                    new { id },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                );
                result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public async Task<User> UpdateStudentBioByUserId(User model)
        {
            try
            {
                var result = await connection.QueryAsync<User>(
                    SpName.UserBioUpdate,
                    new
                    {
                        model.Id,
                        model.Bio
                    },
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
        public async Task<int> AddPasswordLoginToUser(User user)
        {
            try
            {
                if (user.Id.HasValue)
                {
                    var result = await connection.QueryAsync<decimal>(
                            SpName.AddPassLoginToUser,
                            new
                            {
                                user.Id,
                                user.Password,
                                user.Login
                            },
                            dbTransaction,
                            commandType: CommandType.StoredProcedure
                    );
                    return decimal.ToInt32(result.FirstOrDefault());
                }
                else
                {
                    return -1;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task ChangePassword(User user)
        {
            try
            {
                if (user.Id.HasValue)
                {
                    var result = await connection.QueryAsync(
                            SpName.UserChangePassword,
                            new
                            {
                                user.Id,
                                user.Password,
                                user.Login
                            },
                            dbTransaction,
                            commandType: CommandType.StoredProcedure);
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<List<User>> GetUserByRoleId(int Id)
        {
            try
            {
                var result = await connection.QueryAsync<User>(
                    SpName.GetUsersByRoleId,
                    new { Id },
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
        #endregion

        #region User_Role 
        public async Task<int> User_RoleAdd(User_Role model)
        {
            try
            {
                var result = await connection.QueryAsync<int>(
                        SpName.UserRoleInsert,
                        new
                        {
                            model.UserId,
                            model.RoleId
                        },
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

        public async Task<int> User_RoleDelete(User_Role model)
        {
            try
            {
                var result = await connection.QueryAsync<decimal>(
                    SpName.UserRoleDelete,
                    new
                    {
                        model.UserId,
                        model.RoleId
                    },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure);
                return decimal.ToInt32(result.FirstOrDefault());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task User_RoleDeleteById(int id)
        {
            try
            {
                await connection.QueryAsync<decimal>(
                SpName.UserRoleDeleteById,
                new
                 {
                    id
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

        public async Task<List<User_Role>> User_RoleSelectByRoleId(int RoleId)
        {
            try
            {
                var result = await connection.QueryAsync<User_Role>(
                 SpName.UserRoleSelectRoleId,
                 new { RoleId },
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

        public async Task<IEnumerable<Role>> UserRolesSelectByUserId(int UserId)
        {
            try
            {
                var result = await connection.QueryAsync<Role>(
                SpName.UserRoleSelectUserId,
                new { UserId },
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

        public async Task<List<User_Role>> GetAllUser_Role()
        {
            try
            {
                var result = await connection.QueryAsync<User_Role>(
                SpName.UserRoleSelectAll,
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

        public async Task<List<User_Role>> GetUser_RoleByUserId(int UserId)
        {
            try
            {
                var result = await connection.QueryAsync<User_Role>(
                SpName.UserRoleSelectUserId,
                new { UserId },
                dbTransaction,
                commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UserSkill 
        public async Task<int> UserSkillAdd(User_Skill userSkill)
        {
            try
            {
                var result = await connection.QueryAsync<int>(
                    SpName.UserSkillInsert,
                    new 
                    { 
                        userID = userSkill.UserId, 
                        skillID = userSkill.SkillId
                    },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                    );
                return Convert.ToInt32(result.FirstOrDefault());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task UserSkillDelete(User_Skill userSkill)
        {
            try
            {
                var result = await connection.QueryAsync<int>(
                    SpName.UserSkillDelete,
                    new { userID = userSkill.UserId, skillID = userSkill.SkillId },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                    );
                Convert.ToInt32(result.FirstOrDefault());
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public async Task<bool> UsersSkillDeleteAll(int id)
        {
            try
            {
                var result = await connection.QueryAsync<bool>(
                    SpName.AllUsersSkillDelete,
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

        //метод сделан для работы  тестов 
        public async Task<bool> IsUserSkillPresent(User_Skill userSkill)
        {
            List<User_Skill> result;

            try
            {
                result = connection.Query<User_Skill>(
                    SpName.UserSkillSelect,
                    new { userID = userSkill.UserId, skillID = userSkill.SkillId },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure
                    ).ToList();
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            if (result.Count == 1 && result[0].UserId == userSkill.UserId && result[0].SkillId == userSkill.SkillId) return true;
            return false;
        }

        #endregion

        #region GetStudentInGroup 
        public async Task<List<User>> GetStudentsInGroup(int groupId)
        {
            try
            {
                string procName = "GetStudentsInGroup";
                var result = await connection.QueryAsync<User, City, StudentGroup, User>(
                        procName,
                        (user, city, studentGroup) =>
                        {
                            user.City = city;
                            user.StudentGroup = studentGroup;
                            return user;
                        },
                        param: new { groupId },
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

        #region Get User Blank fields in Journal

        public async Task<List<UserBlankFields>> GetUserBlankFields(int userId)
        {
            try
            {
                var result = await connection.QueryAsync<UserBlankFields>(
                    SpName.UserGetBlankFields,
                    new { userId },
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
        #endregion

        #region Get Student With Skills By UserId
        public async Task<User> GetStudentWithSkillsByUserId(int userId)
        {
            try
            {
                var userDictionary = new Dictionary<int, User>();

                var result = await connection.QueryAsync<User, CourseProgramSkill, User>(
                        SpName.StudentWithSkillsByStudentId,
                        (user, skill) =>
                        {
                            User userEntry;
                            if (!userDictionary.TryGetValue((int)user.Id, out userEntry))
                            {
                                userEntry = user;
                                userEntry.Skills = new List<CourseProgramSkill>();
                                userDictionary.Add((int)userEntry.Id, userEntry);
                            }
                            userEntry.Skills.Add(skill);
                            return userEntry;

                        },
                        param: new { userId },
                        dbTransaction,
                        commandType: CommandType.StoredProcedure,
                        splitOn: "Id");
                return result.FirstOrDefault();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Get Current Teachers By StudentId
        public async Task<IEnumerable<User>> GetCurrentTeachersByStudentId(int userId)
        {
            Thread.Sleep(5000);

            try
            {
                return await connection.QueryAsync<User>(
                    SpName.CurrentTeachersByStudentId,
                    new { userId },
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

        #region UpdateStudentProfile
        public async Task UpdateStudentProfile(User model)
        {
            Thread.Sleep(5000);

            try
            {
                DynamicParameters parameters = new DynamicParameters(new
                {
                    model.Id,
                    model.FirstName,
                    model.LastName,
                    model.Patronymic,
                    model.BirthDate,
                    model.Email,
                    model.Phone,
                    model.Photo
                });

                await connection.QueryAsync(
                       SpName.StudentProfileUpdate,
                       parameters,
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
    }
}