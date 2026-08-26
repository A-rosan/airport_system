using airport_system.Enums;
using airport_system.Interfaces;
namespace airport_system.Models
{
    public abstract class Employee(int id, string name, string employeeId, EmployeeRole role) : Person(id, name), INotifiable
    {
        public string EmployeeId { get; set; } = employeeId;
        public EmployeeRole Role { get; set; } = role;

        public override void DisplayInfo()
        {
            Console.WriteLine($"Employee ID: {Id}, Name: {Name}, Employee ID: {EmployeeId}, Role: {Role}");
        }

        public abstract void PerformDuty();
        public void SendNotification(string message)
        {
            // Implementation for sending notification
            Console.WriteLine($"Notification for {Role} {Name}: {message}");
        }
    }
}
