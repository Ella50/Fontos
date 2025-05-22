using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace adatbazisKezeles
{
    //Solution Explorer
    //mappa névre -> Manage NuGet Packages -> Browse: MySqlConnector (delfines) letöltése

    internal class Program
    {
        static void Main(string[] args)
        {
            //var buulde; var parancssor; var reader is működik
            MySqlConnectionStringBuilder builde = new MySqlConnectionStringBuilder{Server = "127.0.0.1", Database = "ingatlan", UserID = "root", Password = "" };
            MySqlConnection kapcsolat = new MySqlConnection(builde.ConnectionString);
            kapcsolat.Open();

            MySqlCommand parancssor = kapcsolat.CreateCommand();
            //parancssor.CommandText = "SELECT * FROM `sellers`;";
            // MySqlDataReader reader = parancssor.ExecuteReader();

            /* while (reader.Read())
             {
                 Console.WriteLine($"{reader.GetInt64(0)} {reader.GetString(1)} {reader.GetString(2)}");  //id, name, telefon
             }*/


            /* parancssor.CommandText = "SELECT name FROM sellers WHERE id = (SELECT sellerId FROM realestates WHERE area = (SELECT max(area) FROM `realestates`));";
            MySqlDataReader reader = parancssor.ExecuteReader();

            while (reader.Read())
             {
                 Console.WriteLine($"{reader.GetString(0)}"); 
             }*/


            parancssor.CommandText = "SELECT max(area) FROM `realestates`;";
            MySqlDataReader reader = parancssor.ExecuteReader();
            int negyzetmeter = 0;
            while (reader.Read())
            {
                negyzetmeter = reader.GetInt32(1);


                Console.WriteLine($"{reader.GetString(0)}"); 
            }
            parancssor.CommandText = $"SELECT sellerId FROM `realestates` WHERE area = {negyzetmeter};";
            reader = parancssor.ExecuteReader();
            int sellerID = 0;

            while (reader.Read())
            {
                sellerID = reader.GetInt32(0);

            }

            parancssor.CommandText = $"SELECT name FROM `sellers` WHERE id = {sellerID};";
            reader = parancssor.ExecuteReader();


          /*  ITT NINCS BEFEJEZVE
           *  while (reader.Read())
            {
                sellerID = reader.GetInt32(0);

            }

            */



            kapcsolat.Close();

            Console.ReadKey();
        }
    }
}
