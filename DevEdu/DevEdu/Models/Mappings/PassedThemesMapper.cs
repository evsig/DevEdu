using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevEdu.Data.Models;
using DevEdu.Models.InputModels;

namespace DevEdu.Models.Mappings
{
    public class PassedThemesMapper
    {
        public static ThemePass ToDataModel(AttestationThemesForPass model)
        {
            return new ThemePass
            {
                ThemeId = model.ThemeId,
                TheoryPass = model.IsTheoryPassed,
                PracticePass = model.IsPracticePassed,
            };
        }
    }
}
