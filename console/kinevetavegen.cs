using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace kinevet_a_vegen
{
    internal class Program
    {
        internal class data
        {
            public string szin;
            public int startingp;
            public int currentp;
            public bool kint = false;

            public data(string color, int staringp)
            {
                szin = color;
                this.startingp = staringp;
                this.currentp = staringp;
            }

        }
        static void Main(string[] args)
        {


            List<int> lepesek = new List<int>();

            StreamReader beolvas = new StreamReader("kocka.txt");

            while(!beolvas.EndOfStream)
            {
                int szam = int.Parse(beolvas.ReadLine());
                lepesek.Add(szam);
            }

            beolvas.Close();
    

            List<data> players = new List<data>();

            players.Add(new data("piros", 0));
            players.Add(new data("kék", 10));
            players.Add(new data("sárga", 20));
            players.Add(new data("zöld", 30));


            int currentplayer = 0;

            foreach(int lepes in lepesek) 
            {
                if (players[currentplayer].kint)
                {
                    players[currentplayer].currentp += lepes;
                }
                else if (!players[currentplayer].kint && lepes == 6)
                {
                    players[currentplayer].kint = true;
                }

                foreach(data player in players)
                {
                    if (players[currentplayer].currentp == player.currentp && players[currentplayer] != player)
                    {
                        player.kint = false;
                        player.currentp = player.startingp;
                        Console.WriteLine($"{players[currentplayer].szin} kiütötte {player.szin}");
                    }
                }


                if (players[currentplayer].currentp >= players[currentplayer].startingp+40) 
                {
                    Console.WriteLine($"{players[currentplayer].szin} nyert");
                    break;
                }
                currentplayer = (currentplayer++) % players.Count;
            }







            Console.ReadKey();
        }
    }
}
