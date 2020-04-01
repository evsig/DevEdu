using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class UserAttestationWideMapper
    {
        public static List<UserAttestation> ToDataModels(UserAttestationWideInputModel wideModel)
        {
            List<UserAttestation> listOfAttestations = new List<UserAttestation>();
            foreach (var item in wideModel.PassedThemes)
            {
                ThemePass theme = PassedThemesMapper.ToDataModel(item);
                UserAttestation attestation = new UserAttestation
                {
                    UserId = wideModel.StudentId,
                    AttestationThemeId = theme.ThemeId,
                    TheoryPassed = theme.TheoryPass,
                    PracticePassed = theme.PracticePass,
                };
                listOfAttestations.Add(attestation);
            }
            return listOfAttestations;
        }
    }
}
