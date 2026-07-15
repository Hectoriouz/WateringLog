using System;

public class WateringLogService
{
    public List<WateringLogEntry> GetEntriesByPlant(List<WateringLogEntry> logEntries, string searchedPlantName)
    {
        List<WateringLogEntry> matchingEntries = new List<WateringLogEntry>();

        

        foreach (WateringLogEntry entry in logEntries)
        {
            if (searchedPlantName.Trim().ToLower() == entry.PlantName.Trim().ToLower())
            {
                matchingEntries.Add(entry);
            }
        }

        return matchingEntries;
    }

    // Get unique plant names from log entries
    public List<string> GetPlantNames(List<WateringLogEntry> logEntries)
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

    public void DeleteEntry(List<WateringLogEntry> logEntries, WateringLogEntry entry)
    {
        logEntries.Remove(entry);
    }

}

