using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevEdu.Data.Models;
using DevEdu.Data.Storages;
using DevEdu.Tests.Mocks;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DevEdu.Tests
{
    [TestFixture]
    class CourseProgramTests
    {
        List<Course> listCourse = new List<Course>(CourseMock.listCourse);
        List<CourseProgram> coursePrograms;
        private CourseStorage storage;
        [Test]
        public async Task GeneralTest()
        {
            IDbConnection connection = new SqlConnection(DBConnection.connString);
            connection.Open();
            IDbTransaction transaction = connection.BeginTransaction();
            storage = new CourseStorage(connection, transaction);

            try
            {
                await CreateCourseProgram();
                await UpdateCourseProgram();
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

        public async Task CreateCourseProgram()
        {
            coursePrograms = new List<CourseProgram>(CourseProgramMock.listCourseProgram.Select(x => (CourseProgram)x.Clone()));
            var courseId = await storage.CourseAddOrUpdate((Course)listCourse[0].Clone());

            coursePrograms[1].CourseId = courseId;
            coursePrograms[0].CourseId = courseId;

            var courseProgramId = await storage.CourseProgramAddOrUpdate(coursePrograms[0]);
            coursePrograms[0].Id = courseProgramId;
            coursePrograms[1].Id = courseProgramId;
            var courseProgram = await storage.CourseProgramGetById(courseProgramId);
            //courseProgram.ProgramDetails = coursePrograms[1].ProgramDetails;
            Assert.IsTrue(DeepEqual(coursePrograms[0], courseProgram));
        }

        public async Task UpdateCourseProgram()
        {
            var courseProgramId = await storage.CourseProgramAddOrUpdate(coursePrograms[1]);
            var courseProgram = await storage.CourseProgramGetById(courseProgramId);
           // courseProgram.ProgramDetails = coursePrograms[1].ProgramDetails;
            Assert.IsTrue(DeepEqual(courseProgram, coursePrograms[1]));
        }

        private bool DeepEqual(Object expected, Object actual)
        {
            var expectedProperty = expected.GetType().GetProperties();
            var actualProperty = actual.GetType().GetProperties();
            for (int i = 0; i < expectedProperty.Length; i++)
            {
                var expVal = expectedProperty[i].GetValue(expected);
                var actVal = actualProperty[i].GetValue(actual);
                if (!(expectedProperty is List)) continue;
                if (!expVal.Equals(actVal))
                    return false;
            }
            return true;
        }
    }
}
