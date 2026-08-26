using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Exceptions
{
    public class DuplicatePassengerException(string message):AirportExceptions(message)
    {
    }
}
