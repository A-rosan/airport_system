using airport_system.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Models
{
    public class Manager (int id ,string name ,string managerId): Employee(id, name, managerId, EmployeeRole.Manager)
    {
        public override void PerformDuty()
        {
            Console.WriteLine($"Manager {Name} is overseeing airport operations.");
        }
    }
}
