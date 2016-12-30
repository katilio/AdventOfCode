using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventDay15
{
    class Program
    {
        public class Disc
        {
            public int index;
            public int totalPositions;
            public int initialPosition;
            public int currentPosition;
        }

        public static void InitializeDiscList(ref List<Disc> List)
        {
            List.Add(new Disc { index = 0, totalPositions = 17, initialPosition = 5, currentPosition = 5 });
            List.Add(new Disc { index = 1, totalPositions = 19, initialPosition = 8, currentPosition = 8 });
            List.Add(new Disc { index = 2, totalPositions = 7, initialPosition = 1, currentPosition = 1 });
            List.Add(new Disc { index = 3, totalPositions = 13, initialPosition = 7, currentPosition = 7 });
            List.Add(new Disc { index = 4, totalPositions = 5, initialPosition = 1, currentPosition = 1 });
            List.Add(new Disc { index = 5, totalPositions = 3, initialPosition = 0, currentPosition = 0 });
            //Part 2
            List.Add(new Disc { index = 6, totalPositions = 11, initialPosition = 0, currentPosition = 0 });

        }

        public static void UpdateDiscPositions(ref List<Disc> List, int Tick, int CurrentDiscSlot)
        {
            List[CurrentDiscSlot].currentPosition = (List[CurrentDiscSlot].initialPosition + Tick) % List[CurrentDiscSlot].totalPositions;
        }

        public static bool CanBallFall(ref List<Disc> List, int Tick)
        {
            int CurrentDiscSlot = 0;
            for (int i = Tick+1; i < List.Count() + Tick +1; i++)
            {
                UpdateDiscPositions(ref List, i, CurrentDiscSlot);
                if (List[CurrentDiscSlot].currentPosition == 0)
                {
                    //Part 2, == 5 for Part 1
                    if (CurrentDiscSlot == 6) { return true; }
                    CurrentDiscSlot++;
                }
                else
                {
                    return false;
                }
            }
            return false;

        }


        static void Main(string[] args)
        {
            bool foundNumber = false;
            int Tick = 0;
            List<Disc> DiscList = new List<Disc>();
            InitializeDiscList(ref DiscList);

            while (!foundNumber)
            {
                for (int i = 0; foundNumber == false; i++)
                {
                    if (CanBallFall(ref DiscList, i))
                    {
                        foundNumber = true;
                    }
                    Tick++;
                }
            }

            Console.Write(Tick-1);
            Console.ReadLine();
        }
    }
}
