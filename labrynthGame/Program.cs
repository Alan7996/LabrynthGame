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
        public static int X_ORIGIN = X_SIZE / 2, Y_ORIGIN = Y_SIZE / 2;
        public static int printPosX = 35, printPosY = 5, printPosX2 = 0, printPosY2 = 0;
        public static int mapScale = 4;
        public static Random r = new Random();
    }
    class Room
    {
        private int x, y;

        private int width, height;
        
        private bool hasUp, hasDown, hasLeft, hasRight;
        private Room up, down, left, right;
        // Position for door; if -1, no door
        private int upDoor = -1, downDoor = -1, leftDoor = -1, rightDoor = -1;

        private bool isSet = false;

        public Room(int x, int y, Room[,] lab)
        {
            // Set room coordinate
            this.x = x;
            this.y = y;

            // Randomly determine the room's width, height
            this.width = r.Next(3, 8);
            if (this.width % 2 == 1) this.width += 1;
            this.height = r.Next(3, 8);
            if (this.height % 2 == 1) this.height += 1;

            // Randomly determine if the room has rooms in each direction
            if (y == 0) this.hasUp = false;
            else this.hasUp = (r.Next(2) > 0 ? true : false);
            if (y == Y_SIZE - 1) this.hasDown = false;
            else this.hasDown = (r.Next(2) > 0 ? true : false);
            if (x == 0) this.hasLeft = false;
            else this.hasLeft = (r.Next(2) > 0 ? true : false);
            if (x == X_SIZE - 1) this.hasRight = false;
            else this.hasRight = (r.Next(2) > 0 ? true : false);

            lab[x, y] = this;
        }

        // Get/Set Methods
        public int GetX()
        {
            return this.x;
        }
        public int GetY()
        {
            return this.y;
        }
        public int GetWidth()
        {
            return this.width;
        }
        public int GetHeight()
        {
            return this.height;
        }
        public bool GetHasUp()
        {
            return this.hasUp;
        }
        public bool GetHasDown()
        {
            return this.hasDown;
        }
        public bool GetHasLeft()
        {
            return this.hasLeft;
        }
        public bool GetHasRight()
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
        public Room GetDown()
        {
            return this.down;
        }
        public Room GetLeft()
        {
            return this.left;
        }
        public Room GetRight()
        {
            return this.right;
        }
        public void SetUp(Room room)
        {
            this.up = room;
        }
        public void SetDown(Room room)
        {
            this.down = room;
        }
        public void SetLeft(Room room)
        {
            this.left = room;
        }
        public void SetRight(Room room)
        {
            this.right = room;
        }
        public int GetUpDoor()
        {
            return this.upDoor;
        }
        public int GetDownDoor()
        {
            return this.downDoor;
        }
        public int GetLeftDoor()
        {
            return this.leftDoor;
        }
        public int GetRightDoor()
        {
            return this.rightDoor;
        }
        public void SetDoorPos()
        {
            if (this.hasUp == true) upDoor = r.Next(1, this.width * mapScale);
            if (this.hasDown == true) downDoor = r.Next(1, this.width * mapScale);
            if (this.hasLeft == true) leftDoor = r.Next(1, this.height * mapScale);
            if (this.hasRight == true) rightDoor = r.Next(1, this.height * mapScale);
        }
        public void SetSet(bool inp)
        {
            this.isSet = inp;
        }
        public bool GetSet()
        {
            return this.isSet;
        }

        // Other class methods
        public void PrintCoord()
        {
            WriteLine($"({this.x}, {this.y})");
        }
        public void SetDirRoom(string dir, Room room)
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
        public void DiscDirRoom(string dir)
        {
            if (dir == "u")
            {
                if (this.down != null)
                {
                    this.hasDown = false;
                    this.down.SetHasUp(false);
                    this.down.SetUp(null);
                }
                if (this.left != null)
                {
                    this.hasLeft = false;
                    this.left.SetHasRight(false);
                    this.left.SetRight(null);
                }
                if (this.right != null)
                {
                    this.hasRight = false;
                    this.right.SetHasLeft(false);
                    this.right.SetLeft(null);
                }
            }
            else if (dir == "d")
            {
                if (this.up != null)
                {
                    this.hasUp = false;
                    this.up.SetHasDown(false);
                    this.up.SetDown(null);
                }
                if (this.left != null)
                {
                    this.hasLeft = false;
                    this.left.SetHasRight(false);
                    this.left.SetRight(null);
                }
                if (this.right != null)
                {
                    this.hasRight = false;
                    this.right.SetHasLeft(false);
                    this.right.SetLeft(null);
                }
            }
            else if (dir == "l")
            {
                if (this.up != null)
                {
                    this.hasUp = false;
                    this.up.SetHasDown(false);
                    this.up.SetDown(null);
                }
                if (this.down != null)
                {
                    this.hasDown = false;
                    this.down.SetHasUp(false);
                    this.down.SetUp(null);
                }
                if (this.right != null)
                {
                    this.hasRight = false;
                    this.right.SetHasLeft(false);
                    this.right.SetLeft(null);
                }
            }
            else
            {
                if (this.up != null)
                {
                    this.hasUp = false;
                    this.up.SetHasDown(false);
                    this.up.SetDown(null);
                }
                if (this.down != null)
                {
                    this.hasDown = false;
                    this.down.SetHasUp(false);
                    this.down.SetUp(null);
                }
                if (this.left != null)
                {
                    this.hasLeft = false;
                    this.left.SetHasRight(false);
                    this.left.SetRight(null);
                }
            }
        }
        // Specifically for origin room
        public void SetOriginNearby(Room up, Room down, Room left, Room right)
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
        }
    }
    class Player
    {
        // player's position in the room
        private int x, y;
        private string[] sprite = new string[1];

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;

            sprite[0] = "@";
            //sprite[1] = "@@";
        }

        // Get/Set Methods
        public int GetX()
        {
            return this.x;
        }
        public int GetY()
        {
            return this.y;
        }
        public void SetX(int x)
        {
            this.x = x;
        }
        public void SetY(int y)
        {
            this.y = y;
        }

        public void PrintChar(int x, int y)
        {
            SetCursorPosition(x, y);
            Write(sprite[0]);
            //SetCursorPosition(x, y + 1);
            //Write(sprite[1]);
        }
    }
    class Program
    {
        // Room methods
        static void MakeRoomSet(Room room, Room[] allArray, Room[,] lab)
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
                room.SetDoorPos();
                room.SetSet(true);
            }
        }
        static bool CheckOneConnection(Room room)
        {
            if (room.GetHasUp() && !room.GetHasDown() && !room.GetHasLeft() && !room.GetHasRight()
                || room.GetHasDown() && !room.GetHasUp() && !room.GetHasLeft() && !room.GetHasRight()
                || room.GetHasLeft() && !room.GetHasUp() && !room.GetHasDown() && !room.GetHasRight()
                || room.GetHasRight() && !room.GetHasUp() && !room.GetHasDown() && !room.GetHasLeft())
                return true;
            return false;
        }
        // Array methods
        static void ArrayAppend(Room[] array, Room room)
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
        static int ArrayValidLength(Room[] array)
        {
            int res = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null) res++;
            }
            return res;
        }
        // Print methods
        static void PrintMap(Room[,] map)
        {
            Console.Clear();
            string[] EXIST_ROOM = new string[4];
            for (int i = 0; i <= map.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= map.GetUpperBound(1); j++)
                {
                    if (map[i, j] != null)
                    {
                        SetCursorPosition(12 * i, 8 * j);
                        EXIST_ROOM[0] = (map[i, j].GetHasUp() ? "┼        ┼" : "┼────────┼");
                        Write(EXIST_ROOM[0]);
                        SetCursorPosition(12 * i, 8 * j + 1);
                        EXIST_ROOM[1] = (map[i, j].GetHasLeft() ? "  " : "│ ") + "      " + (map[i, j].GetHasRight() ? "  " : " │");
                        Write(EXIST_ROOM[1]);
                        SetCursorPosition(12 * i, 8 * j + 2);
                        EXIST_ROOM[2] = (map[i, j].GetHasLeft() ? "  " : "│ ") + "  @@@ " + (map[i, j].GetHasRight() ? "  " : " │");
                        Write(EXIST_ROOM[2]);
                        SetCursorPosition(12 * i, 8 * j + 3);
                        Write(EXIST_ROOM[2]);
                        SetCursorPosition(12 * i, 8 * j + 4);
                        Write(EXIST_ROOM[1]);
                        SetCursorPosition(12 * i, 8 * j + 5);
                        EXIST_ROOM[3] = (map[i, j].GetHasDown() ? "┼        ┼" : "┼────────┼");
                        Write(EXIST_ROOM[3]);

                        // Print inbetween rooms (room connectedness)
                        if (map[i, j].GetHasUp())
                        {
                            SetCursorPosition(12 * i, 8 * j - 2);
                            Write("│   ││   │");
                            SetCursorPosition(12 * i, 8 * j - 1);
                            Write("│   ││   │");
                        }
                        if (map[i, j].GetHasDown())
                        {
                            SetCursorPosition(12 * i, 8 * (j + 1) - 2);
                            Write("│   ││   │");
                            SetCursorPosition(12 * i, 8 * (j + 1) - 1);
                            Write("│   ││   │");
                        }
                        if (map[i, j].GetHasLeft())
                        {
                            SetCursorPosition(12 * i - 2, 8 * j);
                            Write("─");
                            SetCursorPosition(12 * i - 2, 8 * j + 2);
                            Write("─");
                            SetCursorPosition(12 * i - 2, 8 * j + 3);
                            Write("─");
                            SetCursorPosition(12 * i - 2, 8 * j + 5);
                            Write("─");
                        }
                        if (map[i, j].GetHasRight())
                        {
                            SetCursorPosition(12 * (i + 1) - 2, 8 * j);
                            Write("─");
                            SetCursorPosition(12 * (i + 1) - 2, 8 * j + 2);
                            Write("─");
                            SetCursorPosition(12 * (i + 1) - 2, 8 * j + 3);
                            Write("─");
                            SetCursorPosition(12 * (i + 1) - 2, 8 * j + 5);
                            Write("─");
                        }
                    }
                }
                WriteLine();
            }
        }
        static void HighlightCurrRoom(Room room)
        {
            string[] EXIST_ROOM = new string[4];
            int i = room.GetX(), j = room.GetY();
            Console.ForegroundColor = ConsoleColor.Red;

            SetCursorPosition(12 * i, 8 * j);
            EXIST_ROOM[0] = (room.GetHasUp() ? "┼        ┼" : "┼────────┼");
            Write(EXIST_ROOM[0]);
            SetCursorPosition(12 * i, 8 * j + 1);
            EXIST_ROOM[1] = (room.GetHasLeft() ? "  " : "│ ") + "      " + (room.GetHasRight() ? "  " : " │");
            Write(EXIST_ROOM[1]);
            SetCursorPosition(12 * i, 8 * j + 2);
            EXIST_ROOM[2] = (room.GetHasLeft() ? "  " : "│ ") + "  @@@ " + (room.GetHasRight() ? "  " : " │");
            Write(EXIST_ROOM[2]);
            SetCursorPosition(12 * i, 8 * j + 3);
            Write(EXIST_ROOM[2]);
            SetCursorPosition(12 * i, 8 * j + 4);
            Write(EXIST_ROOM[1]);
            SetCursorPosition(12 * i, 8 * j + 5);
            EXIST_ROOM[3] = (room.GetHasDown() ? "┼        ┼" : "┼────────┼");
            Write(EXIST_ROOM[3]);

            Console.ForegroundColor = ConsoleColor.White;
        }
        static void PrintArray(Room[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null) WriteLine("NULL");
                else array[i].PrintCoord();
            }
        }
        static Tuple<int, int> PrintRoom(Room room, int posX, int posY)
        {
            Console.Clear();
            int mapX = room.GetWidth() * 2, mapY = room.GetHeight();
            int charX = 2 + posX * mapScale * 2, charY = 1 + posY * mapScale;
            mapX *= mapScale; mapY *= mapScale;
            mapX += 1; mapY += 2;

            printPosX2 = printPosX + mapX; printPosY2 = printPosY + mapY;
            //WriteLine($"Room : ({room.GetWidth()}, {room.GetHeight()}) : Player : {player.GetX()}, {player.GetY()}");
            //WriteLine($"Map : {mapX}, {mapY}");
            
            SetCursorPosition(printPosX, printPosY);
            string horizUp = "";
            string horizDown = "";
            string horizEmpty = "";
            for (int i = 0; i < mapX; i++)
            {
                // make sure to include both the door and the position next to it (for graphical reasons)
                if (i == room.GetUpDoor() || i == room.GetUpDoor() - 1 || i == room.GetUpDoor() + 1) horizUp += " ";
                else horizUp += "─";
                if (i == room.GetDownDoor() || i == room.GetDownDoor() - 1 || i == room.GetDownDoor() + 1) horizDown += " ";
                else horizDown += "─";
                horizEmpty += " ";
            }
            Write("┌" + horizUp + "┐");
            for (int i = 0; i < (mapY - 2); i++)
            {
                SetCursorPosition(printPosX, printPosY + i + 1);
                string leftDoor = (i == room.GetLeftDoor() - 1) ? " " : "│";
                string rightDoor = (i == room.GetRightDoor() - 1) ? " " : "│";
                Write(leftDoor + horizEmpty + rightDoor);
            }
            SetCursorPosition(printPosX, printPosY + mapY - 1);
            Write("└" + horizDown + "┘");

            //WriteLine($"Char : {printPosX + charX}, {printPosY + charY}");
            return new Tuple<int, int>(printPosX + charX, printPosY + charY);
        }
        // Main
        static void Main(string[] args)
        {
            Room[] allRooms = new Room[X_SIZE * Y_SIZE];
            Room[] endRooms = new Room[2 * (X_SIZE + Y_SIZE) - 4];
            Room[,] lab = new Room[X_SIZE, Y_SIZE];

            // Create origin room (at the center of the map)
            Room origin = new Room(X_ORIGIN, Y_ORIGIN, lab);
            ArrayAppend(allRooms, origin);

            // Create first four rooms adjacent to origin
            Room upRoom = new Room(X_ORIGIN, Y_ORIGIN - 1, lab);
            Room downRoom = new Room(X_ORIGIN, Y_ORIGIN + 1, lab);
            Room leftRoom = new Room(X_ORIGIN - 1, Y_ORIGIN, lab);
            Room rightRoom = new Room(X_ORIGIN + 1, Y_ORIGIN, lab);
            ArrayAppend(allRooms, upRoom);
            ArrayAppend(allRooms, downRoom);
            ArrayAppend(allRooms, leftRoom);
            ArrayAppend(allRooms, rightRoom);
            origin.SetOriginNearby(upRoom, downRoom, leftRoom, rightRoom);
            origin.SetDoorPos();
            origin.SetSet(true);

            // Create the whole map
            bool allRoomSet = false;
            while (!allRoomSet)
            {
                int len = ArrayValidLength(allRooms);
                for (int i = 0; i < len; i++)
                {
                    MakeRoomSet(allRooms[i], allRooms, lab);
                }

                allRoomSet = true;
                foreach (Room room in allRooms)
                {
                    // if at least one room is NOT set, repeat loop
                    if (room != null) allRoomSet &= room.GetSet();
                }
            }

            // Add all rooms that are at the edge of the map to endRooms
            int endRoomsCount = 0;
            foreach (Room room in allRooms)
            {
                if (room != null && (room.GetX() == 0 || room.GetX() ==
                  X_SIZE - 1 || room.GetY() == 0 || room.GetY() == Y_SIZE - 1))
                {
                    if (CheckOneConnection(room))
                    {
                        ArrayAppend(endRooms, room);
                        endRoomsCount++;
                    }
                    // need to improve, what if no rooms are at the border?
                }
            }
            // Select one of the elements of endRooms as the final exitRoom
            Room exitRoom = endRooms[r.Next(endRoomsCount)];

            // All background setup should be complete by now

            // Gameplay
            Room currRoom = origin;
            Player player = new Player(origin.GetWidth() / 2, origin.GetHeight() / 2);
            Console.CursorVisible = false;

            bool mOpen = false;
            Tuple<int, int> pos = PrintRoom(currRoom, player.GetX(), player.GetY());
            player.PrintChar(pos.Item1, pos.Item2);
            while (true)
            {
                ConsoleKey inp = Console.ReadKey(true).Key;
                // If an arrow is pressed, go to that direction
                // need to make room transition when entering through a door
                if (inp == ConsoleKey.UpArrow)
                {
                    if (pos.Item2 != printPosY + 1)
                    {
                        SetCursorPosition(pos.Item1, pos.Item2);
                        Write(" ");
                        pos = new Tuple<int, int>(pos.Item1, pos.Item2 - 1);
                        player.PrintChar(pos.Item1, pos.Item2);
                    }
                    else
                    {
                        if (currRoom.GetUpDoor() == pos.Item1 - printPosX - 2
                          || currRoom.GetUpDoor() == pos.Item1 - printPosX - 1)
                        {
                            currRoom = currRoom.GetUp();
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            pos = new Tuple<int, int>(printPosX + currRoom.GetDownDoor() + 1, printPosY2 - 2);
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                }
                if (inp == ConsoleKey.DownArrow)
                {
                    if (pos.Item2 != printPosY2 - 2)
                    {
                        SetCursorPosition(pos.Item1, pos.Item2);
                        Write(" ");
                        pos = new Tuple<int, int>(pos.Item1, pos.Item2 + 1);
                        player.PrintChar(pos.Item1, pos.Item2);
                    }
                    else
                    {
                        if (currRoom.GetDownDoor() == pos.Item1 - printPosX - 2
                          || currRoom.GetDownDoor() == pos.Item1 - printPosX - 1)
                        {
                            currRoom = currRoom.GetDown();
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            pos = new Tuple<int, int>(printPosX + currRoom.GetUpDoor() + 1, printPosY + 1);
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                }
                if (inp == ConsoleKey.LeftArrow)
                {
                    if (pos.Item1 != printPosX + 2)
                    {
                        SetCursorPosition(pos.Item1, pos.Item2);
                        Write(" ");
                        pos = new Tuple<int, int>(pos.Item1 - 1, pos.Item2);
                        player.PrintChar(pos.Item1, pos.Item2);
                    }
                    else
                    {
                        if (currRoom.GetLeftDoor() == pos.Item2 - printPosY)
                        {
                            currRoom = currRoom.GetLeft();
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            pos = new Tuple<int, int>(printPosX2, printPosY + currRoom.GetRightDoor());
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                }
                if (inp == ConsoleKey.RightArrow)
                {
                    if (pos.Item1 != printPosX2)
                    {
                        SetCursorPosition(pos.Item1, pos.Item2);
                        Write(" ");
                        pos = new Tuple<int, int>(pos.Item1 + 1, pos.Item2);
                        player.PrintChar(pos.Item1, pos.Item2);
                    }
                    else
                    {
                        if (currRoom.GetRightDoor() == pos.Item2 - printPosY)
                        {
                            currRoom = currRoom.GetRight();
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            pos = new Tuple<int, int>(printPosX + 2, printPosY + currRoom.GetLeftDoor());
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                }
                // If user presses 'm', show or hide the map
                if (inp == ConsoleKey.M)
                {
                    SetCursorPosition(0, 0);
                    if (!mOpen) {
                        // Display the map after clearing the screen
                        PrintMap(lab);
                        HighlightCurrRoom(currRoom);
                        //WriteLine();
                        //exitRoom.PrintCoord();
                        mOpen = true;
                    }
                    else
                    {
                        // Close map and return to original screen by using
                        // previously saved gameState
                        mOpen = false;
                        PrintRoom(currRoom, pos.Item1, pos.Item2);
                        player.PrintChar(pos.Item1, pos.Item2);
                    }
                }
            }
        }
    }
}
