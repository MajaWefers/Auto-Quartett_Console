﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;

namespace Auto_Quartett_Console
{
    class Program
    {
        //Autokarten werden erstellt
        public static Autokarte[] autos = new Autokarte[4] {
            new Autokarte("VW Phaeton", 250, 309, 15.7, 12, 6, 6.7, 600, 500),
            new Autokarte("VW New Beetle", 185, 85, 8.7, 4, 2, 10.9, 419, 527),
            new Autokarte("VW Touareg", 225, 230, 12.2, 10, 4.9, 7.8, 600, 555),
            new Autokarte() };

        public static string[] eigenschaft = new string[9] {"modell", "geschwindigkeit","leistung","verbrauch","zylinder",
                                              "hubraum", "beschleunigung","zuladung","ladevolumen" };
        public static string[] einheit = new string[9] { "", "kmh", "kW", "Liter", "Zyl", "Liter", "sec", "kg", "Liter" };

        //Variablen zur STEUERUNG
        public static string nochmal;               //Steuerung des Hauptmenüs
        public static int ansicht_vergleich;     //Wahl, ob Ansicht oder Vergleich
        public static int vergleich;
        public static bool eingabe_korrekt;       //Grundlegende Eingabeprüfung
        public static Random nr = new Random();
        public static int zufall1;
        public static int zufall2;

        //Variablen, um die Vergleichsergebnisse zu speichern
        public static bool groesser;
        public static int zaehler_x;
        public static int zaehler_y;

        static void Main(string[] args)
        {
            //TODO Neueingabe
            
            Menü_Steuerung();
        }

        static void Menü_Steuerung()
        {
            do
            {
                Console.WriteLine("Möchten Sie Karten einzeln ansehen (1) oder vergleichen (2)?");

                //Eingabeprüfung
                ansicht_vergleich = Pruefe_Eingabe();
                zufall1 = nr.Next(0, 3); //0 inklusiv, 3 exklusiv

                if (ansicht_vergleich == 1)
                {
                    Console.WriteLine();
                    for (int i = 0; i < 2; i++)
                    {
                        Zeige_einzelne_Karte(autos[i]);
                    }
                }
                if (ansicht_vergleich == 2)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ihre Spielkarte:");
                    Zeige_einzelne_Karte(autos[zufall1]);

                    vergleich = Vergleich_Auswahl(autos[zufall1]);

                    if (vergleich == 9)
                    {
                        Vergleiche_alles();
                    }
                    else if (vergleich == 10)
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

        static void Vergleiche_einen_Wert()
        {
            do
            {
                zufall2 = nr.Next(0, 3);
            } while (zufall1 == zufall2);

            groesser = Vergleich_Karten_Ausgabe(autos[zufall1], autos[zufall2], vergleich);
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

        static void Vergleiche_alles()
        {
            //Zurücksetzen des Zählers: Für die Ausgabe, wie viele Werte pro Auto groesser sind (s.u. if)
            zaehler_x = 0;
            zaehler_y = 0;

            do
            {
                zufall2 = nr.Next(0, 2);
            } while (zufall1 == zufall2);

            //In dieser Funktion wird auch der Zaehler hochgezählt
            Vergleich_Karten_Ausgabe(autos[zufall1], autos[zufall2], vergleich);
            Console.WriteLine();
            
            groesser = zaehler_x > zaehler_y;

            //Ausagbe des Vergleichsergebnisses
            if (groesser)
            {
                Console.WriteLine(autos[zufall1].modell + " SIEGT insgesamt mit "
                                  + zaehler_x + " zu " + zaehler_y +
                                  " über " + autos[zufall2].modell + ".");
            }
            else
            {
                Console.WriteLine(autos[zufall1].modell + " VERLIERT insgesamt mit "
                                  + zaehler_x + " zu " + zaehler_y + ".");
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

        //Diese Funktion wird sowohl für den Vergleich eines einzelnen Wertes als auch für den Vergleich aller Werte genutzt.
        //Sie wird zu Beginn der Funktionen aufgerufen.
        public static bool Vergleich_Karten_Ausgabe(Autokarte auto_x, Autokarte auto_y, int vergleich)
        {
            string laengster_string = "Ladevolumen:     " + auto_x.ladevolumen + " Liter";

            //groesser = Wert_pruefen(auto_x, auto_y, vergleich);

            //Zeile MODELL
            Ausgabe_x(laengster_string, auto_x.modell.ToUpper());
            Ausgabe_y(                  auto_y.modell.ToUpper());

            //Ist der entsprechende Vergleich ausgewählt, wird für den/die Wert/e geprüft, ob auto_x > auto_y groesser ist.
            //Für den ausgewählten Vergleich wird die entsprechende Farbe pro Auto gesetzt.
            //Ebenso wird in den Funktionen "Farbe_setzen" auch der Zähler für die Ausgabe in "Vergleiche_alles()" hochgezählt.

            //Zeile GESCHWINDIGKEIT
            if (vergleich == 1 || vergleich == 9)
            { Farbe_setzen_auto_x(groesser = auto_x.geschwindigkeit > auto_y.geschwindigkeit); }
            Ausgabe_x(laengster_string, "Geschwindigkeit: " + auto_x.geschwindigkeit + " kmh");

            if (vergleich == 1 || vergleich == 9)
            { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Geschwindigkeit: " + auto_y.geschwindigkeit + " kmh");

            //Zeile LEISTUNG
            if (vergleich == 2 || vergleich == 9)
            { Farbe_setzen_auto_x(groesser = auto_x.leistung > auto_y.leistung); }
            Ausgabe_x(laengster_string, "Leistung:        " + auto_x.leistung + " kW");

            if (vergleich == 2 || vergleich == 9)
            { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Leistung:        " + auto_y.leistung + " kW");

            //Zeile VERBRAUCH
            if (vergleich == 3 || vergleich == 9)
            { Farbe_setzen_auto_x(groesser = auto_x.verbrauch > auto_y.verbrauch); }
            Ausgabe_x(laengster_string, "Verbrauch:       " + auto_x.verbrauch + " Liter");

            if (vergleich == 3 || vergleich == 9)
            { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Verbrauch:       " + auto_y.verbrauch + " Liter");

            //Zeile ZYLINDER
            if (vergleich == 4 || vergleich == 9)
            { Farbe_setzen_auto_x(groesser = auto_x.zylinder > auto_y.zylinder); }
            Ausgabe_x(laengster_string, "Zylinder:        " + auto_x.zylinder + " Zyl");

            if (vergleich == 4 || vergleich == 9)
            { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Zylinder:        " + auto_y.zylinder + " Zyl");

            //Zeile HUBRAUM
            if (vergleich == 5 || vergleich == 9)
            { Farbe_setzen_auto_x(groesser = auto_x.hubraum > auto_y.hubraum); }
            Ausgabe_x(laengster_string, "Hubraum:         " + auto_x.hubraum + " Liter");

            if (vergleich == 5 || vergleich == 9) { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Hubraum:         " + auto_y.hubraum + " Liter");

            //Zeile BESCHLEUNIGUNG
            if (vergleich == 6 || vergleich == 9)
            { Farbe_setzen_auto_x(groesser = auto_x.beschleunigung > auto_y.beschleunigung); }
            Ausgabe_x(laengster_string, "Beschleunigung:  " + auto_x.beschleunigung + " sec");

            if (vergleich == 6 || vergleich == 9)
            { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Beschleunigung:  " + auto_y.beschleunigung + " sec");

            //Zeile ZULADUNG
            if (vergleich == 7 || vergleich == 9)
            { Farbe_setzen_auto_x(groesser = auto_x.zuladung > auto_y.zuladung); }
            Ausgabe_x(laengster_string, "Zuladung:        " + auto_x.zuladung + " kg");

            if (vergleich == 7 || vergleich == 9)
            { Farbe_setzen_auto_y(groesser); }
            Ausgabe_y(                  "Zuladung:        " + auto_y.zuladung + " kg");

            //Zeile LADEVOLUMEN
            if (vergleich == 8 || vergleich == 9)
            { Farbe_setzen_auto_x(groesser = auto_x.ladevolumen > auto_y.ladevolumen); }
            Ausgabe_x(laengster_string, "Ladevolumen:     " + auto_x.ladevolumen + " Liter");

            if (vergleich == 8 || vergleich == 9)
            { Farbe_setzen_auto_y(groesser);      }
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
                //Für die Ausgabe in "Vergleiche_alles()"
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
                //Für die Ausgabe in "Vergleiche_alles()"
                zaehler_y++;
            }
            return groesser;
        }

        /*static int laengster_string()
        {
            int laenge = eigenschaft[0].Length;
            for (int i = 1; i <= 8; i++)
            {
                if (eigenschaft[i].Length > laenge)
                {
                    laenge = eigenschaft[i].Length;
                }
            }
            return laenge;
        }*/

        /*public static void Ausgabe_x(Autokarte auto_x, int i)
        {
            string laengster_str = laengste_kartenzeile(auto_x);
            string lz = "";
            int leerz = laengster_str.Length - eigenschaftszeile(auto_x, i).Length;
            for (int k = 0; k < leerz + 3; k++)
            {
                lz += " ";
            }
            Console.Write(eigenschaftszeile(auto_x, i));
            Console.ResetColor();
            Console.Write(lz + "--   ");
        }

        public static void Ausgabe_y(Autokarte auto_y, int i)
        {
            Console.WriteLine(eigenschaftszeile(auto_y, i));
            Console.ResetColor();
        }

        static string eigenschaftszeile(Autokarte auto_x, int i)
        {
            string kartenzeile = "";
            if (i == 0)
            {
                kartenzeile = auto_x.GetType().GetField(eigenschaft[i]).GetValue(auto_x).ToString().ToUpper();
            }
            else
            {
                kartenzeile = eigenschaft_str(i) + auto_x.GetType().GetField(eigenschaft[i]).GetValue(auto_x) + " " + einheit[i];
            }
            return kartenzeile;
        }

        static string laengste_kartenzeile(Autokarte auto_x)
        {
            //Wegen Modell erst mit Zeile 2 (= Index "1") der Karte anfangen
            string laengste_kartenzeile = eigenschaft_str(1) + auto_x.GetType().GetField(eigenschaft[1]).GetValue(auto_x) + " " + einheit[0];
            for (int i = 2; i <= 8; i++)
            {
                string kartenzeile = eigenschaft_str(i) +
                                     auto_x.GetType().GetField(eigenschaft[i]).GetValue(auto_x) + " " + einheit[i];
                if (kartenzeile.Length > laengste_kartenzeile.Length)
                {
                    laengste_kartenzeile = kartenzeile;
                }
            }
            return laengste_kartenzeile;
        }

        //Wie viele Leerzeichen sollen hinter die Eigenschaften
        static string eigenschaft_str(int i)
        {
            string lz = "";
            int leerz = laengster_str() - eigenschaft[i].Length;
            for (int k = 0; k < leerz + 1; k++)
            {
                lz += " ";
            }
            return eigenschaft[i] + ":" + lz;
        }

        //Längster String für eigenschaft_str (Eigenschaft)
        static int laengster_str()
        {
            int laenge = eigenschaft[1].Length;
            for (int i = 2; i <= 8; i++)
            {
                if (eigenschaft[i].Length > laenge)
                {
                    laenge = eigenschaft[i].Length;
                }
            }
            return laenge;
        }*/

    }
}
