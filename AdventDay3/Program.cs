using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventDay3
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] triangleData = { "" };
            int possibleTriangles = 0;
            int impossibleTriangles = 0;

            foreach (var line in File.ReadLines(@"input.txt"))
            {
                //var arr = Regex.Split(line.Trim(), @"(\d+)[ ]+(\d+)[ ]+(\d+)");

                triangleData = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                int sum = Convert.ToInt32(triangleData[0]) + Convert.ToInt32(triangleData[1]) + Convert.ToInt32(triangleData[2]);
                int max = Math.Max((Math.Max((Convert.ToInt32(triangleData[0])), Convert.ToInt32(triangleData[1]))), Convert.ToInt32(triangleData[2]));
                if (sum - max > max)
                {
                    possibleTriangles = possibleTriangles + 1;
                }
                //foreach (string cluster in triangleData)
                //{
                //    int sum = Convert.ToInt32(triangleData[cluster])
                //    if (cluster.Length == 3)
                //    {
                //        int side1 = Convert.ToInt32(cluster[0].ToString());
                //        int side2 = Convert.ToInt32(cluster[1].ToString());
                //        int side3 = Convert.ToInt32(cluster[2].ToString());
                //        //int sum = side1 + side2 + side3;
                //        int max = Math.Max((Math.Max(side1, side2)), side3);
                //        //if ((side1 + side2) > side3 && (side1 + side3) > side2 && (side2 + side3) > side1)
                //        if ((sum - max) > max)
                //        {
                //            possibleTriangles = possibleTriangles + 1;
                //        }
                //        else { impossibleTriangles++; }
                //    }

            }

            Console.Write("Number of possible triangles is: " + possibleTriangles);
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
