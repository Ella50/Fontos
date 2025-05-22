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
using System.Runtime.CompilerServices;
using static barlang_form.Barlangok;

namespace barlang_form
{
    public partial class Barlangok : Form
    {
        public Barlangok()
        {
            InitializeComponent();
        }

        public void kiiras(/*double atlag*/)
        {
            filelabel.Visible = true;
            filelabel.Text = $"Barlangok száma: {lista.Count} db";

            //masiklabel.Text = $"Miskolci barlangok átlagos mélysége: {atlag:#.###)} m"
            //checkedListBox1.Visible = true;



        }


        internal class Barlang
        {
            public int azon { get; private set; }
            public string nev { get; private set; }
            public string telepules { get; private set; }
            public string vedettseg { get; set; }


            private int H = 0;
            private int M = 0;

            public int hossz
            {
                get
                {
                    return H;
                }
                set
                {
                    if (H <= value || value == 0)
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
                    this.azon = int.Parse(s[0]);
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

            public override string ToString()
            {
                return $"Azon: {azon}\nNév: {nev}\nHossz: {hossz}\nMélység: {melyseg}\nTelepülés: {telepules}\nVédettség: {vedettseg}".ToString(); //szépen írja ki az adatokat (mintha Console.Writeline lenne)
            }


        }

        static List<Barlang> lista = new List<Barlang>();
        //public SortedSet <string> vedettseg = new SortedSet <string>();
        Form2 f2 = new Form2(); //új form, de nem kell


        private void button1_Click(object sender, EventArgs e)
        {
            /*console feladatok megoldása:
             * 
             openFileDialog1.ShowDialog(this)

            StreamReader sr = new StreamReader("..\\..\\..\\barlangok.txt", Encoding.UTF8);
            while (!sr.EndOfStream)
            {
                Barlang tmp = new Barlang(sr.ReadLine());
                if (tmp.hossz != 0)
                {
                    barlangok.Add(tmp);
                    vedettsegek.Add(tmp.vedettseg);
                }
            }
           
            sr.Close();

            checkedListBox1.Items.Clear();
            foreach (var tmp in vedettsegek) chechkedListBox1.Items.Add(tmp);

             richtextbox.Location = new Point((checkedListBox1.Location.X + checkedListBox1.Size.Width + 10),checkedListBox1.Lovation.Y);


                        int db = 1;
            int melysegek = 0;

            for (int i = 0; i < barlangok.Count; i++)
            {
                if (barlangok[i].telepules == "Miskolc")
                {
                    melysegek += i;
                    db ++;
                }
            }

            double atlag = melysegek / db;

             */




            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader beolvas = new StreamReader(ofd.FileName);
                    while (!beolvas.EndOfStream)
                    {
                        Barlang tmp = new Barlang(beolvas.ReadLine());
                        if (tmp.hossz != 0)
                        {
                            lista.Add(tmp);
                        }
                    }
                    filelabel.Text = ofd.FileName;
                    beolvas.Close();
                }
                catch
                {
                    MessageBox.Show("Nem megfelő fájl");
                }

            }




        }

        private void kilep_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        static int alaphossz;
        static int alapmely;
        int ujhossz;
        int ujmely;

        static int id;
        private void kereses_btn_Click(object sender, EventArgs e)
        {
            int id = int.Parse(azon_txtb.Text);

            if (id > lista.Count)
            {
                MessageBox.Show("Ezzel az azonosítóval nem létezik barlang");
                azon_txtb.Clear();
            }
            else
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (id == lista[i].azon)
                    {
                        barlangneve.Text = $"Barlang neve: {lista[i].nev}";
                        hossz_txtb.Text = lista[i].hossz.ToString();
                        mely_txtb.Text = lista[i].melyseg.ToString();

                        alaphossz = lista[i].hossz;
                        alapmely = lista[i].melyseg;

                        mentes_btn.Enabled = true;
                    }
                }
            }

        }

        private void hossz_txtb_TextChanged(object sender, EventArgs e)
        {
            int ujhossz = int.Parse(hossz_txtb.Text);

        }

        private void mely_txtb_TextChanged(object sender, EventArgs e)
        {
            int ujmely = int.Parse(mely_txtb.Text);
        }


        private void mentes_btn_Click(object sender, EventArgs e)
        {
            if (ujhossz < alaphossz)
            {
                MessageBox.Show("A hossz nem lehet kisebb a korábbi értéknél");
            }
            if (ujmely < alapmely)
            {
                MessageBox.Show("A mélység nem lehet kisebb a korábbi értéknél");
            }

            else
            {
                for (int i = 0; i < lista.Count; i++)
                {
                    if (id == lista[i].azon)
                    {
                        lista[i].hossz = ujhossz;
                        lista[i].melyseg = ujmely;
                        mentes_btn.Enabled = false;
                    }
                }

            }





            hossz_txtb.Clear();
            mely_txtb.Clear();
            azon_txtb.Clear();

        }



        public void Barlangok_Load(object sender, EventArgs e)
        {
            //Text = "Barlangok"; //form neve
            //filelabel.Visible = false;
            //masiklabel.Visible = false;

            //checkedListBox1.Visible = false;
            //checkedListBox1.AutoSize = true;
            //checkedListBox1.CheckOnClick = true;
            //richTextBox;


        }

           

    }
}
