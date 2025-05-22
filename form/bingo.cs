using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace BingoGUI2
{
    public partial class Form1 : Form
    {
        public int[] tol = new int[] { 1, 16, 31, 46, 61 }; //mettől mehetnek a számok tömb (benne van)
        public int[] ig = new int[] { 16, 31, 46, 61, 76 }; //meddig mehetnek a számok (nincs benne)

        public int[,] szamok = new int[5, 5];

        public TextBox[,] boxes = new TextBox[5, 5];  //mátrix
        public TextBox txb_filename = new TextBox();

        public Form1()
        {
            InitializeComponent();
        }

        private void kozepso()
        {
            boxes[2, 2].Text = "X";
            boxes[2, 2].Enabled = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Bingo";
            Size = new Size(200, 320);
            MinimumSize = Size;
            MaximumSize = Size;
            ClientSize = new Size(200, 301);


            Button btn_general = new Button(); //gomb létrehozása
            btn_general.Text = "Kártya generálása";
            btn_general.Size = new Size(150, 50);
            btn_general.Location = new Point(25, 10);
            btn_general.Click += new System.EventHandler(btn_general_Click);
            Controls.Add(btn_general); //vezérlőkhez adja a gombot


            Button btn_save = new Button(); 
            btn_save.Text = "Mentés";
            btn_save.Size = new Size(150, 50);
            btn_save.Location = new Point(25, 231);
            btn_save.Click += new System.EventHandler(btn_save_Click);
            Controls.Add(btn_save);


            
            txb_filename.Text = "bingo.txt";
            txb_filename.Size = new Size(150, 50);
            txb_filename.Location = new Point(25, 211);
            Controls.Add(txb_filename);

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    boxes[i, j] = new TextBox(); //bele tesz egy textbox-ot a mátrixba
                    boxes[i, j].Text = i.ToString() + j.ToString();
                    boxes[i, j].Size = new Size(25, 25);
                    boxes[i, j].Location = new Point(25 + i * 31, 60 + j * 31); //i = oszlop és j = sor
                    boxes[i, j].Visible = false;
                    boxes[i, j].AutoSize = false;
                    boxes[i, j].TextAlign = HorizontalAlignment.Center;

                    Controls.Add(boxes[i, j]);
                }
            }
        }
        private void btn_general_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();



            for (int i = 0; i < 5; i++) //i az y koordináta (oszlop)
            {
                HashSet<int> set = new HashSet<int>();

                for (int j = 0; j < 5; j++) //j az x koordináta (sor)
                {
                    int halmazHossz = set.Count; //(indulásnál 0)
                    int a = 0;

                    while (set.Count != halmazHossz + 1) //csinál új számot
                    {
                        a = rnd.Next(tol[i], ig[i]);
                        set.Add(a);
                    }

                    boxes[i, j].Text = a.ToString();
                    szamok[i, j] = a;
                    boxes[i, j].Visible = true;
                    
                }
            }
            kozepso();

            foreach (var item in boxes)
            {
                item.LostFocus += new EventHandler(boxes_TextChange); //elvesszük a kijelölést a textboxról, akkor futtatja le az ellenőrzést
            }


        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(txb_filename.Text, false, Encoding.UTF8);

            int ii = 0;

            foreach (var item in boxes)
            {
                ii++;
                if (ii % 5 == 0)
                {
                    sw.WriteLine(item.Text);
                    continue;
                }
                else
                {
                    sw.Write($"{item.Text}; ");
                }
            }
            sw.Close();
        }
        private void boxes_TextChange(object sender, EventArgs e)
        {
            //adott oszlopban megfelelőek-e a számok
            try
            {
                bool hiba = false;

                for (int i = 0; i < 5; i++)
                {
                    if (hiba) break; //ha hiba volt, akkor kilép

                    for (int j = 0; j < 5; j++)
                    {
                        if (i == 2 && j == 2) continue; //akkor csak menjen a következőre

                        if (int.Parse(boxes[i, j].Text) < tol[i] || int.Parse(boxes[i, j].Text) >= ig[i]) //nem megfelelő szám
                        {
                            boxes[i, j].Text = szamok[i, j].ToString(); //visszateszi az eredeti számra, hogyha rosszra lett változtatva
                            kozepso();
                            hiba = true;
                        }

                        HashSet<string> vizsga = new HashSet<string>();

                        for (int k = 0; k < 5; k++)
                        {
                            vizsga.Add(boxes[i, k].Text);
                            //if (j == k) continue; //önmagát ne vizsgálja
                        }
                        if (vizsga.Count != 5)
                        {
                            for (int k = 0; k < 5; k++)
                            {
                                boxes[i, k].Text = szamok[i, k].ToString();
                            }
                            kozepso();
                            hiba = true;
                        }
                    }

                        for (int k = 0; k < 5; k++)
                        {
                            if (i == 2 && k == 2) continue; //középsőt ugorja át
                            szamok[i, k] = int.Parse(boxes[i, k].Text);
                        }

                    
                }
            }
            catch (Exception)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        boxes[i, j].Text = szamok[i, j].ToString();
                    }
                    kozepso();
                }
            }
            
        }
    }
}
