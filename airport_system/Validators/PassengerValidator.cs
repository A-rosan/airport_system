using airport_system.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Validators
{
    public static class PassengerValidator
    {
        public static bool IsValidPassportNumber(string passportNumber)
        {
            passportNumber = passportNumber.Trim();

            if (passportNumber == string.Empty)
            {
                return false;
            }

            if (!char.IsLetter(passportNumber[0]))
            {
                return false;
            }

            for (int i = 1; i < passportNumber.Length; i++)
            {
                if (!char.IsDigit(passportNumber[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

