using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http.Headers;

namespace hatvanhat_jatek
{
    public partial class Form1 : Form
    {
        public static string[] cardColor = new string[] { "", "piros", "zöld", "tök", "makk" };
        public static string[] cardColorEn = new string[] { "", "heart", "leaf", "bell", "acorn" };
        public static string[] cardName= new string[] { "", "", "alsó", "felső", "király", "", "", "hetes", "nyolcas", "kilences", "tizes", "ász" };
        public static string[] cardNameEn = new string[] { "", "", "unter", "ober", "king", "", "", "seven", "eight", "nine", "ten", "ace" };
        public Card[] pakli = new Card[20];
        Image back = Image.FromFile($"hungarian-playing-cards-master\\cards-medium\\back.png");

        class Player
        {
            public string name { get; private set; }
            //public int point;

            public Player(string name)
            {
                this.name = name;
            }
        }
        public class Card
        {
            public string name { get; private set; }
            public byte color { get; private set; }
            public byte value { get; private set; }
            public Image imageLarge { get; private set; } //363x585
            public Image imageMedium { get; private set; } //181x293
            public Image imageSmall{ get; private set; } //91x146

            public Card(string line, string[] cColor, string[] cName)
            {
                this.name = (line.Split(';'))[1];
                this.color = byte.Parse((line.Split(';'))[2]);
                this.value = byte.Parse((line.Split(';'))[3]);

                this.imageLarge = Image.FromFile($"hungarian-playing-cards-master\\cards-large\\{cColor[color]}-{cName[value]}.png");
                this.imageMedium = Image.FromFile($"hungarian-playing-cards-master\\cards-medium\\{cColor[color]}-{cName[value]}.png");
                this.imageSmall = Image.FromFile($"hungarian-playing-cards-master\\cards-small\\{cColor[color]}-{cName[value]}.png");
            }


        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] fileData = File.ReadAllLines("magyarkartya.txt", Encoding.UTF8);

            int db = 0;
            foreach (var item in fileData)
            {
                Card tmp = new Card(item, cardColorEn, cardNameEn);
                if (tmp.value >= 7 && tmp.value <= 9) continue;
                pakli[db++] = tmp; //berakja és növeli az értékét 1-gyel
            }

            int x = Screen.PrimaryScreen.WorkingArea.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Height;
            Size = new Size(x, y);
            MinimumSize = new Size(x, y);
            MaximumSize = new Size(x, y);
            WindowState = FormWindowState.Maximized; //teljes képernyő
            FormBorderStyle = FormBorderStyle.FixedToolWindow;

            Text = "Hatvanhat";
            txbPlayer1.Text = "1. játékos neve:";
            txbPlayer2.Text = "2. játékos neve:";
            button1.Text = "Játék";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Player[] players = new Player[2];
            players[0] = new Player(txbPlayer1.Text);
            players[1] = new Player(txbPlayer2.Text);

            txbPlayer1.Visible = false;
            txbPlayer2.Visible = false;
            button1.Visible = false;

            PictureBox pakliPlace = new PictureBox();
            pakliPlace.Size = back.Size; //medium
            pakliPlace.Location = new Point(10, 10);
            pakliPlace.Image = back;
            Controls.Add(pakliPlace);


            Random rnd = new Random();

            int oszto =

            //keverés
            int n = pakli.Length;

            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);

                Card v = pakli[k];
                pakli[k] = pakli[n];
                pakli[n] = v;
            }





        }
    }
}
