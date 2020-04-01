using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.InputModels
{
    public class JournalFullInputModel
    {
        public int? Id { get; set; }
        public int UserID { get; set; }
        public int LessonID { get; set; }
        public bool IsAbsent { get; set; }
        public string Feadback { get; set; }
        public string AbsentReason { get; set; }
    }
}
