
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Runtime.CompilerServices;

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

// Menu
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

// Log new watering
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


    List<WateringLogEntry> logEntries = LoadLogEntries();

    WateringLogEntry entry = new WateringLogEntry(
        plantName, 
        DateTime.Parse(dateInput),
        int.Parse(waterAmountInput),  
        fertilizerInput.ToLower() == "yes"
    );

    logEntries.Add(entry);

    SaveLogEntries(logEntries);

    Console.WriteLine("Log saved to file!");
    PressEnterToContinue();
    
}    

// View watering logs
void ViewWateringLogs()
{
    // View watering logs
    IntroTitle();
    Console.WriteLine("\n");

        List<WateringLogEntry> logEntries = LoadLogEntries();

        foreach (WateringLogEntry entry in logEntries)
        {
            DisplayLog(entry);
        }
        PressEnterToContinue();
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

// View latest watering log
void ViewLatestWateringLog()
{
    IntroTitle();

    List<WateringLogEntry> logEntries = LoadLogEntries();

    if (logEntries.Count > 0)
    {
        int lastIndex = logEntries.Count - 1;
        WateringLogEntry latestLog = logEntries[lastIndex];

        DisplayLog(latestLog);
        PressEnterToContinue();
    }
    else
    {
        Console.WriteLine("No watering logs found.");
        PressEnterToContinue();
    }

}

// Display log entry
void DisplayLog(WateringLogEntry entry)
{
    Console.WriteLine("----------------------------------");
    Console.WriteLine($"Plant Name: {entry.PlantName}");
    Console.WriteLine($"Date Watered: {entry.WateringDate}");
    Console.WriteLine($"Amount of Water Used: {entry.WaterAmount} liters");
    Console.WriteLine($"Fertilizer Used: {entry.FertilizerUsed}");
    Console.WriteLine("----------------------------------");
}

// Get list of watering log entries
List<WateringLogEntry> LoadLogEntries()
{
    if (File.Exists(filePath))
    {
        List<WateringLogEntry> logEntries = new List<WateringLogEntry>();

        foreach (string line in File.ReadAllLines(filePath))
        {   
            WateringLogEntry lineObject = WateringLogEntry.Parse(line);
            logEntries.Add(lineObject);
        }
        return logEntries;
    }
    else
    {
        Console.WriteLine("No watering logs found.");
        PressEnterToContinue();
        List<WateringLogEntry> logEntries = new List<WateringLogEntry>();
        return logEntries;
    }
}

// Save list of watering log entries
void SaveLogEntries(List<WateringLogEntry> logEntries)
{
    List<string> newList = new List<string>();

    foreach (WateringLogEntry entry in logEntries)
    {
        string listItem = entry.ToString();
        newList.Add(listItem);
    }

    File.WriteAllLines(filePath, newList);
}


// Search for specific plant logs
void SearchPlant()
{
    IntroTitle();
    bool foundPlant = false;
    List<WateringLogEntry> logEntries = LoadLogEntries();

    while (!foundPlant)
    {
        Console.WriteLine("Enter the name of the plant: ");
        Console.WriteLine("\nType 0 to go back");
        string userSearch = Console.ReadLine() ?? "";
        if (userSearch == "0")
        {
            return;
        }

        foreach (WateringLogEntry entry in logEntries)
        {
            if (entry.PlantName.ToLower() == userSearch.ToLower())
            {
                DisplayLog(entry);
                foundPlant = true;
            }
        }

        if (!foundPlant)
        {
            Console.WriteLine("No logs found for that plant.");
        }

    }   
    PressEnterToContinue();
}

// Delete watering log
void DeleteWateringLog()
{
    IntroTitle();

    List<WateringLogEntry> logEntries = LoadLogEntries();

    List<string> plantNames = new List<string>();

    foreach (WateringLogEntry entry in logEntries)
    {
        string plantName = entry.PlantName;

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
            string plantName = logEntries[i].PlantName;

            if (plantName.ToLower() == searchedPlantName.ToLower())
            {
                matchingIndexes.Add(i);
                Console.WriteLine($"{matchingIndexes.Count}. ");
                DisplayLog(logEntries[i]);
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

    SaveLogEntries(logEntries);

    Console.WriteLine("\nLog deleted successfully.\n");


    PressEnterToContinue();
}