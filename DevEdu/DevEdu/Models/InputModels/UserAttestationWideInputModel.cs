using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.InputModels
{
    public class UserAttestationWideInputModel
    {
        public int StudentId { get; set; }

        public int TeacherId { get; set; }
        
        public List<AttestationThemesForPass> PassedThemes { get; set; }
    }
}
