using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevEdu.Models.OutputModels;
using DevEdu.Data;
using DevEdu.Data.Models;

namespace DevEdu.Models.Mappings
{
    public class TimeTableWithCourseNameMapper
    {
        public static List<TimeTableWithCourseNameOutputModel> ToOutputModels(List<TimeTable> timeTables)
        {
            List<TimeTableWithCourseNameOutputModel> result = new List<TimeTableWithCourseNameOutputModel>();

            foreach (TimeTable table in timeTables)
            {
                result.Add(ToOutputModel(table));
            }

            return result;
        }
        public static TimeTableWithCourseNameOutputModel ToOutputModel(TimeTable timeTable)
        {
            return new TimeTableWithCourseNameOutputModel
            {
                Id = (int)timeTable.Id,
                CourseName = timeTable.Course.Name,
                RoomNumber = timeTable.RoomNumber,
                Day = timeTable.Day,
                TimeStart = timeTable.TimeStart.ToString(@"hh\:mm"),
                TimeEnd = timeTable.TimeEnd.ToString(@"hh\:mm")
            };
        }
    }
}
