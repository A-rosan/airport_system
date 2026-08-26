
using airport_system.Models;
using airport_system.Exceptions;
namespace airport_system.Service
{
    public static class AirportManager
    {
        public static List<Flight> FlightList { get; } = new List<Flight>();
        private static readonly List<Employee> _employee= new();
        public static IReadOnlyList<Employee> EmployeeList => _employee;


        //adding flight to the flight list
        public static void AddFlight(Flight flight)
        {
            //if flight with the same flight number already exists, throw an exception
            //using FindFlight method to check if flight already exists
            if (FindFlight(flight.FlightNumber) != null)
            {
                throw new DuplicateFlightException($"Flight {flight.FlightNumber} already exists.");
            }
            FlightList.Add(flight);
        }
        //finding flight by flight number
        public static Flight? FindFlight(
            string flightNumber
            )
        {
            // Use StringComparison.OrdinalIgnoreCase for case-insensitive comparison
            // This will return the first flight that matches the flight number, or null if no match is found
            return FlightList.FirstOrDefault(f => f.FlightNumber.Equals(flightNumber, StringComparison.OrdinalIgnoreCase));
        }

        //method to remove flight from the flight list
        public static void RemoveFlight(string flightNumber)
        {
            Flight? flight = FindFlight(flightNumber);
            if (flight == null)
            {
                throw new FlightNotFoundException($"Flight {flightNumber} not found.");
            }
            FlightList.Remove(flight);
        }

        //adding passenger to a flight
        public static void AddPassengerToFlight(string flightNumber, Passenger passenger)
        {
            Flight? flight = FindFlight(flightNumber);

            if (flight == null)
            {
                throw new FlightNotFoundException(
                    $"Flight {flightNumber} not found."
                );
            }

            bool passengerAlreadyBooked = FlightList
                .Any(f => f.FlightNumber != flightNumber &&
                    f.Passengers.Any(p =>
                        p.Id == passenger.Id ||
                        p.PassportNumber.Equals(
                            passenger.PassportNumber,
                            StringComparison.OrdinalIgnoreCase
                        )
                    )
                );

            if (passengerAlreadyBooked)
            {
                throw new DuplicatePassengerException(
                    $"Passenger with ID {passenger.Id} or passport number " +
                    $"{passenger.PassportNumber} is already assigned to another flight."
                );
            }

            flight.AddPassengers(passenger);
        }

        //adding employee to the employee list
        public static void AddEmployee(Employee employee)
        {
            if (FindEmployee(employee.EmployeeId) != null)
            {
                throw new DuplicateEmployeeException(
                    $"Employee with ID {employee.EmployeeId} already exists."
                );
            }

            _employee.Add(employee);
        }

        //method to view employees
        public static void ViewEmployees()
        {
            if (_employee.Count == 0)
            {
                Console.WriteLine("No employees available.");
                return;
            }

            foreach (Employee employee in _employee)
            {
                employee.DisplayInfo();
                Console.WriteLine("--------------------");
            }
        }
        //methode to find an employee by ID

        public static Employee? FindEmployee(string employeeId)
        {
            return _employee.FirstOrDefault(e =>
                e.EmployeeId.Equals(
                    employeeId,
                    StringComparison.OrdinalIgnoreCase
                )
            );
        }
        //methode to remove an employee by employeeId 
        public static void RemoveEmployee(string employeeId)
        {
            Employee? employee = FindEmployee(employeeId);

            if (employee == null)
            {
                throw new EmployeeNotFoundException(
                    $"Employee with EmployeeID {employeeId} not found."
                );
            }

            _employee.Remove(employee);
        }
    }
}
