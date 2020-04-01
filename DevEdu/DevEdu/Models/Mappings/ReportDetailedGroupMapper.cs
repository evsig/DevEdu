using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class ReportDetailedGroupMapper
    {
        public static List<ReportDetailedGroupOutputModel> ToOutputModels(List<ReportDetailedGroup> groups)
        {
            List<ReportDetailedGroupOutputModel> result = new List<ReportDetailedGroupOutputModel>();

            foreach (ReportDetailedGroup group in groups)
            {
                result.Add(ToOutputModel(group));
            }

            return result;
        }

        public static ReportDetailedGroupOutputModel ToOutputModel(ReportDetailedGroup currentGroup)
        {
            return new ReportDetailedGroupOutputModel
            {
                Id = currentGroup.Id,
                StartDate = currentGroup.StartDate.ToString("dd.MM.yyyy"),
                EndDate = currentGroup.EndDate.ToString("dd.MM.yyyy"),
                StudentCount = currentGroup.StudentCount,
                TeacherCount = currentGroup.TeacherCount
            };
        }
    }
}
