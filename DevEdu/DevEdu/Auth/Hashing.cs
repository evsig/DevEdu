using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace DevEdu.Auth
{
    public class Hashing
    {
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        //шифруем пароль
        public static string HashUserPassword(string password)
        {
            return  BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        //сравниваем пароль с хешем
        public static bool ValidateUserPassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}
