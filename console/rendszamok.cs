using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Net.NetworkInformation;

namespace rendzsamok
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> betuk = new List<string>();
            List<string> mgh = new List<string>();
            List<string> msh = new List<string>();

            StreamReader beolvas = new StreamReader("betuk.txt");

            string elsoSor = beolvas.ReadLine();
            string[] osszbetuk = elsoSor.Split(';');

            foreach (var item in osszbetuk)
            {
                betuk.Add(item);
            }


            string masodSor = beolvas.ReadLine();
            string[] mghbetuk = masodSor.Split(';');

            string harmadSor = beolvas.ReadLine();
            string[] mshbetuk = harmadSor.Split(';');


            foreach (var item in mghbetuk)
            {
                mgh.Add(item);
            }


            foreach (var item in mshbetuk)
            {
                msh.Add(item);
            }


            List<string> tiltott = new List<string>();


            string negyedSor = beolvas.ReadLine();
            string[] tiltottBetuk = negyedSor.Split(';');

            foreach (var item in tiltottBetuk)
            {
                tiltott.Add(item);
            }

            beolvas.Close();



            int db = 0;
            StreamWriter kiir = new StreamWriter("rendszamok.txt");


            string[] rendszam = new string[7];

            for (int i = 0; i < betuk.Count; i++) //elso betu
            {
                rendszam[0] = betuk[i];

                for (int m = 0; m < betuk.Count; m++) //masodik betu
                {
                    rendszam[1] = betuk[m];

                    bool tovabb = false;//vizsgálat

                    if (rendszam[0] == rendszam[1])
                    {
                        string elsoketto = betuk[i] + betuk[m];


                        if (!tiltott.Contains(elsoketto))
                        {
                            tovabb = true;
                        }
                    }

                    else if (mgh.Contains(rendszam[0]) && mgh.Contains(rendszam[1]))
                        {
                            tovabb = true;
                        }

                    else if (msh.Contains(rendszam[0]) && msh.Contains(rendszam[1]))
                        {
                            tovabb = true;
                        }
                    

                    if (tovabb == true)
                    {

                        for (int r = 0; r < betuk.Count; r++) //harmadik betu
                        {
                            rendszam[2] = betuk[r];

                            for (int t = 0; t < betuk.Count; t++)
                            {
                                rendszam[3] = betuk[t]; //negyedik betu

                                kiir.WriteLine($"{rendszam[0]}{rendszam[1]}*{rendszam[2]}{rendszam[3]}-001 ... {rendszam[0]}{rendszam[1]}*{rendszam[2]}{rendszam[3]}-999");
                                db += 999;

                            }

                        }
                    }
                }
            }
 

            kiir.Close();
            Console.WriteLine($"Összes rendszám: {db}");


            Console.ReadKey();
        }

    }
}
