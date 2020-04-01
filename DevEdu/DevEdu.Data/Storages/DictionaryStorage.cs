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
    public class DictionaryStorage
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction dbTransaction;
        public DictionaryStorage(string dbСon)
        {
            this.connection = new SqlConnection(dbСon);
        }
        public DictionaryStorage(IDbConnection dbСon, IDbTransaction dbTran)
        {
            this.connection = dbСon;
            this.dbTransaction = dbTran;
        }

        internal static class SpName
        {
            public const string CitySelectAll = "City_SelectAll";
            public const string CitySelectById = "City_SelectById";
            public const string CityInsert = "City_Insert";
            public const string CityUpdate = "City_Update";
            public const string CityDelete = "City_Delete";

            public const string RoleSelectAll = "Role_SelectAll";
            public const string RoleSelectById = "Role_SelectById";
            public const string RoleInsert = "Role_Insert";
            public const string RoleUpdate = "Role_Update";
            public const string RoleDelete = "Role_DeleteById";

            public const string AttestationThemeSelectAll = "AttestationTheme_SelectAll";
            public const string AttestationThemeSelectById = "AttestationTheme_SelectById";
            public const string AttestationThemeInsert = "AttestationTheme_Insert";
            public const string AttestationThemeUpdate = "AttestationTheme_Update";
            public const string AttestationThemeDelete = "AttestationTheme_Delete";
        }

        #region CityStorage

        public async Task<List<City>> CitiesGetAll()
        {
            {
                try
                {
                    var result = await connection.QueryAsync<City>(
                        SpName.CitySelectAll,
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

        public async Task<City> CityGetByID(int? id)
        {
            {
                try
                {
                    var result = await connection.QueryAsync<City>(
                        SpName.CitySelectById,
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

        public async Task<int?> CityAddOrUpdate(City model)
        {
            string procName = SpName.CityInsert;
            if (model.Id.HasValue) { procName = SpName.CityUpdate; }

            {
                if (procName == SpName.CityInsert)
                {
                    try
                    {
                        var result = await
                        connection.QueryAsync<int>(
                            procName,
                            new
                            {
                                model.Name
                            },
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
                else
                {
                    try
                    {
                        var result = await
                        connection.QueryAsync<int>(
                            procName,
                            new
                            {
                                model.Id,
                                model.Name
                            },
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

        public async Task CityDelete(int? id)
        {
            {
                try
                {
                    await connection.QueryAsync<int?>(
                    SpName.CityDelete,
                    new { Id = id },
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

        #endregion

        #region RoleStorage

        public async Task<List<Role>> RoleGetAll()
        {
            {
                try
                {
                    var result = await connection.QueryAsync<Role>(
                        SpName.RoleSelectAll,
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

        public async Task<Role> RoleGetByID(int? id)
        {
            {
                try
                {
                    var result = await connection.QueryAsync<Role>(
                        SpName.RoleSelectById,
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

        public async Task<int?> RoleAddOrUpdate(Role model)
        {
            string procName = SpName.RoleInsert;
            if (model.Id.HasValue) { procName = SpName.RoleUpdate; }

            {
                if (procName == SpName.RoleInsert)
                {
                    try
                    {
                        var result = await
                        connection.QueryAsync<int>(
                            procName,
                            new
                            {
                                model.Name
                            },
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
                else
                {
                    try
                    {
                        var result = await
                        connection.QueryAsync<int>(
                            procName,
                            new
                            {
                                model.Id,
                                model.Name
                            },
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

        public async Task RoleDelete(int? id)
        {
            {
                try
                {
                    await connection.QueryAsync<int>(
                         SpName.RoleDelete,
                         new { Id = id },
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

        #endregion

        #region AttestationTheme

        public async Task<List<AttestationTheme>> AttestationThemeGetAll()
        {
            {
                try
                {
                    var result = await connection.QueryAsync<AttestationTheme>(
                    SpName.AttestationThemeSelectAll,
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

        public async Task<AttestationTheme> AttestationThemeGetByID(int? id)
        {
            {
                try
                {
                    var result = await connection.QueryAsync<AttestationTheme>(
                        SpName.AttestationThemeSelectById,
                    new { id },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure);
                    return result.FirstOrDefault();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }
        }

        public async Task<int?> AttestationThemeAddOrUpdate(AttestationTheme model)
        {
            {
                try
                {
                    if (model.Id.HasValue)
                    {
                        var result = await connection.QueryAsync<int>(
                            SpName.AttestationThemeUpdate,
                            new
                            {
                                model.Id,
                                model.CourseId,
                                model.Theme
                            },
                            dbTransaction,
                            commandType: CommandType.StoredProcedure);
                        return result.FirstOrDefault();
                    }
                    else
                    {
                        var result = await connection.QueryAsync<int>(
                            SpName.AttestationThemeInsert,
                            new
                            {
                                model.CourseId,
                                model.Theme
                            },
                            dbTransaction,
                            commandType: CommandType.StoredProcedure);
                        return result.FirstOrDefault();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    throw ex;
                }
            }

        }



        public async Task AttestationThemeDelete(int? id)
        {
            {
                try
                {
                    await connection.QueryAsync<AttestationTheme>(
                        SpName.AttestationThemeDelete,
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
        }
        #endregion
    }
}