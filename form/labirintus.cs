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

namespace LabirintusGUI
{
    public partial class Form1 : Form
    {
        public CheckBox[,] boxes = new CheckBox[20, 20];  //mátrix
        public Form1()
        {
            InitializeComponent();
        }

        public void Torles(int oszlop, int sor)
        {
            for (int i = 0; i < oszlop; i++)
            {
                for (int j = 0; j < sor; j++)
                {
                    Controls.Remove(boxes[i, j]); //cleareli az egész mátrixot (nagyjából működik)
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Text = "Labirintus készítő";
            Size = new Size(500, 600);
            MinimumSize = Size;
            MaximumSize = Size;
            ClientSize = new Size(500, 600);


            Button btn_general = new Button();
            btn_general.Text = "Induló labirintus létrehozása";
            btn_general.Size = new Size(150, 30);
            btn_general.Location = new Point(25, 60);
            btn_general.Click += new System.EventHandler(btn_general_Click);
            Controls.Add(btn_general);


            Button btn_save = new Button();
            btn_save.Text = "Labirintus mentése";
            btn_save.Size = new Size(150, 30);
            btn_save.Location = new Point(180, 60);
            btn_save.Click += new System.EventHandler(btn_save_Click);
            Controls.Add(btn_save);

        }

        private void btn_general_Click(object sender, EventArgs e)
        {
            

            int sorok = int.Parse(sor.Text);
            int oszlopok = int.Parse(oszlop.Text);

            Torles(sorok, oszlopok);

            for (int i = 0; i < oszlopok; i++)
            {
                for (int j = 0; j < sorok; j++)
                {

                    boxes[i, j] = new CheckBox(); 
                    boxes[i, j].Size = new Size(15, 15);
                    boxes[i, j].Location = new Point(25 + i * 20, 100 + j * 20); 
                    Controls.Add(boxes[i, j]);
                }
            }

            for (int i = 0; i < oszlopok; i++) //i az y koordináta (oszlop)
            {

                  for (int j = 0; j < sorok; j++) //j az x koordináta (sor)
                  {
                        if (i == 0 || j == 0 || i == (oszlopok-1) || j == (sorok-1))
                        {
                            boxes[i, j].Checked = true;
                            boxes[i, j].Enabled = false;
                            
                            if((i == 0 && j == 1) || (i == (oszlopok-1) && j == (sorok-2)))
                            {
                                boxes[i, j].Checked = false;
                            }
                        }
                        
                        else
                        {
                            boxes[i, j].Checked = false;
                    }


                  }
            }


                }
        private void btn_save_Click(object sender, EventArgs e)
        {
            
            StreamWriter sw = new StreamWriter($"Lab{index.Text}.txt", false, Encoding.UTF8);
            try
            {
            int sorok = int.Parse(sor.Text);
            int oszlopok = int.Parse(oszlop.Text);

            for (int i = 0; i < oszlopok; i++)
            {
                for (int j = 0; j < sorok; j++)
                {
                    if (boxes[i, j].Checked == true)
                    {
                        sw.Write("X");
                    }
                    else
                    {
                        sw.Write(" ");
                    }
                }
                sw.WriteLine(); //új sor kezdése
            }
                MessageBox.Show("Állomány mentése sikeres");
                sw.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Állomány mentése sikertelen");
            }

        }
    }
}
