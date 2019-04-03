using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace labrynthGame
{
    class Room
    {
        private int width, height;

        // Position for door; if 0, no door
        private bool hasUp, hasDown, hasLeft, hasRight;
        private Room up, down, left, right;
        private int upDoor, downDoor, leftDoor, rightDoor;

        private bool isSet = false;

        public Room (int w, int h)
        {
            // Set room size
            this.width = w;
            this.height = h;

            // Randomly determine if the room has rooms in each direction
            Random r = new Random();
            this.hasUp = (r.Next(100) > 50 ? true : false);
            this.hasDown = (r.Next(100) > 50 ? true : false);
            this.hasLeft = (r.Next(100) > 50 ? true : false);
            this.hasRight = (r.Next(100) > 50 ? true : false);
        }
        public void SetSet (bool inp)
        {
            this.isSet = inp;
        }
        public bool GetSet ()
        {
            return this.isSet;
        }
        public void SetDirRoom (string dir, Room room)
        {
            if (dir == "u")
            {
                this.hasUp = true;
                this.up = room;
            }
            else if (dir == "d")
            {
                this.hasDown = true;
                this.down = room;
            }
            else if (dir == "l")
            {
                this.hasLeft = true;
                this.left = room;
            }
            else
            {
                this.hasRight = true;
                this.right = room;
            }
            
        }
        public void SetNearbyRooms (Room up, Room down, Room left, Room right)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }
        public string RoomDirection (Room target)
        {
            if (this.up == target) return "u";
            else if (this.down == target) return "d";
            else if (this.left == target) return "l";
            else if (this.right == target) return "r";
            else return "x";
        }
    }
    class Program
    {
        // Room methods
        static void SetReverseConnection (Room origin, Room adjacent)
        {
            string dir = origin.RoomDirection(adjacent);
            if (dir == "u" || dir == "d" || dir == "l" || dir == "r")
                adjacent.SetDirRoom(dir, origin);
            else WriteLine("Room not next to origin");
        }
        static void GenRandRoomConnection (Room room)
        {

        }
        // Array methods
        static void ArrayAppend (Room[] array, Room room)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null) array[i] = room;
                return;
            }
        }
        // Main
        static void Main(string[] args)
        {
            // Map Size
            int x = 9, y = 9;
            Room[] allRooms = new Room[x * y];
            Room[,] labrynth = new Room[x, y]; /* start room (4, 4) */

            Random r = new Random();
            
            Room origin = new Room(10, 10); /* Initialize origin room */
            labrynth[x / 2, y / 2] = origin;
            ArrayAppend(allRooms, origin);

            // Initialize first four rooms adjacent to origin
            Room upRoom = new Room(r.Next(3, 10), r.Next(3, 10));
            Room downRoom = new Room(r.Next(3, 10), r.Next(3, 10));
            Room leftRoom = new Room(r.Next(3, 10), r.Next(3, 10));
            Room rightRoom = new Room(r.Next(3, 10), r.Next(3, 10));
            labrynth[x, y + 1] = upRoom;
            labrynth[x, y - 1] = downRoom;
            labrynth[x - 1, y] = leftRoom;
            labrynth[x + 1, y] = rightRoom;
            ArrayAppend(allRooms, upRoom);
            ArrayAppend(allRooms, downRoom);
            ArrayAppend(allRooms, leftRoom);
            ArrayAppend(allRooms, rightRoom);

            labrynth[4, 4].SetNearbyRooms(upRoom, downRoom, leftRoom, rightRoom);
            SetReverseConnection(origin, upRoom);
            SetReverseConnection(origin, downRoom);
            SetReverseConnection(origin, leftRoom);
            SetReverseConnection(origin, rightRoom);

            origin.SetSet(true);

            foreach (Room room in allRooms)
            {
                if (!room.GetSet())
                {
                    
                }
            }
        }
    }
}
