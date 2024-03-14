using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;
//using System.Windows.Forms; Planned to use for copying password to clipboard

class PasswordGenerator
{
    private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
    private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string NumberChars = "0123456789";
    private const string SymbolChars = "!@#$%^&*";

    public static string GeneratePassword(int length, bool includeSymbols, bool includeNumbers, bool includeLowercase, bool includeUppercase)
    {
        string validChars = "";

        if (includeSymbols)
            validChars += SymbolChars;
        if (includeNumbers)
            validChars += NumberChars;
        if (includeLowercase)
            validChars += LowercaseChars;
        if (includeUppercase)
            validChars += UppercaseChars;

        if (validChars.Length == 0)
        {
            throw new ArgumentException("At least one character type must be selected.");
        }

        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] randomBytes = new byte[length];
            rng.GetBytes(randomBytes);

            char[] password = new char[length];
            for (int i = 0; i < length; i++)
            {
                password[i] = validChars[randomBytes[i] % validChars.Length];
            }

            return new string(password);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {


        Console.Title = "Password Generator | whoswhip";
        Console.WriteLine("Password Generator");
        Console.WriteLine("-------------------------");
        Console.WriteLine("[1] Normal");
        Console.WriteLine("[2] Randomized Length");
        Console.WriteLine("[3] Exit");
        Console.Write("Option: ");
        string? choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Normal.Run(args);
                break;
            case "2":
                Randomlength.Run(args);
                break;
            case "3":
                Console.WriteLine("Exiting...");
                Thread.Sleep(150);
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }
    public static void Restart(string[] args)
    {
        Main(args);
    }
}
class Savetofile
{
    public static void Run(string password)
    {
        string directory = Environment.CurrentDirectory;
        string fileName = "Generated_Password.txt";
        string filePath = Path.Combine(directory, fileName);
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.Write(password);
        }
        Console.WriteLine($"Saved to {filePath}");
        File.Open(filePath, FileMode.Open);
    }
}
class Normal
{
    public static void Run(string[] args)
    {
        bool generateAnother = true;

        while (generateAnother)
        {
            Console.Clear();
            Console.Title = "Password Generator | whoswhip";
            Console.WriteLine("Password Generator");
            Console.WriteLine("-------------------------");
            Console.Write("Print out password? (y/n): ");
            bool quick = Console.ReadLine().ToLower() == "y";
            Console.WriteLine("-------------------------");
            Console.Write("Enter the length of the password: ");
            int length = int.Parse(Console.ReadLine());

            Console.Write("Include symbols? (y/n): ");
            bool includeSymbols = Console.ReadLine().ToLower() == "y";

            Console.Write("Include numbers? (y/n): ");
            bool includeNumbers = Console.ReadLine().ToLower() == "y";

            Console.Write("Include lowercase characters? (y/n): ");
            bool includeLowercase = Console.ReadLine().ToLower() == "y";

            Console.Write("Include uppercase characters? (y/n): ");
            bool includeUppercase = Console.ReadLine().ToLower() == "y";

            string password = PasswordGenerator.GeneratePassword(length, includeSymbols, includeNumbers, includeLowercase, includeUppercase);
            if (quick)
            {
                Console.WriteLine($"Generated Password: {password}");
            }

            Console.Write("Save to file? (y/n): ");
            bool save = Console.ReadLine().ToLower() == "y";
            if (save)
            {
                Savetofile.Run(password);
            }

            Console.Write("Generate another password? (y/n): ");
            generateAnother = Console.ReadLine().ToLower() == "y";
            if (!generateAnother)
            {
                Console.Clear();
                Program.Restart(args);
            }
        }
    }
}
class Randomlength
{
    public static void Run(string[] args)
    {
        bool generateAnother2 = true;



        while (generateAnother2)
        {
            Console.Clear();
            Console.Title = "Password Generator | whoswhip";
            Console.WriteLine("Password Generator");
            Console.WriteLine("-------------------------");
            Random rand = new Random();
            int length = rand.Next(10, 10000);
            Console.WriteLine($"Length = {length}");
            Console.WriteLine("-------------------------");
            Console.Write("Print out password? (y/n): ");
            bool quick = Console.ReadLine().ToLower() == "y";
            Console.WriteLine("-------------------------");
            Console.Write("Include symbols? (y/n): ");
            bool includeSymbols = Console.ReadLine().ToLower() == "y";

            Console.Write("Include numbers? (y/n): ");
            bool includeNumbers = Console.ReadLine().ToLower() == "y";

            Console.Write("Include lowercase characters? (y/n): ");
            bool includeLowercase = Console.ReadLine().ToLower() == "y";

            Console.Write("Include uppercase characters? (y/n): ");
            bool includeUppercase = Console.ReadLine().ToLower() == "y";
            
            string password = PasswordGenerator.GeneratePassword(length, includeSymbols, includeNumbers, includeLowercase, includeUppercase);
            if (quick)
            {
                Console.WriteLine($"Generated Password: {password}");
            }

            Console.Write("Save to file? (y/n): ");
            bool save = Console.ReadLine().ToLower() == "y";
            if (save)
            {
                Savetofile.Run(password);
            }
            Console.Write("Generate another password? (y/n): ");
            generateAnother2 = Console.ReadLine().ToLower() == "y";
            if (!generateAnother2)
            {
                Console.Clear();
                Program.Restart(args);
            }
        }
    }
}
