using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fromSarokban
{
    public partial class Form1 : Form
    {
        static int X = 0;
        static int Y = 0;
        public Form1()
        {
            InitializeComponent();
        }

        public void mozgatas(int x, int y)
        {
            this.Location = new Point(x, y);
        }

        private void balfel_btn_Click(object sender, EventArgs e)
        {
            X = 0;
            Y = 0;
            mozgatas(X, Y);
        }

        private void jobbfel_btn_Click(object sender, EventArgs e)
        {
            X = Screen.PrimaryScreen.WorkingArea.Width - Width;  //form szélességet vonjuk ki a képernyőből
            Y = 0;
            mozgatas(X, Y);
        }

        private void balle_btn_Click(object sender, EventArgs e)
        {
            X = 0;
            Y = Screen.PrimaryScreen.WorkingArea.Height - Height;
            mozgatas(X, Y);
        }

        private void jobble_btn_Click(object sender, EventArgs e)
        {
            X = Screen.PrimaryScreen.WorkingArea.Width - Width;
            Y = Screen.PrimaryScreen.WorkingArea.Height - Height;
            mozgatas(X, Y);
        }
    }
}
