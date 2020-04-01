using Dapper;
using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEdu.Data.Storages
{
    public class UserAttestationStorage
    {
        private readonly IDbConnection connection;
        private readonly IDbTransaction dbTransaction;
        public UserAttestationStorage(string dbСon)
        {
            this.connection = new SqlConnection(dbСon);
        }
        public UserAttestationStorage(IDbConnection dbСon, IDbTransaction dbTran)
        {
            this.connection = dbСon;
            this.dbTransaction = dbTran;
        }

        internal static class SpName
        {
            public const string UserAttestationSelectAll = "UserAttestation_SelectAll";
            public const string UserAttestationSelectById = "UserAttestation_SelectById";
            public const string UserAttestationInsert = "UserAttestation_Insert";
            public const string UserAttestationUpdate = "UserAttestation_Update";
            public const string UserAttestationDelete = "UserAttestation_Delete";
            public const string UserAttestationEdit = "UserAttestation_Edit";
        }

        #region AttestationTheme

        public async Task<List<UserAttestation>> UsersAttestationGetAll()
        {
            try
            {
                var result = await connection.QueryAsync<UserAttestation>(
                    SpName.UserAttestationSelectAll,
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

        public async Task<UserAttestation> UserAttestationGetByID(int? id)
        {
            try
            {
                var result = await connection.QueryAsync<UserAttestation>(
                    SpName.UserAttestationSelectById,
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

        public async Task<int?> UserAttestationAddOrUpdate(UserAttestation model)
        {
            try
            {
                if (model.Id.HasValue) 
                {
                    var result = await connection.QueryAsync<decimal>(
                            SpName.UserAttestationUpdate,
                            new
                            {
                                model.Id,
                                model.UserId,
                                model.AttestationThemeId,
                                model.TheoryPassed,
                                model.PracticePassed
                            },
                            dbTransaction,
                            commandType: CommandType.StoredProcedure);
                    return decimal.ToInt32(result.FirstOrDefault());
                }
                else
                {
                    var result = await connection.QueryAsync<decimal>(
                            SpName.UserAttestationInsert,
                            new
                            {
                                model.UserId,
                                model.AttestationThemeId,
                                model.TheoryPassed,
                                model.PracticePassed
                            },
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

        public async Task<UserAttestation> UserAttestationDelete(int? id)
        {
            try
            {
                var result = await connection.QueryAsync<UserAttestation>(
                    SpName.UserAttestationDelete,
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

        public async Task UserAttestationEdit(List<UserAttestation> attestations)
        {
            try
            {
                foreach (var attestation in attestations)
                {
                    var result = await connection.QueryAsync(
                    SpName.UserAttestationEdit,
                    new
                    {
                        attestation.UserId,
                        attestation.AttestationThemeId,
                        attestation.TheoryPassed,
                        attestation.PracticePassed
                    },
                    dbTransaction,
                    commandType: CommandType.StoredProcedure);
                    result.FirstOrDefault();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
