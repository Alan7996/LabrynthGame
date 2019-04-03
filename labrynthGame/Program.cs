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

        public Room(int x, int y, Room[,] lab)
        {
            // Set room size
            this.x = x;
            this.y = y;
            
            Random r = new Random();
            // Randomly determine the room's width, height
            this.width = r.Next(3, 10);
            this.height = r.Next(3, 10);

            // Randomly determine if the room has rooms in each direction
            if (y == 0) this.hasUp = false;
            else this.hasUp = (r.Next(100) > 50 ? true : false);
            if (y == Y_SIZE - 1) this.hasDown = false;
            else this.hasDown = (r.Next(100) > 50 ? true : false);
            if (x == 0) this.hasLeft = false;
            else this.hasLeft = (r.Next(100) > 50 ? true : false);
            if (x == X_SIZE - 1) this.hasRight = false;
            else this.hasRight = (r.Next(100) > 50 ? true : false);

            lab[x, y] = this;
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
                    room.hasDown = true;
                    room.SetDirRoom("d", this);
                }
            }
            else if (dir == "d")
            {
                this.hasDown = true;
                this.down = room;
                if (room.GetUp() == null)
                {
                    room.hasUp = true;
                    room.SetDirRoom("u", this);
                }
            }
            else if (dir == "l")
            {
                this.hasLeft = true;
                this.left = room;
                if (room.GetRight() == null)
                {
                    room.hasRight = true;
                    room.SetDirRoom("r", this);
                }
            }
            else
            {
                this.hasRight = true;
                this.right = room;
                if (room.GetLeft() == null)
                {
                    room.hasLeft = true;
                    room.SetDirRoom("l", this);
                }
            }
            
        }
        public void SetNearbyRooms (Room up, Room down, Room left, Room right)
        {
            this.up = up;
            if (up != null) up.SetDirRoom("d", this);

            this.down = down;
            if (down != null) down.SetDirRoom("u", this);

            this.left = left;
            if (left != null) left.SetDirRoom("r", this);

            this.right = right;
            if (right != null) right.SetDirRoom("l", this);
        }
    }
    class Program
    {
        // Room methods
        static void MakeRoomSet (Room room, Room[] allArray, Room[,] lab)
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
                    else room.SetHasUp(false);
                }
                if (room.GetHasDown() && room.GetDown() == null)
                {
                    if (lab[room.GetX(), room.GetY() + 1] == null)
                    {
                        Room newRoom = new Room(room.GetX(), room.GetY() + 1, lab);
                        room.SetDirRoom("d", newRoom);
                        ArrayAppend(allArray, newRoom);
                    }
                    else room.SetHasDown(false);
                }
                if (room.GetHasLeft() && room.GetLeft() == null)
                {
                    if (lab[room.GetX() - 1, room.GetY()] == null)
                    {
                        Room newRoom = new Room(room.GetX() - 1, room.GetY(), lab);
                        room.SetDirRoom("l", newRoom);
                        ArrayAppend(allArray, newRoom);
                    }
                    else room.SetHasLeft(false);
                }
                if (room.GetHasRight() && room.GetRight() == null)
                {
                    if (lab[room.GetX() + 1, room.GetY()] == null)
                    {
                        Room newRoom = new Room(room.GetX() + 1, room.GetY(), lab);
                        room.SetDirRoom("r", newRoom);
                        ArrayAppend(allArray, newRoom);
                    }
                    else room.SetHasRight(false);
                }
                room.SetSet(true);
            }
        }
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
        // Main
        static void Main(string[] args)
        {
            Room[] allRooms = new Room[X_SIZE * Y_SIZE];
            Room[] endRooms = new Room[2 * (X_SIZE + Y_SIZE) - 4];
            Room[,] lab = new Room[X_SIZE, Y_SIZE];

            // Initialize origin room (at the center of the map)
            Room origin = new Room(X_ORIGIN, Y_ORIGIN, lab);
            ArrayAppend(allRooms, origin);

            // Initialize first four rooms adjacent to origin
            Room upRoom = new Room(X_ORIGIN, Y_ORIGIN - 1, lab);
            Room downRoom = new Room(X_ORIGIN, Y_ORIGIN + 1, lab);
            Room leftRoom = new Room(X_ORIGIN - 1, Y_ORIGIN, lab);
            Room rightRoom = new Room(X_ORIGIN + 1, Y_ORIGIN, lab);
            ArrayAppend(allRooms, upRoom);
            ArrayAppend(allRooms, downRoom);
            ArrayAppend(allRooms, leftRoom);
            ArrayAppend(allRooms, rightRoom);
            origin.SetNearbyRooms(upRoom, downRoom, leftRoom, rightRoom);
            origin.SetSet(true);

            bool allRoomSet = false;

            // Completely initialize the whole map
            while (!allRoomSet)
            {
                foreach (Room room in allRooms)
                {
                    MakeRoomSet(room, allRooms, lab);
                }
                allRoomSet = true;
                // maybe this can be imporved?
                foreach (Room room in allRooms)
                {
                    if (room != null && !room.GetSet()) allRoomSet = false;
                }
            }

            foreach (Room room in allRooms)
            {
                if (room != null && (room.GetX() == 0 || room.GetX() == 
                  X_SIZE - 1 || room.GetY() == 0 || room.GetY() == Y_SIZE - 1))
                {
                    ArrayAppend(endRooms, room); /* I don't think this is correct */ 
                }
            }

            // Try printing the entire map and end rooms and see how this works
        }
    }
}
