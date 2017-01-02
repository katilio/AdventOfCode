using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventDay18
{
    class Program
    {
        static void Main(string[] args)
        {
            List<char[]> Grid = new List<char[]>();
            int numberOfSafeTiles = 0;
            //Adding imaginary safe tiles on the sides
            string input = ".^^^^......^...^..^....^^^.^^^.^.^^^^^^..^...^^...^^^.^^....^..^^^.^.^^...^.^...^^.^^^.^^^^.^^.^..^.^.";
            foreach (char Char in input)
            {
                if (Char == '.') { numberOfSafeTiles++; }
            }
            numberOfSafeTiles -= 2; //Remove the extra imaginary safe tiles on the sides

            for (int i = 0; i < 400000; i++)
            {
                Grid.Add(input.ToCharArray());
            }

            for (int i = 1; i < 400000; i++)
            {
                for (int o = 1; o < Grid[i].Length-1; o++)
                {
                    if ((Grid[i-1][o-1] == '.' && Grid[i - 1][o] == '^' && Grid[i - 1][o + 1] == '^') 
                        || (Grid[i - 1][o - 1] == '^' && Grid[i - 1][o] == '^' && Grid[i - 1][o + 1] == '.')
                        || (Grid[i - 1][o - 1] == '.' && Grid[i - 1][o] == '.' && Grid[i - 1][o + 1] == '^')
                        || (Grid[i - 1][o - 1] == '^' && Grid[i - 1][o] == '.' && Grid[i - 1][o + 1] == '.')
                        )
                    {
                        Grid[i][o] = '^';
                    }
                    else
                    {
                        Grid[i][o] = '.';
                        numberOfSafeTiles++;
                    }
                }
            }

            //For visualization with small numbers / debugging only
            //for (int i = 0; i < 40; i++)
            //{
            //    for (int o = 1; o < Grid[i].Length - 1; o++)
            //    {
            //        Console.Write(Grid[i][o]);
            //    }
            //    Console.Write("\n");
            //}
            Console.Write("Number of safe tiles = " + numberOfSafeTiles);
            Console.ReadLine();
        }
    }
}
