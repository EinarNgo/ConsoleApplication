using System;
using System.IO;

namespace Blodtrykksapp
{
    public class Program
    {
        public static void menu()
        {
            Console.Clear();
            Metoder.WriteToJsonFile();
            Console.WriteLine("Velkommen til Blodtrykk! \nHva vil du gjøre?");
            Console.WriteLine("\t1 - Registrer blodtrykk");
            Console.WriteLine("\t2 - Logg av blodtrykk");
            Console.WriteLine("\t3 - Slett blodtrykk");
            Console.WriteLine("\t4 - Lukk konsoll app");
            Console.Write("Ditt valg? \n");
            bool check = true;
            do
            {
                string choice = Console.ReadLine();
                if (HjelpeMetoder.checkInt(choice) == false)
                {
                    check = false;
                }
                else if (Convert.ToInt32(choice) == 1)
                {
                    Console.Clear();
                    Metoder.registrerBlodtrykk();
                }
                else if (Convert.ToInt32(choice) == 2)
                {
                    Console.Clear();
                    Metoder.loggBlodtrykk();
                }
                else if (Convert.ToInt32(choice) == 3)
                {
                    Console.Clear();
                    Metoder.slettBlodtrykk();
                }
                else if (Convert.ToInt32(choice) == 4)
                {
                    Console.Clear();
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Ugyldig tall, prøv igjen");
                    check = false;
                }
            } while (check == false);
        }

        public static void Main(string[] args)
        {
            string dirParameter = AppDomain.CurrentDomain.BaseDirectory + @"\file.json";
            if (File.Exists(dirParameter) == true)
            {
                Metoder.readFromJsonFile();
            }
            menu();
        }
    }
}
