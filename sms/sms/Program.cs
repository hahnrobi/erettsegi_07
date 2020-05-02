using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace sms
{
    class Program
    {
        static string[] gombok = new string[] { "", "", "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ" };
        static int betuToSzam(char betu)
        {
            for (int i = 2; i < gombok.Length; i++)
            {
                if (gombok[i].Contains(char.ToUpper(betu))) //ToUpper (nagybetűt csinál belőle, így akár kicsi lett beírva akár nagy, így fixen nagybetű lesz)
                    return i;
            }
            return 0; //Ha valami csoda folytán nem lenne közte a betű.
        }
        static void Main(string[] args)
        {
            #region 1. feladat
            Console.WriteLine("1. feladat");
            Console.Write("Írj be egy betűt: ");
            char betu = Console.ReadLine()[0];
            Console.WriteLine("A(z) {0} karakterhez tartozó szám a {1}", betu, betuToSzam(betu));
            #endregion
            #region 2. feladat
            Console.WriteLine("2. feladat");
            Console.Write("Írj be egy szót: ");
            string szo = Console.ReadLine();

            Console.Write("A(z) {0} szó a következő számsorral vihető be:", szo);
            foreach (char character in szo)
            {
                Console.Write(betuToSzam(character));
            }
            Console.Write("\n");
            #endregion
            #region 3. feladat
            Console.WriteLine("3. feladat");
            StreamReader sr = new StreamReader("szavak.txt");
            string[] szavak;

            //Szavak számának meghatározása
            int szavakSzama = 0;
            while (!sr.EndOfStream)
            {
                sr.ReadLine();
                szavakSzama++;
            }

            sr.BaseStream.Position = 0; //Ugrás vissza a fájl elejére.

            szavak = new string[szavakSzama];
            int counter = 0;
            while (!sr.EndOfStream)
            {
                szavak[counter++] = sr.ReadLine();
            }

            #endregion
            #region 4. feladat
            Console.WriteLine("4. feladat");
            //Maxiumkeresés tétele tisztán.
            int maxSzoIndex = 0;
            for (int i = 1; i < szavak.Length; i++)
            {
                if (szavak[maxSzoIndex].Length < szavak[i].Length)
                {
                    maxSzoIndex = i;
                }
            }

            Console.WriteLine("A leghosszabb szó: '{0}', hossza {1} betű.", szavak[maxSzoIndex], szavak[maxSzoIndex].Length);
            #endregion
            #region 5. feladat
            Console.WriteLine("5. feladat");
            int rovidSzoKriterium = 5;
            int rovidSzoMennyiseg = 0;

            for (int i = 0; i < szavak.Length; i++)
            {
                if (szavak[i].Length <= 5)
                    rovidSzoMennyiseg++;
            }

            Console.WriteLine("{0} darab rövid szó található a fájlban.", rovidSzoMennyiseg);
            #endregion
            #region 6. feladat
            Console.WriteLine("6. feladat");
            StreamWriter sw = new StreamWriter("kodok.txt");
            foreach (string egyszo in szavak)
            {
                string aktualisSzo = "";
                foreach (char character in egyszo)
                {
                    aktualisSzo += betuToSzam(character).ToString();
                }
                sw.WriteLine(aktualisSzo);
            }
            #endregion
            #region 7-8-9. feladat
            //Ki kell gyűjteni az egyező kódokhoz tartozó szavakat. Ehhez több fajta megoldás is létezik, de az egyik legegyszerűbb a C#-ba beépített Dicionary használata.
            Dictionary<string, List<string>> szotar = new Dictionary<string, List<string>>();

            foreach (string egyszo in szavak)
            {
                string kodom = "";
                foreach (char character in egyszo)
                {
                    kodom += betuToSzam(character);
                }
                if (szotar.ContainsKey(kodom))
                {
                    szotar[kodom].Add(egyszo);
                }
                else
                {
                    szotar.Add(kodom, new List<string>());
                    szotar[kodom].Add(egyszo);
                }
            }

            //7. feladat része
            Console.WriteLine("7. feladat");
            Console.Write("Írj be egy számsort: ");
            string szamSor = Console.ReadLine();

            if (szotar.ContainsKey(szamSor))
            {
                Console.WriteLine("Ennek a kódsornak megfeleltethető szavak:");
                foreach (string egyszo in szotar[szamSor])
                {
                    Console.WriteLine(egyszo);
                }
            }
            else
            {
                Console.WriteLine("Ehhez a kódsorhoz nem találtunk megfelelő szót.");
            }

            //8. feladat része
            Console.WriteLine("8. feladat");
            foreach (string kod in szotar.Keys)
            {
                if (szotar[kod].Count > 1)
                {
                    foreach (string egyszo in szotar[kod])
                    {
                        Console.Write("{0} : {1}, ", egyszo, kod);
                    }
                }
            }
            Console.Write("\n");

            //9. feladat része
            string legtobbKod = null;
            foreach (string kod in szotar.Keys)
            {
                if (legtobbKod == null)
                {
                    legtobbKod = kod;
                }
                else
                {
                    if (szotar[kod].Count > szotar[legtobbKod].Count)
                    {
                        legtobbKod = kod;
                    }
                }
            }

            Console.WriteLine("A legtöbb szónak megfeleltethető kód: {0} ({1} darab)", legtobbKod, szotar[legtobbKod].Count);
            foreach (string egyszo in szotar[legtobbKod])
            {
                Console.WriteLine(egyszo);
            }
            #endregion
        }
    }
}
