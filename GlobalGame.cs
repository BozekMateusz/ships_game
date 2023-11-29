using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statki
{
    static class GlobalGame
    {
        public static Map player1map;
        public static Map player2map;

        public static int turn = 0;
        public static fieldState target;
        private static string[] descriptions = {"Trafiony!","Pudło!","Trafiony, zatopiony!!","Ostatni oktęt zatopiony!"};

        public static bool VICTORYGRANTED = false;

        public static bool Strike(Field field, int Ycord, int Xcord, Map map)
        {
            int description = 0;

            if(field.state == fieldState.OccupiedUndiscovered || field.state == fieldState.Undiscovered || field.state == fieldState.Warship)
            {
                if(field.state == fieldState.OccupiedUndiscovered)
                {
                    field.state = fieldState.OccupiedStriked;
                    bool drowned = true;

                    for (int y = -1; y <= 1; y++)
                    {
                        for (int x = -1; x <= 1; x++)
                        {
                            if(!new List<int> {0,1,2,3,4,5,6,7,8,9}.Contains(Ycord + y) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(Xcord + x) || map.fields[Ycord + y,Xcord + x].state != fieldState.OccupiedUndiscovered)
                            {
                                
                            }
                            else
                            {
                                drowned = false;
                            }
                        }
                    }

                    if(drowned)
                    {
                        field.state = fieldState.WarshipDestroyed;


                        List<Cord> ship = new List<Cord>();
                        ship.Add(new Cord(Ycord, Xcord));


                        int FixedYcord = Ycord;
                        int FixedXcord = Xcord;

                        for(int i = 0; i<4; i++)
                        {
                            for (int x = -1; x <= 1; x++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(Ycord + y) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(Xcord + x) || map.fields[FixedYcord + y, FixedXcord + x].state != fieldState.OccupiedStriked)
                                    {
                                        
                                    }
                                    else
                                    {
                                        ship.Add(new Cord(FixedYcord + y, FixedXcord + x));
                                        map.fields[FixedYcord + y, FixedXcord + x].state = fieldState.WarshipDestroyed;

                                        FixedXcord = ship[ship.Count - 1].x;
                                        FixedYcord = ship[ship.Count - 1].y;
                                    }
                                }
                            }
                        }

                        

                        foreach(Cord element in ship)
                        {
                            for (int x = -1; x <= 1; x++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    if(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(element.y + y) && new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(element.x + x))
                                    {
                                        map.fields[element.y + y, element.x + x].state = fieldState.Striked;
                                    }
                                    
                                }
                            }
                        }

                        foreach (Cord element in ship)
                        {                     
                            map.fields[element.y, element.x].state = fieldState.WarshipDestroyed;
                        }


                        map.aliveBattleships--;
                        if(map.aliveBattleships == 0)
                        {
                            description = 3;
                            VICTORYGRANTED = true;
                        }
                        else
                        {
                            description = 2;
                        }
                        
                    }
                    else
                    {
                        field.state = fieldState.OccupiedStriked;
                        description = 0;
                    }


                    
                }
                else if(field.state == fieldState.Undiscovered)
                {
                    field.state = fieldState.Striked;
                    description = 1;
                    changeTurn();
                }
                else if(field.state == fieldState.Warship)
                {

                    field.state = fieldState.OccupiedStriked;
                    bool drowned = true;

                    for (int y = -1; y <= 1; y++)
                    {
                        for (int x = -1; x <= 1; x++)
                        {
                            if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(Ycord + y) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(Xcord + x) || map.fields[Ycord + y, Xcord + x].state != fieldState.OccupiedUndiscovered)
                            {

                            }
                            else
                            {
                                drowned = false;
                            }
                        }
                    }

                    if (drowned)
                    {
                        field.state = fieldState.WarshipDestroyed;


                        List<Cord> ship = new List<Cord>();
                        ship.Add(new Cord(Ycord, Xcord));


                        int FixedYcord = Ycord;
                        int FixedXcord = Xcord;

                        for (int i = 0; i < 4; i++)
                        {
                            for (int x = -1; x <= 1; x++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(Ycord + y) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(Xcord + x) || map.fields[FixedYcord + y, FixedXcord + x].state != fieldState.OccupiedStriked)
                                    {

                                    }
                                    else
                                    {
                                        ship.Add(new Cord(FixedYcord + y, FixedXcord + x));
                                        map.fields[FixedYcord + y, FixedXcord + x].state = fieldState.WarshipDestroyed;

                                        FixedXcord = ship[ship.Count - 1].x;
                                        FixedYcord = ship[ship.Count - 1].y;
                                    }
                                }
                            }
                        }



                        foreach (Cord element in ship)
                        {
                            for (int x = -1; x <= 1; x++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    if (new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(element.y + y) && new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(element.x + x))
                                    {
                                        map.fields[element.y + y, element.x + x].state = fieldState.Striked;
                                    }

                                }
                            }
                        }

                        foreach (Cord element in ship)
                        {
                            map.fields[element.y, element.x].state = fieldState.WarshipDestroyed;
                        }


                        map.aliveBattleships--;
                        if (map.aliveBattleships == 0)
                        {
                            description = 3;
                            VICTORYGRANTED = true;
                        }
                        else
                        {
                            description = 2;
                        }

                    }
                    else
                    {
                        field.state = fieldState.OccupiedStriked;
                        description = 0;
                    }
                    field.state = fieldState.OccupiedStriked;
                    description = 0;
                }
                TurnSummary(description);
                return true;              
            }
            else
            {
                //błąd
                return false;
            }
        }

        public static void TurnSummary(int descID)
        {
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            if(turn != 0)
            {
                Console.WriteLine("Rezultat: " + descriptions[descID]);
            }
            else
            {
                Console.WriteLine("Rezultat: " + descriptions[descID]);
            }
            
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
            player1map.drawMap();
            Console.WriteLine("");
            player2map.drawMap();
        }

        private static void changeTurn()
        {
            if(turn == 0)
            {
                turn = 1;
            }
            else
            {
                turn = 0;
            }
        }
    }
}
