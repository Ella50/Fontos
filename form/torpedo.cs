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

namespace Torpedó
{
    public partial class Form1 : Form
    {
        static CheckBox[,] checkboxes = new CheckBox[10, 10];
        static int[] allowedLengths = { 1, 1, 1, 1, 1, 0, 0, 0, 0, 0 };
        static int[] lengthsOnBoard = new int[10];

        public Form1()
        {
            InitializeComponent();

            #region Create checboxes
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    checkboxes[i, j] = new CheckBox();
                    checkboxes[i, j].Location = new Point(65 + i * 20, 100 + j * 20);
                    checkboxes[i, j].Size = new Size(20, 20);
                    this.Controls.Add(checkboxes[i, j]);
                }
            }
            #endregion
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            bool isValidBoard = validateBoard(checkboxes);

            #region Save the board
            if(isValidBoard)
            {
                StreamWriter writer = new StreamWriter(fileNameInput.Text + ".txt", true);
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        writer.Write(checkboxes[j, i].Checked ? "1" : "0");
                    }
                    writer.Write("\n"); //;?
                }
                
                writer.Close();
                ClearBoxes();
                MessageBox.Show("A hajók elhelyezése sikeresen mentve!");
            } else
            {
                MessageBox.Show("A hajók hossza nem megfelelő!");
            }
            #endregion
        }

        private bool validateBoard(CheckBox[,] board)
        {
            // Reset lengthsOnBoard array
            Array.Clear(lengthsOnBoard, 0, lengthsOnBoard.Length);

            // Create a temporary array to track counted checkboxes
            bool[,] counted = new bool[10, 10];

            #region Validate the board
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (!checkboxes[i, j].Checked || counted[i, j]) continue;

                    // Check horizontal length
                    int horizontalLength = 1;
                    while (j + horizontalLength < 10 && checkboxes[i, j + horizontalLength].Checked && !counted[i, j + horizontalLength])
                    {
                        // Ensure no adjacent vertical checkboxes are checked
                        if ((i > 0 && checkboxes[i - 1, j + horizontalLength].Checked) ||
                            (i < 9 && checkboxes[i + 1, j + horizontalLength].Checked))
                        {
                            MessageBox.Show("A hajók nem érhetnek össze!");
                            return false;
                        }
                        counted[i, j + horizontalLength] = true; // Mark as counted
                        horizontalLength++;
                    }
                    if (horizontalLength > 1)
                    {
                        lengthsOnBoard[horizontalLength - 1]++;
                        continue;
                    }

                    // Check vertical length
                    int verticalLength = 1;
                    while (i + verticalLength < 10 && checkboxes[i + verticalLength, j].Checked && !counted[i + verticalLength, j])
                    {
                        // Ensure no adjacent horizontal checkboxes are checked
                        if ((j > 0 && checkboxes[i + verticalLength, j - 1].Checked) ||
                            (j < 9 && checkboxes[i + verticalLength, j + 1].Checked))
                        {
                            MessageBox.Show("A hajók nem érhetnek össze!");
                            return false;
                        }
                        counted[i + verticalLength, j] = true; // Mark as counted
                        verticalLength++;
                    }
                    if (verticalLength > 1)
                    {
                        lengthsOnBoard[verticalLength - 1]++;
                        continue;
                    }

                    // 1x1 ship
                    lengthsOnBoard[0]++;
                }
            }

            debug.Text = string.Join(", ", lengthsOnBoard);
            return allowedLengths.SequenceEqual(lengthsOnBoard);
            #endregion
        }

        private void LoadBoard(CheckBox[,] board)
        {
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    checkboxes[i, j].Checked = board[i, j].Checked;
                }
            }
        }

        private void ClearBoxes()
        {
            #region Clear checkboxes
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    checkboxes[i, j].Checked = false;
                }
            }
            #endregion
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            ClearBoxes();
        }

        private void fileNameInput_TextChanged(object sender, EventArgs e)
        {
            saveBtn.Enabled = fileNameInput.Text.Length > 0;
        }
    }
}