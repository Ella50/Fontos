using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator2024
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region Változók
        static int sign = 0;
        static int dot = 0; //előjel/tizedes vessző van-e benne

        static double result = 0.0;
        static bool resultBool = false;
        static bool operatorBool = false;
        static string oper = "";

        static int btnColorIndex = 0; //button1
        static List<Color> listColor = new List<Color>();



        #endregion

        private void operation()
        {
            if (oper == "+")
            {
                result += double.Parse(txbDisplay.Text);
            }

            else if (oper == "/" && double.Parse(txbDisplay.Text) != 0)
            {
                result /= double.Parse(txbDisplay.Text);
            }

            if (oper == "*")
            {
                result *= double.Parse(txbDisplay.Text);
            }

            if (oper == "-")
            {
                result -= double.Parse(txbDisplay.Text);
            }

            string resultText = result.ToString();
            int resultlength = length(resultText);
            if (resultText.Length < resultlength)
            {
                txbDisplay.Text = resultText;
            }
            else
            {
                txbDisplay.Text = resultText.Substring(0, resultlength);
            }
            resultBool = true;

            if (double.Parse(txbDisplay.Text) == 0 || Math.Abs(result) > 99999999999999 || Math.Abs(result) < 0.000000000000)
            {
                txbDisplay.Text = "Hiba";
                result = 0;
                resultBool = false;
                operatorBool = false;
            }
        }


        static int length(string displaytext)
        {
            if (displaytext.Contains(","))
            {
                dot = 1;
            }
            else
            {
                dot = 0;
            }

            if (displaytext.Contains("-"))
            {
                sign = 1;
            }
            else
            {
                sign = 0;
            }

            return 14 + sign + dot;
        }
        private void Display(string btn)
        {
            string textDisplay = txbDisplay.Text;
            int maxlength = length(textDisplay);

            if (btn == "+-")
            {
                txbDisplay.Text = (double.Parse(textDisplay)* -1).ToString();
                return;
            }

            if (btn == "C")
            {
                txbDisplay.Text = "0";
                return;
            }

            if (btn == "AC")
            {
                txbDisplay.Text = "0";
                result = 0;
                return;
            }

            if (btn == "s") //gyök
            {
                result = Math.Sqrt(double.Parse(textDisplay));

                string resultText = result.ToString();
                int resultlength = length(resultText); //szöveg hoszzúsága (nem lehet több 14-nél)

                if (resultText.Length < resultlength)
                {
                    txbDisplay.Text = resultText;
                }
                else
                {
                    txbDisplay.Text = resultText.Substring(0, resultlength);
                }
                resultBool =  true;
                operatorBool = false;
                return;
            }

            if (btn == "/")
            {
                if (!resultBool && !operatorBool)
                {
                    result = double.Parse(textDisplay);
                    operatorBool = true;
                    resultBool = true;
                    oper = "/";
                    return;
                }

                else if (resultBool && !operatorBool)
                {
                    operatorBool = true;
                    oper = "/";
                    return;
                }

                else if (resultBool && operatorBool)
                {
                    operation();
                    oper = "/";
                }
                return;
                
            }

            if (btn == "*")
            {
                if (!resultBool && !operatorBool)
                {
                    result = double.Parse(textDisplay);
                    operatorBool = true;
                    resultBool = true;
                    oper = "*";
                    return;
                }

                else if (resultBool && !operatorBool)
                {
                    operatorBool = true;
                    oper = "*";
                    return;
                }

                else if (resultBool && operatorBool)
                {
                    operation();
                    oper = "*";
                }
                return;
            }


            if (textDisplay.Length == maxlength)
            {
                return;
            }

            else 
            {
                if (btn == "," && dot == 0)
                {
                    txbDisplay.Text += ",";
                    return;
                } 

                else if (btn == "," && dot == 1)
                {
                    return;
                }

                else
                {
                    if (textDisplay == "0")
                    {
                        txbDisplay.Text = btn;
                    }

                    else
                    {
                        txbDisplay.Text += btn;
                    }
                }
            }
        }

      
        private void Form1_Load(object sender, EventArgs e)
        {
            txbDisplay.Text = "0";
            listColor.Add(Color.White);
            listColor.Add(Color.Magenta);
            listColor.Add(Color.LavenderBlush);
            listColor.Add(Color.Pink);
            listColor.Add(Color.Coral);
            txbDisplay.BackColor = listColor[btnColorIndex];
        }

        private void btnOff_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e) //btnColor
        {
            btnColorIndex++;
            if (btnColorIndex >= listColor.Count) 
            {
                btnColorIndex = 0;
            }
            txbDisplay.BackColor = listColor[btnColorIndex];
        }

        #region Billentyűk
        private void btnPlusMinus_Click(object sender, EventArgs e)
        {
          Display("+-");
        }

        private void btnComma_Click(object sender, EventArgs e)
        {
            Display(",");
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            Display("0");
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            Display("1");
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            Display("2");
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            Display("3");
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            Display("4");
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            Display("5");
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            Display("6");
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            Display("7");
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            Display("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Display("9");
        }

        private void btnC_Click(object sender, EventArgs e)
        {
            Display("C");
        }

        private void btnCA_Click(object sender, EventArgs e)
        {
            Display("AC");
        }

        private void btnGyok_Click(object sender, EventArgs e)
        {
            Display("s");
        }

        private void btnDivi_Click(object sender, EventArgs e)
        {
            Display("/");
        }

        private void btnMulti_Click(object sender, EventArgs e)
        {
            Display("*");
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            Display("-");
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            Display("+");
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            Display("=");
        }
        #endregion
    }
}
