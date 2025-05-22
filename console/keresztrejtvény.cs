using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Keresztrejtvény
{
    class KeresztrejtvenyRacs
    {
        private List<string> adatsorok;
        private char[,] racs;
        private int[,] sorszamok;

        public int oszlopokDb { get; set; }
        public int sorokDb { get; set; }


        private void BeolvasAdatsorok(string forras)
        {
            adatsorok = new List<string>(File.ReadAllLines(forras));
        }

        private void FeltoltRacs()
        {
            for (int i = 0; i < sorokDb; i++)
            {
                for (int j = 0; j < oszlopokDb; j++)
                {
                    racs[i, j] = adatsorok[i][j];
                }
            }
        }

        public KeresztrejtvenyRacs(string forras)
        {
            BeolvasAdatsorok(forras);
            sorokDb = adatsorok.Count;
            oszlopokDb = adatsorok[0].Length;
            this.racs = new char[sorokDb, oszlopokDb];
            FeltoltRacs();
            this.sorszamok = new int[sorokDb, oszlopokDb];
            Sorszamozas();


        }

        public void Megjelenites()
        {
            Console.WriteLine("6. feladat: A beolvasott keresztjretvény");
            for (int i = 0; i < sorokDb; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < oszlopokDb; j++)
                {
                    Console.Write(racs[i, j] == '#' ? "##" : "[]");
                }
                Console.WriteLine();
            }

        }

        public int MaxFuggoleges()
        {
            int maxfugg = 0;
            for (int j = 0; j < oszlopokDb; j++)
            {
                int hossz = 0;
                for (int i = 0; i < sorokDb; i++)
                {
                    if (racs[i, j] == '-') hossz++;
                    else hossz = 0;
                    maxfugg = Math.Max(maxfugg, hossz);
                }
            }
            return maxfugg;
        }



        public void VizszintesStat()
        {
            Dictionary<int, int> stat = new Dictionary<int, int>();

            for (int i = 0; i < sorokDb; i++)
            {
                int hossz = 0;
                for (int j = 0; j < oszlopokDb; j++)
                {
                    if (racs[i, j] == '-')
                    {
                        hossz++;
                    }
                    else
                    {
                        if (hossz >= 2)
                        {
                            if (!stat.ContainsKey(hossz))
                            {
                                stat[hossz] = 0;
                            }
                            stat[hossz]++;

                        }
                        hossz = 0;
                    }
                }
                if (hossz >= 2)
                {
                    if (!stat.ContainsKey(hossz))
                    {
                        stat[hossz] = 0;
                    }
                    stat[hossz]++;
                }
            }
            Console.WriteLine("8. feladat: Víszszintes szavak statisztikája");
            foreach (var item in stat.OrderBy(x => x.Key))
            {
                Console.WriteLine($"\t{item.Key} betűs: {item.Value} db");
            }
        }

        private void Sorszamozas()
        {
            int szam = 1;
            for (int i = 0; i < sorokDb; i++)
            {
                for (int j = 0; j < oszlopokDb; j++)
                {
                    if (racs[i, j] == '-')
                    {
                        bool ujSzam = false;

                        if (j == 0 || racs[i, j - 1] == '#')
                        {
                            if (j + 1 < oszlopokDb && racs[i, j + 1] == '-')
                            {
                                ujSzam = true;
                            }
                        }

                        if (i == 0 || racs[i - 1, j] == '#')
                        {
                            if (i + 1 < sorokDb && racs[i + 1, j] == '-')
                            {
                                ujSzam = true;
                            }
                        }

                        if (ujSzam)
                        {
                            sorszamok[i, j] = szam++;
                        }
                    }
                }
            }


        }

        public void MegjelenitSorszamok()
        {
            Console.WriteLine("9. feladat: A keresztrejtvény számokkal");
            for (int i = 0; i < sorokDb; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < oszlopokDb; j++)
                {
                    if (sorszamok[i, j] > 0) Console.Write($"{sorszamok[i, j]:00}");
                    else Console.Write(racs[i, j] == '#' ? "##" : "[]");
                }
                Console.WriteLine();
            }
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var rejtveny = new KeresztrejtvenyRacs("kr2.txt");
            Console.WriteLine($"5. feladat: A keresztrejtvény mérete \n\tSorok száma: {rejtveny.sorokDb} \n\tOszlopok száma: {rejtveny.oszlopokDb}");
            rejtveny.Megjelenites();

            Console.WriteLine($"7. feladat:A leghosszabb függőleges szó hossza: {rejtveny.MaxFuggoleges()} karakter");

            rejtveny.VizszintesStat();
            rejtveny.MegjelenitSorszamok();

            Console.ReadKey();
        }   
    }
}
