using airport_system.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace airport_system.Models
{
    public class PassengerServiceEmployee(int id, string name, string PassengerServiceNumber) : Employee(id, name, PassengerServiceNumber, EmployeeRole.PassengerService)
    {
        public override void PerformDuty()
        {
            Console.WriteLine($"Passenger Service Employee {Name} is assisting passengers.");
        }
    }
}
