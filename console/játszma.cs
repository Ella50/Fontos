using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Játszma5
{
    internal class Program
    {
        internal class Játék
        {
            public string allas; 
            public string aNev;
            public string fNev;

            public Játék(string allas, string aNev, string fNev) 
            {
                this.allas = allas;
                this.aNev = aNev;
                this.fNev = fNev;
            }

            public void Hozzáad(string eredmeny)
            {
                int allasA;
                int allasF;
                if (eredmeny == "A")
                {
                    allasA = +10;
                }
                else
                {
                    allasF = +10;
                }
            }

            public void NyertLabdamenetekSzáma(string eredmeny)
            {

            }
            /*
            static void JátékVége(string allas)
            {
                bool vege = false;

                int nyertA;
                int nyertF;
                int kulonbseg = 0;
                nyertA = NyertLabdamenetekSzáma('A');
                nyertF = NyertLabdamenetekSzáma('F');
                kulonbseg = Math.Abs(nyertA - nyertF);
                if((nyertA >= 4 || nyertF >= 4) && kulonbseg >= 2)
                {
                    return vege = true;
                }
            
            }
            */

        }
       
        static void Main(string[] args)
        {
            StreamReader beolvas = new StreamReader("labdamenetek5.txt");
            string labdamenetek = "";

            while (!beolvas.EndOfStream)
            {
                labdamenetek += beolvas.ReadLine();
            }

    

            #region 3. feladat
            Console.WriteLine($"3. feladat: Labdamenetek száma: {labdamenetek.Length}");
            #endregion



            #region 4. feladat
            int adogato = 0;

            for (int i = 0; i < labdamenetek.Length; i++)
            {
                if (labdamenetek[i] == 'A')
                {
                    adogato += 1;
                }
            }

            double szazalek = (double)adogato / (double)labdamenetek.Length * 100;

            Console.WriteLine($"4. feladat: Az adogató játékos {szazalek}%-ban nyerte meg a labdameneteket.");

            #endregion


            #region 5. feladat

            int legtobbA = 1; //db
            int legtobb = 0; //leghosszabb 'A' sorozat

            for (int i = 1; i < labdamenetek.Length; i++)
            {
                if (labdamenetek[i] != labdamenetek[i - 1])
                {
                    legtobbA = 0;

                }
                legtobbA++;

                if (legtobbA > legtobb)
                {
                    legtobb = legtobbA;
                }
            }

            Console.WriteLine($"5. fealdat: Leghosszabb sorozat: {legtobb}");

            #endregion


            #region 6. feladat


            #endregion


            #region 7. feladat

          //  string[] próbajáték = { new Játék("FAFFA", "Mahut", "Isner") } ;

            #endregion




            Console.ReadKey();



        }
    }
}
