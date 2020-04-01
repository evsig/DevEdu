using DevEdu.Data.Models;
using DevEdu.Models.InputModels;
using DevEdu.Models.OutputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevEdu.Models.Mappings
{
    public class UserAttestationMapper
    {
        public static List<UserAttestationOutputModel> ToOutputModels(List<UserAttestation> attestations)
        {
            List<UserAttestationOutputModel> result = new List<UserAttestationOutputModel>();

            foreach (UserAttestation attestation in attestations)
            {
                result.Add(ToOutputModel(attestation));
            }

            return result;
        }

        public static UserAttestationOutputModel ToOutputModel(UserAttestation attestation)
        {
            return new UserAttestationOutputModel
            {
                Id = attestation.Id,
                UserId = attestation.UserId,
                AttestationThemeId = attestation.AttestationThemeId,
                TheoryPassed = attestation.TheoryPassed,
                PracticePassed = attestation.PracticePassed
            };
        }

        public static List<UserAttestation> ToDataModels(List<UserAttestationInputModel> attestations)
        {
            List<UserAttestation> result = new List<UserAttestation>();

            foreach (UserAttestationInputModel attestation in attestations)
            {
                result.Add(ToDataModel(attestation));
            }

            return result;
        }

        public static UserAttestation ToDataModel(UserAttestationInputModel attestation)
        {
            return new UserAttestation
            {
                Id = (int?)attestation.Id,
                UserId = attestation.UserId,
                AttestationThemeId = attestation.AttestationThemeId,
                TheoryPassed = attestation.TheoryPassed,
                PracticePassed = attestation.PracticePassed
            };
        }
    }
}
