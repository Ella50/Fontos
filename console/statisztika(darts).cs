using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace darts_statisztika
{
    class data
    {
        public bool playerId;
        public string[] types = new string[3];
        public int[] scores = new int[3];

        public data(string line)
        {
            string[] strings = line.Split(';');

            try
            {
                this.playerId = strings[0] == "1";
                for (int i = 0; i < 3; i++)
                {
                    if (strings[i + 1][0] == 'D' || strings[i + 1][0] == 'T') //elso karakter azonos D-vel
                    {
                        this.types[i] = strings[i + 1][0].ToString();
                        this.scores[i] = int.Parse(strings[i + 1].Substring(1)); //második karaktertől a végéig
                    }
                    else
                    {
                        this.types[i] = "";
                        this.scores[i] = int.Parse(strings[i + 1]); //ha nincs benne betű, sima szám van
                    }
                }
            }
            catch (Exception) //ha hibás adat (amikor B betű), akkor errort tesz bele
            {
                this.types[0] = "error";
            }
          

        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Variables
            data[] tomb = new data[300];
            int tombIndex = 0;
            List<data> lista = new List<data>();

            #endregion


            #region 1. feladat
            //Console.WriteLine("1. feladat");

            string fileName = "dobasok.txt";
            StreamReader streamreader = new StreamReader(fileName);

            while (!streamreader.EndOfStream) 
            {
                data tmp = new data(streamreader.ReadLine());
                if (tmp.types[0] != "error")
                {
                    lista.Add(tmp);
                    tomb[tombIndex++] = tmp; //növeli a tombindexet
                }
            }

            streamreader.Close();

            #endregion


            #region 2. feladat

            Console.WriteLine("2. feladat");
            Console.WriteLine($"Körök száma (tömb): {tombIndex}");
            Console.WriteLine($"Körök száma (lista): {lista.Count}");

            #endregion


            #region 3. feladat
            Console.WriteLine("3. feladat:");
            int bullseye = 0;   //db

            //listával
            foreach(var item in lista)
            {
                if (item.scores[2] == 25 && item.types[2] == "D")
                {
                    bullseye++;
                }
            }

            Console.WriteLine($"3.dobásra Bullseye (lista): {bullseye}");

            //tömbbel
            bullseye = 0;

            foreach (var item in tomb)
            {
                if (item == null)
                {
                    continue; //menjen a következőre
                }

                if (item.scores[2] == 25 && item.types[2] == "D")
                {
                    bullseye++;
                }
            }

            Console.WriteLine($"3.dobásra Bullseye (tömb): {bullseye}");
            #endregion


            #region 4. feladat
            Console.WriteLine("4. feladat");
            Console.Write("Adja meg a szektor értékét! Szektor= ");

            string szektor = Console.ReadLine();

            int szekOssz1 = 0, szekOssz2 = 0;

            int db180_1 = 0,  db180_2 = 0; //5. feladathoz

            foreach (var item in lista)
            {
                //if (item == null) continue; KELL TÖMBNÉL

                string akarmi = ""; //5. feladathoz
                for (int i = 0; i < 3; i++)
                {
                    if (item.playerId && szektor == item.types[i] + item.scores[i].ToString()) //item.playerId true, akkor 1. játékos
                    {
                        szekOssz1++;
                    }
                    else if (!item.playerId && szektor == item.types[i] + item.scores[i].ToString()) //item.playerId true, akkor 1. játékos
                    {
                        szekOssz2++;
                    }

                    akarmi += item.types[i] + item.scores[i].ToString();

                }

                if (item.playerId && akarmi == "T20T20T20") db180_1 ++;
                else if (!item.playerId && akarmi == "T20T20T20") db180_2++;

            }

            Console.WriteLine($"Az 1. játékos a(z) {szektor} szektoros dobásainek száma: {szekOssz1}");
            Console.WriteLine($"Az 2. játékos a(z) {szektor} szektoros dobásainek száma: {szekOssz2}");

            #endregion


            #region 5. feladat
            Console.WriteLine("5. feladat");
            Console.WriteLine($"Az 1. játékos {db180_1} db 180-ast dobott");
            Console.WriteLine($"Az 2. játékos {db180_2} db 180-ast dobott");

            #endregion

            Console.ReadKey();

        }
    }
}
