using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace barlang
{
    internal class Barlang
    {
        public int azon { get; private set; } //it lehet csak beállítani, nem lehet átírni classon kívűl
        public string nev { get; private set; }
        public string telepules { get; private set; }
        public string vedettseg { get; set; }


        private int H = 0;
        private int M = 0;

        public int hossz //megváltozhat (tovább fedezik a barlangot)
        {  
            get
            {
                return H;
            }
            set
            {
                if (H <= value || value == 0) //kiszedett érték(value) kisebb, mint amit(h) adnék neki, akkor egyenlő lesz az értékkel és ha 0-val egyenlő az érték ha nincs adat (de lehet barlang mélysége is 0 alapból)
                {
                    H = value;
                }
            }
        } 
        public int melyseg
        {
            get
            {
                return M;
            }
            set
            {
                if (M <= value) 
                {
                    M = value;
                }
            }
        }

        public Barlang(string sor)
        {
            try
            {
                string[] s = sor.Split(';');
                this.azon = int.Parse(s[0]); //
                this.nev = s[1];
                this.hossz = int.Parse(s[2]);
                this.melyseg = int.Parse(s[3]);
                this.telepules = s[4];
                this.vedettseg = s[5];
            }
            catch (Exception)
            {
                hossz = 0;
            }
            
        }

        public override string ToString()
        {
            return $"Azon: {azon}\nNév: {nev}\nHossz: {hossz}\nMélység: {melyseg}\nTelepülés: {telepules}\nVédettség: {vedettseg}".ToString(); //szépen írja ki az adatokat (mintha Console.Writeline lenne)
        }


    }
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 1. feladat
            //Adatok beolvasása (class)

            //Teszteset
            /*Barlang a = new Barlang("1;Nev;500;50;Telepules;vedettseg"); 
            Console.WriteLine(a.ToString());
            a.hossz = 600;
            Console.WriteLine(a.ToString());
            a.hossz = 400; //kevesebbre nem fogja beállítani
            Console.WriteLine(a.ToString());*/


            List<Barlang> barlangok = new List<Barlang>();

            StreamReader sr = new StreamReader("..\\..\\..\\barlangok.txt", Encoding.UTF8); //nem a debug mappában van, azért vissza kell lépni (3-mal) abba a mappába, ahol van
            
            while (!sr.EndOfStream)
            {
                Barlang tmp = new Barlang(sr.ReadLine());
                if (tmp.hossz != 0)     //(tmp.hossz == 0) continue; <--- első sor nem lesz ott
                {
                    barlangok.Add(tmp);
                }
            }
           
            sr.Close();

            #endregion

            #region 2. feladat

            Console.WriteLine($"2. feladat: Barlangok száma: {barlangok.Count}");

            #endregion

            #region 3. feladat

            int db = 1;
            int melysegek = 0;

            for (int i = 0; i < barlangok.Count; i++)
            {
                if (barlangok[i].telepules == "Miskolc")
                {
                    melysegek += i;
                    db ++;
                }
            }

            double atlagmely = melysegek / db;

            Console.WriteLine($"3. feladat: Miskolci barlangok átlagos mélysége: {Math.Round(atlagmely, 3)} m");

            #endregion

            #region 4. feladat

            Console.WriteLine("4. feladat: Kérem a védettségi szintet: ");
            string vedettseg = Console.ReadLine();

            for (int i = 0; i < barlangok.Count; i++)
            {
                
            }

            #endregion



            Console.ReadKey();
        }
    }
}
