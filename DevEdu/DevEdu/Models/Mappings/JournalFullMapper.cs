using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class JournalFullMapper
    {
        public static Journal ToDataModel(JournalFullInputModel inputModel)
        {
            Journal model = new Journal();
            model.Id = inputModel.Id;
            model.UserId = inputModel.UserID;
            model.LessonID = inputModel.LessonID;
            model.IsAbsent = inputModel.IsAbsent;
            model.Feadback = inputModel.Feadback;
            model.AbsentReason = inputModel.AbsentReason;
            return model;
        }
    }
}
