

using System;

    public class WateringLogEntry
    {
        // Properties
        private string plantName;
        public string PlantName
        {
        get { return plantName; }
        set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                   throw new ArgumentException("Plant name cannot be empty.");
                } 
                else
                {
                    plantName = value; 
                }
            }
        }


        private double waterAmount;
        public double WaterAmount
        {
        get { return waterAmount; }
        set 
            { 
                if (value < 0)
                {
                   waterAmount = 0;
                } 
                else
                {
                    waterAmount = value; 
                }
            }
        }


        private DateTime wateringDate;
        public DateTime WateringDate
        {
        get { return wateringDate; }
        set 
            { 
                if (DateTime.Now < value)
                {
                   wateringDate = DateTime.Now;
                } 
                else
                {
                    wateringDate = value; 
                }
            }
        }

        private bool fertilizerUsed;
        public bool FertilizerUsed
        {
        get { return fertilizerUsed; }
        set { fertilizerUsed = value; }
        }
    

        // Constructor
        public WateringLogEntry(string plantName, DateTime wateringDate, double waterAmount, bool fertilizerUsed)
        {
            PlantName = plantName;
            WateringDate = wateringDate;
            WaterAmount = waterAmount;
            FertilizerUsed = fertilizerUsed;
        }

        // Method to convert the log entry to a string for saving
        public override string ToString()
        {
            return $"{PlantName};{WateringDate.ToString("yyyy-MM-dd HH:mm:ss")};{WaterAmount};{FertilizerUsed}";
        }

        // Method to parse a string back into a WateringLogEntry object
        public static WateringLogEntry Parse (string line)
        {
            string[] logParts = line.Split(';');
            WateringLogEntry entry = new WateringLogEntry(
                logParts[0], 
                DateTime.Parse(logParts[1]),
                ParseWaterAmount(logParts[2]),  
                bool.Parse(logParts[3])
            );
            return entry;
        }

        // Method to parse a string into a double for water amount
        public static double ParseWaterAmount(string input)
        {
            string normalized = input.Replace(",", ".");
            return double.Parse(normalized, System.Globalization.CultureInfo.InvariantCulture);
        }
    }

