using System;
using System.Collections.Generic;
using System.IO;

namespace Ciklusok_és_fájlok
{

    class adat
    {
        public int x;
        public string y;
        public adat(int i, string sor)
            {
                this.x = i;
                this.y = sor;
            }
     }

    internal class Program
    {
  
        static void Main(string[] args)
        {
            List<string> adatok = new List<string>();     /*lista*/
            string[] tombNev = new string[2];             /*(2 elemes)tömb*/
            int[] tombInt = new int[2];
            bool[] tombBool = new bool[2];
            HashSet<string> halmaz = new HashSet<string>();  /*halmaz*/

            string[] sorok = File.ReadAllLines("ajto.txt"); /* összes sor beolvasása egy tömbbe */

            StreamReader fromFile = new StreamReader("ajto.txt");   /*beolvasás fájlból*/
            StreamWriter toFile = new StreamWriter("ajtoKi.txt");   /*kiírás fájlba*/


            //előltesztelős ciklus

            while (!fromFile.EndOfStream)    /*amíg nincs vlge*/
            {
                adatok.Add(fromFile.ReadLine());
                toFile.WriteLine($"{fromFile.ReadLine()} - ");
            }
            fromFile.Close();
            toFile.Close();


            //számláló ciklus

            for (int i = 0; i < sorok.Length; i++)  /* i++ -> i = i+1 */
            {
                Console.WriteLine(sorok[i]);
            }


            //bejáró cilus - halmazok
            foreach (string item in sorok) 
            {
                Console.WriteLine(item);
            }


            adat[] neve = new adat[sorok.Length];
            for (int i = 0; i < sorok.Length; i++)
            { 
                neve[i] = new adat(i, sorok[i]);
            }




            Console.ReadKey();
        }
    }
}
