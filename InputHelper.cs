using System;


// InputHelper class to handle user input
// This class provides methods to get various types of input from the user, including strings, doubles, dates, integers, and booleans.

public class InputHelper
{
    public static string GetString(string prompt)
    {
        Console.WriteLine(prompt);
        return Console.ReadLine() ?? "";
    }

    public static double GetDouble(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine() ?? "";
            

            string normalized = input.Trim().Replace(",", ".");
            if (double.TryParse(
                normalized,
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture,
                out double result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
    }

    public static DateTime GetDate(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine() ?? "";
            if (DateTime.TryParse(input, out DateTime result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid date (e.g., 2023-03-15).");
            }
        }
    }

    public static int GetInt(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine() ?? "";
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }
    }


    public static bool GetBool(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt + " (yes/no):");
            string input = Console.ReadLine() ?? "";
            if (input.ToLower() == "yes")
            {
                return true;
            }
            else if (input.ToLower() == "no")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
            }
        }
    }

    // Methods to get input with the option to cancel by typing "0" or "cancel"
    public static string? GetStringWithCancel(string prompt)
    {
        Console.WriteLine(prompt + "\nType \"0\" to cancel");
        string input = Console.ReadLine() ?? "";

        if (input == "0")
        {
            return null;
        }

        return input;
    }


    public static double? GetDoubleWithCancel(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt + "\nType \"cancel\" to cancel");
            string input = Console.ReadLine() ?? "";

            if (input.ToLower() == "cancel")
            {
                return null;
            }

            string normalized = input.Trim().Replace(",", ".");

            if (double.TryParse(
                normalized,
                System.Globalization.NumberStyles.Float,
                System.Globalization.CultureInfo.InvariantCulture,
                out double result))
            {
                return result;
            }

            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }


    public static DateTime? GetDateWithCancel(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt + "\nType \"0\" to cancel");
            string input = Console.ReadLine() ?? "";

            if (input == "0")
            {
                return null;
            }

            if (DateTime.TryParse(input, out DateTime result))
            {
                return result;
            }

            Console.WriteLine("Invalid input. Please enter a valid date.");
        }
    }


    public static int? GetIntWithCancel(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt + "\nType \"cancel\" to cancel");
            string input = Console.ReadLine() ?? "";

            if (input.ToLower() == "cancel")
            {
                return null;
            }

            if (int.TryParse(input, out int result))
            {
                return result;
            }

            Console.WriteLine("Invalid input. Please enter a valid number.");
        }
    }


    public static bool? GetBoolWithCancel(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt + " (yes/no)");
            Console.WriteLine("Type \"0\" to cancel");

            string input = Console.ReadLine() ?? "";

            if (input == "0")
            {
                return null;
            }

            if (input.ToLower() == "yes")
            {
                return true;
            }

            if (input.ToLower() == "no")
            {
                return false;
            }

            Console.WriteLine("Invalid input. Please enter yes or no.");
        }
    }
}