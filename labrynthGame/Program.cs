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
        public static int printPosX = 20, printPosY = 10, printPosX2 = 0, printPosY2 = 0;
        public static int mapScale = 5;
        public static bool gameOver = false;
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

        private Item[] itemDrops;

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
        public Item[] GetItemDrops()
        {
            return this.itemDrops;
        }
        public void SetItemDrops(Item[] items)
        {
            this.itemDrops = items;
        }
        public void SetSet(bool inp)
        {
            this.isSet = inp;
        }
        public bool GetSet()
        {
            return this.isSet;
        }

        public void RemoveItem(Item item)
        {
            for (int i = 0; i < this.itemDrops.Length; i++)
            {
                if (this.itemDrops[i] == item)
                {
                    this.itemDrops[i] = null;
                    break;
                }
            }
        }

        // Other class methods
        public void PrintCoord()
        {
            WriteLine($"({this.x}, {this.y})");
        }
        public void SetDirRoom(string dir, Room room)
        {
            // Sets connection to a given direction with a given room
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
        private int health;
        private string[] sprite = new string[1];
        private Item[] inventory = new Item[10];

        public Player(int x, int y)
        {
            this.x = x;
            this.y = y;

            this.health = 100;

            sprite[0] = "@";
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
        public int GetHealth()
        {
            return this.health;
        }
        public void DecHealth(int dmg)
        {
            this.health -= dmg;
            if (this.health < 0) this.health = 0;
        }
        public void IncHealth(int hp)
        {
            this.health += hp;
            if (this.health > 100) this.health = 100;
        }
        public Item[] GetInven()
        {
            return this.inventory;
        }

        public Tuple<int, bool> Attack()
        {
            bool didCrit = false;
            // Attack missed?
            if (r.Next(10) < 1 ? true : false) return new Tuple<int, bool>(0, didCrit);

            int res = r.Next(10, 31);

            // Check critical
            if (r.Next(5) < 1 ? true : false)
            {
                didCrit = true;
                res *= 2;
            }

            return new Tuple<int, bool>(res, didCrit);
        }
        public bool AddItem(Item item)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = item;
                    return true;
                }
            }
            return false;
        }
        public void UseItem(int index)
        {
            SetCursorPosition(32, 25);
            WriteLine($"Healed {inventory[index].GetHeal()} HP!");
            this.IncHealth(inventory[index].GetHeal());
            inventory[index] = null;
            for (int i = index + 1; i < inventory.Length; i++)
            {
                if (inventory[i] != null)
                {
                    inventory[i - 1] = inventory[i];
                    inventory[i] = null;
                }
            }
        }
        public bool TryRun()
        {
            bool res = r.Next(3) > 0 ? true : false;
            return res;
        }

        // Print methods
        public void PrintChar(int x, int y)
        {
            SetCursorPosition(x, y);
            Write(sprite[0]);
            this.PrintHealth();
        }
        public void PrintHealth()
        {
            SetCursorPosition(printPosX, printPosY - 3);
            Write("                          ");
            SetCursorPosition(printPosX, printPosY - 3);
            if (this.health == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                SetCursorPosition(32, 27);
                WriteLine("YOU DIED");
                gameOver = true;
            }
            else
            {

                Write($"Player Health : ");
                for (int i = 0; i < this.health / 10; i++) Write("口");
                if (this.health / 10 == 0 && this.health != 0) Write("|");
            }
        }
    }
    class Enemy
    {
        private int health;

        public Enemy()
        {
            this.health = r.Next(2, 4) * 10;
        }

        public int GetHealth()
        {
            return this.health;
        }
        public void DecHealth(int dmg)
        {
            this.health -= dmg;
            if (this.health < 0) this.health = 0;
        }
        
        public Tuple<int, bool> Attack()
        {
            bool didCrit = false;
            // Attack missed?
            if (r.Next(10) < 1 ? true : false) return new Tuple<int, bool>(0, didCrit);

            int res = r.Next(1, 11);

            // Check critical
            if (r.Next(5) < 1 ? true : false)
            {
                didCrit = true;
                res *= 2;
            }

            return new Tuple<int, bool>(res, didCrit);
        }

        public void PrintHealth()
        {
            SetCursorPosition(printPosX, printPosY - 2);
            Write("                          ");
            SetCursorPosition(printPosX, printPosY - 2);
            Write($"Enemy Health : ");
            for (int i = 0; i < this.health / 10; i++) Write("口");
            if (this.health / 10 == 0 && this.health != 0) Write("|");
        }
    }
    class Item
    {
        private string name;
        private int heal;

        bool isXYSet = false;
        private int x, y;

        public Item()
        {
            this.heal = r.Next(0, 11) * 10;
            this.name = "Potion" + this.heal.ToString();
        }

        public string GetName()
        {
            return this.name;
        }
        public int GetHeal()
        {
            return this.heal;
        }
        public bool GetIsXYSet()
        {
            return this.isXYSet;
        }
        public int GetX()
        {
            return this.x;
        }
        public int GetY()
        {
            return this.y;
        }
        public void SetXY(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.isXYSet = true;
        }
    }
    class Program
    {
        // Room methods
        static void MakeRoomSet(Room room, Room[] allArray, Room[,] lab)
        {
            // Set up all connections inbetween rooms
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
        }
        static bool CheckOneConnection(Room room)
        {
            // Return true if the room has only one door
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
            // Add room object to the first empty index
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
            // Returns the valid length of an array
            // array should to be sorted with all null indices after valid ones
            int res = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null) res++;
            }
            return res;
        }
        static int ArrayValidLength(Item[] array)
        {
            // Returns the valid length of an array
            // array should to be sorted with all null indices after valid ones
            int res = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null) res++;
            }
            return res;
        }
        static Room FirstRoom(Room[,] map)
        {
            // Return the first valid room in the map
            for (int i = 0; i <= map.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= map.GetUpperBound(1); j++)
                {
                    if (map[i, j] != null) return map[i, j];
                }
            }
            return null;
        }
        // Random Encounter
        static Enemy RandonEncounter()
        {
            // Randomly determine if the character encounters an enemy
            bool didEncounter = r.Next(20) < 1 ? true : false;
            if (didEncounter)
            {
                return new Enemy();
            }
            return null;
        }
        static bool EnemyCombat(Player player, Enemy enemy)
        {
            if (enemy == null) return false; // no encounter

            bool didStart = false;
            Console.Clear();
            player.PrintHealth();
            enemy.PrintHealth();

            while (player.GetHealth() != 0 && enemy.GetHealth() != 0)
            {
                // Repeat combat until the death of either player or enemy
                Tuple<int, int> res = BattleSim(player, enemy, ref didStart);
                if (res.Item1 == -1 && res.Item2 == -1)
                {
                    PrintDialogueBox();
                    SetCursorPosition(32, 25);
                    Write("You successfully ran away!");
                    SetCursorPosition(32, 36);
                    Write("Press any key");
                    Console.ReadKey(true);
                    return true;
                }
                player.DecHealth(res.Item2);
                enemy.DecHealth(res.Item1);
                player.PrintHealth();
                enemy.PrintHealth();
            }
            SetCursorPosition(32, 27);
            Write("Enemy defeated!");
            PrintUserInputBox();
            SetCursorPosition(32, 36);
            Write("Press any key");
            Console.ReadKey(true);
            return true;
        }
        static Tuple<int, int> BattleSim(Player player, Enemy enemy, ref bool didStart)
        {
            if (!didStart)
            {
                PrintDialogueBox();
                PrintBattleOptions();
                PrintUserInputBox();
                didStart = true;
            }

            // Clear Potion options
            for (int i = 0; i < 33; i++)
            {
                SetCursorPosition(32, 38 + i);
                Write("                                 ");
            }

            PrintUserInputBox();
            int input;
            int.TryParse(ReadLine(), out input);

            while (input > 3 || input < 1)
            {
                PrintDialogueBox();
                SetCursorPosition(32, 25);
                Write("Invalid choice");
                PrintUserInputBox();
                int.TryParse(ReadLine(), out input);
            }

            Tuple<int, bool> playerAttack = player.Attack();
            Tuple<int, bool> enemyAttack = enemy.Attack();

            switch (input)
            {
                case 1:
                    PrintDmg(playerAttack, enemyAttack);
                    break;
                case 2:
                    PrintItems(player);

                    PrintUserInputBox();
                    int.TryParse(ReadLine(), out input);

                    if (input == 0) return BattleSim(player, enemy, ref didStart);
                    while (input > ArrayValidLength(player.GetInven()) || input < 1)
                    {
                        PrintDialogueBox();
                        SetCursorPosition(32, 25);
                        WriteLine("Invalid choice");
                        PrintUserInputBox();
                        int.TryParse(ReadLine(), out input);
                    }
                    
                    PrintDmg(new Tuple<int, bool>(-2, false), enemyAttack);
                    player.UseItem(input - 1);

                    return new Tuple<int, int>(0, enemyAttack.Item1);
                case 3:
                    if (player.TryRun()) return new Tuple<int, int>(-1, -1);
                    
                    PrintDmg(new Tuple<int, bool>(-1, false), enemyAttack);
                    break;
                default:
                    WriteLine("This should never be printed");
                    break;
            }

            player.PrintHealth();
            enemy.PrintHealth();

            return new Tuple<int, int>(playerAttack.Item1, enemyAttack.Item1);
        }
        // Item methods
        static void CreateItems(Room room)
        {
            Item[] items = new Item[r.Next(4)];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new Item();
            }

            room.SetItemDrops(items);
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
                        // Print the room itself depending on its connections
                        SetCursorPosition(12 * i, 8 * j);
                        EXIST_ROOM[0] = (map[i, j].GetHasUp() ? "┼        ┼" : "┼────────┼ ");
                        Write(EXIST_ROOM[0]);
                        SetCursorPosition(12 * i, 8 * j + 1);
                        EXIST_ROOM[1] = (map[i, j].GetHasLeft() ? "  " : "│ ")
                            + "      " + (map[i, j].GetHasRight() ? "  " : " │ ");
                        Write(EXIST_ROOM[1]);
                        SetCursorPosition(12 * i, 8 * j + 2);
                        EXIST_ROOM[2] = (map[i, j].GetHasLeft() ? "  " : "│ ")
                            + "  @@@ " + (map[i, j].GetHasRight() ? "  " : " │ ");
                        Write(EXIST_ROOM[2]);
                        SetCursorPosition(12 * i, 8 * j + 3);
                        Write(EXIST_ROOM[2]);
                        SetCursorPosition(12 * i, 8 * j + 4);
                        Write(EXIST_ROOM[1]);
                        SetCursorPosition(12 * i, 8 * j + 5);
                        EXIST_ROOM[3] = (map[i, j].GetHasDown() ? "┼        ┼" : "┼────────┼ ");
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
            // Print current room in red on top of the map
            string[] EXIST_ROOM = new string[4];
            int i = room.GetX(), j = room.GetY();
            Console.ForegroundColor = ConsoleColor.Red;

            SetCursorPosition(12 * i, 8 * j);
            EXIST_ROOM[0] = (room.GetHasUp() ? "┼        ┼ " : "┼────────┼ ");
            Write(EXIST_ROOM[0]);
            SetCursorPosition(12 * i, 8 * j + 1);
            EXIST_ROOM[1] = (room.GetHasLeft() ? "  " : "│ ") + "      " + (room.GetHasRight() ? "  " : " │ ");
            Write(EXIST_ROOM[1]);
            SetCursorPosition(12 * i, 8 * j + 2);
            EXIST_ROOM[2] = (room.GetHasLeft() ? "  " : "│ ") + "  @@@ " + (room.GetHasRight() ? "  " : " │ ");
            Write(EXIST_ROOM[2]);
            SetCursorPosition(12 * i, 8 * j + 3);
            Write(EXIST_ROOM[2]);
            SetCursorPosition(12 * i, 8 * j + 4);
            Write(EXIST_ROOM[1]);
            SetCursorPosition(12 * i, 8 * j + 5);
            EXIST_ROOM[3] = (room.GetHasDown() ? "┼        ┼ " : "┼────────┼ ");
            Write(EXIST_ROOM[3]);

            Console.ForegroundColor = ConsoleColor.White;
        }
        static void PrintArray(Room[] array)
        {
            // Print all coordinates in the array for debuggin
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != null) array[i].PrintCoord();
            }
        }
        static Tuple<int, int> PrintRoom(Room room, int posX, int posY)
        {
            // Draws a given room with player at (posX, posY)
            Console.Clear();
            // Coordinate calculations
            int mapX = room.GetWidth() * 2, mapY = room.GetHeight();
            int charX = 2 + posX * mapScale * 2, charY = 1 + posY * mapScale;
            mapX *= mapScale; mapY *= mapScale;
            mapX += 1; mapY += 2;

            printPosX2 = printPosX + mapX; printPosY2 = printPosY + mapY;
            
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

            // Print items in the room
            foreach(Item item in room.GetItemDrops())
            {
                if (item != null)
                {
                    if (!item.GetIsXYSet())
                        item.SetXY(r.Next(printPosX + 4, printPosX2 - 4), r.Next(printPosY + 3, printPosY2 - 3));
                    SetCursorPosition(item.GetX(), item.GetY());
                    Write("!");
                }
            }
            
            return new Tuple<int, int>(printPosX + charX, printPosY + charY);
        }
        static void PrintExitRoom (Room currRoom, Room exitRoom)
        {
            // Print blue box exit at the exit room
            if (currRoom == exitRoom)
            {
                string[] EXIT = new string[4];
                EXIT[0] = "┌───────┐ ";
                EXIT[1] = "│       │ ";
                EXIT[2] = "│       │ ";
                EXIT[3] = "└───────┘ ";
                int i = printPosX + currRoom.GetWidth() * mapScale * 2 / 3;
                int j = printPosY + currRoom.GetHeight() * mapScale / 2;
                Console.ForegroundColor = ConsoleColor.Blue;

                SetCursorPosition(i, j);
                Write(EXIT[0]);
                SetCursorPosition(i, j + 1);
                Write(EXIT[1]);
                SetCursorPosition(i, j + 2);
                Write(EXIT[2]);
                SetCursorPosition(i, j + 3);
                Write(EXIT[3]);

                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        static void PrintItems(Player player)
        {
            Item[] inven = player.GetInven();
            string[] ITEMBOX = new string[3];
            ITEMBOX[0] = "┌────────────────────────────┐ ";
            ITEMBOX[1] = "│                            │ ";
            ITEMBOX[2] = "└────────────────────────────┘ ";

            SetCursorPosition(32, 38);
            WriteLine(ITEMBOX[0]);
            SetCursorPosition(32, 39);
            WriteLine(ITEMBOX[1]);
            SetCursorPosition(34, 39);
            WriteLine("0. Cancel");
            SetCursorPosition(32, 40);
            WriteLine(ITEMBOX[2]);

            // Print inventory
            for (int i = 0; i < inven.Length; i++)
            {
                if (inven[i] != null)
                {
                    SetCursorPosition(32, 41 + 3 * i);
                    Write(ITEMBOX[0]);
                    SetCursorPosition(32, 41 + 3 * i + 1);
                    Write(ITEMBOX[1]);
                    SetCursorPosition(34, 41 + 3 * i + 1);
                    WriteLine($"{i + 1}. {inven[i].GetName()}");
                    SetCursorPosition(32, 41 + 3 * i + 2);
                    Write(ITEMBOX[2]);
                }
            }
        }
        // Print for Battle
        static void PrintBattleOptions()
        {
            string[] ATTACK = new string[5];
            ATTACK[0] = "┌──────────────┐   ";
            ATTACK[1] = "│              │   ";
            ATTACK[2] = "│  1. ATTACK   │   ";
            ATTACK[3] = "│              │   ";
            ATTACK[4] = "└──────────────┘   ";
            string[] ITEM = new string[5];
            ITEM[0] = "┌──────────────┐   ";
            ITEM[1] = "│              │   ";
            ITEM[2] = "│   2. ITEM    │   ";
            ITEM[3] = "│              │   ";
            ITEM[4] = "└──────────────┘   ";
            string[] RUN = new string[5];
            RUN[0] = "┌──────────────┐ ";
            RUN[1] = "│              │ ";
            RUN[2] = "│    3. RUN    │ ";
            RUN[3] = "│              │ ";
            RUN[4] = "└──────────────┘ ";
            
            for (int i = 0; i < ATTACK.Length; i++)
            {
                SetCursorPosition(30, 30 + i);
                Write(ATTACK[i] + ITEM[i] + RUN[i]);
            }
        }
        static void PrintDialogueBox()
        {
            string[] DIALOGUEBOX = new string[7];
            DIALOGUEBOX[0] = "┌────────────────────────────────────────────────────┐ ";
            DIALOGUEBOX[1] = "│                                                    │ ";
            DIALOGUEBOX[2] = "│                                                    │ ";
            DIALOGUEBOX[3] = "│                                                    │ ";
            DIALOGUEBOX[4] = "│                                                    │ ";
            DIALOGUEBOX[5] = "│                                                    │ ";
            DIALOGUEBOX[6] = "└────────────────────────────────────────────────────┘ ";

            for (int i = 0; i < DIALOGUEBOX.Length; i++)
            {
                SetCursorPosition(30, 23 + i);
                Write(DIALOGUEBOX[i]);
            }
        }
        static void PrintDmg(Tuple<int, bool> playerDmg, Tuple<int, bool> enemyDmg)
        {
            PrintDialogueBox();

            SetCursorPosition(32, 25);
            if (playerDmg.Item1 == 0) Write("You missed your attack!");
            else if (playerDmg.Item1 == -1) Write("You failed to run away!");
            else if (playerDmg.Item1 != -2)
            {
                Write($"You inflicted {playerDmg.Item1} damage to the enemy!");
                if (playerDmg.Item2) Write(" Critical hit!");
            }

            SetCursorPosition(32, 26);
            if (enemyDmg.Item1 == 0) Write("Enemy missed its attack!");
            else
            {
                Write($"Emeny inflicted {enemyDmg.Item1} damage to you!");
                if (enemyDmg.Item2) Write(" Critical hit!");
            }

            PrintUserInputBox();
        }
        static void PrintUserInputBox()
        {
            string[] INPUTBOX = new string[3];
            INPUTBOX[0] = "┌────────────────────────────────────────────────────┐ ";
            INPUTBOX[1] = "│                                                    │ ";
            INPUTBOX[2] = "└────────────────────────────────────────────────────┘ ";

            SetCursorPosition(20, 36);
            Write("CHOICE : ");
            for (int i = 0; i < INPUTBOX.Length; i++)
            {
                SetCursorPosition(30, 35 + i);
                Write(INPUTBOX[i]);
            }
            SetCursorPosition(32, 36);
        }
        // Main
        static void Main(string[] args)
        {
            // Recommended Screen Size Minimum :
            /*
             * Buffer width : 110, height : 72
             * Window width : 110, height : 65
             */

            // Background Setup
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
                // Create door locations and create items for each room
                if (room != null)
                {
                    room.SetDoorPos();
                    CreateItems(room);
                }
                // Add rooms at the border of the map AND with only one door
                if (room != null && (room.GetX() == 0 || room.GetX() ==
                  X_SIZE - 1 || room.GetY() == 0 || room.GetY() == Y_SIZE - 1))
                {
                    if (CheckOneConnection(room))
                    {
                        ArrayAppend(endRooms, room);
                        endRoomsCount++;
                    }
                }
            }
            // Select one of the elements of endRooms as the final exitRoom
            Room exitRoom;
            // If there is no room at border, select the first map in the map
            if (endRoomsCount == 0) exitRoom = FirstRoom(lab);
            else exitRoom = endRooms[r.Next(endRoomsCount)];


            // All background setup should be complete by now


            // Gameplay
            Room currRoom = origin;
            Player player = new Player(origin.GetWidth() / 2, origin.GetHeight() / 2);
            Console.CursorVisible = false;

            bool mOpen = false;
            Tuple<int, int> pos = PrintRoom(currRoom, player.GetX(), player.GetY());
            player.PrintChar(pos.Item1, pos.Item2);
            //SetCursorPosition(0, 0);
            //exitRoom.PrintCoord();
            while (!gameOver)
            {
                ConsoleKey inp = Console.ReadKey(true).Key;
                // If an arrow is pressed, go to that direction by one tile
                if (inp == ConsoleKey.UpArrow && mOpen == false)
                {
                    if (pos.Item2 != printPosY + 1)
                    {
                        SetCursorPosition(pos.Item1, pos.Item2);
                        Write(" ");
                        pos = new Tuple<int, int>(pos.Item1, pos.Item2 - 1);
                        player.PrintChar(pos.Item1, pos.Item2);

                        foreach (Item item in currRoom.GetItemDrops())
                        {
                            if (item != null && pos.Item1 == item.GetX() && pos.Item2 == item.GetY())
                            {
                                if (player.AddItem(item)) currRoom.RemoveItem(item);
                            }
                        }

                        // Game over if player enters blue box exit
                        if (currRoom == exitRoom)
                        {
                            int i = printPosX + currRoom.GetWidth() * mapScale * 2 / 3;
                            int j = printPosY + currRoom.GetHeight() * mapScale / 2 + 3;
                            if (pos.Item2 == j && pos.Item1 >= i + 1 && pos.Item1 <= i + 8)
                            {
                                SetCursorPosition(0, 0);
                                WriteLine("Game Cleared");
                                gameOver = true;
                            }
                        }

                        // Random encounter
                        Enemy enemy = RandonEncounter();
                        if (EnemyCombat(player, enemy))
                        {
                            // Print room again after combat 
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            PrintExitRoom(currRoom, exitRoom);
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                    else
                    {
                        if (currRoom.GetUpDoor() == pos.Item1 - printPosX - 2
                          || currRoom.GetUpDoor() == pos.Item1 - printPosX - 1)
                        {
                            currRoom = currRoom.GetUp();
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            pos = new Tuple<int, int>(printPosX + currRoom.GetDownDoor() + 1, printPosY2 - 2);
                            PrintExitRoom(currRoom, exitRoom);
                            //player.SetHealth(10); // Replenish health
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                }
                if (inp == ConsoleKey.DownArrow && mOpen == false)
                {
                    if (pos.Item2 != printPosY2 - 2)
                    {
                        SetCursorPosition(pos.Item1, pos.Item2);
                        Write(" ");
                        pos = new Tuple<int, int>(pos.Item1, pos.Item2 + 1);
                        player.PrintChar(pos.Item1, pos.Item2);

                        foreach (Item item in currRoom.GetItemDrops())
                        {
                            if (item != null && pos.Item1 == item.GetX() && pos.Item2 == item.GetY())
                            {
                                if (player.AddItem(item)) currRoom.RemoveItem(item);
                            }
                        }

                        // Game over if player enters blue box exit
                        if (currRoom == exitRoom)
                        {
                            int i = printPosX + currRoom.GetWidth() * mapScale * 2 / 3;
                            int j = printPosY + currRoom.GetHeight() * mapScale / 2;
                            if (pos.Item2 == j && pos.Item1 >= i + 1 && pos.Item1 <= i + 8)
                            {
                                SetCursorPosition(0, 0);
                                WriteLine("Game Cleared");
                                gameOver = true;
                            }
                        }

                        // Random encounter
                        Enemy enemy = RandonEncounter();
                        if (EnemyCombat(player, enemy))
                        {
                            // Print room again after combat 
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            PrintExitRoom(currRoom, exitRoom);
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                    else
                    {
                        if (currRoom.GetDownDoor() == pos.Item1 - printPosX - 2
                          || currRoom.GetDownDoor() == pos.Item1 - printPosX - 1)
                        {
                            currRoom = currRoom.GetDown();
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            pos = new Tuple<int, int>(printPosX + currRoom.GetUpDoor() + 1, printPosY + 1);
                            PrintExitRoom(currRoom, exitRoom);
                            //player.SetHealth(10); // Replenish health
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                }
                if (inp == ConsoleKey.LeftArrow && mOpen == false)
                {
                    if (pos.Item1 != printPosX + 2)
                    {
                        SetCursorPosition(pos.Item1, pos.Item2);
                        Write(" ");
                        pos = new Tuple<int, int>(pos.Item1 - 1, pos.Item2);
                        player.PrintChar(pos.Item1, pos.Item2);

                        foreach (Item item in currRoom.GetItemDrops())
                        {
                            if (item != null && pos.Item1 == item.GetX() && pos.Item2 == item.GetY())
                            {
                                if (player.AddItem(item)) currRoom.RemoveItem(item);
                            }
                        }

                        // Game over if player enters blue box exit
                        if (currRoom == exitRoom)
                        {
                            int i = printPosX + currRoom.GetWidth() * mapScale * 2 / 3 + 8;
                            int j = printPosY + currRoom.GetHeight() * mapScale / 2;
                            if (pos.Item1 == i && pos.Item2 >= j && pos.Item2 <= j + 3)
                            {
                                SetCursorPosition(0, 0);
                                WriteLine("Game Cleared");
                                gameOver = true;
                            }
                        }

                        // Random encounter
                        Enemy enemy = RandonEncounter();
                        if (EnemyCombat(player, enemy))
                        {
                            // Print room again after combat 
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            PrintExitRoom(currRoom, exitRoom);
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                    else
                    {
                        if (currRoom.GetLeftDoor() == pos.Item2 - printPosY)
                        {
                            currRoom = currRoom.GetLeft();
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            pos = new Tuple<int, int>(printPosX2, printPosY + currRoom.GetRightDoor());
                            PrintExitRoom(currRoom, exitRoom);
                            //player.SetHealth(10); // Replenish health
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                }
                if (inp == ConsoleKey.RightArrow && mOpen == false)
                {
                    if (pos.Item1 != printPosX2)
                    {
                        SetCursorPosition(pos.Item1, pos.Item2);
                        Write(" ");
                        pos = new Tuple<int, int>(pos.Item1 + 1, pos.Item2);
                        player.PrintChar(pos.Item1, pos.Item2);

                        foreach (Item item in currRoom.GetItemDrops())
                        {
                            if (item != null && pos.Item1 == item.GetX() && pos.Item2 == item.GetY())
                            {
                                if (player.AddItem(item)) currRoom.RemoveItem(item);
                            }
                        }

                        // Game over if player enters blue box exit
                        if (currRoom == exitRoom)
                        {
                            int i = printPosX + currRoom.GetWidth() * mapScale * 2 / 3;
                            int j = printPosY + currRoom.GetHeight() * mapScale / 2;
                            if (pos.Item1 == i && pos.Item2 >= j && pos.Item2 <= j + 3)
                            {
                                SetCursorPosition(0, 0);
                                WriteLine("Game Cleared");
                                gameOver = true;
                            }
                        }

                        // Random encounter
                        Enemy enemy = RandonEncounter();
                        if (EnemyCombat(player, enemy))
                        {
                            // Print room again after combat 
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            PrintExitRoom(currRoom, exitRoom);
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                    else
                    {
                        if (currRoom.GetRightDoor() == pos.Item2 - printPosY)
                        {
                            currRoom = currRoom.GetRight();
                            PrintRoom(currRoom, pos.Item1, pos.Item2);
                            pos = new Tuple<int, int>(printPosX + 2, printPosY + currRoom.GetLeftDoor());
                            PrintExitRoom(currRoom, exitRoom);
                            //player.SetHealth(10); // Replenish health
                            player.PrintChar(pos.Item1, pos.Item2);
                        }
                    }
                }
                // If user presses 'm', show or hide the map
                if (inp == ConsoleKey.M)
                {
                    SetCursorPosition(0, 0);
                    if (!mOpen)
                    {
                        mOpen = true;
                        // Display the map and highlight current room
                        PrintMap(lab);
                        HighlightCurrRoom(currRoom);
                    }
                    else
                    {
                        // Close map and return to original screen by using
                        // previously saved charafcter position
                        mOpen = false;
                        PrintRoom(currRoom, pos.Item1, pos.Item2);
                        PrintExitRoom(currRoom, exitRoom);
                        player.PrintChar(pos.Item1, pos.Item2);
                    }
                }
            }
            // TO IMPLEMENT :
            // i TO USE INVENTORY (CAN HEAL FROM THERE)
        }
    }
}