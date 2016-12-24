using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventDay8
{
    class Program
    {
        public static int SizeY = 6;
        public static int SizeX = 50;

        public static void initScreen(ref char[,] screen)
        {
            int column = 0;
            while (column <= screen.GetUpperBound(1))
            {
                for (int i = 0; i <= screen.GetUpperBound(0); i++)
                {
                    screen[i, column] = '-';
                }
                column++;
            }
        }

        public static void displayScreen(char[,] screen)
        {

            int column = 0;
            while (column <= screen.GetUpperBound(0))
            {
                for (int i = 0; i <= screen.GetUpperBound(1); i++)
                {
                    Console.Write(screen[column, i]);
                }
                Console.Write("\n");
                column++;
            }
            Console.Write("\n\n\n");
        }

        public static void makeRectangle(ref char[,] screen, int x, int y)
        {
            int column = 0;
            while (column < x && column <= screen.GetUpperBound(1))
            {
                for (int i = 0; i < y && i <= screen.GetUpperBound(0); i++)
                {
                    screen[i, column] = 'X';
                }
                column++;
            }
        }

        public static void rotateRow(ref char[,] screen, int rowToMove, int spacesToMove)
        {
            //Make copy of current row
            char[] rowCopy = new char[screen.GetUpperBound(1) +1];
            for (int i = 0; i <= screen.GetUpperBound(1); i++)
            {
                rowCopy[i] = screen[rowToMove, i];
            }

            //Re-initialize the row in screen
            for (int i = 0; i <= screen.GetUpperBound(1); i++)
            {
                screen[rowToMove, i] = '-';
            }

            //Copy the copy back to Screen in the updated indices
            for (int i = 0; i < rowCopy.Length; i++)
            {
                int copyIndex = i + spacesToMove;
                if (copyIndex > rowCopy.Length) { copyIndex = (copyIndex % rowCopy.Length); }
                else if (copyIndex == rowCopy.Length) { copyIndex = (copyIndex % rowCopy.Length); }
                screen[rowToMove, copyIndex] = rowCopy[i];
            }
        }

        public static void rotateColumn(ref char[,] screen, int columnToMove, int spacesToMove)
        {
            //Make copy of current column
            char[] columnCopy = new char[screen.GetUpperBound(0) + 1];
            for (int i = 0; i <= screen.GetUpperBound(0); i++)
            {
                columnCopy[i] = screen[i, columnToMove];
            }

            //Re-initialize the column in screen
            for (int i = 0; i <= screen.GetUpperBound(0); i++)
            {
                screen[i, columnToMove] = '-';
            }

            //Copy the copy back to Screen in the updated indices
            for (int i = 0; i < columnCopy.Length; i++)
            {
                int copyIndex = i + spacesToMove;
                if (copyIndex > columnCopy.Length) { copyIndex = (copyIndex % columnCopy.Length); }
                else if (copyIndex == columnCopy.Length) { copyIndex = (copyIndex % columnCopy.Length); }
                screen[copyIndex, columnToMove] = columnCopy[i];
            }
        }


        static void Main(string[] args)
        {
            List<string> input = Advent.Shared.getInputList("Day8.txt");
            char[,] screen = new char[SizeY, SizeX];
            initScreen(ref screen);
            //TEST
            //makeRectangle(ref screen, 49, 40);
            //displayScreen(screen);
            //Console.Write("\n\n\n");
            //rotateRow(ref screen, 1, 34);
            //displayScreen(screen);
            //rotateRow(ref screen, 2, 30);
            //displayScreen(screen);
            //rotateRow(ref screen, 3, 30);
            //displayScreen(screen);
            //rotateColumn(ref screen, 3, 3);
            //displayScreen(screen);
            //rotateColumn(ref screen, 3, 3);
            //displayScreen(screen);
            //Console.ReadLine();
            //TEST

            //PART 1
            foreach (string line in input)
            {
                if (line.StartsWith("rec"))
                {
                    string s = line.Remove(0, "rect ".Length);
                    var a = s.Split('x');
                    int x = int.Parse(a[0]);
                    int y = int.Parse(a[1]);
                    makeRectangle(ref screen, x, y);
                }
                else if (line.StartsWith("rotate row"))
                {
                    var s = line.Split(' ');
                    var on = s[1];
                    var row = int.Parse(s[2].Substring(2));
                    var amount = int.Parse(s[4]);
                    rotateRow(ref screen, row, amount);
                }
                else if (line.StartsWith("rotate column"))
                {
                    var s = line.Split(' ');
                    var on = s[1];
                    var column = int.Parse(s[2].Substring(2));
                    var amount = int.Parse(s[4]);
                    rotateColumn(ref screen, column, amount);
                }
                Console.Write("\n ____________ \n" + line + "\n");
                displayScreen(screen);
            }
            displayScreen(screen);
            int pixelsOn = 0;
            foreach (char character in screen)
            {
                if (character == 'X') { pixelsOn++; }
            }
            Console.Write("\n\n\n" + pixelsOn);
            Console.ReadLine();
        }
    }
}
