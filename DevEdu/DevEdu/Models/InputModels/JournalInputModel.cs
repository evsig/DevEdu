using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.InputModels
{
    public class JournalInputModel
    {
        public int GroupId { get; set; }
        public string Date { get; set; }
        public List<int> UserIDs { get; set; }
        public int TeacherId { get; set; }
    }
}
