# <p align=center>Ciklusok</p>

```c#
            //előltesztelős ciklus

            while (!fromFile.EndOfStream)    /*amíg nincs vlge*/
            {
                adatok.Add(fromFile.ReadLine());
                toFile.WriteLine($"{fromFile.ReadLine()} - ");
            }
            fromFile.Close();
            toFile.Close();


            //számláló ciklus

            for (int i = 0; i < sorok.Length; i++)  /* i++ -> i = i+1 */
            {
                Console.WriteLine(sorok[i]);
            }


            //bejáró cilus - halmazok
            foreach (string item in sorok) 
            {
                Console.WriteLine(item);
            }


            adat[] neve = new adat[sorok.Length];
            for (int i = 0; i < sorok.Length; i++)
            { 
                neve[i] = new adat(i, sorok[i]);
            }

```