# <p align=center> Fajl </p>

## Fájlbeolvasás
```c#
using System.IO;

class Barlang
    {
        public int azon { get; private set; }
        public string nev { get; private set; }

        public Barlang(string sor)
        {
            try
            {
                string[] s = sor.Split(';');
                this.azon = int.Parse(s[0]);
                this.nev = s[1];

            }
            catch (Exception)
            {
                hossz = 0;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Barlang> barlangok = new List<Barlang>();
            StreamReader sr = new StreamReader("..\\..\\..\\barlangok.txt", Encoding.UTF8); //nem a debug mappában van, azért vissza kell lépni (3-mal) abba a mappába, ahol van
            
            while (!sr.EndOfStream)
            {
                Barlang tmp = new Barlang(sr.ReadLine());
                if (tmp.hossz != 0)//(tmp.hossz == 0) continue; <-első sor nem lesz ott
                {
                    barlangok.Add(tmp);
                }
            }
           
            sr.Close();
        }
    }


```



## CSV Fájlbeolvasás
```c#
        class Ad
        {
            public Category Category;
            public DateTime createAt;
            public bool freeOfChange = true;
            public int id;

            public Ad(string sor)
            {
                string[] s = sor.Split(';');
                this.id= int.Parse(s[0]);
                if (s[6] == "0")
                {
                    this.freeOfChange = false;
                }
                this.createAt = DateTime.Parse(s[8]);
                this.Category = new Category(int.Parse(s[12]), s[13]);
            }

            public static List<Ad> LoadFromCsv(string fileName)
            {
                List<Ad> listAd = new List<Ad>();
                StreamReader sr = new StreamReader(fileName, Encoding.UTF8);
                string line = sr.ReadLine();
                //sr.ReadToEnd().Skip(1);
                while (!sr.EndOfStream)
                {
                    Ad adat = new Ad(sr.ReadLine());
                    listAd.Add(adat);
                }
                sr.Close();
                return listAd;
            }
        }

        static void Main(string[] args)
        {
            List<Ad> adatok = Ad.LoadFromCsv("realestates.csv");
        }

```

```c#
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
```

## Fájlkiírás
```c# 
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
```
```c#
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
```