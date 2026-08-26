## 📁 Project Structure

```text
airport_system/
│
├── Enums/
│   ├── EmployeeRole.cs
│   └── FlightStatus.cs
│
├── Exceptions/
│   ├── AirportExceptions.cs
│   └── ...
│
├── Factories/
│   └── EmployeeFactory.cs
│
├── Interfaces/
│   └── INotifiable.cs
│
├── Models/
│   ├── Person.cs
│   ├── Passenger.cs
│   ├── Employee.cs
│   ├── Flight.cs
│   └── ...
│
├── Service/
│   ├── AirportConsole.cs
│   └── AirportManager.cs
│
├── Validators/
│   ├── PersonValidator.cs
│   ├── PassengerValidator.cs
│   ├── EmployeeValidator.cs
│   └── FlightValidator.cs
│
├── Program.cs
└── airport_system.csproj
```

## 🛠️ Features

- Add, view, and delete flights
- Add, view, and remove passengers
- Add, view, and delete employees
- Change flight status
- Validate flight capacity
- Validate passenger and employee information
- Perform employee-specific duties
- Send notifications to passengers and employees
- Handle errors using custom exceptions

## 🧱 OOP Concepts Used

- Encapsulation
- Inheritance
- Polymorphism
- Abstraction
- Interfaces
- Enums
- Factory Design Pattern
- Custom Exceptions
- Collections
- LINQ

## ▶️ How to Run

Make sure you have the **.NET SDK** installed.

```bash
dotnet run
```

## 🖥️ System Menu

```text
1. Add a flight
2. Add a passenger
3. Add Employee
4. View flights
5. View passengers
6. Change flight status
7. Delete Passenger
8. Delete Flight
9. View employees
10. Delete employees
11. Perform Employee Duty
12. Send Employee Notification
13. Exit
```

## 🎯 Project Purpose

This project was created to practice and demonstrate **C# Object-Oriented Programming** concepts by developing a practical Airport Management System with validation, custom exceptions, interfaces, inheritance, polymorphism, and the Factory Design Pattern.

## 👨‍💻 Author

**Abdullah Al-Rosan**
