# <p align=center> Windows form </p>

```c#

public int[] tol = new int[] { 1, 16, 31, 46, 61 };
public TextBox[,] boxes = new TextBox[5, 5];
public TextBox txb_filename = new TextBox();

filelabel.Text = $"Barlangok száma: {lista.Count} db";

private void hossz_txtb_TextChanged(object sender, EventArgs e)
    {
       int ujhossz = int.Parse(hossz_txtb.Text);
       hossz_txtb.Clear();
       mentes_btn.Enabled = false;
    }


private void btnOff_Click(object sender, EventArgs e)
    {
         Application.Exit();
    }


 foreach (var item in boxes)
    {
        item.LostFocus += new EventHandler(boxes_TextChange); //elvesszük a kijelölést a textboxról, akkor futtatja le az ellenőrzést
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


```

## Gomb létrehozás
```c#
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
Controls.Add(btn_general);
```


## Mátrix
```c#

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


```

## Fájlba kiírás
```c#
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

```