using DevEdu.Data.Models;
using DevEdu.Models.OutputModels;
using System.Collections.Generic;

namespace DevEdu.Models.Mappings
{
    public class UserBlankFieldsMapper
    {
        public static List<UserBlankFieldsOutputModel> ToOutputModels(List<UserBlankFields> fields)
        {
            List<UserBlankFieldsOutputModel> result = new List<UserBlankFieldsOutputModel>();

            foreach (UserBlankFields field in fields)
            {
                result.Add(ToOutputModel(field));
            }

            return result;
        }

        public static UserBlankFieldsOutputModel ToOutputModel(UserBlankFields currentField)
        {
            return new UserBlankFieldsOutputModel
            {
                JournalId = currentField.JournalId,
                LessonDate = currentField.LessonDate.ToString("dd.MM.yyyy")
            };
        }
    }
}