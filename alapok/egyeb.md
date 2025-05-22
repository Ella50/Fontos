
## Random számok
```c# 
List<int> nyeroSzamok = new List<int>();
            Random veletlenszam = new Random();
            int nyeroSzam;

            for (int i = 0; i < 5; i++)
            {
                do
                {
                    nyeroSzam = veletlenszam.Next(1, 90);
                }
                while (nyeroSzamok.Contains(nyeroSzam));

                nyeroSzamok.Add(nyeroSzam);
            }
```