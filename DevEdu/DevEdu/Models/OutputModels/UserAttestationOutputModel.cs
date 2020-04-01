using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.OutputModels
{
    public class UserAttestationOutputModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int AttestationThemeId { get; set; }
        public bool TheoryPassed { get; set; }
        public bool PracticePassed { get; set; }
    }
}
