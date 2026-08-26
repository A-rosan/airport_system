using System;
using System.Collections.Generic;
using System.Text;
using airport_system.Interfaces;
namespace airport_system.Models
{
    public class Passenger(int id, string name, string passportNumber) : Person(id, name), INotifiable
    {
        public String PassportNumber { get; set; } = passportNumber;

        public override void DisplayInfo()
        {
            Console.WriteLine($"Passenger ID: {Id}, Name: {Name}, Passport Number: {PassportNumber}");
        }

        public void SendNotification(string message) {

            Console.WriteLine($"Notification for passenger {Name}: " + message);
        
        }
    }
}
