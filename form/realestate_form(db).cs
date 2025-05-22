using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace RealEstateGUI
{
    public partial class Form1 : Form
    {

        static List<Seller> sellers = new List<Seller>(); //az activeseller lista is ugyanez akar lenni 
        static MySqlConnection kapcsolat;


        public Form1()
        {
            InitializeComponent();


            //Kapcsolat a szerverrel


            var builder = new MySqlConnectionStringBuilder
            {
                Server = "127.0.0.1",
                UserID = "root",
                Password = "",
                Database = "ingatlan"
            };

            kapcsolat = new MySqlConnection(builder.ConnectionString);
            kapcsolat.Open();


            btnSellers.BackColor = Color.Green;
            btnSellers.Text = "Aktív ügynökök";
            sellers = fullRead();
            listboxSellers.Items.Clear();

            foreach (var item in sellers)
            {
                listboxSellers.Items.Add(item.Name);
            }
        }



 
        
        static List<Seller> activeRead() //Beolvasás, lekérdezés
        {
            List<Seller> a = new List<Seller>();
            var command = kapcsolat.CreateCommand();
            command.CommandText = "select * from sellers where id in (select sellerid from realestates) order by name";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Seller tmp = new Seller();
                tmp.Id = reader.GetInt32("id");
                tmp.Name = reader.GetString("name");
                tmp.Phone = reader.GetString("phone");
                a.Add(tmp);

            }
            reader.Close();
            return a;
        
        }
        

        static List<Seller> fullRead() //Beolvasás, lekérdezés
        {
            List<Seller> a = new List<Seller>();
            var command = kapcsolat.CreateCommand();
            command.CommandText = "select * from sellers order by name";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Seller tmp = new Seller();
                tmp.Id = reader.GetInt32("id");
                tmp.Name = reader.GetString("name");
                tmp.Phone = reader.GetString("phone");
                a.Add(tmp);

            }
            reader.Close();
            return a;

        }




        private void Form1_Load(object sender, EventArgs e)
        {
            btnHirdetesek.Enabled = false;
            listboxCoordinates.Items.Clear();


        }

        private void Form1_Out(object sender, EventArgs e)
        {
            kapcsolat.Close();
        }

        private void btnSellers_Click(object sender, EventArgs e)
        {
            listboxCoordinates.Items.Clear();
            btnHirdetesek.Enabled = false;
            lblCount.Text = "Hirdetések száma: ";


            if (btnSellers.BackColor == Color.Red)
            {

                btnSellers.BackColor = Color.Green;
                btnSellers.Text = "Aktív ügynökök";
                sellers = fullRead();
                listboxSellers.Items.Clear();

                foreach (var item in sellers)
                {
                    listboxSellers.Items.Add(item.Name);
                }

            }
            else
            {
                btnSellers.BackColor = Color.Red;
                btnSellers.Text = "Összes ügynök";
                sellers = activeRead();
                listboxSellers.Items.Clear();

                foreach (var item in sellers)
                {
                    listboxSellers.Items.Add(item.Name);
                }
            }

        }

        private void btnHirdetesek_Click(object sender, EventArgs e)
        {
            /*var command = kapcsolat.CreateCommand();
            command.CommandText = $"select count(id) from realestates where sellerid = {sellers[listboxSellers.SelectedIndex].Id}";
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                lblCount.Text = $"Hirdetések száma: {reader.GetInt32(0)}";


            }
            reader.Close();
            */
             

            //vagy:
             

            var command = kapcsolat.CreateCommand();
            command.CommandText = $"select * from realestates where sellerid = {sellers[listboxSellers.SelectedIndex].Id}";
            var reader = command.ExecuteReader();
            listboxCoordinates.Items.Clear();

            while (reader.Read())
            {
                string a = "";
                a += $"Hirdetések id-ja: {reader.GetInt32("id")}\t";
                a += $"Szobák száma: {reader.GetInt32("rooms")}\t";
                a += $"Terület: {reader.GetInt32("area")} m2\t";
                a += $"Kordináta: {reader.GetString("latlong")}\t";



                listboxCoordinates.Items.Add(a);

                //vagy egyesével:
                //listBoxCordinates.Items.Add($"Hirdetsesk {reader.Get...}
                //listBoxCordinates.Items.Add($"Szobak {reader.Get...}



            }
            reader.Close();
            lblCount.Text = $"Hirdetések száma: {listboxCoordinates.Items.Count.ToString()}";
            //Tex = $"{(listboxCoordinates.Items.Count / 5).ToString()}";


        }

        private void listboxSellers_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnHirdetesek.Enabled = true;
            listboxCoordinates.Items.Clear();
            lblCount.Text = "Hirdetések száma: ";

            lblSellername.Text = $"Eladó neve: {sellers[listboxSellers.SelectedIndex].Name}";
            lblSellerphone.Text = $"Eladó telefonszáma: {sellers[listboxSellers.SelectedIndex].Phone}";

        }
    }

    class Ad
    {
        public int Area;
        public int Floors;
        public int Id;
        public int Rooms;
        public Category Category;
        public DateTime CreateAt;
        public string Description;
        public string ImageUrl;
        public string Latlong;
        public bool FreeOfCharge;
        public Seller Seller;

        public double DistanceTo(double x, double y)
        {
            double a = x - double.Parse(Latlong.Split(',')[0].Replace(".", ","));
            double b = y - double.Parse(Latlong.Split(',')[1].Replace(".", ","));
            return Math.Sqrt(a * a + b * b);
        }
    }
    class Seller
    {
        public int Id { get; set; }
        public string Name;
        public string Phone;
    }
    class Category
    {
        public int Id;
        public string Name;
    }
}
