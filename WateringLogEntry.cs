

namespace WateringLog
{
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


        private int waterAmount;
        public int WaterAmount
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
        public WateringLogEntry(string plantName, int waterAmount, DateTime wateringDate, bool fertilizerUsed)
        {
            PlantName = plantName;
            WaterAmount = waterAmount;
            WateringDate = wateringDate;
            FertilizerUsed = fertilizerUsed;
        }

        // Method to convert the log entry to a string for saving
        public override string ToString()
        {
            return $"{PlantName};{WateringDate.ToString("yyyy-MM-dd HH:mm:ss")};{WaterAmount};{FertilizerUsed}";
        }
    }
}
