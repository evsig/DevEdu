using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using System;
using System.Collections.Generic;

namespace DevEdu.Models.Mappings
{
    public class TimeTableMapper
    {
        public static TimeTable ToDataModel(TimeTableInputModel inputModel)
        {
            var result = new TimeTable
            {
                Id = inputModel.Id,
                GroupId = inputModel.GroupId,
                RoomNumber = inputModel.RoomNumber,
                Day = Convert.ToDateTime(inputModel.Day),
                TimeStart = TimeSpan.Parse(inputModel.TimeStart),
                TimeEnd = TimeSpan.Parse(inputModel.TimeEnd)
            };
            return result;
        }
    }
}
