using DevEdu.Data;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Tests
{
	[TestFixture]
	class CourseProgramSkillTests
	{

		CourseStorage courseStorage;
		List<CourseProgramSkill> listCourseProgramSkill = new List<CourseProgramSkill>();
		List<Course> listCourses = new List<Course>();
		List<CourseProgram> listcourseProgram = new List<CourseProgram>();

		#region Setup
		public async Task Setup()
		{
			listCourseProgramSkill.AddRange(CourseProgramSkillMocks.listCourseProgramSkill.Select(x => (CourseProgramSkill)x.Clone()));
			listCourses.AddRange(CourseMock.listCourse.Select(x => (Course)x.Clone()));
			listcourseProgram.AddRange(CourseProgramMock.listCourseProgram.Select(x => (CourseProgram)x.Clone()));

			for (int i = 0; i < listCourses.Count; i++)
			{
				listCourses[i].Id = await courseStorage.CourseAddOrUpdate((Course)listCourses[i].Clone());
				listcourseProgram[i].CourseId = (int)listCourses[i].Id;
				listcourseProgram[i].Id = await courseStorage.CourseProgramAddOrUpdate(listcourseProgram[i]);
				listCourseProgramSkill[i].CourseProgramId = (int)listcourseProgram[i].Id;
				listCourseProgramSkill[i].Id = await courseStorage.CourseProgramSkillAddOrUpdate(listCourseProgramSkill[i]);
			}
		}
		#endregion
		[Test]
		public async Task CourseProgramSkillTest()
		{
			IDbConnection connection = new SqlConnection(DBConnection.connString);
			connection.Open();
			IDbTransaction transaction = connection.BeginTransaction();
			courseStorage = new CourseStorage(connection, transaction);
			try
			{
				await Setup();
				await TestSelects();
				foreach (CourseProgramSkill CourseProgramSkillToDelete in listCourseProgramSkill)
				{
					await CourseProgramSkillTestDelete(CourseProgramSkillToDelete);
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			finally
			{
				transaction.Rollback();
			}
		}


		private async Task TestSelects()
		{
			List<CourseProgramSkill> actualList = await courseStorage.CourseProgramSkillsGetAll();
			for (int i = 0; i < listCourseProgramSkill.Count; i++)
			{
				Assert.Contains(listCourseProgramSkill[i], actualList);
			}
			CourseProgramSkill actual = await courseStorage.CourseProgramSkillGetById((int)listCourseProgramSkill[0].Id);
			Assert.AreEqual(listCourseProgramSkill[0], actual);
		}

		private async Task CourseProgramSkillTestDelete(CourseProgramSkill CourseProgramSkillToDelete)
		{
			List<CourseProgramSkill> actualCPS1 = await courseStorage.CourseProgramSkillsGetAll();
			await courseStorage.CourseProgramSkillDelete(CourseProgramSkillToDelete);
			List<CourseProgramSkill> actualCPS2 = await courseStorage.CourseProgramSkillsGetAll();
			Assert.AreEqual(actualCPS1.Count() - 1, actualCPS2.Count());
			Assert.False(actualCPS2.Contains(CourseProgramSkillToDelete));
		}
	}
}
