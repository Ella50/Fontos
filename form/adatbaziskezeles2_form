using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RealEstateGUI
{
    public partial class Form1 : Form
    {
        MySqlConnection kapcsolat;

        public Form1()
        {
            InitializeComponent();
            Csatlakozas();
            SellersBetoltese();
        }

        private void Csatlakozas()
        {
            MySqlConnectionStringBuilder builder =
                new MySqlConnectionStringBuilder
                {
                    Server = "127.0.0.1",
                    Database = "ingatlan",
                    UserID = "root",
                    Password = ""
                };

            kapcsolat = new MySqlConnection(builder.ConnectionString);
            kapcsolat.Open();
        }

        private void SellersBetoltese()
        {
            MySqlCommand cmd = kapcsolat.CreateCommand();
            cmd.CommandText = "SELECT id, name, phone FROM sellers;";

            MySqlDataReader reader = cmd.ExecuteReader();

            List<Seller> sellers = new List<Seller>();

            while (reader.Read())
            {
                sellers.Add(new Seller
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Phone = reader.GetString(2)
                });
            }

            reader.Close();

            listBoxSellers.DataSource = sellers;
            listBoxSellers.DisplayMember = "Name";
        }

        private void listBoxSellers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Seller selected = listBoxSellers.SelectedItem as Seller;

            if (selected != null)
            {
                labelName.Text = selected.Name;
                labelPhone.Text = selected.Phone;
            }
        }

        private void buttonLoadAds_Click(object sender, EventArgs e)
        {
            Seller selected = listBoxSellers.SelectedItem as Seller;

            if (selected == null) return;

            MySqlCommand cmd = kapcsolat.CreateCommand();
            cmd.CommandText = $"SELECT COUNT(*) FROM realestates WHERE sellerId = {selected.Id};";

            object result = cmd.ExecuteScalar();

            int db = Convert.ToInt32(result);

            labelAdCount.Text = db.ToString();
        }
    }

    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
