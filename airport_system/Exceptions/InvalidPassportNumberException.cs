using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Exceptions
{
    public class InvalidPassportNumberException(string message):AirportExceptions(message)
    {
    }
}
