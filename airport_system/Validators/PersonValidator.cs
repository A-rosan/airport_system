using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Validators
{
    public class PersonValidator
    {

        public static bool IsValidPersonId(int id)
        {
             return id > 0;
        }

    }
}
