using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class StudentGroupMapper
    {
        public static StudentGroup ToDataModel(StudentGroupInputModel model)
        {
            return new StudentGroup
            {
                UserId = model.UserId,
                GroupId = model.GroupId,
                Rating =  model.Rating
            };
        }
    }
}
