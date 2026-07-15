# WateringLog

WateringLog is a C# console application for keeping track of plant watering.

This is a learning project built while studying C# and .NET. The focus has been on object-oriented design, file handling, input validation, and separating responsibilities across classes.

## Features

- Add a new watering log
- View all watering logs
- View the latest watering log
- Search logs by plant name
- Delete a specific log
- Save logs to a text file
- Input validation for numeric values, dates, and booleans, with cancel support

## Project Structure

- `WateringLogEntry.cs` - Represents a single watering entry (PlantName, WateringDate, WaterAmount, FertilizerUsed), including parsing to/from file format.
- `WateringLogStorage.cs` - Loads and saves log entries to file.
- `WateringLogService.cs` - Business logic such as finding entries by plant name, retrieving unique plant names, and deleting entries.
- `InputHelper.cs` - Handles user input and validation, including cancel-supported input.
- `Program.cs` - Controls UI and user flow, delegating logic to the classes above.

## Log Format

Each watering entry is stored in a text file (`wateringlog.txt`) using the following format:

```text
PlantName;Date;WaterAmount;Fertilizer
```

Example:

```text
Tomato;02/07/2026;1.5;yes
```

## Built With

- C#
- .NET
- Visual Studio Code

## What I Learned

- Object-oriented design: classes, responsibilities, encapsulation
- Separating UI, business logic, and data access into distinct layers
- Refactoring from procedural code (arrays, indexes) to OOP (`List<T>`, objects)
- Input validation with cancel support
- File handling and custom serialization/parsing (`ToString()` / `Parse()`)
- Identifying and removing duplicated logic
- Finding and fixing bugs: off-by-one errors, unused parameters, whitespace/case handling

## Future Improvements

- Edit existing logs
- Statistics (total water used, number of logs, etc.)
- Sort logs
- Store data in a SQL database instead of a text file
- Better date validation

## Running the Project

Clone the repository:

```bash
git clone https://github.com/Hectoriouz/WateringLog.git
```

Navigate to the project:

```bash
cd WateringLog
```

Run the application:

```bash
dotnet run
```

## About

Part of my journey learning C# and software development through Codecademy and personal projects.