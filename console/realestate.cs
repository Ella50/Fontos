using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;

namespace RealEstate
{
    internal class Program
    {
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

                //this.sellerId;
                //sellerName
                //sellerPhone
                //categoryId
                //categoryName
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


        public static double DistanceTo(string latLong, string koordinata)
        {


            string[] k = koordinata.Split(',');
            double kszel = double.Parse(k[0].Replace(".", ","));
            double khosz = double.Parse(k[1].Replace(".", ","));

            string[] l = latLong.Split(',');
            double lszel = double.Parse(l[0].Replace(".", ","));
            double lhosz = double.Parse(l[1].Replace(".", ","));

            double tavolsag = Math.Sqrt(Math.Pow(kszel - lszel, 2) +  Math.Pow(khosz - lhosz, 2));
            //double tavolsag = Math.Pow(Math.Abs((kszel - lszel) * (khosz - lhosz)), 2);


            return tavolsag;
        }

        static void Main(string[] args)
        {
            List<Ad> adatok = Ad.LoadFromCsv("realestates.csv");

            #region 6. feladat

            double osszeg = 0;
            double db = 0;
            double atlag = 0;

            for (int i = 0; i < adatok.Count; i++)
            {
                if (adatok[i].floors == 0)
                {
                    db++;
                    osszeg += adatok[i].area;
                }
                

            }

            atlag = osszeg / db;
            Console.WriteLine($"6. feladat: Földszinti ingatlanok átlagos területe: {Math.Round(atlag, 1)} m2 ");

            #endregion



            #region 7. feladat

            double mintav = 100;
            int minindex = 0;


            for (int i = 0;i < adatok.Count; i++)
            {
                if (mintav > DistanceTo(adatok[i].latLong, "47.4164220114023,19.066342425796986") && adatok[i].freeOfChange == true)
                {
                    mintav = DistanceTo(adatok[i].latLong, "47.4164220114023,19.066342425796986");
                    minindex = i;
                    
                }
            }

            Console.WriteLine($"8. feladat: Mesevár Óvodához légvonalban legközelebbi tehermentes ingatlan adatai:\n" +
                $"\tEladó neve: {adatok[minindex].Seller.name}\n" +
                $"\tEladó telefonja: {adatok[minindex].Seller.phone}\n" +
                $"\tAlapterulet: {adatok[minindex].area}\n" +
                $"\tSzobák száma: {adatok[minindex].rooms}");



            #endregion



            Console.ReadKey();
        }
    }
}
