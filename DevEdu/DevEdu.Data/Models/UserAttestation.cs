using System;
using System.Collections.Generic;
using System.Text;

namespace DevEdu.Data.Models
{
    public class UserAttestation
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int AttestationThemeId { get; set; }
        public bool TheoryPassed { get; set; }
        public bool PracticePassed { get; set; }

        public override bool Equals(object obj)
        {
            return obj is UserAttestation attestation &&
                   Id == attestation.Id &&
                   UserId == attestation.UserId &&
                   AttestationThemeId == attestation.AttestationThemeId &&
                   TheoryPassed == attestation.TheoryPassed &&
                   PracticePassed == attestation.PracticePassed;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, UserId, AttestationThemeId, TheoryPassed, PracticePassed);
        }
    }
}
