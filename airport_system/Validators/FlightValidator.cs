using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Validators
{
    public static class FlightValidator
    {
        public static bool IsValidCapacity(int capacity)
        {
            return capacity > 0;
        }
    }
}
