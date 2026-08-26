using airport_system.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Models
{
    public class SecurityEmployee (int id, string name, string securityId) : Employee(id, name, securityId, EmployeeRole.Security)
    {
        public override void PerformDuty()
        {
            Console.WriteLine($"Security Employee {Name} is ensuring airport security.");
        }
    }
}
