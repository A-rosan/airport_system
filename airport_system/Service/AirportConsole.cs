using airport_system.Enums;
using airport_system.Exceptions;
using airport_system.Factories;
using airport_system.Interfaces;
using airport_system.Models;
using airport_system.Validators;

namespace airport_system.Service
{

    public static class AirportConsole
    {
        private static int ReadInt(string message)
        {
            Console.Write(message);

            string? input = Console.ReadLine();

            if (!int.TryParse(input, out int result))
            {
                throw new FormatException("Input is not a valid integer.");
            }

            return result;
        }
        //system menu
        private static void ShowMenu()
        {
            Console.WriteLine("------------------------");
            Console.WriteLine("Welcome to the Airport System!");
            Console.WriteLine("------------------------");

            Console.WriteLine("Please select an option:");
            Console.WriteLine("1. Add a flight");
            Console.WriteLine("2. Add a passenger");
            Console.WriteLine("3. Add Employee");
            Console.WriteLine("4. View flights");
            Console.WriteLine("5. View passengers");
            Console.WriteLine("6. Change flight status");
            Console.WriteLine("7. Delete Passenger");
            Console.WriteLine("8. Delete Flight");
            Console.WriteLine("9. View employees");
            Console.WriteLine("10. Delete employees");
            Console.WriteLine("11. Perform Employee Duty");
            Console.WriteLine("12. Send Employee Notification");
            Console.WriteLine("13. Exit");
        }
        //method to add flight
        private static void AddFlight()
        {

            string flightNumber = ReadRequiredString("Enter flight number: ");

            string destination = ReadRequiredString("Enter destination: ");

            int capacity = ReadInt("Enter capacity: ");
            if (!FlightValidator.IsValidCapacity(capacity))
            {
                throw new InvalidFlightCapacityException(
                    $"Invalid flight capacity: {capacity}"
                );
            }

            Flight flight = new Flight(flightNumber, destination, capacity, FlightStatus.Scheduled);

            AirportManager.AddFlight(flight);
            Console.WriteLine("Flight added successfully.");

        }
        //method to change flight status
        private static void ChangeFlightStatus(string flightNumber)
        {
            Console.WriteLine("You selected: Change flight status");

            Flight? flight = AirportManager.FindFlight(flightNumber);

            if (flight == null)
            {
                throw new FlightNotFoundException(
                    $"Flight {flightNumber} not found."
                );
            }

            Console.WriteLine("Select new status:");
            DisplayFlightStatus();

            int statusChoice = ReadInt("Enter status number: ");

            int statusValue = statusChoice - 1;

            if (!Enum.IsDefined(typeof(FlightStatus), statusValue))
            {
                throw new InvalidFlightStatusException(
                    $"Invalid status selection: {statusChoice}."
                );
            }

            FlightStatus newStatus = (FlightStatus)statusValue;

            flight.ChangeStatus(newStatus);

            Console.WriteLine(
                $"Flight {flight.FlightNumber} status changed to {flight.Status}."
            );
        }
        //display the available flight status
        private static void DisplayFlightStatus()
        {
            int counter = 1;

            foreach (FlightStatus status in Enum.GetValues<FlightStatus>())
            {
                Console.WriteLine($"{counter}. {status}");
                counter++;
            }
        }

        //method to remove flight
        private static void RemoveFlight(string flightNumber)
        {
            Console.WriteLine("You selected: Remove a flight");

            Flight? flight = AirportManager.FindFlight(flightNumber);

            if (flight == null)
            {
                throw new FlightNotFoundException(
                    $"Flight {flightNumber} not found."
                );
            }

            AirportManager.RemoveFlight(flightNumber);

            Console.WriteLine(
                $"Flight {flightNumber} to {flight.Destination} removed successfully."
            );
        }

        //method to add passenger
        private static void AddPassenger()
        {

            Console.WriteLine("You selected: Add a passenger");

            int id = ReadInt("Enter an integer passenger id: ");

            if (!PersonValidator.IsValidPersonId(id))
            {
                throw new InvalidPersonIdException(
                    $"Invalid Person Id: {id}"
                );
            }

            string name = ReadRequiredString("Enter passenger name: ");

            string passportNumber = ReadRequiredString(
                "Enter passenger passport number: "
            );

            if (!PassengerValidator.IsValidPassportNumber(passportNumber))
            {
                throw new InvalidPassportNumberException(
                    $"Invalid Passport Number: {passportNumber}"
                );
            }

            Passenger passenger = new Passenger(id, name, passportNumber);

            string passengerFlightNumber = ReadRequiredString("Enter flight number: ");
            AirportManager.AddPassengerToFlight(
                    passengerFlightNumber,
                    passenger
             );

            Console.WriteLine($"Passenger {passenger.Name} added successfully.");

            passenger.SendNotification(
                $"You have been added to flight {passengerFlightNumber}.\nWe look forward to serving you."
            );

        }

        //method to remove passenger
        private static void RemovePassenger(string flightNumber, int passengerId)
        {
            Flight? flight = AirportManager.FindFlight(flightNumber);

            if (flight == null)
            {
                throw new FlightNotFoundException(
                    $"Flight {flightNumber} not found."
                );
            }

            Passenger? passenger = flight.Passengers
                .FirstOrDefault(p => p.Id == passengerId);

            if (passenger == null)
            {
                throw new PassengerNotFoundException(
                    $"Passenger with ID {passengerId} not found on flight {flightNumber}."
                );
            }

            flight.RemovePassenger(passengerId);

            Console.WriteLine(
                $"Passenger {passenger.Name} removed from flight {flightNumber}."
            );
        }

        //method to add employee

      private static void AddEmployee()
        {
            Console.WriteLine("You selected: Add an employee");

            int id = ReadInt("Enter an integer id:");
            if (!PersonValidator.IsValidPersonId(id)) {

                throw new InvalidPersonIdException($"Invalid Person Id :{ id}");
            
            }

            string name = ReadRequiredString("Enter employee name: ");

            string employeeId = ReadRequiredString("Enter employee ID: ");
            if (!EmployeeValidator.IsValidEmployeeId(employeeId))
            {
                throw new InvalidEmployeeIdException(
                    $"Invalid employee ID: {employeeId}."
                );
            }

            Console.WriteLine("Select an employee role:");
            DisplayEmployeeRoles();

            int roleNum = ReadInt("Enter role number: ");

            int roleValue = roleNum - 1;

            if (!Enum.IsDefined(typeof(EmployeeRole), roleValue))
            {
                throw new InvalidEmployeeRoleException(
                    $"Invalid employee role selection: {roleNum}."
                );
            }

            EmployeeRole role = (EmployeeRole)roleValue;

            Employee employee = EmployeeFactory.Create(
                role,
                id,
                name,
                employeeId
            );

            AirportManager.AddEmployee(employee);

            Console.WriteLine(
                $"Employee {employee.Name} - {employee.Role} added successfully."
            );
        }
        private static void DisplayEmployeeRoles()
        {
            int counter = 1;
            foreach (EmployeeRole role in Enum.GetValues<EmployeeRole>())
            {
                Console.WriteLine($"{counter}. {role}");
                counter++;
            }

        }
        private static void ViewFlights()
        {
            Console.WriteLine("You selected: View flights");
            List<Flight> flights = AirportManager.FlightList;
            if (flights.Count == 0)
            {
                Console.WriteLine("No flights available.");
                return;
            }
            foreach (Flight flight in flights)
            {
                Console.WriteLine($"Flight: {flight.FlightNumber}");
                Console.WriteLine($"Destination: {flight.Destination}");
                Console.WriteLine($"Capacity: {flight.Capacity}");
                Console.WriteLine($"Status: {flight.Status}");
                Console.WriteLine($"Passengers: {flight.GetPassengersCount()}");
                Console.WriteLine("--------------------");
            }
        }
        private static void ViewPassengers(string flightNumber)
        {
            Flight? flight = AirportManager.FindFlight(flightNumber);

            if (flight == null)
            {
                throw new FlightNotFoundException(
                    $"Flight {flightNumber} not found."
                );
            }

            if (flight.Passengers.Count == 0)
            {
                Console.WriteLine("No passengers on this flight.");
                return;
            }

            Console.WriteLine($"Passengers on flight {flightNumber}:");

            foreach (Passenger passenger in flight.Passengers)
            {
                Console.WriteLine($"Passenger ID: {passenger.Id}");
                Console.WriteLine($"Passenger Name: {passenger.Name}");
                Console.WriteLine(
                    $"Passenger Passport Number: {passenger.PassportNumber}"
                );

                Console.WriteLine("--------------------");
            }
        }
        //methode to view employees
        private static void ViewEmployees()
        {
            Console.WriteLine("You selected: View employees");
            AirportManager.ViewEmployees();
        }
        //methode to remove employee 
        private static void RemoveEmployee(string employeeId)
        {
            Employee? employee = AirportManager.FindEmployee(employeeId);

            if (employee == null)
            {
                throw new EmployeeNotFoundException(
                    $"Employee with EmployeeID {employeeId} not found."
                );
            }

            AirportManager.RemoveEmployee(employeeId);

            Console.WriteLine(
                $"Employee {employee.Name} removed successfully."
            );
        }
        //methode for PerformEmployeeDuty  
        private static void PerformEmployeeDuty(string employeeId)
        {

            Employee? employee = AirportManager.FindEmployee(employeeId);
            if (employee == null)
            {

                throw new EmployeeNotFoundException(
                        $"Employee with EmployeeID {employeeId} not found."
                    );
            }
            employee.PerformDuty();

        }
        private static void SendNotification(INotifiable notifiable)
        {
            string message = ReadRequiredString("Enter notification message: ");

            notifiable.SendNotification(message);

        }
        //method to send notification to employee
       private static void SendEmployeeNotification(string employeeId)
        {
            Employee? employee = AirportManager.FindEmployee(employeeId);

            if (employee == null)
            {
                throw new EmployeeNotFoundException(
                    $"Employee with EmployeeID {employeeId} not found."
                );
            }

            SendNotification(employee);
        }
        //method to check the string input
        private static string ReadRequiredString(string message)
        {
            Console.Write(message);

            string input = Console.ReadLine()?.Trim() ?? "";

            if (input == string.Empty)
            {
                throw new FormatException("Input cannot be empty.");
            }

            return input;
        }
        public static void Run()
        {
            while (true)
            {
                try
                {
                    ShowMenu();

                    int option = ReadInt("Enter your option: ");

                    Console.Clear();

                    switch (option)
                    {
                        case 1:
                            AddFlight();
                            break;

                        case 2:
                            AddPassenger();
                            break;

                        case 3:
                            AddEmployee();
                            break;

                        case 4:
                            ViewFlights();
                            break;

                        case 5:
                            string flightPassengerNumber =
                                ReadRequiredString("Enter flight number: ");

                            ViewPassengers(flightPassengerNumber);
                            break;

                        case 6:
                            string flightNumberForStatus =
                                ReadRequiredString("Enter flight number: ");

                            ChangeFlightStatus(flightNumberForStatus);
                            break;

                        case 7:
                            string flightNumberForRemoval =
                                ReadRequiredString("Enter flight number: ");

                            ViewPassengers(flightNumberForRemoval);

                            int passengerId =
                                ReadInt("Enter Passenger ID: ");

                            RemovePassenger(
                                flightNumberForRemoval,
                                passengerId
                            );
                            break;

                        case 8:
                            string flightNumberToDelete =
                                ReadRequiredString("Enter flight number to delete: ");

                            RemoveFlight(flightNumberToDelete);
                            break;

                        case 9:
                            ViewEmployees();
                            break;

                        case 10:
                            string employeeNumber =
                                ReadRequiredString("Enter the EmployeeId: ");

                            RemoveEmployee(employeeNumber);
                            break;

                        case 11:
                            string employeeNumberForDuty =
                                ReadRequiredString("Enter the EmployeeId: ");

                            PerformEmployeeDuty(employeeNumberForDuty);
                            break;

                        case 12:
                            string employeeIdForNotification =
                                ReadRequiredString("Enter the EmployeeId: ");

                            SendEmployeeNotification(employeeIdForNotification);
                            break;

                        case 13:
                            Console.WriteLine("Goodbye!");
                            return;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                    
                }
                catch (AirportExceptions ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Input error: {ex.Message}");
                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
