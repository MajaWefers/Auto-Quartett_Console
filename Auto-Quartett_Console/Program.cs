using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto_Quartett_Console
{
    class Program
    {
        public struct Autokarte
        {
            public string modell;
            public int    geschwindigkeit;
            public int    leistung;
            public double verbrauch;
            public int    zylinder;
            public double hubraum;
            public double beschleunigung;
            public int    zuladung;
            public int    ladevolumen;  // { get; set; }

            //TODO: public Autokarte ohne auto_x davor
            public static void Autokarte_fuellen (ref Autokarte auto_x,  string name, int speed, int leist, double verbr, 
                                           int zyl, double hubr, double beschl, int zulad, int ladevol)
            {
                auto_x.modell          = name;
                auto_x.geschwindigkeit = speed;
                auto_x.leistung        = leist;
                auto_x.verbrauch       = verbr;
                auto_x.zylinder        = zyl;
                auto_x.hubraum         = hubr;
                auto_x.beschleunigung  = beschl;
                auto_x.zuladung        = zulad;
                auto_x.ladevolumen     = ladevol;
            }
        }
        
        //Autokarten werden erstellt
        public static Autokarte auto1 = new Autokarte();
        public static Autokarte auto2 = new Autokarte();
        public static Autokarte auto3 = new Autokarte();

        //Variablen zur STEUERUNG
        public static string nochmal;               //Für Steuerung des Hauptmenüs
        public static int    ansicht_vergleich;     //Wahl, ob Ansicht oder Vergleich
        public static int    vergleichsfeld;
        public static bool   eingabe_korrekt;       //Grundlegende Eingabeprüfung

        //
        public static bool   groesser;
        public static string groesser_ges = "";     //
        
        public static int zaehler_x;
        public static int zaehler_y;

        static void Main(string[] args)
        {
            /*new Autokarte {beschleunigung=2,
            modell="sf"
            };*/
            //Den Feldern der Autokarten Werte zuweisen
            Autokarte.Autokarte_fuellen(ref auto1, "VW Phaeton", 250, 309, 15.7, 12, 6, 6.7, 600, 500);
            Autokarte.Autokarte_fuellen(ref auto2, "VW New Beetle", 185, 85, 8.7, 4, 2, 10.9, 419, 527);
            Autokarte.Autokarte_fuellen(ref auto3, "VW Touareg", 225, 230, 12.2, 10, 4.9, 7.8, 600, 555);

            //Benutzerfragen
            Menü_Steuerung();
            
        }

        static void Menü_Steuerung()
        {
            do
            {
                Console.WriteLine("Möchten Sie Karten einzeln ansehen (1) oder vergleichen (2)?");

                //Eingabeprüfung
                ansicht_vergleich = Pruefe_Eingabe();

                if (ansicht_vergleich == 1)
                {
                    Console.WriteLine();
                    Zeige_einzelne_Karte(auto1);
                    Zeige_einzelne_Karte(auto2);
                    Zeige_einzelne_Karte(auto3);
                }
                if (ansicht_vergleich == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ihre Spielkarte:");
                    Zeige_einzelne_Karte(auto1);

                    vergleichsfeld = Vergleich_Auswahl(auto1);
                    zaehler_x = 0;
                    zaehler_y = 0;

                    if (vergleichsfeld == 9)
                    {
                        Vergleiche_alles();
                    }
                    else if (vergleichsfeld == 10)
                    {
                    }
                    else
                    {
                        Vergleiche_einen_Wert();
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Möchten Sie noch einmal spielen (j/n)?");
                nochmal = Console.ReadLine();
                Console.WriteLine();

            } while (nochmal != "n");
        }

        static int Pruefe_Eingabe()
        {
            do
            {
                eingabe_korrekt = Int32.TryParse(Console.ReadLine(), out ansicht_vergleich);
                if (ansicht_vergleich != 1 && ansicht_vergleich != 2)
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte geben Sie 1 oder 2 ein.");
                    eingabe_korrekt = false;
                }
            } while (eingabe_korrekt != true);

            return ansicht_vergleich;
        }

        static void Vergleiche_alles()
        {
            Vergleich_Karten_Ausgabe(auto1,auto2,vergleichsfeld);
            //groesser_ges = Alles_vergleichen(auto1, auto2);
            Console.WriteLine();

            groesser = zaehler_x > zaehler_y;
            //Ausagbe des Vergleichsergebnisses

            if (groesser)
            {
                Console.WriteLine("Insgesamt siegt " + auto1.modell.ToUpper() +
                                    " mit " + zaehler_x + " zu " + zaehler_y +
                                    " über " + auto2.modell.ToUpper() + ".");
            }
            else
            {
                Console.WriteLine("Insgesamt siegt " + auto2.modell.ToUpper() +
                                    " mit " + zaehler_y + " zu " + zaehler_x +
                                    " über " + auto1.modell.ToUpper() + ".");
            }
            Console.WriteLine();
        }

        static void Vergleiche_einen_Wert()
        {
            groesser = Vergleich_Karten_Ausgabe(auto1, auto2, vergleichsfeld);
            Console.WriteLine();

            if (groesser)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sie haben gewonnen!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Sie haben verloren!");
                Console.ResetColor();
            }

            Console.WriteLine();
        }

        static void Zeige_einzelne_Karte(Autokarte auto_x)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(auto_x.modell.ToUpper());
            Console.ResetColor();
            Console.WriteLine("Geschwindigkeit: " + auto_x.geschwindigkeit + " kmh");
            Console.WriteLine("Leistung:        " + auto_x.leistung + " kW");
            Console.WriteLine("Verbrauch:       " + auto_x.verbrauch + " Liter");
            Console.WriteLine("Zylinder:        " + auto_x.zylinder + " Zyl");
            Console.WriteLine("Hubraum:         " + auto_x.hubraum + " Liter");
            Console.WriteLine("Beschleunigung:  " + auto_x.beschleunigung + " sec");
            Console.WriteLine("Zuladung:        " + auto_x.zuladung + " kg");
            Console.WriteLine("Ladevolumen:     " + auto_x.ladevolumen + " Liter");
            Console.WriteLine();
        }

        static int Vergleich_Auswahl(Autokarte auto_x)
        {
            Console.WriteLine("Welchen Wert möchten Sie vergleichen?");
            Console.WriteLine("1:  Geschwindigkeit");
            Console.WriteLine("2:  Leistung");
            Console.WriteLine("3:  Verbrauch");
            Console.WriteLine("4:  Zylinder");
            Console.WriteLine("5:  Hubraum");
            Console.WriteLine("6:  Beschleunigung");
            Console.WriteLine("7:  Zuladung");
            Console.WriteLine("8:  Ladevolumen");
            Console.WriteLine("9:  alle");
            Console.WriteLine("10: Abbruch");

            int vergleich_wahl;

            do
            {
                eingabe_korrekt = Int32.TryParse(Console.ReadLine(), out vergleich_wahl);
               
                if (   vergleich_wahl != 1 && vergleich_wahl != 2 && vergleich_wahl != 3 && vergleich_wahl != 4
                    && vergleich_wahl != 5 && vergleich_wahl != 6 && vergleich_wahl != 7 && vergleich_wahl != 8
                    && vergleich_wahl != 9 && vergleich_wahl != 10)
                {
                    Console.WriteLine("Ungültige Eingabe. Bitte geben Sie einen der obigen Werte ein.");
                    eingabe_korrekt = false;
                }
                
            } while (eingabe_korrekt != true);
                
            Console.WriteLine();
            return vergleich_wahl;
        }

        public static bool Vergleich_Karten_Ausgabe(Autokarte auto_x, Autokarte auto_y, int vergleichsfeld)
        {
            string laengster_string = "Ladevolumen:     " + auto_x.ladevolumen + " Liter";

            //groesser = Wert_pruefen(auto_x, auto_y, vergleichsfeld);

            Ausgabe_x(laengster_string, auto_x.modell.ToUpper());
            Ausgabe_y(                  auto_y.modell.ToUpper());
            //Ist das entsprechende Vergleichsfeld ausgewählt, prüft Wert_pruefen, ob auto_x > auto_y groesser ist.
            //Für den ausgewählten Vergleich wird die entsprechende Farbe pro Auto gesetzt. 
            if (vergleichsfeld == 1 || vergleichsfeld == 9)
            {
                Farbe_setzen_auto_x(groesser = auto_x.geschwindigkeit > auto_y.geschwindigkeit);
            }
            Ausgabe_x(laengster_string, "Geschwindigkeit: " + auto_x.geschwindigkeit + " kmh");

            if (vergleichsfeld == 1 || vergleichsfeld == 9) { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Geschwindigkeit: " + auto_y.geschwindigkeit + " kmh");

            if (vergleichsfeld == 2 || vergleichsfeld == 9)
            {
                Farbe_setzen_auto_x(groesser = auto_x.leistung > auto_y.leistung);
            }
            Ausgabe_x(laengster_string, "Leistung:        " + auto_x.leistung + " kW");

            if (vergleichsfeld == 2 || vergleichsfeld == 9) { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Leistung:        " + auto_y.leistung + " kW");

            if (vergleichsfeld == 3 || vergleichsfeld == 9)
            {
                Farbe_setzen_auto_x(groesser = auto_x.verbrauch > auto_y.verbrauch);
            }
            Ausgabe_x(laengster_string, "Verbrauch:       " + auto_x.verbrauch + " Liter");

            if (vergleichsfeld == 3 || vergleichsfeld == 9) { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Verbrauch:       " + auto_y.verbrauch + " Liter");

            if (vergleichsfeld == 4 || vergleichsfeld == 9)
            {
                Farbe_setzen_auto_x(groesser = auto_x.zylinder > auto_y.zylinder);
            }
            Ausgabe_x(laengster_string, "Zylinder:        " + auto_x.zylinder + " Zyl");

            if (vergleichsfeld == 4 || vergleichsfeld == 9) { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Zylinder:        " + auto_y.zylinder + " Zyl");

            if (vergleichsfeld == 5 || vergleichsfeld == 9)
            {
                Farbe_setzen_auto_x(groesser = auto_x.hubraum > auto_y.hubraum);
            }
            Ausgabe_x(laengster_string, "Hubraum:         " + auto_x.hubraum + " Liter");

            if (vergleichsfeld == 5 || vergleichsfeld == 9) { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Hubraum:         " + auto_y.hubraum + " Liter");

            if (vergleichsfeld == 6 || vergleichsfeld == 9)
            {
                Farbe_setzen_auto_x(groesser = auto_x.beschleunigung > auto_y.beschleunigung);
            }
            Ausgabe_x(laengster_string, "Beschleunigung:  " + auto_x.beschleunigung + " sec");

            if (vergleichsfeld == 6 || vergleichsfeld == 9) { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Beschleunigung:  " + auto_y.beschleunigung + " sec");

            if (vergleichsfeld == 7 || vergleichsfeld == 9)
            {
                Farbe_setzen_auto_x(groesser = auto_x.zuladung > auto_y.zuladung);
            }
            Ausgabe_x(laengster_string, "Zuladung:        " + auto_x.zuladung + " kg");

            if (vergleichsfeld == 7 || vergleichsfeld == 9) { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Zuladung:        " + auto_y.zuladung + " kg");

            if (vergleichsfeld == 8 || vergleichsfeld == 9)
            {
                Farbe_setzen_auto_x(groesser = auto_x.ladevolumen > auto_y.ladevolumen);
            }
            Ausgabe_x(laengster_string, "Ladevolumen:     " + auto_x.ladevolumen + " Liter");

            if (vergleichsfeld == 8 || vergleichsfeld == 9) { Farbe_setzen_auto_y(groesser);      }
            Ausgabe_y(                  "Ladevolumen:     " + auto_y.ladevolumen + " Liter");
            
            return groesser;
        }

        public static void Ausgabe_x(string laengster_string, string auto_feld)
        {
            //Ausgabe von auto_x und dem Trennungsstrich "--"
            string laenge_lz = "";
            Console.Write(lz_abstand(  laengster_string,
                                       auto_feld,
                                       out laenge_lz
                                    )
                          );
            Console.ResetColor();
            Console.Write(laenge_lz + "--   ");
        }

        public static void Ausgabe_y(string auto_feld)
        {
            Console.WriteLine(auto_feld);
            Console.ResetColor();
        }

        static string lz_abstand (string laengster_str, string text2, out string lz)
        {
            lz = "";
            int leerz = laengster_str.Length - text2.Length;
            for (int i = 0; i < leerz+3; i++)
            {
                lz += " ";
            } 
            return text2;
        }

        public static bool Farbe_setzen_auto_x(bool groesser)
        {   
            if (groesser)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                zaehler_x++;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            return groesser;
        }

        public static bool Farbe_setzen_auto_y(bool groesser)
        {
            if (groesser)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                zaehler_y++;
            }
            return groesser;
        }
    }
}


/* public static string Alles_vergleichen(Autokarte auto_x, Autokarte auto_y)
 {
     string laengster_string = "Ladevolumen:     " + auto_x.ladevolumen + " Liter";
     int vergleichsfeld = 0;
     //Zaehlervariablen zurücksetzen
     zaehler_x = 0;
     zaehler_y = 0;

     //AUSGABE
     Ausgabe_x(laengster_string, auto_x.modell.ToUpper());
     Ausgabe_y(auto_y.modell.ToUpper());

     vergleichsfeld = 1;
         groesser = Wert_pruefen(auto_x, auto_y, vergleichsfeld);
         zaehler_plus(groesser, ref zaehler_x, ref zaehler_y);

         Farbe_setzen_auto_x(groesser);
         Ausgabe_x(laengster_string, "Geschwindigkeit: " + auto_x.geschwindigkeit + " kmh");

         Farbe_setzen_auto_y(groesser);
         Ausgabe_y("Geschwindigkeit: " + auto_y.geschwindigkeit + " kmh");

     vergleichsfeld = 2;
         groesser = Wert_pruefen(auto_x, auto_y, vergleichsfeld);
         zaehler_plus(groesser, ref zaehler_x, ref zaehler_y);

         Farbe_setzen_auto_x(groesser);
         Ausgabe_x(laengster_string, "Leistung:        " + auto_x.leistung + " kW");

         Farbe_setzen_auto_y(groesser);
         Ausgabe_y("Leistung:        " + auto_y.leistung + " kW");

     vergleichsfeld = 3;
         groesser = Wert_pruefen(auto_x, auto_y, vergleichsfeld);
         zaehler_plus(groesser, ref zaehler_x, ref zaehler_y);

         Farbe_setzen_auto_x(groesser);
         Ausgabe_x(laengster_string, "Verbrauch:       " + auto_x.verbrauch + " Liter");

         Farbe_setzen_auto_y(groesser);
         Ausgabe_y("Verbrauch:       " + auto_y.verbrauch + " Liter");

     vergleichsfeld = 4;
         groesser = Wert_pruefen(auto_x, auto_y, vergleichsfeld);
         zaehler_plus(groesser, ref zaehler_x, ref zaehler_y);

         Farbe_setzen_auto_x(groesser);
         Ausgabe_x(laengster_string, "Zylinder:        " + auto_x.zylinder + " Zyl");

         Farbe_setzen_auto_y(groesser);
         Ausgabe_y("Zylinder:        " + auto_y.zylinder + " Zyl");

     vergleichsfeld = 5;
         groesser = Wert_pruefen(auto_x, auto_y, vergleichsfeld);
         zaehler_plus(groesser, ref zaehler_x, ref zaehler_y);

         Farbe_setzen_auto_x(groesser);
         Ausgabe_x(laengster_string, "Hubraum:         " + auto_x.hubraum + " Liter");

         Farbe_setzen_auto_y(groesser);
         Ausgabe_y("Hubraum:         " + auto_y.hubraum + " Liter");

     vergleichsfeld = 6;
         groesser = Wert_pruefen(auto_x, auto_y, vergleichsfeld);
         zaehler_plus(groesser, ref zaehler_x, ref zaehler_y);

         Farbe_setzen_auto_x(groesser);
         Ausgabe_x(laengster_string, "Beschleunigung:  " + auto_x.beschleunigung + " sec");

         Farbe_setzen_auto_y(groesser);
         Ausgabe_y("Beschleunigung:  " + auto_y.beschleunigung + " sec");

     vergleichsfeld = 7;
         groesser = Wert_pruefen(auto_x, auto_y, vergleichsfeld);
         zaehler_plus(groesser, ref zaehler_x, ref zaehler_y);

         Farbe_setzen_auto_x(groesser);
         Ausgabe_x(laengster_string, "Zuladung:        " + auto_x.zuladung + " kg");

         Farbe_setzen_auto_y(groesser);
         Ausgabe_y("Zuladung:        " + auto_y.zuladung + " kg");

     vergleichsfeld = 8;
         groesser = Wert_pruefen(auto_x, auto_y, vergleichsfeld);
         zaehler_plus(groesser, ref zaehler_x, ref zaehler_y);

         Farbe_setzen_auto_x(groesser);
         Ausgabe_x(laengster_string, "Ladevolumen:     " + auto_x.ladevolumen + " Liter");

         Farbe_setzen_auto_y(groesser);
         Ausgabe_y("Ladevolumen:     " + auto_y.ladevolumen + " Liter");

     //Zurückgegeben werden beide Werte als String.
     //Da nur ein Rückgabewert möglich ist, soll so ein Zugriff auf beide Ergebnisse erfolgen.
     return zaehler_x.ToString() + zaehler_y.ToString();
 }*/

/*public static void zaehler_plus(bool groesser, ref int zaehler_x, ref int zaehler_y)
{
if (groesser)
{
    zaehler_x++;
}
else
{
    zaehler_y++;
}
}*/

/*public static bool Wert_pruefen(Autokarte auto_x, Autokarte auto_y, int vergleichsfeld)
{
    bool groesser = true;
    switch (vergleichsfeld)
    {
        case 1:
            groesser = auto_x.geschwindigkeit > auto_y.geschwindigkeit;
            break;
        case 2:
            groesser = auto_x.leistung > auto_y.leistung;
            break;
        case 3:
            groesser = auto_x.verbrauch > auto_y.verbrauch;
            break;
        case 4:
            groesser = auto_x.zylinder > auto_y.zylinder;
            break;
        case 5:
            groesser = auto_x.hubraum > auto_y.hubraum;
            break;
        case 6:
            groesser = auto_x.beschleunigung > auto_y.beschleunigung;
            break;
        case 7:
            groesser = auto_x.zuladung > auto_y.zuladung;
            break;
        case 8:
            groesser = auto_x.ladevolumen > auto_y.ladevolumen;
            break;
    }
    return groesser;
}*/
