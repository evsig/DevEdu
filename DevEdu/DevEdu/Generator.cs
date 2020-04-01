using DevEdu.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevEdu
{
    public class Generator
    {
        Random rnd = new Random();

        private int minPassLength = 8;
        private int minLoginLength = 6;
        bool isLowerCaseChar, isUpperCaseChar, isSymbol, isNumber;

        private string[] lowerCaseCharArray = new string[]
        {
            "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"
        };

        private string[] upperCaseCharArray = new string[]
        {
            "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
        };

        private string[] symbolArr = new string[]
        {
            "!","@","#","$","%","*","&"
        };

        private int[] numberArr = new int[]
        {
            1,2,3,4,5,6,7,8,9,0
        };


        public string GeneratePass()
        {
            string character = "";
            StringBuilder pass = new StringBuilder();
            int passLength = rnd.Next(minPassLength, 21);

            for (int i = 0; i < passLength; i++)
            {
                int whatSymbol = rnd.Next(0, 5);

                switch (whatSymbol)
                {
                    case 1:
                        isLowerCaseChar = true;
                        break;
                    case 2:
                        isUpperCaseChar = true;
                        break;
                    case 3:
                        isSymbol = true;
                        break;
                    case 4:
                        isNumber = true;
                        break;
                }

                if (isLowerCaseChar)
                {
                    int index = rnd.Next(0, lowerCaseCharArray.Length);
                    character = lowerCaseCharArray[index];
                    isLowerCaseChar = false;
                }
                if (isUpperCaseChar)
                {
                    int index = rnd.Next(0, upperCaseCharArray.Length);
                    character = upperCaseCharArray[index];
                    isUpperCaseChar = false;
                }
                if (isSymbol)
                {
                    int index = rnd.Next(0, symbolArr.Length);
                    character = symbolArr[index];
                    isSymbol = false;
                }
                if (isNumber)
                {
                    int index = rnd.Next(0, numberArr.Length);
                    character = numberArr[index].ToString();
                    isNumber = false;
                }
                pass.Append(character);
            }
            return pass.ToString();
        }

        public string GenerateLogin(Role role)
        {            
            string character = "";
            StringBuilder login = new StringBuilder();
            login.Append(role.Name);
            int loginLength = rnd.Next(minLoginLength, 16);

            for (int i = 0; i < loginLength; i++)
            {
                int whatSymbol = rnd.Next(0, 3);

                switch (whatSymbol)
                {
                    case 1:
                        isLowerCaseChar = true;
                        break;
                    case 2:
                        isUpperCaseChar = true;
                        break;
                    case 3:
                        isNumber = true;
                        break;
                }

                if (isLowerCaseChar)
                {
                    int index = rnd.Next(0, lowerCaseCharArray.Length);
                    character = lowerCaseCharArray[index];
                    isLowerCaseChar = false;
                }
                if (isUpperCaseChar)
                {
                    int index = rnd.Next(0, upperCaseCharArray.Length);
                    character = upperCaseCharArray[index];
                    isUpperCaseChar = false;
                }                
                if (isNumber)
                {
                    int index = rnd.Next(0, numberArr.Length);
                    character = numberArr[index].ToString();
                    isNumber = false;
                }
                login.Append(character);
            }
            return login.ToString();
        }       
    }
}
