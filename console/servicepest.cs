using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Schema;

namespace ServicePest_megoldott
{
    internal class data
    {
        public string szerelo;
        public List<string> gepek = new List<string>();
        public bool[] munka = new bool[7];
        public int minosites;

        public data(string sor) 
        {
            string [] szet = sor.Split(',');
            this.szerelo = szet[0];

            int utolso = szet.Length-1;
            this.minosites = int.Parse(szet[utolso]);

            for (int i = 1; i < szet.Length-8; i++) //gépek
            {
                gepek.Add(szet[i]);
            }

            int f = 6;
            for (int i = szet.Length - 2; i >= szet.Length-9; i--)
            {
                if (szet[i] == "X")
                {
                    munka[f] = true;
                }
                f--;
            }

        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 1. feladat
            List<data> szerelok = new List<data>();
            try
            {
                string[] sorok = File.ReadAllLines("pest.csv", Encoding.UTF8);
               
                foreach (string sor in sorok)
                {
                    szerelok.Add(new data(sor));
                }
                Console.WriteLine("1. feladat:\n\tA Pest.csv nevű fájl beolvasása sikeres");
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                Console.WriteLine("1. feladat:\n\tA Pest.csv nevű fájl beolvasása sikertelen");
                else if (e is FormatException) Console.WriteLine($"1. feladat:\n\tHibás adat");
                else Console.WriteLine($"1. feladat:\n\tEgyéb hiba ({e.Message})");


                Console.ReadKey();
                Environment.Exit(0);
            }
            #endregion



            #region 2. feladat


            int legtobbIndex = 0;
            for (int i = 0; i < szerelok.Count; i++)
            {
                if (szerelok[i].gepek.Count > szerelok[legtobbIndex].gepek.Count)
                {
                    legtobbIndex = i;
                }
            }

            Console.Write($"2. feladat: \n\tA legtöbb ({szerelok[legtobbIndex].gepek.Count} db) különböző típusó berendezéshez értő szerelők azonosítóka:");

            int szDB = 0;   //3. fealdathoz
            int vDB = 0;
            int osszeg = 0; //4. feladathoz


            foreach (var item in szerelok)
            {
                if (item.gepek.Count == szerelok[legtobbIndex].gepek.Count)
                {
                    Console.Write($"{item.szerelo}, ");
                }

                if (item.munka[5]) szDB++;    //3. feladathoz
                if (item.munka[6]) vDB++;     //ha true akkor ++

                osszeg+= item.minosites;
            }

            Console.WriteLine("\b\b "); //utolsó karaktert(vesszőt) letörli
            #endregion



            #region 3. feladat
            Console.WriteLine($"3. fealdat\n\tSzombatonként {szDB}, vasárnaponként {vDB} szerelő áll rendelkezésre");

            #endregion


            #region 4. feladat

            double atlag = (double)osszeg/ (double)szerelok.Count;

            int dbFeletti = 0;
            foreach(var item in szerelok)
            {
                if(item.minosites > atlag) 
                {
                    dbFeletti++;
                }
            }


            Console.WriteLine($"4. fealdat\n\tA szerelők átlagosan {atlag:#.#} pontot kaptak. A szerelők {(double)dbFeletti/(double)szerelok.Count*100:#}%-a az átlagnál magasabb pontszámot kapott"); //atlag:#.00 - két tizedesjegy

            #endregion



            #region 5. feladat

            Console.Write($"5. feladat\n\tMindhárom gázzal működő berendezéshez értő szerelők");

            foreach (var item in szerelok)
            {
                int gazdb = 0;
                foreach (var item2 in item.gepek)
                {
                    if (item2 == "C" || item2 == "G" || item2 == "K") 
                    {
                        gazdb++;
                    }
                }
                

                if (gazdb == 3)
                {
                    Console.Write(item.szerelo + ", ");
                }
            }

            Console.WriteLine("\b\b ");

            #endregion




            #region 6. feladat

            Console.Write("6. feladat\n\tAdja meg a keresendő házatartási eszköz nevének rövidítését: ");
            string keszulek = Console.ReadLine();

            if (keszulek != "C" && keszulek != "KG" && keszulek != "KH" && keszulek != "K" && keszulek != "P" && keszulek != "G" && keszulek != "MG" && keszulek != "MGT")
            {
                Console.WriteLine("Helytelen rövidítést adott meg!");
            }

            else
            {
                string [] napok = {"Hétfő", "Kedd", "Szerda", "Csütörtök", "Péntek", "Szombat", "Vasárnap" };
                foreach (var item in szerelok) 
                {
                    bool erthozza = false;
                    foreach (var item2 in item.gepek)
                    {
                        if (item2 == keszulek)
                        {
                            erthozza = true;
                            break;
                        }
                    }
                    if (erthozza) //ha true
                    {
                        for (int i = 0; i < item.munka.Length; i++)
                        {
                            if (item.munka[i])
                            {
                                napok[i] = "";
                            }
                        }
                    }
                }
                Console.Write($"\tA keresett típushoz a következő napokon NEM áll rendelkezésre szerelő: ");
                foreach (var item in napok)
                {
                    Console.Write(item + " ");
                }
            }

            #endregion



            Console.ReadKey();
        }
    }
}
