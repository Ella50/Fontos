using MySqlConnector;
using System;
using System.Collections.Generic;

namespace adatbazisKezeles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MySqlConnectionStringBuilder builde =
                new MySqlConnectionStringBuilder
                {
                    Server = "127.0.0.1",
                    Database = "ingatlan",
                    UserID = "root",
                    Password = "" //mysql
                };

            MySqlConnection kapcsolat = new MySqlConnection(builde.ConnectionString);
            kapcsolat.Open();

            MySqlCommand parancssor = kapcsolat.CreateCommand();



            Console.WriteLine("6. feladat:");

            parancssor.CommandText = "SELECT AVG(area) FROM realestates WHERE floors = 0;";
            MySqlDataReader reader = parancssor.ExecuteReader();

            if (reader.Read())
            {
                double atlag = reader.GetDouble(0);
                Console.WriteLine($"A földszinti ingatlanok átlagos alapterülete: {atlag:F2} m2");
            }

            reader.Close();



            Console.WriteLine("\n8. feladat:");

            double ovodaLat = 47.4164220114023;
            double ovodaLon = 19.066342425796986;

            parancssor.CommandText = @"
                SELECT r.id, r.description, r.rooms, r.area,
                       s.name, s.phone,
                       r.latlong
                FROM realestates r
                JOIN sellers s ON r.sellerId = s.id
                WHERE r.freeOfCharge = 1;";

            reader = parancssor.ExecuteReader();

            double minTav = double.MaxValue;
            string legjobbLeiras = "";
            int legjobbSzoba = 0;
            int legjobbTerulet = 0;
            string legjobbNev = "";
            string legjobbTelefon = "";

            while (reader.Read())
            {
                string latlong = reader.GetString(6);
                string[] coords = latlong.Split(',');

                double lat = double.Parse(coords[0], System.Globalization.CultureInfo.InvariantCulture);
                double lon = double.Parse(coords[1], System.Globalization.CultureInfo.InvariantCulture);

                double dx = lat - ovodaLat;
                double dy = lon - ovodaLon;
                double tav = Math.Sqrt(dx * dx + dy * dy);

                if (tav < minTav)
                {
                    minTav = tav;
                    legjobbLeiras = reader.GetString(1);
                    legjobbSzoba = reader.GetInt32(2);
                    legjobbTerulet = reader.GetInt32(3);
                    legjobbNev = reader.GetString(4);
                    legjobbTelefon = reader.GetString(5);
                }
            }

            reader.Close();

            Console.WriteLine("A Mesevár óvodához legközelebbi tehermentes ingatlan:");
            Console.WriteLine($"Leírás: {legjobbLeiras}");
            Console.WriteLine($"Szobák száma: {legjobbSzoba}");
            Console.WriteLine($"Alapterület: {legjobbTerulet} m2");
            Console.WriteLine($"Eladó: {legjobbNev}");
            Console.WriteLine($"Telefonszám: {legjobbTelefon}");


            kapcsolat.Close();
            Console.ReadKey();
        }
    }
}
