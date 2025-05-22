using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace matrix
{
    internal class Program
    {
        static void kiir(int[,] a)
        {
           
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write($"{ a[i, j] } \t");

                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------\n");
        }

        static void Main(string[] args)
        {


            #region 1. feladat: Mátrix feltöltése

            int oszlop;
            int sor;

            do
            {
                Console.Write("Oszlopok száma: ");
                oszlop = int.Parse(Console.ReadLine());

                Console.Write("Sorok száma: ");
                sor = int.Parse(Console.ReadLine());

            } while (oszlop < 1 || oszlop > 10 || sor < 1 || sor > 10);




            int[,] matrix = new int[oszlop, sor];

            Random random = new Random();

            for (int i = 0; i < oszlop; i++)
            {
                for (int j = 0; j < sor; j++)
                {
                    matrix[i, j] = random.Next(1, 50);
                }

            }


            #endregion



            #region 2. feladat: Mátrix kiíratása szépen
            Console.WriteLine("2. feladat: Mátrix ");

            /*
            for (int i = 0; i < oszlop; i++)
            {
                for (int j = 0; j < sor; j++)
                {
                    Console.Write(matrix[i, j] + "\t");

                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------");
            */

            kiir(matrix);

            #endregion



            #region 3. feladat: Tükrözés a főátlóra 
            Console.WriteLine("3. feladat: Tükrözés a főátlóra ");

            // (csak szimmetrikus mátrixxal lehet tükrözni)

            if (oszlop != sor)
            {
                Console.WriteLine("A mátrixot nem lehet tükrözni");
            }
            else
            {
                int[,] tükrözveFő = new int[oszlop, sor];
                for (int i = 0; i < oszlop; i++)
                {
                    for (int j = 0; j < sor; j++)
                    {
                        tükrözveFő[j, i] = matrix[i, j];
                    }

                }

                //kiíratás
                kiir(tükrözveFő);


                #endregion



                #region 4. feladat: Tükrözés a mellékátlóra 
                
                Console.WriteLine("4. feladat: Tükrözés a mellékátlóra ");

                /*NEM JÓ
                if (oszlop != sor)
                {
                    Console.WriteLine("A mátrixot nem lehet tükrözni");
                }
                else
                {
                    int[,] tükrözveMellék = new int[oszlop, sor];
                    for (int i = 0; i < oszlop; i++)
                    {
                        for (int j = sor - 1; j >= 0; j--)
                        {
                            tükrözveMellék[j, i] = matrix[i, j];
                        }
                        
                    }
                    kiir(tükrözveMellék);
                }

                */
               

                
                #endregion


                Console.ReadKey();
            }
        }
    }
}
