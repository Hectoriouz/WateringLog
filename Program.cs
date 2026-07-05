
using System.Security.Cryptography.X509Certificates;
using System.Linq;

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
            DeleteWateringLog();
            break;

        case "6":
            Console.WriteLine("Exiting the program. Goodbye!");
            isRunning = false;
            break;

        default:
            Console.WriteLine("Invalid choice. Please choose a real option");
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
    Console.WriteLine("5. Delete Watering Logs\n");
    Console.WriteLine("6. Exit\n");
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
    Console.WriteLine("\nType \"0\" to cancel");
    string plantName = Console.ReadLine() ?? "";
        
    if (plantName == "0")
        {
            return;
        }

    Console.WriteLine("Please enter the date you watered the plant (DD/MM/YYYY): ");
    Console.WriteLine("\nType \"0\" to cancel");
    string dateInput = Console.ReadLine() ?? "";

    if (dateInput == "0")
        {
            return;
        }

    Console.WriteLine("Please enter the amount of water used in liters:");
    Console.WriteLine("\nType \"Cancel\" to cancel");
    string waterAmountInput = Console.ReadLine() ?? "";

    if (waterAmountInput.ToLower() == "cancel")
        {
            return;
        }

    double waterAmount;
    bool isValidWaterAmount = double.TryParse(waterAmountInput, out waterAmount);
    while (!isValidWaterAmount)
        {
            Console.WriteLine("Invalid input. Please enter a valid number for the amount of water used in liters (e.g., 0.5):");
            waterAmountInput = Console.ReadLine() ?? "";
            isValidWaterAmount = double.TryParse(waterAmountInput, out waterAmount);
        }
      
    Console.WriteLine("Did you use fertilizer? (yes/no): ");
    Console.WriteLine("\nType \"0\" to cancel");
    string fertilizerInput = Console.ReadLine() ?? "";

    if (fertilizerInput == "0")
        {
            return;
        }

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
        List<string> logEntries = File.ReadAllLines(filePath).ToList();
        
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
            List<string> logEntries = File.ReadAllLines(filePath).ToList();

            if (logEntries.Count > 0)
                {
                    int lastIndex = logEntries.Count - 1;
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
        List<string> logEntries = File.ReadAllLines(filePath).ToList();
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

void DeleteWateringLog()
{
    IntroTitle();

    if (!File.Exists(filePath))
    {
        Console.WriteLine("No watering logs found.");
        PressEnterToContinue();
        return;
    }

    List<string> logEntries = File.ReadAllLines(filePath).ToList();
    List<string> plantNames = new List<string>();

    foreach (string entry in logEntries)
    {
        string[] entryParts =  entry.Split(";");
        string plantName = entryParts[0];

        if (!plantNames.Contains(plantName))
        {
            plantNames.Add(plantName);
        }
    }

    for (int i = 0; i < plantNames.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plantNames[i]}");
    }

    Console.WriteLine("\n0. Cancel");

    Console.WriteLine("Here are the plants with a watering log, which one would you like to delete?");

    string userSearch = Console.ReadLine() ?? "";

    Console.WriteLine("\n==================================\n");

    int searchedPlant;
    bool isValidSearchName = int.TryParse(userSearch, out searchedPlant);
    while (!isValidSearchName || searchedPlant > plantNames.Count || searchedPlant < 0)
    {
        Console.WriteLine("\nInvalid input. \nPlease enter a valid number that matches the plant you want to show logs for");
        userSearch = Console.ReadLine() ?? "";
        isValidSearchName = int.TryParse(userSearch, out searchedPlant);
        
        for (int i = 0; i < plantNames.Count; i++)
        {
        Console.WriteLine("\n");
        Console.WriteLine($"{i + 1}. {plantNames[i]}");
        }
        Console.WriteLine("\n0. Cancel");
    }

    if (searchedPlant == 0)
    {
        return;
    }
    
    string searchedPlantName = plantNames[searchedPlant-1];

    List<int> matchingIndexes = new List<int>();

    for (int i = 0; i < logEntries.Count; i++)
        {
            string entry = logEntries[i];

            string[] entryParts = entry.Split(';');
            string plantName = entryParts[0];

            if (plantName.ToLower() == searchedPlantName.ToLower())
            {
                matchingIndexes.Add(i);
                Console.WriteLine($"{matchingIndexes.Count}. ");
                DisplayLog(entry);
            }
            
        }
    Console.WriteLine("\n0. Cancel");
    Console.WriteLine("\nWhich log would you like to delete?\n");

    string userInputLogToDelete = Console.ReadLine() ?? "";

    int logToDelete;

    bool isValidSearchName2 = int.TryParse(userInputLogToDelete, out logToDelete);

    if (logToDelete == 0)
    {
        return;
    }

    while (!isValidSearchName2 || logToDelete > matchingIndexes.Count || logToDelete < 0)
    {
        Console.WriteLine("\nInvalid input. \nPlease enter a valid number that matches the plant you want to show logs for");
        userInputLogToDelete = Console.ReadLine() ?? "";
        isValidSearchName2 = int.TryParse(userInputLogToDelete, out logToDelete);

        for (int i = 0; i < matchingIndexes.Count; i++)
        {
        Console.WriteLine($"{i + 1}. ");
        DisplayLog(logEntries[matchingIndexes[i]]);
        Console.WriteLine("\n0. To cancel");
        }
    }

    int realIndex = matchingIndexes[logToDelete - 1];
    logEntries.RemoveAt(realIndex);
    File.WriteAllLines(filePath, logEntries);
    Console.WriteLine("\nLog deleted successfully.\n");


    PressEnterToContinue();
}