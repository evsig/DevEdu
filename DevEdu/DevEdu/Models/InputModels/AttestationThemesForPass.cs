using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.InputModels
{
    public class AttestationThemesForPass
    {
        public int ThemeId { get; set; }
        public bool IsTheoryPassed {get; set;}
        public bool IsPracticePassed { get; set; }
    }
}
