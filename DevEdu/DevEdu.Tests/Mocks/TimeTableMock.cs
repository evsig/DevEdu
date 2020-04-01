using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Tests.Mocks
{
    public static class TimeTableMock
    {
        public static List<TimeTable> listTimetable = new List<TimeTable>
        {
            new TimeTable()
            {
                GroupId = 1,// change it in test
                RoomNumber = 101,
                Day = new DateTime(2019, 7, 21),
                TimeStart = new TimeSpan(9, 0, 0),
                TimeEnd = new TimeSpan(14, 0, 0)
            },
            new TimeTable()
            {
                GroupId = 2,// change it in test
                RoomNumber = 202,
                Day = new DateTime(2019, 10, 9),
                TimeStart = new TimeSpan(15, 0, 0),
                TimeEnd = new TimeSpan(19, 0, 0)
            }
        };
    }
}
