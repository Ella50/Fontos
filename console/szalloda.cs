using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace szalloda_megoldas
{
    internal class Adat
    {
        public int fSorszam;
        public byte szobaSzam;
        public int erkNap;
        public int tavNap;
        public byte vendegek;
        public bool reggeli;
        public string ID;

        public int ejszakak;

        public int szobaAr;

        public Adat(string sor, int[,] honapok)
        {
            string[] szet = sor.Split(' ');
            this.fSorszam = int.Parse(szet[0]);
            this.szobaSzam = byte.Parse(szet[1]);
            this.erkNap = int.Parse(szet[2]);
            this.tavNap = int.Parse(szet[3]);
            this.vendegek = byte.Parse(szet[4]);
            this.reggeli = szet[5] == "1";
            this.ID = szet[6];
            this.ejszakak = this.tavNap - this.erkNap;
        }

    }

    internal class Program
    {
        static List<string> screenText = new List<string>();
        static List<Adat> foglalasok = new List<Adat>();

            //3. feladat
        static int[,] honapok = new int[12, 3];  //matrix
        

        static int szobaAr(int i)
        {
            int ii = 0;
            int szobaAr = 0;
            //amíg nem találtuk meg + a tömbön belül vagyunk
            while (ii < honapok.GetLength(0) && !(foglalasok[i].erkNap > honapok[ii, 1] && foglalasok[i].erkNap <= honapok[ii, 1] + honapok[ii, 0]) )
            {
                ii++;
            }

            if (ii < honapok.GetLength(0)) //megtalálta
            {
                szobaAr = honapok[ii, 2];
                if (foglalasok[i].reggeli)
                {
                    szobaAr += (1100 * foglalasok[i].vendegek);
                }
                if (foglalasok[i].vendegek == 3)
                {
                    szobaAr += 2000;
                }

            }
            return szobaAr;

        }

   
        static void Main(string[] args)
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;
            //Console.WriteLine("* 3-letter Win32 API Name: {0}", ci.ThreeLetterWindowsLanguageName);

            

            try
            {
                string[] txtS = File.ReadAllLines(ci.ThreeLetterISOLanguageName + ".lng");  //próbálja megnyitni a magyart
                screenText.AddRange(txtS);
            }
            catch (Exception)
            {
                string[] txtS = File.ReadAllLines("eng.lng");   //ha nem sikerül, akkor megnyitja az angolt
                screenText.AddRange(txtS); //egész tömböt egy listába
            }




            #region 1. feladat

            string[] sorok = File.ReadAllLines("pitypang.txt").Skip(1).ToArray(); //automatikusan lezárja, nem kell lezárni     Skip - kihagyja az első sort

            string[] honapSorok = File.ReadAllLines("honapok.txt"); //3. feladat
            for (int i = 0; i < honapSorok.Length; i = i+4) //négyessével vannak az adatok
            {
                for (int j = 1; j < 4; j++) //nullát kihagyja
                {
                    honapok[i / 4, j-1] = int.Parse(honapSorok[i + j]); //[mátrix sorszáma, j = oszlopszám]
                }
            }


            foreach(var item in sorok)
            {
                foglalasok.Add(new Adat(item, honapok));
            }

            Console.WriteLine(screenText[0] + foglalasok.Count);

            #endregion






            #region 2. feladat


            int maxIndex = 0;

            for(int i = 1; i < foglalasok.Count; i++) 
            {
                if (foglalasok[i].ejszakak > foglalasok[maxIndex].ejszakak)
                {
                    maxIndex = i;
                }
            }

            Console.Write(screenText[1]); 
            Console.WriteLine($"{foglalasok[maxIndex].ID} ({foglalasok[maxIndex].erkNap}) - {foglalasok[maxIndex].ejszakak}");

            #endregion





            #region 3. feladat


            for (int i = 0;i < foglalasok.Count; i++)
            {
                foglalasok[i].szobaAr = szobaAr(i);
            }

            StreamWriter ki = new StreamWriter("bevetel.txt", false, Encoding.UTF8);

            int osszes = 0;
            foreach(var item in foglalasok)
            {
                ki.Write($"{item.fSorszam}:{item.ejszakak * item.szobaAr}");
                osszes += (item.szobaAr * item.ejszakak);
            }

            ki.Close();


            Console.WriteLine($"{screenText[2]}{osszes:### ### ### ###} {screenText[3]}");

            #endregion





            #region 4. feladat

            int evNap = honapok[honapok.GetLength(0)-1, 1] + honapok[honapok.GetLength(0)-1, 0];
            int[, ] Napok = new int[evNap, 28];
            
            foreach(var item in foglalasok)
            {
                for (int i = item.erkNap; i < item.tavNap; i++)
                {
                    Napok[i, 0] += item.vendegek;
                    Napok[i, item.szobaSzam] += item.vendegek;

                }
            }

            Console.WriteLine(screenText[4]);

            for (int i = 0; i < honapok.GetLength(0); i++)
            {
                int haviNap = 0;
                for (int j = honapok[i, 1]; j < honapok[i, 1] + honapok[i, 0]; j++)
                {
                    haviNap += Napok[j, 0];
                }
                Console.WriteLine($"\t{i+1}: {haviNap:### ###} {screenText[5]}");
            }



            #endregion






            #region 5. feladat

            #endregion



            Console.ReadKey();

        }
    }
}
