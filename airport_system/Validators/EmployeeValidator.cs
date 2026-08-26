using airport_system.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Validators
{
    public static class EmployeeValidator
    {
        public static bool IsValidEmployeeId(string employeeId)
        {
            employeeId = employeeId.Trim();

            if (employeeId == string.Empty)
            {
                return false;
            }
            
            if (!char.IsLetter(employeeId[0]))
            {
                return false;
            }

            for (int i = 1; i < employeeId.Length; i++)
            {
                if (!char.IsDigit(employeeId[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
