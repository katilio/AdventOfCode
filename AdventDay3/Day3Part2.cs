using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Advent
{
    public class Shared
    {
        public static string[] getInput(string filename)
        {
            string[] input = File.ReadAllLines(@filename).Select(s => s.TrimStart()).ToArray();
            return input;
        }
        public static List<string> getInputList(string filename)
        {
            List<string> input = File.ReadAllLines(@filename).Select(s => s.TrimStart()).ToList<string>();
            return input;
        }
    }
}

namespace AdventDay3
{
    class Program
    {
        private static int[,] GetColumnTriangles(string[] input)
        {
            int[,] triangles = new int[input.Length, 3];
            var row = 0;
            var col = 0;
            foreach (string s in input)
            {
                var values = s.Split(new string[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
                triangles[col, row] = int.Parse(values[0]);
                triangles[col + 1, row] = int.Parse(values[1]);
                triangles[col + 2, row] = int.Parse(values[2]);
                row++;

                if (row == 3)
                {
                    row = 0;
                    col += 3;
                }
            }

            return triangles;
        }

        private static int GetValidTriangles(int[,] triangles)
        {
            var isValid = true;
            var validTriangles = 0;
            for (int i = 0; i < triangles.GetLength(0); i++)
            {
                isValid = (triangles[i, 0] + triangles[i, 1] > triangles[i, 2])
                        &&
                           (triangles[i, 2] + triangles[i, 0] > triangles[i, 1])
                        &&
                           (triangles[i, 1] + triangles[i, 2] > triangles[i, 0]);

                if (isValid)
                    validTriangles++;
            }

            return validTriangles;
        }

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines(@"Day3.txt").Select(s => s.TrimStart()).ToArray();
            int possibleTriangles = 0;
            int impossibleTriangles = 0;


            var columnTriangles = GetColumnTriangles(input);

            var validColumnTriangles = GetValidTriangles(columnTriangles);


            Console.WriteLine($"Among the listed triangles in columns, { validColumnTriangles } are possible(s).");

            Console.Write("Number of possible triangles is: " + possibleTriangles);
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
