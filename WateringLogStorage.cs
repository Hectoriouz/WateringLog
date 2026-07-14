using System;

class WateringLogStorage
{
    string filePath = "wateringlog.txt";

    // Get list of watering log entries
    public List<WateringLogEntry> LoadLogEntries()
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
            List<WateringLogEntry> logEntries = new List<WateringLogEntry>();
            return logEntries;
        }
    }

    // Save list of watering log entries
    public void SaveLogEntries(List<WateringLogEntry> logEntries)
    {
        List<string> newList = new List<string>();

        foreach (WateringLogEntry entry in logEntries)
        {
            string listItem = entry.ToString();
            newList.Add(listItem);
        }

        File.WriteAllLines(filePath, newList);
    }
}
