using System;
using System.Collections.Generic;
using System.IO;

namespace tarsalgo
{
    internal class adat
    {
        public byte ora;
        public byte perc;
        public byte kod;
        public bool be = true;
        public adat(string adatSor)  //konstruktor
        {
            string[] strings = adatSor.Split(' ');
            this.ora = byte.Parse(strings[0]);
            this.perc = byte.Parse(strings[1]);
            this.kod = byte.Parse(strings[2]);
            if (strings[3] == "ki")
            {
                this.be = false;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Első feladat
            List<adat> lista = new List<adat>();
            StreamReader reader = new StreamReader("ajto.txt");

            while (!reader.EndOfStream)
            {
                lista.Add(new adat(reader.ReadLine()));
            }
            reader.Close();
            int hosszusag = lista.Count;    //tömb hosszúság egyenlő a lista elemeinek számával

            #endregion

            #region Második feladat

            int ii = 0; //az első elem 0

            while (!lista[ii].be)   //addig menjen, amíg nincs be
            {
                ii++;
            }

            Console.WriteLine($"2. feladat:\n\t" + $"Az első belépő kódja: {lista[ii].kod}");

            ii = hosszusag - 1;
            while (lista[ii].be)    //addig menjen, amíg van be
            {
                ii--;       //csökkentem az indexet, visszafele megyek?
            }

            Console.WriteLine($"2. feladat:\n\t" + $"Az utolsó kilépő kódja: {lista[ii].kod}");

            #endregion

            #region Harmadik feladat

            HashSet<byte> halmaz = new HashSet<byte>();

            for (int i = 0; i < hosszusag; i++) //lista összes elemein végig megy, beleraktuk az összes kódot
            {
                halmaz.Add(lista[ii].kod);
            }

            /* ????
            StreamWriter writer = new StreamWriter("athaladas.txt");

            while (halmaz.Counter > 0)
            {
                if (halmaz[ii] != 0)
                {
                    writer.WriteLine($"{ii + 1} {halmaz[ii]}");
                }
            }
            writer.Close(); 
            */

            //vagy

            int[] tömb = new int[100]; //van minden elemnek értéke (0)

            foreach (var item in lista) // lista sorozatot járjuk be, item nevű változóba teszi bele
            {
                tömb[item.kod - 1]++;    //sorszám(kód) és indexbeli különbség miatt: -1
                                         //++ <- +1db annak a kód indexének, amivel átmentek az ajtón?
            }


            StreamWriter writer = new StreamWriter("athaladas.txt");

            for (ii = 0; ii< 100 ; ii++)    //ciklus váltózó definiálás; ciklus meddig menjen el; ii értéke hogyan változzon
            {
                if (tömb[ii] != 0)
                {
                    writer.WriteLine($"{ii+1} {tömb[ii]}");
                }
            }
            writer.Close();
            #endregion

            #region Negyedik feladat
            #endregion



            Console.ReadKey();
        }
    }
}
