using airport_system.Enums;
using airport_system.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace airport_system.Models

{
    public class Flight
    {

        
        public String FlightNumber { get; set; }
        public String Destination { get; set; }

        public int Capacity { get; set; }

        // List to hold passengers on the flight
        // Use a private list to store passengers and expose it as a read-only list
        // This ensures that passengers can only be added through the AddPassenger method, which enforces the capacity constraint.
        private readonly List<Passenger> _passengers = new();
        public IReadOnlyList<Passenger> Passengers => _passengers;

        public FlightStatus Status { get; set; }

        //initialize the flight constructor
        public Flight(string flightNumber, string destination, int capacity, FlightStatus status)
        {
            FlightNumber = flightNumber;
            Destination = destination;
            Capacity = capacity;
            Status = status;
        }
        //method to add passenger to the flight
        public void AddPassengers(Passenger passenger)
        {
            if (_passengers.Any(p =>
                 p.Id == passenger.Id ||
                 p.PassportNumber.Equals(
                 passenger.PassportNumber,
                 StringComparison.OrdinalIgnoreCase)))
            {
                throw new DuplicatePassengerException(
                    $"Passenger with ID {passenger.Id} or passport number {passenger.PassportNumber} already exists on flight {FlightNumber}."
                );
            }

            if (_passengers.Count >= Capacity)
            {
                throw new FlightFullException(
                    "Flight is full. Cannot add more passengers."
                );
            }

            _passengers.Add(passenger);
        }
        //methode to remove passenger from the flight
        public void RemovePassenger(int passengerId)
        {
            //Finding the passenger by ID.
            //If not found → throw PassengerNotFoundException.
            //If found → remove them from the internal passenger list.
            Passenger? passengerToRemove = _passengers.FirstOrDefault(p => p.Id == passengerId); //return null if not found

            if (passengerToRemove != null)
            {
                _passengers.Remove(passengerToRemove);
            }
            else
            {
                throw new PassengerNotFoundException($"Passenger with ID {passengerId} not found on flight {FlightNumber}.");
            }
        }

        //method to get Passengers count 

        public int GetPassengersCount()
        {
            return _passengers.Count;
        }

        //changing status of the flight
        public void ChangeStatus(FlightStatus newStatus)
        {
            switch (Status)
            {
                
                case FlightStatus.Scheduled:
                    // Only allow changing to Boarding from Scheduled
                    // If the new status is not Boarding, throw an exception
                    if (newStatus != FlightStatus.Boarding)
                    {
                        throw new InvalidFlightStatusException(
                            $"Flight {FlightNumber} cannot change from {Status} to {newStatus}."
                        );
                    }
                    break;

                case FlightStatus.Boarding:
                    // Only allow changing to Departed from Boarding
                    // If the new status is not Departed, throw an exception
                    if (newStatus != FlightStatus.Departed)
                    {
                        throw new InvalidFlightStatusException(
                            $"Flight {FlightNumber} cannot change from {Status} to {newStatus}."
                        );
                    }
                    break;

                case FlightStatus.Departed:
                    // Only allow changing to Arrived from Departed
                    // If the new status is not Arrived, throw an exception
                    if (newStatus != FlightStatus.Arrived)
                    {
                        throw new InvalidFlightStatusException(
                            $"Flight {FlightNumber} cannot change from {Status} to {newStatus}."
                        );
                    }
                    break;

                case FlightStatus.Arrived:
                    throw new InvalidFlightStatusException(
                        $"Flight {FlightNumber} has already arrived."
                    );

                case FlightStatus.Cancelled:
                    throw new InvalidFlightStatusException(
                        $"Flight {FlightNumber} is cancelled."
                    );
            }

            Status = newStatus;
        }
    }
}
