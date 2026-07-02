# 🌱 WateringLog

WateringLog is a simple C# console application for keeping track of plant watering.

This project was created as a learning project while studying C# and .NET. The goal is not only to build a useful application, but also to practice programming concepts such as methods, loops, arrays, file handling, input validation, and software design.

## ✨ Features

- Add a new watering log
- View all watering logs
- View the latest watering log
- Search logs by plant name
- Save logs to a text file
- Input validation for numeric values

## 📂 Log Format

Each watering entry is stored in a text file (`wateringlog.txt`) using the following format:

```text
PlantName;Date;WaterAmount;Fertilizer
```

Example:

```text
Tomato;02/07/2026;1.5;yes
```

## 🛠 Built With

- C#
- .NET
- Visual Studio Code

## 📚 What I Learned

This project has helped me practice:

- Variables
- Methods
- Switch statements
- While loops
- Arrays
- File handling
- Input validation with `TryParse()`
- Refactoring duplicated code
- Breaking problems into smaller methods

## 🚀 Future Improvements

Planned features include:

- Edit existing logs
- Delete logs
- Statistics (total water used, number of logs, etc.)
- Sort logs
- Store data using classes instead of plain text
- Replace arrays with `List<T>`
- Better date validation

## ▶️ Running the Project

Clone the repository:

```bash
git clone https://github.com/YOUR_USERNAME/WateringLog.git
```

Navigate to the project:

```bash
cd WateringLog
```

Run the application:

```bash
dotnet run
```

## 👨‍💻 About

This project is part of my journey learning C# and software development through Codecademy and personal projects.