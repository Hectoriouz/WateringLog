
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Runtime.CompilerServices;

string filePath = "wateringlog.txt";

bool isRunning = true;
WateringLogStorage storage = new WateringLogStorage();

// Main program loop
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

    string choice = InputHelper.GetString("");

    return choice;
}

// Log new watering
void LogWatering()
{
// Log new watering
    IntroTitle();
    Console.WriteLine("Welcome!");
    Console.WriteLine("\n");

    string? plantName = InputHelper.GetStringWithCancel("Please enter the name of the plant you would like to log: ");
        
    if (plantName == null)
        {
            return;
        }

    DateTime? dateInput = InputHelper.GetDateWithCancel("Please enter the date you watered the plant (DD/MM/YYYY): ".ToString() ?? "");

    if (dateInput == null)
        {
            return;
        }

    double? waterAmountInput = InputHelper.GetDoubleWithCancel("Please enter the amount of water used in liters (e.g., 0.5): ");

    if (waterAmountInput == null)
        {
            return;
        }
      
    bool? fertilizerInput = InputHelper.GetBoolWithCancel("Did you use fertilizer? (yes/no): ");

    if (fertilizerInput == null)
        {
            return;
        }

    Console.WriteLine("Perfect! Here is the information you entered: \n");
    Console.WriteLine("==================================");
    Console.WriteLine($"Plant Name: {plantName}");
    Console.WriteLine($"Date Watered: {dateInput.Value.ToString("yyyy-MM-dd HH:mm:ss")}");
    Console.WriteLine($"Amount of Water Used: {waterAmountInput.Value} liters");
    Console.WriteLine($"Fertilizer Used: {fertilizerInput.Value}");
    Console.WriteLine("==================================");

    List<WateringLogEntry> logEntries = storage.LoadLogEntries();

    WateringLogEntry entry = new WateringLogEntry(
        plantName, 
        dateInput.Value,
        waterAmountInput.Value,  
        fertilizerInput.Value
    );

    logEntries.Add(entry);

    storage.SaveLogEntries(logEntries);

    Console.WriteLine("Log saved to file!");
    PressEnterToContinue();
    
}    

// View watering logs
void ViewWateringLogs()
{
    // View watering logs
    IntroTitle();
    Console.WriteLine("\n");

        List<WateringLogEntry> logEntries = storage.LoadLogEntries();

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
    InputHelper.GetString("\nPress Enter to continue...");
}

// View latest watering log
void ViewLatestWateringLog()
{
    IntroTitle();

    List<WateringLogEntry> logEntries = storage.LoadLogEntries();

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


// Search for specific plant logs
void SearchPlant()
{
    IntroTitle();
    bool foundPlant = false;
    List<WateringLogEntry> logEntries = storage.LoadLogEntries();

    while (!foundPlant)
    {
        string? userSearch = InputHelper.GetStringWithCancel("Enter the name of the plant: ");

        if (userSearch == null)
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

    List<WateringLogEntry> logEntries = storage.LoadLogEntries();

    List<string> plantNames = GetPlantNames(logEntries);

    for (int i = 0; i < plantNames.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plantNames[i]}");
    }

    Console.WriteLine("\n==================================\n");

    int? searchedPlant = InputHelper.GetIntWithCancel(
        "\n0. Cancel\nEnter the number of the plant you want to delete logs for:"
    );

    if (searchedPlant == null)
    {
        return;
    }

    while (searchedPlant > plantNames.Count)
    {
        Console.WriteLine("Please choose a plant from the list.");
        
        searchedPlant = InputHelper.GetIntWithCancel(
            "\n0. Cancel\nEnter the number of the plant you want to delete logs for:"
        );

        if (searchedPlant == null)
        {
            return;
        }
    }
    
    string searchedPlantName = plantNames[searchedPlant.Value-1];

    List<int> matchingIndexes = GetMatchingIndexes(logEntries, searchedPlantName);

    int? userInputLogToDelete = InputHelper.GetIntWithCancel("\n0. Cancel\nEnter the number of the log you want to delete: ");

    if (userInputLogToDelete == null)
    {
        return;
    }

    while (userInputLogToDelete > matchingIndexes.Count || userInputLogToDelete < 0)
    {
        userInputLogToDelete = InputHelper.GetIntWithCancel("\n0. Cancel\nInvalid Input. Enter the number of the log you want to delete: ");

        DisplayMatchingLogs(logEntries, matchingIndexes);
    }
    

    int realIndex = matchingIndexes[userInputLogToDelete.Value - 1];
    logEntries.RemoveAt(realIndex);

    storage.SaveLogEntries(logEntries);

    Console.WriteLine("\nLog deleted successfully.\n");


    PressEnterToContinue();
}

// Get unique plant names from log entries
List<string> GetPlantNames(List<WateringLogEntry> logEntries)
{
    List<string> plantNames = new List<string>();

    foreach (WateringLogEntry entry in logEntries)
    {
        string plantName = entry.PlantName;

        if (!plantNames.Contains(plantName))
        {
            plantNames.Add(plantName);
        }
    }

    return plantNames;
}

List<int> GetMatchingIndexes(List<WateringLogEntry> logEntries, string searchedPlantName)
{
    List<int> matchingIndexes = new List<int>();

    for (int i = 0; i < logEntries.Count; i++)
    {
        string plantName = logEntries[i].PlantName;

        if (plantName.ToLower() == searchedPlantName.ToLower())
        {
            matchingIndexes.Add(i);
        }
    }

    return matchingIndexes;
}

// Display log entry with index
void DisplayLogWithIndex(WateringLogEntry entry, int index)
{
    Console.WriteLine($"Log {index + 1}:");
    DisplayLog(entry);
}

// Display matching logs
void DisplayMatchingLogs(List<WateringLogEntry> logEntries, List<int> matchingIndexes)
{
    for (int i = 0; i < matchingIndexes.Count; i++)
    {
        DisplayLogWithIndex(logEntries[matchingIndexes[i]], i);
    }

    Console.WriteLine("\n0. To cancel");
}

// Get log to delete
int? GetLogToDelete(List<int> matchingIndexes, List<WateringLogEntry> logEntries)
{
    int? userInput = InputHelper.GetIntWithCancel(
        "\nEnter the number of the log you want to delete: "
    );

    while (userInput > matchingIndexes.Count || userInput < 0)
    {
        Console.WriteLine("Invalid input.");

        DisplayMatchingLogs(logEntries, matchingIndexes);

        userInput = InputHelper.GetIntWithCancel(
            "\nEnter the number of the log you want to delete: "
        );

        if (userInput == null)
        {
            return null;
        }
    }

    return userInput;
}
