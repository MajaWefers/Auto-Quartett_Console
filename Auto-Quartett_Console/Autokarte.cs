using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Quartett_Console
{

    public struct Autokarte
    {
        public string modell;
        public int geschwindigkeit;
        public int leistung;
        public double verbrauch;
        public int zylinder;
        public double hubraum;
        public double beschleunigung;
        public int zuladung;
        public int ladevolumen;  // { get; set; }

        public Autokarte(string name, int speed, int leist, double verbr,
                          int zyl, double hubr, double beschl, int zulad, int ladevol)
        {
            modell = name;
            geschwindigkeit = speed;
            leistung = leist;
            verbrauch = verbr;
            zylinder = zyl;
            hubraum = hubr;
            beschleunigung = beschl;
            zuladung = zulad;
            ladevolumen = ladevol;
        }
    }
}
