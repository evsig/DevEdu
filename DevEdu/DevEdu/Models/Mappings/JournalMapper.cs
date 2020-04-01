using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class JournalMapper
    {
        public static List <Journal> ToModel(JournalInputModel journalInputModel)
        {
            List<Journal> journals = new List<Journal>();
            for(int i = 0; i<journalInputModel.UserIDs.Count;i++)
            {
                Journal model = new Journal();
                model.Lesson = new Lesson();
                model.Lesson.Date = Convert.ToDateTime(journalInputModel.Date);
                model.UserId = journalInputModel.UserIDs[i];
                model.Lesson.GroupId = journalInputModel.GroupId;
                journals.Add(model);
            }
            return journals;
        }
        public static int ToTeacherId(JournalInputModel journalInputModel)
        {
            int teacherId = journalInputModel.TeacherId;
            return teacherId;
        }


        public static List<JournalOutputModel> ToOutputModels(List<Journal> journals)
        {
            List<JournalOutputModel> result = new List<JournalOutputModel>();

            foreach (Journal journal in journals)
            {
                result.Add(ToOutputModel(journal));
            }

            return result;
        }

        public static JournalOutputModel ToOutputModel(Journal journal)
        {
            return new JournalOutputModel
            {
                Id = (int)journal.Id,
                UserId =journal.UserId,
                IsAbsent=journal.IsAbsent,
                Feadback = journal.Feadback,
                AbsentReason=journal.AbsentReason
            };
        }
    }
}
