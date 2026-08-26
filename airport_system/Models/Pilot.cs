using airport_system.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Models
{
    public class Pilot(int id ,string name ,string pilotId): Employee(id, name, pilotId, EmployeeRole.Pilot)
    {
        public override void PerformDuty()
        {
            Console.WriteLine($"Pilot {Name} is performing flight duties.");
        }
    }
}
