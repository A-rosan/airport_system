using airport_system.Enums;
using airport_system.Exceptions;
using airport_system.Models;

namespace airport_system.Factories
{
    public static class EmployeeFactory
    {
        public static Employee Create(
            EmployeeRole role,
            int id,
            string name,
            string employeeId)
        {
            switch (role)
            {
                case EmployeeRole.Pilot:
                    return new Pilot(id, name, employeeId);

                case EmployeeRole.Security:
                    return new SecurityEmployee(id, name, employeeId);

                case EmployeeRole.Manager:
                    return new Manager(id, name, employeeId);

                case EmployeeRole.PassengerService:
                    return new PassengerServiceEmployee(id, name, employeeId);

                default:
                    throw new InvalidEmployeeRoleException(
                        $"Unsupported employee role: {role}"
                    );
            }
        }
    }
}