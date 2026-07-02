
using System.Security.Cryptography.X509Certificates;

string filePath = "wateringlog.txt";

bool isRunning = true;

while (isRunning == true)
{
    string choice = ShowMenu();

    switch (choice)
    {
        case "1":
            LogWatering();
            break;

        case "2":
            ViewWateringLogs();
            break;

        case "3":
            ViewLatestWateringLog();
            break;

        case "4":
            SearchPlant();
            break;

        case "5":
            Console.WriteLine("Exiting the program. Goodbye!");
            isRunning = false;
            break;

        default:
            Console.WriteLine("Invalid choice. Please choose 1, 2, 3, or 4.");
            PressEnterToContinue();
            break;
    }
}

string ShowMenu()
{
    // Vis menuen
    Console.WriteLine("==================================\n");
    Console.WriteLine("Watering Log\n");
    Console.WriteLine("\n");
    Console.WriteLine("1. Log New Watering\n");
    Console.WriteLine("2. View Watering Logs\n");
    Console.WriteLine("3. View Latest Watering Log\n");
    Console.WriteLine("4. Search for specific plant logs\n");
    Console.WriteLine("5. Exit\n");
    Console.WriteLine("\n");
    Console.WriteLine("==================================\n");

    // Læs brugerens valg
    string choice = Console.ReadLine() ?? "";

    // Giv valget tilbage
    return choice;
}


void LogWatering()
{
// Log new watering
        IntroTitle();
        Console.WriteLine("Welcome!");
        Console.WriteLine("\n");

        Console.WriteLine("Please enter the name of the plant you would like to log: ");
        string plantName = Console.ReadLine();

        Console.WriteLine("Please enter the date you watered the plant (DD/MM/YYYY): ");
        string dateInput = Console.ReadLine();

        
        Console.WriteLine("Please enter the amount of water used in liters:");
        string waterAmountInput = Console.ReadLine();
        double waterAmount;
        bool isValidWaterAmount = double.TryParse(waterAmountInput, out waterAmount);
        while (!isValidWaterAmount)
        {
            Console.WriteLine("Invalid input. Please enter a valid number for the amount of water used in liters (e.g., 0.5):");
            waterAmountInput = Console.ReadLine();
            isValidWaterAmount = double.TryParse(waterAmountInput, out waterAmount);
        }
      
        Console.WriteLine("Did you use fertilizer? (yes/no): ");
        string fertilizerInput = Console.ReadLine();

        Console.WriteLine("Perfect! Here is the information you entered: \n");
        Console.WriteLine("==================================");
        Console.WriteLine($"Plant Name: {plantName}");
        Console.WriteLine($"Date Watered: {dateInput}");
        Console.WriteLine($"Amount of Water Used: {waterAmount} liters");
        if (fertilizerInput.ToLower() == "yes")
        {
            Console.WriteLine("Fertilizer Used: Yes");
        }
        else
        {
        Console.WriteLine("Fertilizer Used: No");
        }
        Console.WriteLine("==================================");
        

        string logEntry = $"{plantName};{dateInput};{waterAmount};{fertilizerInput}{Environment.NewLine}";
        File.AppendAllText(filePath, logEntry);
        Console.WriteLine("Log saved to file!");
        PressEnterToContinue();
}    

void ViewWateringLogs()
{
    // View watering logs
    IntroTitle();
    Console.WriteLine("\n");

    if (File.Exists(filePath))
    {
        string[] logEntries = File.ReadAllLines(filePath);
        foreach (string entry in logEntries)
        {
            DisplayLog(entry);
        }
        PressEnterToContinue();
    }
    else
    {
        Console.WriteLine("No watering logs found.");
        PressEnterToContinue();
    }
}

void IntroTitle()
{
    Console.WriteLine("==================================");
    Console.WriteLine("WATERING LOG");
    Console.WriteLine("==================================");
}

void PressEnterToContinue()
{
    Console.WriteLine("\nPress Enter to continue...");
    Console.ReadLine();
}

void ViewLatestWateringLog()
{
    IntroTitle();

    if (File.Exists(filePath))
        {
            string[] logEntries = File.ReadAllLines(filePath);

            if (logEntries.Length > 0)
                {
                    int lastIndex = logEntries.Length - 1;
                    string latestLog = logEntries[lastIndex];

                    DisplayLog(latestLog);
                    PressEnterToContinue();
                }
            else
                {
                    Console.WriteLine("No watering logs found.");
                    PressEnterToContinue();
                }
        }
    else
        {
            Console.WriteLine("No watering logs found.");
            PressEnterToContinue();
        }
}

void DisplayLog(string log)
{
    string[] entryParts = log.Split(';');

    Console.WriteLine("----------------------------------");
    Console.WriteLine($"Plant Name: {entryParts[0]}");
    Console.WriteLine($"Date Watered: {entryParts[1]}");
    Console.WriteLine($"Amount of Water Used: {entryParts[2]} liters");
    Console.WriteLine($"Fertilizer Used: {entryParts[3]}");
    Console.WriteLine("----------------------------------");
}

void SearchPlant()
{
    IntroTitle();
    Console.WriteLine("Enter the name of the plant: ");
    string userSearch = Console.ReadLine() ?? "";

    bool foundPlant = false;

    if (File.Exists(filePath))
    {
        string[] logEntries = File.ReadAllLines(filePath);
        foreach (string entry in logEntries)
        {
            string[] entryParts = entry.Split(';');
            string plantName = entryParts[0];

            if (plantName.ToLower() == userSearch.ToLower())
            {
                DisplayLog(entry);
                foundPlant = true;
            }
        }

        if (!foundPlant)
        {
            Console.WriteLine("No logs found for that plant");
        }
        
        PressEnterToContinue();
    }
        else
    {
        Console.WriteLine("No watering logs found.");
        PressEnterToContinue();
    }
}