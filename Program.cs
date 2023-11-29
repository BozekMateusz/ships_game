using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statki
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalGame.player1map = new Map();
            GlobalGame.player1map.drawMap();
            GlobalGame.player2map = new Map(true);
            GlobalGame.player2map.drawMap();
            Console.Clear();
            GlobalGame.player1map.drawMap();
            Console.WriteLine("");
            GlobalGame.player2map.drawMap();

            while (!GlobalGame.VICTORYGRANTED)
            {
                if (GlobalGame.turn == 0)
                {
                    int Ycord = 0;
                    int Xcord = 0;
                    int flag = 0;

                    while ((!new List<int> {0,1,2,3,4,5,6,7,8,9}.Contains(Ycord) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(Ycord)) || flag == 0)
                    {
                        flag = 1;
                        Console.WriteLine("Podaj pole na które chcesz strzelić: ");
                        Console.Write("Podaj koordynat pionowy (od a do j): ");
                        Ycord = (int)Console.ReadLine().ToLower()[0] - 97;
                        Console.Write("Podaj koordynat poziomy (od 0 do 9): ");
                        Xcord = int.Parse(Console.ReadLine());
                    }
                    flag = 0;

                    GlobalGame.Strike(GlobalGame.player2map.fields[Ycord, Xcord], Ycord, Xcord, GlobalGame.player2map);
                }
                else
                {
                    int randomWait = new Random().Next(6, 54);
                    int randomWait2 = new Random().Next(6, 54);
                    System.Threading.Thread.Sleep(randomWait);
                    int Ycord = new Random().Next(0, 10);
                    System.Threading.Thread.Sleep(randomWait2);
                    int Xcord = new Random().Next(0, 10);
                    GlobalGame.Strike(GlobalGame.player1map.fields[Ycord, Xcord], Ycord, Xcord, GlobalGame.player1map);
                }
            }

            if (GlobalGame.VICTORYGRANTED)
            {
                Console.Write("\n\n\n\nGRA ZAKOŃCZONA\n\n\n\n");
            }

            Console.ReadKey();
        }
    }
}
