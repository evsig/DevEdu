using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Tests.Mocks
{
    class UserAttestationMock
    {
        public static List<UserAttestation> listAttestation = new List<UserAttestation>
            {
                new UserAttestation()
                {
                    UserId = 1262,
                    AttestationThemeId = 26,
                    TheoryPassed = true,
                    PracticePassed = true

                },
                new UserAttestation()
                {
                    UserId = 1478,
                    AttestationThemeId = 27,
                    TheoryPassed = false,
                    PracticePassed = false
                }
            };
    }
}
