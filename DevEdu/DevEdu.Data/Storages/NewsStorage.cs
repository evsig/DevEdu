using Dapper;
using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Data.Storages
{
    public class NewsStorage
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction dbTransaction;
        public NewsStorage(string dbСon)
        {
            this.connection = new SqlConnection(dbСon);
        }
        public NewsStorage(IDbConnection dbСon, IDbTransaction dbTran)
        {
            this.connection = dbСon;
            this.dbTransaction = dbTran;
        }

        internal static class SpName
        {
            public const string NewsSelectAll = "News_SelectAll";
            public const string NewsSelectById = "News_SelectById";
            public const string NewsInsert = "News_Insert";
            public const string NewsUpdate = "News_UpdateById";
            public const string NewsDelete = "News_DeleteById";
            public const string NewsInsertForAll = "News_InsertForAll";            public const string NewsInsertForGroup = "News_InsertForGroup";            public const string NewsInsertForUser = "News_InsertForUser";            public const string NewsSelectAllForStudent = "News_SelectAllForStudent";            public const string NewsSelectForTeacherFromHR = "News_SelectForTeacherFromHR";
        }
        public async Task<List<News>> NewsGetAll()
        {
            try
            {
                var result = await connection.QueryAsync<News>(
                    SpName.NewsSelectAll,
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
        public async Task<List<News>> NewsGetAllByUserRegistrationDate(int studentId)
        {
            try
            {
                var newsDictionary = new Dictionary<int, News>();

                var result = await connection.QueryAsync<News, User, News>(
                    SpName.NewsSelectAllForStudent,
                    (news, user) => {
                        News newsEntry;
                        if (!newsDictionary.TryGetValue((int)news.Id, out newsEntry))
                        {
                            newsEntry = news;
                            newsDictionary.Add((int)newsEntry.Id, newsEntry);
                        }                        
                        newsEntry.User = user;
                        return newsEntry;
                    },
                    new { studentId },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure,
                    splitOn: "FirstName");

                return result.ToList();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<List<News>> GetNewsForTeacherFromHR(int teacherId)
        {
            try
            {
                var result = await connection.QueryAsync<News>(
                    SpName.NewsSelectForTeacherFromHR,
                    new { teacherId},
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

        public async Task<News> NewsGetById(int id)
        {
            try
            {
                var result = await connection.QueryAsync<News>(
                    SpName.NewsSelectById,
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

        public async Task<int> NewsAddOrUpdate(News model)
        {
            try
            {
                DynamicParameters parameter = new DynamicParameters(new
                {
                    model.Title,
                    model.Content,
                    model.AuthorId,
                    model.RecipientID,
                    model.GroupID
                });

                if (model.Id.HasValue)
                {
                    parameter.Add("@ID", model.Id);
                    var result = await connection.QueryAsync<decimal>(
                        SpName.NewsUpdate,
                        parameter,
                        dbTransaction,
                        commandType: CommandType.StoredProcedure
                    );
                    return decimal.ToInt32(result.FirstOrDefault());
                }
                else
                {
                    var result = await connection.QueryAsync<decimal>(
                            SpName.NewsInsert,
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

        public async Task<int> NewsDelete(int id)
        {
            try
            {
               var result = await connection.QueryAsync<int>(
                    SpName.NewsDelete,
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
    }
}
