using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Exceptions
{
    public class InvalidFlightStatusException (string message): AirportExceptions(message)
    {
    }
}
