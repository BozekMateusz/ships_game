using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statki
{
    class Map
    {
        public Field[,] fields = new Field[10,10];
        public List<Structure> pieces = new List<Structure>();
        public int aliveBattleships = 10;

        public Map()
        {
            
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    fields[i, j] = new Field();
                    fields[i, j].state = fieldState.Undiscovered;
                }
            }
            drawMap();
            setup();
        }

        public Map(bool bot)
        {
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    fields[i, j] = new Field();
                    fields[i, j].state = fieldState.Undiscovered;
                }
            }
            setupBot();
        }

        public void drawMap()
        {
            Console.Write("*|0|1|2|3|4|5|6|7|8|9|\n");
            for (int i = 0; i < fields.GetLength(0); i++)
            {
                Console.Write(Convert.ToChar(i+65) + "|");

                for (int j = 0; j < fields.GetLength(1); j++)
                {
                    Console.Write(stateToSign(this.fields[i,j].state) + "|");
                }
                Console.Write("\n");
            }
 
        }



        private string stateToSign(fieldState state)
        {
            fieldState[] states = 
            { 
                fieldState.OccupiedUndiscovered,
                fieldState.Undiscovered,
                fieldState.OccupiedStriked,
                fieldState.Striked,
                fieldState.WarshipDestroyed,
                fieldState.Warship
            };


            int index = 9;
            string[] signs = { ".", ".", "X", "■", "F", "W"};
            for (int i = 0; i < states.Length; i++)
            {
                if(state == states[i])
                {
                    index = i;
                }
            }

            return signs[index];
        }

        private void setup()
        {
            bool correct = false;
            while (!correct)
            {
                Console.WriteLine("Podaj lokalizację statku 4-masztowego, zapisz lokalizację za pomocą 3 znaków, pierwszy znak będzie literą od a do j , drugi znak będzie cyfrą od 0 do 9, a 3 litera będzie kierunkiem ułożnia statku ('l' - w lewo, 'r' - w prawo, 'u' - w górę, 'd' - w dół), np statek z pola b2 do pola b5 będzie zapisany jako 'b1r' lub 'b4l': ");
                string raw = Console.ReadLine();
                if (raw.Length == 3 && new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" }.Contains(raw[0].ToString()) && new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }.Contains(raw[1].ToString()) && new List<string> { "u", "d", "l", "r" }.Contains(raw[2].ToString()))
                {
                    if (raw[2] == 'l')
                    {
                        bool legal = true;
                        for (int vector = 0; vector <= 3; vector++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                for (int x = -1; x <= 1; x++)
                                {
                                    if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x - vector)) || this.fields[(int)raw[0] - 97 + y, int.Parse(raw[1].ToString()) + x - vector].state != fieldState.Warship)
                                    {
                                        for(int i = 0; i < 4; i++)
                                        {
                                            if(!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) - i))
                                            {
                                                legal = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        legal = false;
                                    }
                                }
                            }
                        }

                        if (legal)
                        {
                            try
                            {
                                this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 1].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 2].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 3].state = fieldState.Warship;
                                this.pieces.Add(new Structure(4, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 1], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 2], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 3] }));
                                correct = true;
                            }
                            catch
                            {
                                legal = false;
                                correct = false;
                            }

                        }

                    }
                    else if (raw[2] == 'r')
                    {
                        bool legal = true;
                        for (int vector = 0; vector <= 3; vector++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                for (int x = -1; x <= 1; x++)
                                {
                                    if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x + vector)) || this.fields[(int)raw[0] - 97 + y, int.Parse(raw[1].ToString()) + x + vector].state != fieldState.Warship)
                                    {
                                        for (int i = 0; i < 4; i++)
                                        {
                                            if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + i))
                                            {
                                                legal = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        legal = false;
                                    }
                                }
                            }
                        }

                        if (legal)
                        {
                            try
                            {
                                this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 1].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 2].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 3].state = fieldState.Warship;
                                this.pieces.Add(new Structure(4, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 1], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 2], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 3] }));
                                correct = true;
                            }
                            catch
                            {
                                legal = false;
                                correct = false;
                            }
                        }

                    }
                    else if (raw[2] == 'd')
                    {
                        bool legal = true;
                        for (int vector = 0; vector <= 3; vector++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                for (int x = -1; x <= 1; x++)
                                {
                                    if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y + vector)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x)) || this.fields[(int)raw[0] - 97 + y + vector, int.Parse(raw[1].ToString()) + x].state != fieldState.Warship)
                                    {
                                        for (int i = 0; i < 4; i++)
                                        {
                                            if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + i) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString())))
                                            {
                                                legal = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        legal = false;
                                    }
                                }
                            }
                        }

                        if (legal)
                        {
                            try
                            {
                                this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97 + 1, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97 + 2, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97 + 3, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                this.pieces.Add(new Structure(4, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 + 1, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 + 2, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 + 3, int.Parse(raw[1].ToString())] }));
                                correct = true;
                            }
                            catch
                            {
                                legal = false;
                                correct = false;
                            }
                        }
                    }
                    else if (raw[2] == 'u')
                    {
                        bool legal = true;
                        for (int vector = 0; vector <= 3; vector++)
                        {
                            for (int y = -1; y <= 1; y++)
                            {
                                for (int x = -1; x <= 1; x++)
                                {
                                    if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y - vector)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x)) || this.fields[(int)raw[0] - 97 + y - vector, int.Parse(raw[1].ToString()) + x].state != fieldState.Warship)
                                    {
                                        for (int i = 0; i < 4; i++)
                                        {
                                            if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 - i) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString())))
                                            {
                                                legal = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        legal = false;
                                        correct = false;
                                    }
                                }
                            }
                        }

                        if (legal)
                        {
                            try
                            {
                                this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97 - 1, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97 - 2, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                this.fields[(int)raw[0] - 97 - 3, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                this.pieces.Add(new Structure(4, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 - 1, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 - 2, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 - 3, int.Parse(raw[1].ToString())] }));
                                correct = true;
                            }
                            catch
                            {
                                legal = false;
                            }
                        }

                    }
                    drawMap();
                }
            }

            correct = false;

            for (int count = 0; count <= 1; count++)
            {
                while (!correct)
                {
                    Console.WriteLine("Podaj lokalizację statku 3 - masztowego, zapisz lokalizację za pomocą 3 znaków, pierwszy znak będzie literą od a do j , drugi znak będzie cyfrą od 0 do 9, a 3 litera będzie kierunkiem ułożnia statku('l' - w lewo, 'r' - w prawo, 'u' - w górę, 'd' - w dół), np statek z pola b2 do pola b5 będzie zapisany jako 'b1r' lub 'b4l': ");
                    string raw = Console.ReadLine();
                    if (raw.Length == 3 && new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" }.Contains(raw[0].ToString()) && new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }.Contains(raw[1].ToString()) && new List<string> { "u", "d", "l", "r" }.Contains(raw[2].ToString()))
                    {
                        if (raw[2] == 'l')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 2; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x - vector)) || this.fields[(int)raw[0] - 97 + y, int.Parse(raw[1].ToString()) + x - vector].state != fieldState.Warship)
                                        {
                                            for (int i = 0; i < 3; i++)
                                            {
                                                if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) -i))
                                                {
                                                    legal = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            legal = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 1].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 2].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(3, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 1], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 2] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                    correct = false;
                                }

                            }

                        }
                        else if (raw[2] == 'r')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 2; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x + vector)) || this.fields[(int)raw[0] - 97 + y, int.Parse(raw[1].ToString()) + x + vector].state != fieldState.Warship)
                                        {
                                            for (int i = 0; i < 3; i++)
                                            {
                                                if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + i))
                                                {
                                                    legal = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            legal = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 1].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 2].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(3, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 1], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 2] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                    correct = false;
                                }
                            }

                        }
                        else if (raw[2] == 'd')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 2; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y + vector)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x)) || this.fields[(int)raw[0] - 97 + y + vector, int.Parse(raw[1].ToString()) + x].state != fieldState.Warship)
                                        {
                                            for (int i = 0; i < 3; i++)
                                            {
                                                if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + i) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString())))
                                                {
                                                    legal = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            legal = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97 + 1, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97 + 2, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(3, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 + 1, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 + 2, int.Parse(raw[1].ToString())] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                    correct = false;
                                }
                            }
                        }
                        else if (raw[2] == 'u')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 2; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y - vector)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x)) || this.fields[(int)raw[0] - 97 + y - vector, int.Parse(raw[1].ToString()) + x].state != fieldState.Warship)
                                        {
                                            for (int i = 0; i < 3; i++)
                                            {
                                                if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 - i) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString())))
                                                {
                                                    legal = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            legal = false;
                                            correct = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97 - 1, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97 - 2, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(3, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 - 1, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 - 2, int.Parse(raw[1].ToString())] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                }
                            }

                        }
                        drawMap();
                    }
                }

                correct = false;
            }

            for (int count = 0; count <= 2; count++)
            {
                while (!correct)
                {
                    Console.WriteLine("Podaj lokalizację statku 2-masztowego, zapisz lokalizację za pomocą 3 znaków, pierwszy znak będzie literą od a do j , drugi znak będzie cyfrą od 0 do 9, a 3 litera będzie kierunkiem ułożnia statku ('l' - w lewo, 'r' - w prawo, 'u' - w górę, 'd' - w dół), np statek z pola b2 do pola b5 będzie zapisany jako 'b1r' lub 'b4l': ");
                    string raw = Console.ReadLine();
                    if (raw.Length == 3 && new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" }.Contains(raw[0].ToString()) && new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }.Contains(raw[1].ToString()) && new List<string> { "u", "d", "l", "r" }.Contains(raw[2].ToString()))
                    {
                        if (raw[2] == 'l')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 1; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x - vector)) || this.fields[(int)raw[0] - 97 + y, int.Parse(raw[1].ToString()) + x - vector].state != fieldState.Warship)
                                        {
                                            for (int i = 0; i < 2; i++)
                                            {
                                                if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) - i))
                                                {
                                                    legal = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            legal = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 1].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(2, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) - 1] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                    correct = false;
                                }

                            }

                        }
                        else if (raw[2] == 'r')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 1; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x + vector)) || this.fields[(int)raw[0] - 97 + y, int.Parse(raw[1].ToString()) + x + vector].state != fieldState.Warship)
                                        {
                                            for (int i = 0; i < 3; i++)
                                            {
                                                if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + i))
                                                {
                                                    legal = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            legal = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 1].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(2, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString()) + 1] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                    correct = false;
                                }
                            }

                        }
                        else if (raw[2] == 'd')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 1; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y + vector)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x)) || this.fields[(int)raw[0] - 97 + y + vector, int.Parse(raw[1].ToString()) + x].state != fieldState.Warship)
                                        {
                                            for (int i = 0; i < 3; i++)
                                            {
                                                if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + i) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString())))
                                                {
                                                    legal = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            legal = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97 + 1, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(2, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 + 1, int.Parse(raw[1].ToString())] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                    correct = false;
                                }
                            }
                        }
                        else if (raw[2] == 'u')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 1; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y - vector)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x)) || this.fields[(int)raw[0] - 97 + y - vector, int.Parse(raw[1].ToString()) + x].state != fieldState.Warship)
                                        {
                                            for (int i = 0; i < 3; i++)
                                            {
                                                if (!new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 - i) || !new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString())))
                                                {
                                                    legal = false;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            legal = false;
                                            correct = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.fields[(int)raw[0] - 97 - 1, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(2, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())], this.fields[(int)raw[0] - 97 - 1, int.Parse(raw[1].ToString())] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                }
                            }

                        }
                        drawMap();
                    }
                }

                correct = false;
            }

            for (int count = 0; count <= 3; count++)
            {
                while (!correct)
                {
                    Console.WriteLine("Podaj lokalizację statku 1-masztowego, zapisz lokalizację za pomocą 3 znaków, pierwszy znak będzie literą od a do j , drugi znak będzie cyfrą od 0 do 9, a 3 litera będzie kierunkiem ułożnia statku ('l' - w lewo, 'r' - w prawo, 'u' - w górę, 'd' - w dół), np statek z pola b2 do pola b5 będzie zapisany jako 'b1r' lub 'b4l': ");
                    string raw = Console.ReadLine();
                    if (raw.Length == 3 && new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" }.Contains(raw[0].ToString()) && new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }.Contains(raw[1].ToString()) && new List<string> { "u", "d", "l", "r" }.Contains(raw[2].ToString()))
                    {
                        if (raw[2] == 'l')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 0; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x - vector)) || this.fields[(int)raw[0] - 97 + y, int.Parse(raw[1].ToString()) + x - vector].state != fieldState.Warship)
                                        {

                                        }
                                        else
                                        {
                                            legal = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(1, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                    correct = false;
                                }

                            }

                        }
                        else if (raw[2] == 'r')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 0; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x + vector)) || this.fields[(int)raw[0] - 97 + y, int.Parse(raw[1].ToString()) + x + vector].state != fieldState.Warship)
                                        {

                                        }
                                        else
                                        {
                                            legal = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(1, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                    correct = false;
                                }
                            }

                        }
                        else if (raw[2] == 'd')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 0; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y + vector)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x)) || this.fields[(int)raw[0] - 97 + y + vector, int.Parse(raw[1].ToString()) + x].state != fieldState.Warship)
                                        {

                                        }
                                        else
                                        {
                                            legal = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(1, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                    correct = false;
                                }
                            }
                        }
                        else if (raw[2] == 'u')
                        {
                            bool legal = true;
                            for (int vector = 0; vector <= 0; vector++)
                            {
                                for (int y = -1; y <= 1; y++)
                                {
                                    for (int x = -1; x <= 1; x++)
                                    {
                                        if (!(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains((int)raw[0] - 97 + y - vector)) || !(new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }.Contains(int.Parse(raw[1].ToString()) + x)) || this.fields[(int)raw[0] - 97 + y - vector, int.Parse(raw[1].ToString()) + x].state != fieldState.Warship)
                                        {

                                        }
                                        else
                                        {
                                            legal = false;
                                            correct = false;
                                        }
                                    }
                                }
                            }

                            if (legal)
                            {
                                try
                                {
                                    this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())].state = fieldState.Warship;
                                    this.pieces.Add(new Structure(1, new Field[] { this.fields[(int)raw[0] - 97, int.Parse(raw[1].ToString())] }));
                                    correct = true;
                                }
                                catch
                                {
                                    legal = false;
                                }
                            }

                        }
                        drawMap();
                    }
                }

                correct = false;
            }
            
           
        }

        private void setupBot()
        {
            string map = Presets.presets[new Random().Next(0, 10)];
            int iteration = 0;

            for(int i = 0; i < fields.GetLength(0); i++)
            {
                for(int j = 0; j< fields.GetLength(1); j++)
                {
                    if (map[iteration]== '.')
                    {
                        fields[i, j].state = fieldState.Undiscovered;
                    }
                    else
                    {
                        fields[i, j].state = fieldState.OccupiedUndiscovered;
                    }
                    iteration++;
                }
            }
        }

    }
}
