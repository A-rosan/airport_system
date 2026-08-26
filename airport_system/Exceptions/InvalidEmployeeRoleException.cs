using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Exceptions
{
    public class InvalidEmployeeRoleException(string message): AirportExceptions(message)
    {
    }
}
