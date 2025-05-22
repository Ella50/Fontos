# <p align=center>Class</p>

## 1.
```c#
class Barlang
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
}

```

## 2.
```c#
class KeresztrejtvenyRacs
    {
        private List<string> adatsorok;
        private char[,] racs;
        private int[,] sorszamok;

        public int oszlopokDb { get; set; }
        public int sorokDb { get; set; }


        private void BeolvasAdatsorok(string forras)
        {
            adatsorok = new List<string>(File.ReadAllLines(forras));
        }

        private void FeltoltRacs()
        {
            for (int i = 0; i < sorokDb; i++)
            {
                for (int j = 0; j < oszlopokDb; j++)
                {
                    racs[i, j] = adatsorok[i][j];
                }
            }
        }

        public KeresztrejtvenyRacs(string forras)
        {
            BeolvasAdatsorok(forras);
            sorokDb = adatsorok.Count;
            oszlopokDb = adatsorok[0].Length;
            this.racs = new char[sorokDb, oszlopokDb];
            FeltoltRacs();
            this.sorszamok = new int[sorokDb, oszlopokDb];
            Sorszamozas();


        }
    }

```

## Adatbazishoz
```c#
        class Ad
        {
            public int area;
            public Category Category;
            public DateTime createAt;
            public string description;
            public int floors;
            public bool freeOfChange = true;
            public int id;
            public string imageUrl;
            public string latLong;
            public int rooms;
            public Seller Seller;

            public Ad(string sor)
            {
                string[] s = sor.Split(';');
                this.id= int.Parse(s[0]);
                this.rooms = int.Parse(s[1]);
                this.latLong = s[2];
                this.floors = int.Parse(s[3]);
                this.area = int.Parse(s[4]);
                this.description = s[5];
                if (s[6] == "0")
                {
                    this.freeOfChange = false;
                }
                this.imageUrl = s[7]; 
                this.createAt = DateTime.Parse(s[8]);

                this.Seller = new Seller(int.Parse(s[9]), s[10], s[11]);
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

        class Seller
        {
            public int id;
            public string name;
            public string phone;

            public Seller(int id, string name, string phone)
            {
                this.id = id;
                this.name = name;
                this.phone = phone;
            }
        }

        class Category
        {
            public int id;
            public string name;

            public Category(int id, string name)
            {
                this.id = id;
                this.name = name;
            }
        }
    
```
```c#
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
```