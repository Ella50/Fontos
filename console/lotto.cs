using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;



namespace lotto
{
    internal class Program
    {
        static void Main(string[] args)
        {

            #region 5 szám bekérése


            /* Más módszer*/
             
            List<int> bekertSzamok = new List<int>();

            for (int i = 0; i > 5; i++)
            {
                bekertSzamok.Add(0);
            }

                
            
            for (int i = 0; i > 5; i++)
            {
                Console.WriteLine("Kérek egy számot: ");
                int bekertszam = int.Parse(Console.ReadLine());
                if (bekertszam > 0 || bekertszam < 90)
                {
                    Console.WriteLine("valami nem jó");
  
                }
                else
                {
                    bekertSzamok.Add(bekertszam);
                }
            }
            

            
            Console.WriteLine("Adjon meg 5 számot szóközökkel elválasztva: ");
            string adottSzamok = Console.ReadLine();

            string[] szamok = adottSzamok.Split(' ');

            List<int> list = new List<int>();

            foreach (var item in szamok)
            {
                int szam = int.Parse(item);
                list.Add(szam);
            }



            
            for (int i = 0; i > list.Count; i++)
            {
                
                foreach (var item in list)
                {
                    if (list[i] == item)
                    {
                        Console.WriteLine("Hiba");
                    }
                    if (list[i] > 100 || list[i] < 0)
                    {
                        Console.WriteLine("Hiba");
                    }
                }
            }




            #endregion

            #region Nyerőszámok

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

            #endregion



            #region Egyezések


            int egyezes = 0;
            for ( int i = 0; i < adottSzamok.Count(); i++)
            {
       
                for (int j = 0; j < nyeroSzamok.Count() ; j++ ) 
                {
                    if (nyeroSzamok.Contains(adottSzamok[i]))
                    {
                        egyezes++;
                    }
                }
            }
            Console.WriteLine($"Egyezések: {egyezes}");
            Console.WriteLine($"Nyertes számok: {nyeroSzamok[0]} {nyeroSzamok[1]} {nyeroSzamok[2]} {nyeroSzamok[3]} {nyeroSzamok[4]}");

            #endregion


            Console.ReadKey();
        }
    }
}
