using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels
{
    public class JournalOutputModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public bool IsAbsent { get; set; }
        public string Feadback { get; set; }
        public string AbsentReason { get; set; }
    }
}
