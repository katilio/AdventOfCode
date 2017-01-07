using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventDay19
{
    class Program
    {
        public static uint ClosestSmallerPowerOf2(uint x)
        {
            x = x | (x >> 1);
            x = x | (x >> 2);
            x = x | (x >> 4);
            x = x | (x >> 8);
            x = x | (x >> 16);
            return x - (x >> 1);
        }

        public static uint getSafePosition1(uint n)
        {
            // find value of L for the equation
            //uint intValue = n;
            //uint MSB = intValue & 0xFFFF0000;
            uint MSB = ClosestSmallerPowerOf2(n);
            uint valueOfL = n - MSB;
            uint safePosition = 2 * valueOfL + 1;

            return safePosition;
        }

        public static int IntPow(int x, int pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }

        public static int getSafePosition2(int n)
        {
            int solution;
            int PP3 = (int)Math.Log(n, 3);
            int NP3 = (int)Math.Log(n, 3) + 1;
            PP3 = IntPow(3, PP3);
            NP3 = IntPow(3, NP3);
            int diff = NP3 - n;
            if (n < (2 * PP3))
            {
                solution = n - PP3;   
            }
            else { solution = 2 * (n - (2 * PP3)) + PP3; }
            //For N, get the next higher power of 3 (NP3) and previous power of 3 (PP3)
            //Difference between N and NP3 or PP3
            //If N is between PP3+1 and PP3+PP3, solution is 1 for PP3+1, 2 for PP3+2, etc
            //If N is higher than 2*PP3, then solution is PP3+2x, where x is the difference between N and 2*PP3
            return solution;
        }

        static void Main(string[] args)
        {
            //Part 1
            uint puzzleInput1 = 3014387;
            uint solution = getSafePosition1(puzzleInput1);
            Console.Write("Solution = " + solution+ "\n\n");
            //Console.ReadLine();
            //Part 2
            int puzzleInput2 = 3014387;

            int solution2 = getSafePosition2(puzzleInput2);
            Console.Write(solution2);
            Console.ReadLine();


        }
    }
}
