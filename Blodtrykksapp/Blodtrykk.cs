using System;

namespace Blodtrykksapp
{
    //Klasse for blodtrykk, så det er mulig å lage Listr<T> med et objekt
    //Enkel klasse med konstruktør for ID, Time, SysBlodtrykk og DiaBlodtrykk
    //Har også en toString som overrider toString metoden
    public class Blodtrykk
    {
        public int id { get; set; }
        public DateTime time { get; set; }
        public int sysBlodtrykk { get; set; }
        public int diaBlodtrykk { get; set; }

        public Blodtrykk(int idNr, DateTime timeToday, int sysBlodtrykkNr, int diaBlodtrykkNr)
        {
            id = idNr;
            time = timeToday;
            sysBlodtrykk = sysBlodtrykkNr;
            diaBlodtrykk = diaBlodtrykkNr;
        }

        public override string ToString()
        {
            return "ID: " + id + "   Dato: " + time + "   Overtrykk: " + sysBlodtrykk + "   Undertrykk: " + diaBlodtrykk;
        }
    }
}
