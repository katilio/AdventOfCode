using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using Advent;

namespace AdventDay3
{
    class Program
    {
        public struct Car
        {
            public int posX;
            public int posY;
            public int direction;
            public int[,] part2Matrix;

            public void moveCar(string direction, ref Car car)
            {
                string turning = direction[0].ToString();

                switch (turning)
                {
                    case ("R"):
                        if (car.direction == 4) { car.direction = 1; }
                        else { car.direction++; }
                        break;
                    case ("L"):
                        if (car.direction == 1) { car.direction = 4; }
                        else { car.direction--; }
                        break;
                }

                int blocksToMove = Convert.ToInt32(direction.Substring(1, direction.Length - 1));

                switch (car.direction)
                {
                    case (1):
                        for (int i = 1; i <= blocksToMove; i++)
                        {
                            car.posY++;
                            AddToMatrix(ref car.part2Matrix, ref car);
                        }
                        break;
                    case (2):
                        for (int i = 1; i <= blocksToMove; i++)
                        {
                            car.posX++;
                            AddToMatrix(ref car.part2Matrix, ref car);
                        }
                        break;
                    case (3):
                        for (int i = 1; i <= blocksToMove; i++)
                        {
                            car.posY--;
                            AddToMatrix(ref car.part2Matrix, ref car);
                        }
                        break;
                    case (4):
                        for (int i = 1; i <= blocksToMove; i++)
                        {
                            car.posX--;
                            AddToMatrix(ref car.part2Matrix, ref car);
                        }
                        break;
                }
            }

            public void AddToMatrix(ref int[,] matrix, ref Car car)
            {
                matrix[car.posX+500, car.posY+500]++;
                if (matrix[car.posX+500, car.posY+500] >= 2)
                {
                    Console.WriteLine("First location revisited at " + car.posX + " " + car.posY + ", distance from start = " + (Math.Abs(car.posX) + Math.Abs(car.posY)));
                }
            }
        }


        static void Main(string[] args)
        {
            string[] input = Shared.getInput("Day1.txt");
            string direction;
            Car car = new Car();
            car.posX = 0;
            car.posY = 0;
            car.direction = 1;
            car.part2Matrix = new int[1000, 1000];
            string[,] visited = new string[input[0].Trim().Length, 2];
            int row = 0;
            int column = 0;


            do
            {
                int breakIndex = input[0].IndexOf(',');
                if (breakIndex == -1)
                {
                    direction = input[0].Substring(0, (input[0].Length));
                    input[0] = "";
                }

                else
                {
                    direction = input[0].Substring(0, breakIndex);
                    input[0] = input[0].Remove(0, breakIndex + 2);
                }
                car.moveCar(direction, ref car);
                visited[row, column] = car.posX.ToString();
                column++;
                visited[row, column] = car.posY.ToString();
                column = 0;
                row++;
                //if (row > 1)
                //{
                //    for (int visitedDirection = 0; visitedDirection < row - 1; visitedDirection++)
                //    {
                //        if (Convert.ToInt32(visited[visitedDirection, 0]) == car.posX && Convert.ToInt32(visited[visitedDirection, 1]) == car.posY)
                //        {
                //            Console.Write("Revisited at index " + visitedDirection + " and direction: " + visited[visitedDirection, 0] + " " + visited[visitedDirection, 1] + ".\n");
                //        }
                //        {

                //        }
                //    }
                //}
            } while (input[0].Length > 0);
            int distance = Math.Abs(car.posX) + Math.Abs(car.posY);
            Console.Write(distance);
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}