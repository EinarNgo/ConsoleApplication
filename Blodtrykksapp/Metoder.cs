using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Blodtrykksapp
{
    public class Metoder
    {
        static List<Blodtrykk> logg = new List<Blodtrykk>();

        //Metode for å registrere blodtrykk
        public static void registrerBlodtrykk()
        {
            bool check = true;
            int overTrykk = 0;
            int underTrykk = 0;

            Console.WriteLine("Registrer blodtrykk");
            Console.WriteLine("Overtrykk: 80 - 300");
            //Do-while løkke for å registrere over/undertrykk
            do
            {
                string choice = Console.ReadLine();
                if (HjelpeMetoder.checkInt(choice) == false)
                {
                    check = false;
                }
                else if (Convert.ToInt32(choice) < 80 || Convert.ToInt32(choice) > 300)
                {
                    check = false;
                    Console.WriteLine("Ugyldige verdier, prøv igjen:");
                }
                else
                {
                    overTrykk = Convert.ToInt32(choice);
                    check = true;
                }
            } while (check == false);

            Console.WriteLine("Undertrykk: 50 - 200");
            do
            {
                string choice = Console.ReadLine();
                if (HjelpeMetoder.checkInt(choice) == false)
                {
                    check = false;
                }
                else if (Convert.ToInt32(choice) < 50 || Convert.ToInt32(choice) > 200)
                {
                    check = false;
                    Console.WriteLine("Ugyldige verdier, prøv igjen:");
                }
                else
                {
                    underTrykk = Convert.ToInt32(choice);
                    check = true;
                }
            } while (check == false);

            //Legger registrerte verdiene til listen av Blodtrykk verdier
            logg.Add(new Blodtrykk(logg.Count + 1, DateTime.Now, overTrykk, underTrykk));
            WriteToJsonFile();
            Console.Clear();
            Console.WriteLine("\nTidligere målinger:");
            HjelpeMetoder.writeList(logg);

            if (overTrykk > 140 || underTrykk > 90)
            {
                Console.WriteLine("\nDe registrerte verdiende indikerer høyt blodtrykk. Normalt blodtrykk: Overtrykk 100-140 og undertrykk 60-90");
            }

            //Spør brukeren om neste steg med do-while løkke
            do
            {
                Console.WriteLine("\nRegistrer ny blodtrykk eller tilbake til meny?");
                Console.WriteLine("\t1 - Registrer ny");
                Console.WriteLine("\t2 - Meny");
                Console.Write("Ditt valg? \n");
                string choice = Console.ReadLine();
                if (HjelpeMetoder.checkInt(choice) == false)
                {
                    check = false;
                }
                else if (Convert.ToInt32(choice) == 1)
                {
                    Console.Clear();
                    registrerBlodtrykk();
                }
                else if (Convert.ToInt32(choice) == 2)
                {
                    HjelpeMetoder.exitMelding("Redirektet tilbake til meny.");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Ugyldig valg, prøv igjen.");
                    check = false;
                }
            } while (check == false);
        }

        //Metode for Sletting av blodtrykk
        public static void slettBlodtrykk()
        {
            Console.Clear();
            Console.WriteLine("Slett blodtrykk");
            //Forlater slett interface hvis listen er tom
            if (logg.Count == 0)
            {
                HjelpeMetoder.exitMelding("Ingen målinger, redirektet tilbake til meny.");
            }
            else
            {
                HjelpeMetoder.writeList(logg);
                Console.WriteLine("\nHvilken måling vil du slette?");
                Console.WriteLine("\tSkriv in ID");
                Console.WriteLine("\tSkriv in Exit for å gå tilbake til meny");
                Console.Write("Ditt valg? \n");
                bool check = true;
                do
                {
                    string choice = Console.ReadLine();
                    if (choice.Equals("Exit") || choice.Equals("exit"))
                    {
                        HjelpeMetoder.exitMelding("Redirektet tilbake til meny.");
                    }
                    else if (HjelpeMetoder.checkInt(choice) == false)
                    {
                        check = false;
                    }
                    else if (Convert.ToInt32(choice) <= logg.Count && Convert.ToInt32(choice) > 0)
                    {
                        Console.WriteLine("Sletter: " + logg.ElementAt(Convert.ToInt32(choice) - 1));
                        //Sletter målingen på det valgte IDen
                        logg.RemoveAt(Convert.ToInt32(choice) - 1);
                        //Itererer gjennom listen og oppdaterer verdiene til ID så det blir continuelig liste
                        Thread.SpinWait(50000000);
                        for (int i = 0; i < logg.Count; i++)
                        {
                            logg[i].id = i + 1;
                        }
                        WriteToJsonFile();
                        slettBlodtrykk();
                    }
                    else if (HjelpeMetoder.checkInt(choice) == true || Convert.ToInt32(choice) == 0)
                    {
                        Console.WriteLine("Ingen Måling lik: " + choice + ", prøv igjen");
                        check = false;
                    }
                } while (check == false);
            }
        }

        //Metode for å se logg av blodtrykk
        public static void loggBlodtrykk()
        {
            Console.WriteLine("Tidligere målinger");
            if (logg.Count == 0)
            {
                //Redirekter tilbake til hovedmenyen hvis listen er tom
                HjelpeMetoder.exitMelding("Ingen målinger, redirektet tilbake til meny.");
            }
            else
            {
                //Skriver ut liste, sammendrag av verdiene, gir advarsel hvis målingene gir utslag
                HjelpeMetoder.writeList(logg);
                HjelpeMetoder.sammendrag(logg);
                HjelpeMetoder.advarsel(logg);
                HjelpeMetoder.exit();
            }
        }

        //Serializerer listen til Json og skriver den til file.json
        public static void WriteToJsonFile()
        {
            try
            {
                string file = AppDomain.CurrentDomain.BaseDirectory + @"\file.json";
                File.WriteAllText(file, JsonConvert.SerializeObject(logg));
            }
            finally{}
        }

        //Deserializerer listen fra Json og skriver den til listen Logg som tilhører Blodtrykk
        public static void readFromJsonFile()
        {
            try
            {
                string file = AppDomain.CurrentDomain.BaseDirectory + @"\file.json";
                string json = File.ReadAllText(file);
                logg = JsonConvert.DeserializeObject<List<Blodtrykk>>(json);
            }
            finally{}
        }


    }
}
