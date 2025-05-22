using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace reklam
{
    internal class data
    {
        public byte nap;
        public string varos;
        public byte db;

        public data(string sor)
        {
            string[] szoveg = sor.Split(' ');
            this.nap = byte.Parse(szoveg[0]);
            this.varos = szoveg[1];
            this.db = byte.Parse(szoveg[2]);
        }
    }
 
    internal class Program
    {
        //függvények
        static public List<data> lista = new List<data>();

        static int osszes(string varos, byte nap)
        {
            int osszeg = 0;
            foreach (var item in lista)
            {
                if (item.nap == nap)
                {
                    osszeg += (int)item.db;
                }
            }
            return osszeg;
        }

        static void Main(string[] args)
        {
            #region 1. feladat

            
            StreamReader beolvasas = new StreamReader("rendel.txt");    // vagy string[] szoveg = File.ReadAllLines("rendel.txt")

            while (!beolvasas.EndOfStream)
            {
                lista.Add(new data(beolvasas.ReadLine()));
            }
            beolvasas.Close();

            int length = lista.Count;


            /*
             foreach (var item in szoveg)
            {
                list.Add(new data(item))
            }
             */
            #endregion

            #region 2. feladat

            Console.WriteLine($"2. feladat:\nA rendelések száma: {length}");

            #endregion

            #region 3. feladat

            Console.WriteLine("");
            Console.Write("3. feladat:\nAdjon meg egy napot: ");
            byte adottnap = byte.Parse(Console.ReadLine());

            int count = 0; //rendelések megszámolása

            foreach(var item in lista) 
            {
                if (item.nap == adottnap) count++;  //count +=1
            }
            Console.WriteLine($"A rendelések száma az adott napon: {count}");

            #endregion

            #region 4. feladat

            HashSet<byte> rendelésiNapok = new HashSet<byte>(); //halmaz (nincs index) -> ezért foreachet fogunk használni
            HashSet<byte> napok = new HashSet<byte>(); 

            foreach (var item in lista)
            {
                napok.Add(item.nap);
                if (item.varos == "NR")
                {
                    rendelésiNapok.Add(item.nap);
                }
            }

            Console.WriteLine($"4. feladat:");
            if (rendelésiNapok.Count == napok.Count)
            {
                Console.WriteLine($"Minden nap volt rendelés a reklámban nem érintett városból");
            }
            else
            {
                Console.WriteLine($"{napok.Count - rendelésiNapok.Count} nap nem volt a reklámban nem érintett városból rendelés");
            }

            /* Az összes rendelési nap kiírása
            foreach (var item in rendelésiNapok)
            {
                Console.WriteLine(item);
            }

            */
            #endregion

            #region 5. feladat

            int maxIndex = 0; //legnagyobb darabszámú elemnek az indexe

            for (int i = 1; i < length; i++)
            {
                if (lista[i].db > lista[maxIndex].db)
                {
                    maxIndex = i;
                }
            }

            Console.WriteLine($"5. feldat\nA legnagyobb darabszám: {lista[maxIndex].db}, a rendelés napja: {lista[maxIndex].nap}");

            #endregion

            #region 6. feladat



            #endregion

            Console.ReadKey();

        }
        
    }
}
