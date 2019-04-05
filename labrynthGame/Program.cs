using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static labrynthGame.Global;

namespace labrynthGame
{
    class Global
    {
        public static int X_SIZE = 9, Y_SIZE = 9;
        public static int X_ORIGIN = 0, Y_ORIGIN = 0;
    }
    class Room
    {
        private int x, y;

        private int width, height;

        // Position for door; if 0, no door
        private bool hasUp, hasDown, hasLeft, hasRight;
        private Room up, down, left, right;
        private int upDoor, downDoor, leftDoor, rightDoor;

        private bool isSet = false;

        public Room(int x, int y)
        {
            // Set room size
            this.x = x;
            this.y = y;
            
            Random r = new Random();
            // Randomly determine the room's width, height
            this.width = r.Next(3, 10);
            this.height = r.Next(3, 10);

            // Randomly determine if the room has rooms to its RIGHT or DOWN
            // Guarantees continuation to at least one of the two directions
            if (y == Y_SIZE - 1) this.hasDown = false;
            else this.hasDown = (r.Next(2) > 0 ? true : false);

            if (x == X_SIZE - 1) this.hasRight = false;
            else if (this.hasDown == false) this.hasRight = true;
            else this.hasRight = (r.Next(2) > 0 ? true : false);
        }

        // Get/Set Methods
        public int GetX ()
        {
            return this.x;
        }
        public int GetY ()
        {
            return this.y;
        }
        public bool GetHasUp ()
        {
            return this.hasUp;
        }
        public bool GetHasDown ()
        {
            return this.hasDown;
        }
        public bool GetHasLeft ()
        {
            return this.hasLeft;
        }
        public bool GetHasRight ()
        {
            return this.hasRight;
        }
        public void SetHasUp(bool inp)
        {
            this.hasUp = inp;
        }
        public void SetHasDown(bool inp)
        {
            this.hasDown = inp;
        }
        public void SetHasLeft(bool inp)
        {
            this.hasLeft = inp;
        }
        public void SetHasRight(bool inp)
        {
            this.hasRight = inp;
        }
        public Room GetUp()
        {
            return this.up;
        }
        public Room GetDown ()
        {
            return this.down;
        }
        public Room GetLeft ()
        {
            return this.left;
        }
        public Room GetRight()
        {
            return this.right;
        }
        public void SetSet (bool inp)
        {
            this.isSet = inp;
        }
        public bool GetSet ()
        {
            return this.isSet;
        }

        // Other class methods
        public void SetDirRoom (string dir, Room room)
        {
            if (dir == "u")
            {
                this.hasUp = true;
                this.up = room;
                if (room.GetDown() == null)
                {
                    room.SetHasDown(true);
                    room.SetDirRoom("d", this);
                }
            }
            else if (dir == "d")
            {
                this.hasDown = true;
                this.down = room;
                if (room.GetUp() == null)
                {
                    room.SetHasUp(true);
                    room.SetDirRoom("u", this);
                }
            }
            else if (dir == "l")
            {
                this.hasLeft = true;
                this.left = room;
                if (room.GetRight() == null)
                {
                    room.SetHasRight(true);
                    room.SetDirRoom("r", this);
                }
            }
            else
            {
                this.hasRight = true;
                this.right = room;
                if (room.GetLeft() == null)
                {
                    room.SetHasLeft(true);
                    room.SetDirRoom("l", this);
                }
            }
            
        }
        /*public void SetNearbyRooms (Room up, Room down, Room left, Room right)
        {
            this.up = up;
            this.hasUp = true;
            up.SetDirRoom("d", this);

            this.down = down;
            this.hasDown = true;
            down.SetDirRoom("u", this);

            this.left = left;
            this.hasLeft = true;
            left.SetDirRoom("r", this);

            this.right = right;
            this.hasRight = true;
            right.SetDirRoom("l", this);
        }*/
    }
    class Program
    {
        // Room methods
        /*static void MakeRoomSet (Room room, Room[] allArray, Room[,] lab)
        {
            if (room != null && !room.GetSet())
            {
                if (room.GetHasUp() && room.GetUp() == null)
                {
                    if (lab[room.GetX(), room.GetY() - 1] == null)
                    {
                        Room newRoom = new Room(room.GetX(), room.GetY() - 1, lab);
                        room.SetDirRoom("u", newRoom);
                        ArrayAppend(allArray, newRoom);
                    }
                    else
                    {
                        room.SetDirRoom("u", lab[room.GetX(), room.GetY() - 1]);
                    }
                }
                if (room.GetHasDown() && room.GetDown() == null)
                {
                    if (lab[room.GetX(), room.GetY() + 1] == null)
                    {
                        Room newRoom = new Room(room.GetX(), room.GetY() + 1, lab);
                        room.SetDirRoom("d", newRoom);
                        ArrayAppend(allArray, newRoom);
                    }
                    else
                    {
                        room.SetDirRoom("d", lab[room.GetX(), room.GetY() + 1]);
                    }
                }
                if (room.GetHasLeft() && room.GetLeft() == null)
                {
                    if (lab[room.GetX() - 1, room.GetY()] == null)
                    {
                        Room newRoom = new Room(room.GetX() - 1, room.GetY(), lab);
                        room.SetDirRoom("l", newRoom);
                        ArrayAppend(allArray, newRoom);
                    }
                    else
                    {
                        room.SetDirRoom("l", lab[room.GetX() - 1, room.GetY()]);
                    }
                }
                if (room.GetHasRight() && room.GetRight() == null)
                {
                    if (lab[room.GetX() + 1, room.GetY()] == null)
                    {
                        Room newRoom = new Room(room.GetX() + 1, room.GetY(), lab);
                        room.SetDirRoom("r", newRoom);
                        ArrayAppend(allArray, newRoom);
                    }
                    else
                    {
                        room.SetDirRoom("r", lab[room.GetX() + 1, room.GetY()]);
                    }
                }
                room.SetSet(true);
            }
        }*/
        // Array methods
        static void ArrayAppend (Room[] array, Room room)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    array[i] = room;
                    return;
                }
            }
        }
        static int ArrayValidLength (Room[] array)
        {
            int res = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null) res++;
            }
            return res;
        }
        static void PrintMap (Room[,] map)
        {
            string[] EMPTY_ROOM = new string[3];
            string[] EXIST_ROOM = new string[3];
            EMPTY_ROOM[0] = "  -  ";
            EMPTY_ROOM[1] = "|   |";
            EMPTY_ROOM[2] = "  -  ";

            for (int i = 0; i <= map.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= map.GetUpperBound(1); j++)
                {
                    if (map[i, j] == null)
                    {
                        SetCursorPosition(7 * i, 5 * j);
                        Write(EMPTY_ROOM[0]);
                        SetCursorPosition(7 * i, 5 * j + 1);
                        Write(EMPTY_ROOM[1]);
                        SetCursorPosition(7 * i, 5 * j + 2);
                        Write(EMPTY_ROOM[2]);
                    } else
                    {
                        SetCursorPosition(7 * i, 5 * j);
                        EXIST_ROOM[0] = (map[i, j].GetHasUp() ? "     " : "  -  ");
                        Write(EXIST_ROOM[0]);
                        SetCursorPosition(7 * i, 5 * j + 1);
                        EXIST_ROOM[1] = (map[i, j].GetHasLeft() ? " " : "|") + " @ " + (map[i, j].GetHasRight() ? " " : "|");
                        Write(EXIST_ROOM[1]);
                        SetCursorPosition(7 * i, 5 * j + 2);
                        EXIST_ROOM[2] = (map[i, j].GetHasDown() ? "     " : "  -  ");
                        Write(EXIST_ROOM[2]);

                        // Print inbetween rooms (room connectedness)
                        if (map[i, j].GetHasUp())
                        {
                            SetCursorPosition(7 * i, 5 * j - 2);
                            Write("| | |");
                            SetCursorPosition(7 * i, 5 * j - 1);
                            Write("| | |");
                        }
                        if (map[i, j].GetHasDown())
                        {
                            SetCursorPosition(7 * i, 5 * (j + 1) - 2);
                            Write("| | |");
                            SetCursorPosition(7 * i, 5 * (j + 1) - 1);
                            Write("| | |");
                        }
                        if (map[i, j].GetHasLeft())
                        {
                            SetCursorPosition(7 * i - 2, 5 * j);
                            Write("--");
                            SetCursorPosition(7 * i - 2, 5 * j + 1);
                            Write("--");
                            SetCursorPosition(7 * i - 2, 5 * j + 2);
                            Write("--");
                        }
                        if (map[i, j].GetHasRight())
                        {
                            SetCursorPosition(7 * (i + 1) - 2, 5 * j);
                            Write("--");
                            SetCursorPosition(7 * (i + 1) - 2, 5 * j + 1);
                            Write("--");
                            SetCursorPosition(7 * (i + 1) - 2, 5 * j + 2);
                            Write("--");
                        }
                    }
                }
                WriteLine();
            }
        }
        static void PrintArray(Room[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null) WriteLine("NULL");
                else WriteLine($"({array[i].GetX()}, {array[i].GetY()})");
            }
        }
        // Main
        static void Main(string[] args)
        {
            Room[] allRooms = new Room[X_SIZE * Y_SIZE];
            Room[] endRooms = new Room[2 * (X_SIZE + Y_SIZE) - 4];
            Room[,] lab = new Room[X_SIZE, Y_SIZE];

            // Initialize the map with Room objects
            for (int i = 0; i < X_SIZE; i++)
            {
                for (int j = 0; j < Y_SIZE; j++)
                {
                    lab[i, j] = new Room(i, j);
                }
            }

            for (int i = 0; i < X_SIZE; i++)
            {
                for (int j = 0; j < Y_SIZE; j++)
                {
                    if (i != 0)
                    {
                        if (lab[i-1, j].GetHasRight())
                        {
                            lab[i, j].SetDirRoom("l", lab[i - 1, j]);
                        }
                        // still working
                        // consider more cases
                    }
                }
            }

            //PrintArray(allRooms);
            //WriteLine();


            /*foreach (Room room in allRooms)
            {
                if (room != null && (room.GetX() == 0 || room.GetX() == 
                  X_SIZE - 1 || room.GetY() == 0 || room.GetY() == Y_SIZE - 1))
                {
                    ArrayAppend(endRooms, room); //I don't think this is correct
                }
            }*/

            // Try printing the entire map and end rooms and see how this works
            PrintMap(lab);
            WriteLine();

            // Sample random from range
            /*var exclude = new HashSet<int>() { 5, 7, 17, 23 };
            var range = Enumerable.Range(1, 100).Where(i => !exclude.Contains(i));

            var rand = new System.Random();
            int index = rand.Next(0, 100 - exclude.Count);
            range.ElementAt(index);*/
        }
    }
}
