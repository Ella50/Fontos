# <p align=center>Függvény</p>

```c#
public override string ToString()
        {
            return $"Azon: {azon}\nNév: {nev}\nHossz: {hossz}\nMélység: {melyseg}\nTelepülés: {telepules}\nVédettség: {vedettseg}".ToString(); //szépen írja ki az adatokat (mintha Console.Writeline lenne)
        }
```

```c#
static int Ado(string adosav, int alapterulet)
        {
            int fizetendoAdo;
            if (adosav == "A") 
            {
                fizetendoAdo = alapterulet * 800;
            }
            else if (adosav == "B")
            {
                fizetendoAdo = alapterulet * 600;
            }
            else
            {
                fizetendoAdo = alapterulet * 100;
            }
            
            if (fizetendoAdo < 10000) 
            {
                fizetendoAdo = 0;
            }
            return fizetendoAdo;
        }
```

```c#
public int MaxFuggoleges()
        {
            int maxfugg = 0;
            for (int j = 0; j < oszlopokDb; j++)
            {
                int hossz = 0;
                for (int i = 0; i < sorokDb; i++)
                {
                    if (racs[i, j] == '-') hossz++;
                    else hossz = 0;
                    maxfugg = Math.Max(maxfugg, hossz);
                }
            }
            return maxfugg;
        }
```
```c#
public void VizszintesStat()
{
        Dictionary<int, int> stat = new Dictionary<int, int>();

        for (int i = 0; i < sorokDb; i++)
        {
            int hossz = 0;
            for (int j = 0; j < oszlopokDb; j++)
            {
                if (racs[i, j] == '-')
                {
                    hossz++;
                }
                else
                {
                    if (hossz >= 2)
                    {
                        if (!stat.ContainsKey(hossz))
                        {
                            stat[hossz] = 0;
                        }
                        stat[hossz]++;
                    }
                    hossz = 0;
                }
            }
            if (hossz >= 2)
            {
                if (!stat.ContainsKey(hossz))
                {
                    stat[hossz] = 0;
                }
                stat[hossz]++;
            }
        }
        Console.WriteLine("8. feladat: Víszszintes szavak statisztikája");
        foreach (var item in stat.OrderBy(x => x.Key))
        {
            Console.WriteLine($"\t{item.Key} betűs: {item.Value} db");
        }
}

```
```c#
private void Sorszamozas()
{
            int szam = 1;
            for (int i = 0; i < sorokDb; i++)
            {
                for (int j = 0; j < oszlopokDb; j++)
                {
                    if (racs[i, j] == '-')
                    {
                        bool ujSzam = false;

                        if (j == 0 || racs[i, j - 1] == '#')
                        {
                            if (j + 1 < oszlopokDb && racs[i, j + 1] == '-')
                            {
                                ujSzam = true;
                            }
                        }

                        if (i == 0 || racs[i - 1, j] == '#')
                        {
                            if (i + 1 < sorokDb && racs[i + 1, j] == '-')
                            {
                                ujSzam = true;
                            }
                        }

                        if (ujSzam)
                        {
                            sorszamok[i, j] = szam++;
                        }
                    }
                }
            }
}
```
```c#
public void MegjelenitSorszamok()
{
            Console.WriteLine("9. feladat: A keresztrejtvény számokkal");
            for (int i = 0; i < sorokDb; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < oszlopokDb; j++)
                {
                    if (sorszamok[i, j] > 0) Console.Write($"{sorszamok[i, j]:00}");
                    else Console.Write(racs[i, j] == '#' ? "##" : "[]");
                }
                Console.WriteLine();
            }
}
```

```c#
public void Megjelenites()
        {
            Console.WriteLine("6. feladat: A beolvasott keresztjretvény");
            for (int i = 0; i < sorokDb; i++)
            {
                Console.Write("\t");
                for (int j = 0; j < oszlopokDb; j++)
                {
                    Console.Write(racs[i, j] == '#' ? "##" : "[]");
                }
                Console.WriteLine();
            }

        }
```