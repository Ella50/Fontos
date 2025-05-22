using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace epitmenyado
{
    internal class Program
    {
    internal class data
        {
            public string adoszam;
            public string utca;
            public string hazszam;
            public string adosav;
            public int terulet;

            public data(string sor)
            {
                string[] szoveg = sor.Split(' ');
                this.adoszam = szoveg[0];
                this.utca = szoveg[1];
                this.hazszam = szoveg[2];
                this.adosav = szoveg[3];
                this.terulet = int.Parse(szoveg[4]);

            }
        }

        static int Ado(string adosav, int alapterulet)
        {
            int fizetendoAdo;
            if (adosav == "A") 
            {
                fizetendoAdo = alapterulet * 800;
            }
            else if (adosav == "B")
            {
                fizetendoAdo = alapterulet * 600;
            }
            else
            {
                fizetendoAdo = alapterulet * 100;
            }
            
            if (fizetendoAdo < 10000) 
            {
                fizetendoAdo = 0;
            }
            return fizetendoAdo;
        }
        static void Main(string[] args)
        {

            List<data> lista = new List<data>();

            StreamReader beolvas = new StreamReader("utca.txt");

            string elsoSor = beolvas.ReadLine();

            while (!beolvas.EndOfStream)
            {
                lista.Add(new data(beolvas.ReadLine()));
            }

            beolvas.Close();


            #region 2. feladat

            Console.WriteLine($"2. feladat: A mintában {lista.Count} telek szerepel");

            #endregion


            #region 3. feladat

            Console.Write("3. feladat Egy tulajdonos adószáma: ");
            string input = Console.ReadLine();

            bool van = false;
            int i;

            for (i = 0; i < lista.Count; i++)
            {
                if (input == lista[i].adoszam)
                {
                    van = true;
                    Console.WriteLine($"{lista[i].utca} utca {lista[i].hazszam}");
                    
                }

            }

            if (van == false)
            {
               Console.WriteLine("Nem szerepel az adatállományban.");
            }

        

        #endregion


        #region 5. feladat
        Console.WriteLine("5. feladat");

            int dbA = 0;
            int adoA = 0;

            int dbB = 0;
            int adoB = 0;

            int dbC = 0;
            int adoC = 0;

            for (int j = 0; j < lista.Count ; j++)
            {
                if (lista[j].adosav == "A")
                {
                    dbA+= 1;
                    adoA += Ado(lista[j].adosav, lista[j].terulet);
                }

                else if (lista[j].adosav == "B")
                {
                    dbB += 1;
                    adoB += Ado(lista[j].adosav, lista[j].terulet);
                }

               else
                {
                    dbC += 1;
                    adoC += Ado(lista[j].adosav, lista[j].terulet);
                }
            }

            Console.WriteLine($"A sávba {dbA} telek esik, az adó {adoA} Ft");
            Console.WriteLine($"B sávba {dbB} telek esik, az adó {adoB} Ft");
            Console.WriteLine($"C sávba {dbC} telek esik, az adó {adoC} Ft");

            #endregion


            #region 6. feladat

            Console.WriteLine("6. feladat: A többi sávba sorolt utcák: ");

            HashSet<string> utcak = new HashSet<string>();

            for (int j = 0; j < lista.Count; j++)
            {
                utcak.Add(lista[j].utca);
            }


            foreach (var utca in utcak)
            {
                int a = 0;
                int b = 0;
                int c = 0;

                for (i = 0; i < lista.Count; i++)
                {
                    if (utca == lista[i].utca)
                    {
                        if (lista[i].adosav == "A")
                        {
                            a += 1;
                        }
                        if (lista[i].adosav == "B")
                        {
                            b += 1;
                        }
                        if (lista[i].adosav == "C")
                        {
                            c += 1;
                        }
                    }
                }

                if (a > 0 && b > 0 ||
                    a > 0 && c > 0 ||
                    b > 0 && c > 0)
                {
                    Console.WriteLine(utca);
                }
            }
            #endregion




            #region 7. feladat

            StreamWriter kiir = new StreamWriter("fizetendo.txt");
 
            HashSet <string> tulajok = new HashSet<string>();

            for (int j = 0; j < lista.Count; j++)
            {
                tulajok.Add(lista[j].adoszam);
            }


            foreach (var tulaj in tulajok)
            {

                for (i = 0; i < lista.Count; i++)
                {
                    int fizetendo = 0;
                    if (tulaj == lista[i].adoszam)
                    {
                        fizetendo += Ado(lista[i].adosav, lista[i].terulet);
                        kiir.WriteLine($"{tulaj} {fizetendo}");
                    }
                    
                }
            }


            kiir.Close();

            #endregion

            Console.ReadKey();
        }
    }
}
