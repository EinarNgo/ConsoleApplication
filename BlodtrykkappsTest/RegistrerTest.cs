using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blodtrykksapp;
using System;
using System.Collections.Generic;

namespace BlodtrykkappsTest
{
    [TestClass]
    public class RegistrerTest
    {
        //Sjekker om innleste verdien KUN godkjenner tall og ikke bokstaver
        [TestMethod]
        public void checkInnlesteVerdierErInt()
        {
            bool test = HjelpeMetoder.checkInt("1");
            //bool test = HjelpeMetoder.checkInt("-1");
            //bool test = HjelpeMetoder.checkInt("23");
            //bool test = HjelpeMetoder.checkInt("£@£@");
            Assert.AreEqual(test, true);
        }

        //Sjekker om input blir godkjent
        [TestMethod]
        public void checkInnlesteVerdierForTrykk()
        {
            bool check = true;
            int overTrykk = 0;

            //Do-while løkke for å registrere over/undertrykk
            do
            {
                string choice = "100";
                //string choice = "1d00";
                //string choice = "-1212";
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
            Assert.AreEqual(overTrykk, 100);
        }

        //Tester om verdien blir fjernet og listen oppdateres
        [TestMethod]
        public void slettVerdiFraListe()
        {
            List<Blodtrykk> logg = new List<Blodtrykk>();
            logg.Add(new Blodtrykk(1, DateTime.Today, 100, 100));
            logg.Add(new Blodtrykk(2, DateTime.Today, 101, 101));
            logg.Add(new Blodtrykk(3, DateTime.Today, 102, 102));
            logg.Add(new Blodtrykk(4, DateTime.Today, 103, 103));
            logg.Add(new Blodtrykk(5, DateTime.Today, 104, 104));

            List<Blodtrykk> logg1 = new List<Blodtrykk>();
            logg1.Add(new Blodtrykk(1, DateTime.Today, 101, 101));
            logg1.Add(new Blodtrykk(2, DateTime.Today, 102, 102));
            logg1.Add(new Blodtrykk(3, DateTime.Today, 103, 103));
            logg1.Add(new Blodtrykk(4, DateTime.Today, 104, 104));

            /*
            List<Blodtrykk> logg = new List<Blodtrykk>();
            logg.Add(new Blodtrykk(1, DateTime.Today, 100, 100));
            logg.Add(new Blodtrykk(32, DateTime.Today, 1201, 101));
            logg.Add(new Blodtrykk(3, DateTime.Today, 102, 1202));
            logg.Add(new Blodtrykk(34, DateTime.Today, 103, 103));
            logg.Add(new Blodtrykk(5, DateTime.Today, 104, 1024));
             */


            string choice = "1";
            if (Convert.ToInt32(choice) <= logg.Count && Convert.ToInt32(choice) > 0)
            {
                //Sletter målingen på det valgte IDen
                logg.RemoveAt(Convert.ToInt32(choice) - 1);
                //Itererer gjennom listen og oppdaterer verdiene til ID så det blir continuelig liste
                for (int i = 0; i < 4; i++)
                {
                    logg[i].id = i + 1;
                }
            }
            else if (HjelpeMetoder.checkInt(choice) == true || Convert.ToInt32(choice) == 0)
            {
                Console.WriteLine("Ingen Måling lik: " + choice + ", prøv igjen");
            }
            Assert.AreEqual(logg[0].id, logg1[0].id);
        }

        //Tester om sammendrag blir beregnet riktig
        [TestMethod]
        public void beregneSammendrag()
        {
            List<Blodtrykk> logg = new List<Blodtrykk>();
            logg.Add(new Blodtrykk(1, DateTime.Today, 100, 100));
            logg.Add(new Blodtrykk(2, DateTime.Today, 100, 100));
            logg.Add(new Blodtrykk(3, DateTime.Today, 100, 100));
            logg.Add(new Blodtrykk(4, DateTime.Today, 100, 100));
            logg.Add(new Blodtrykk(5, DateTime.Today, 100, 100));
            //logg.Add(new Blodtrykk(5, DateTime.Today, 100, 1002));
            int gjennomsnittSys = 0;
            int gjennomsnittDia = 0;
            for (int i = 0; i < logg.Count; i++)
            {
                gjennomsnittSys += logg[i].sysBlodtrykk;
                gjennomsnittDia += logg[i].diaBlodtrykk;
            }

            gjennomsnittSys = gjennomsnittSys / logg.Count;
            gjennomsnittDia = gjennomsnittDia / logg.Count;

            Assert.AreEqual(gjennomsnittDia+gjennomsnittSys, 200);
        }

        //Sjekker om verdiene blir lagt til riktig og listen utvider seg
        [TestMethod]
        public void checkRegistrerNyBlodtrykk()
        {
            List<Blodtrykk> logg = new List<Blodtrykk>();
            int overTrykk = 100;
            int underTrykk = 100;
            /*
            string overTrykk = "100";
            int underTrykk = 100;

            string overTrykk = "ewf";
            int underTrykk = 100;
             */

            logg.Add(new Blodtrykk(logg.Count + 1, DateTime.Now, overTrykk, underTrykk));


            Assert.AreEqual(1, logg.Count);
        }
    }
}
