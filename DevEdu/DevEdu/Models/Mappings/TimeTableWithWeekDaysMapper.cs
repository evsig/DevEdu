using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevEdu.Models.OutputModels;
using DevEdu.Data;
using DevEdu.Data.Models;
using DevEdu.Models.InputModels;

namespace DevEdu.Models.Mappings
{
    public class TimeTableWithWeekDaysMapper
    {
        public static List<TimeTable> ToDataModels(TimeTableWithWeekDaysInputModel timeTable)
        {
            List<TimeTable> result = new List<TimeTable>();
            DateTime startdate = timeTable.StartDate;

            while (timeTable.EndDate.CompareTo(startdate) >= 0)
            {
                foreach (var weekDay in timeTable.WeekDays)
                {
                    if ((int)startdate.DayOfWeek == weekDay)
                    {
                        result.Add(new TimeTable()
                        {
                            GroupId = timeTable.GroupId,
                            RoomNumber = timeTable.RoomNumber,
                            Day = startdate,
                            TimeStart = TimeSpan.Parse(timeTable.TimeStart),
                            TimeEnd = TimeSpan.Parse(timeTable.TimeEnd)
                        });
                    }
                }

                startdate = startdate.AddDays(1);
            }

            return result;
        }

    }

}
//public int RoomNumber { get; set; }
//public List<int> WeekDays { get; set; }
//public string TimeStart { get; set; }
//public string TimeEnd { get; set; }
//public DateTime StartDate { get; set; }
//public DateTime EndDate { get; set; }
//public int GroupId { get; set; }