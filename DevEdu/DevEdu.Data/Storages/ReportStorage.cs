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
    public class ReportStorage
    {
		private readonly IDbConnection connection;
		private readonly IDbTransaction dbTransaction;
		public ReportStorage(string dbСon)
		{
			this.connection = new SqlConnection(dbСon);
		}
		public ReportStorage(IDbConnection dbСon, IDbTransaction dbTran)
		{
			this.connection = dbСon;
			this.dbTransaction = dbTran;
		}

		internal static class SpName
		{
			public const string AverageRating = "Report_AverageRating";
			public const string DetailRating = "Report_DetailedRating";
			public const string QuantityOfLessonsByTopic = "Report_QuantityOfLessonsByTopic";
		}

		public async Task<List<ReportRating>> GetDetailRating(ReportRating ratings)
		{		
			object tempobj = null;
			if (ratings != null && ratings.StartDate.Year > 1753 && ratings.StartDate.Year < 9999)
				tempobj = new { ratings.StartDate, ratings.EndDate };
			try
			{
				var result = await connection.QueryAsync<ReportRating>(
					SpName.DetailRating,
					tempobj,
					dbTransaction,
					commandType: CommandType.StoredProcedure
					);				return result.ToList();
			}
			catch (SqlException ex)
			{
				throw ex;
			}
		}

		public async Task<List<ReportRating>> GetAverageRating(ReportRating ratings)
		{		
			object tempobj = null;
			if (ratings != null && ratings.StartDate.Year > 1753 && ratings.StartDate.Year < 9999)
				tempobj = new { ratings.StartDate, ratings.EndDate };
			try
			{   			 var result = await connection.QueryAsync<ReportRating>(
					SpName.AverageRating,
					tempobj,
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

		public async Task<List<GroupWithTopicAndQuantity>> GetQuantityOfLessonsByTopic(int themeDetailsID)
		{
			try
			{
				var result = await connection.QueryAsync<GroupWithTopicAndQuantity>(
					SpName.QuantityOfLessonsByTopic,					new { themeDetailsID },
					dbTransaction,
					commandType: CommandType.StoredProcedure
				);				return result.ToList();
			}
			catch (SqlException ex)
			{
				Console.WriteLine(ex.Message);
				throw ex;
			}
		}

	}
}
