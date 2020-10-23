using System;
using System.Collections.Generic;
using System.Threading;

namespace Blodtrykksapp
{
    public class HjelpeMetoder
    {
        //Metode for å sjekke om verdien inn er int
        public static bool checkInt(string i)
        {
            try
            {
                Convert.ToInt32(i);
            }
            catch (Exception e)
            {
                Console.WriteLine("Innleste verdier er ugyldig, prøv igjen.");
                return false;
            }
            return true;
        }

        //Metode for å sjekke om noen av blodtrykkene gir utslag, og gir feilmelding
        public static void advarsel(List<Blodtrykk> logg)
        {
            String advarsel = "\nAdvarsler:\n";
            for (int i = 0; i < logg.Count; i++)
            {
                if (logg[i].sysBlodtrykk > 140 || logg[i].diaBlodtrykk > 90)
                {
                    advarsel += "For høyt blodtrykk den " + logg[i].time + " Med: " + logg[i].sysBlodtrykk + "/" + logg[i].diaBlodtrykk + "\n";
                }
            }
            Console.WriteLine(advarsel);
        }

        //Metode for å gi sammendrag av målingene
        public static void sammendrag(List<Blodtrykk> logg)
        {
            String sammendrag = "\nSammendrag:\n";
            int gjennomsnittSys = 0;
            int gjennomsnittDia = 0;
            for (int i = 0; i < logg.Count; i++)
            {
                gjennomsnittSys += logg[i].sysBlodtrykk;
                gjennomsnittDia += logg[i].diaBlodtrykk;
            }
            sammendrag += "Gjennomsnitt blodtrykk: " + gjennomsnittSys / logg.Count + "/" + gjennomsnittDia / logg.Count;
            Console.WriteLine(sammendrag);
        }

        //Metode for å håndtere redirekt til Meny med sleep
        public static void exitMelding(String melding)
        {
            Console.WriteLine(melding);
            Thread.SpinWait(50000000);
            Program.menu();
        }

        //Metode for å håndtere om valg av forlate interface og gå tilbake til meny
        public static void exit()
        {
            bool check = true;
            Console.WriteLine("Tast Exit for å gå tilbake til meny?");
            do
            {
                string choice = Console.ReadLine();
                if (choice.Equals("Exit") || choice.Equals("exit"))
                {
                    exitMelding("Redirektet tilbake til meny.");
                }
                else
                {
                    check = false;
                    Console.WriteLine("Ugyldig valg, prøv igjen.");

                }
            } while (check == false);
        }

        //Metode for å skrive ut alt i Blodtrykk listen der den henter toString metoden
        public static void writeList(List<Blodtrykk> logg)
        {
            foreach (Blodtrykk aLogg in logg)
            {
                Console.WriteLine(aLogg);
            }
        }
    }
}
