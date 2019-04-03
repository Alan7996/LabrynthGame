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

        private int upDoor, downDoor, leftDoor, rightDoor;
        private Room up, down, left, right;
    }
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            bool roomExist = (r.Next(100) > 50 ? true : false);

            Room[,] Labrynth = new Room[9, 9]; /* start room (4, 4) */


        }
    }
}
