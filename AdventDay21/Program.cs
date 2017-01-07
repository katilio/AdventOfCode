using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventDay21
{
    class Program
    {
        public static void SwapPosition(ref string String, int index1, int index2)
        {
            if (index1 == index2)
            {
                return;
            }
            else
            {
                if (index1 > index2)
                {
                    var tempIndex = index2; index2 = index1; index1 = tempIndex;
                }

            StringBuilder sb = new StringBuilder();
            sb.Append(String.Substring(0, index1));
            sb.Append(String[index2]);
            sb.Append(String.Substring(index1 + 1, (index2 - index1)-1));
            sb.Append(String[index1]);
            sb.Append(String.Substring(index2 + 1, (String.Length - index2)-1));
            String = sb.ToString();
                
            }
        }

        public static void SwapLetters(ref string String, char letter1, char letter2)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < String.Length; i++)
            {
                sb.Append(String[i] == letter1 ? letter2 : String[i] == letter2 ? letter1 : String[i]);
            }
            String = sb.ToString();
        }

        public static void ReversePositions(ref string String, int index1, int index2)
        {
            StringBuilder sb = new StringBuilder();
            if (index1 > 0)
            {
                sb.Append(String.Substring(0, index1));
            }
            for (int i = index2; i >= index1; i--)
            {
                sb.Append(String[i]);
            }
            if (index2 < String.Length - 1)
            {
                sb.Append(String.Substring(index2 + 1, (String.Length - index2) - 1));
            }
            String = sb.ToString();
        }

        public static void MovePositionXToY(ref string String, int index1, int index2)
        {
            StringBuilder sb = new StringBuilder();
            char originalLetter = String[index1];
            if (index1 < index2)
            {
                if (index1 > 0)
                {
                    sb.Append(String.Substring(0, index1));
                }
                for (int i = index1 + 1; i <= index2; i++)
                {
                    sb.Append(String[i]);
                }
                sb.Append(originalLetter);
                if (index2 < String.Length - 1)
                {
                    sb.Append(String.Substring(index2 + 1, (String.Length - index2) - 1));
                }
                String = sb.ToString();
            }

            if (index2 < index1)
            {
                if (index2 > 0)
                {
                    sb.Append(String.Substring(0, index2));
                }
                sb.Append(originalLetter);
                for (int i = index2; i < index1; i++)
                {
                    sb.Append(String[i]);
                }
                if (index1 < String.Length - 1)
                {
                    sb.Append(String.Substring(index1 + 1, (String.Length - index1) - 1));
                }
                String = sb.ToString();
            }
        }

        //Make substring from end (if rotating right) or beginning (if rotating left), 
        //with length equal to number of steps, append that to SB, then append the rest of the string
        public static void Rotate(ref string String, string direction, int steps)
        {
            if (steps <= 0) { return; }
            int realSteps = steps % String.Length;
            if (realSteps == 0) { return; }
            int startIndex = direction == "left" ? realSteps : (String.Length - realSteps);
            StringBuilder sb = new StringBuilder();
            int length = direction == "left" ? String.Length - realSteps : realSteps;
            sb.Append(String.Substring(startIndex, length));
            sb.Append(String.Substring(0, String.Length - sb.Length));
            String = sb.ToString();
        }

        public static void RotateBasedOnPosition(ref string String, char letter)
        {
                var index = String.IndexOf(letter);
                Rotate(ref String, "right", 1);
                Rotate(ref String, "right", index);
                if (index >= 4) { Rotate(ref String, "right", 1); };
        }

        public static void ReversedRotateBasedOnPosition(ref string String, char letter)
        {
                var index = String.IndexOf(letter);
            switch (index)
            {
                case (0):
                    Rotate(ref String, "left", 9);
                    break;
                case (1):
                    Rotate(ref String, "left", 1);
                    break;
                case (2):
                    Rotate(ref String, "left", 6);
                    break;
                case (3):
                    Rotate(ref String, "left", 2);
                    break;
                case (4):
                    Rotate(ref String, "left", 7);
                    break;
                case (5):
                    Rotate(ref String, "left", 3);
                    break;
                case (6):
                    Rotate(ref String, "left", 8);
                    break;
                case (7):
                    Rotate(ref String, "left", 4);
                    break;
            }
        }

        static void Main(string[] args)
        {
            string puzzleInput = "abcdefgh";
            string puzzleInputScrambled = "fbgdceah";
            var input = Advent.Shared.getInput("input.txt");

            //Scramble
            foreach (string str in input)
            {
                Console.Write(str + ":\n");
                var s = str.Split(' ');
                if (s[0].StartsWith("reverse"))
                {
                    ReversePositions(ref puzzleInput, Convert.ToInt32(s[2]), Convert.ToInt32(s[4]));
                }
                else if (s[0].StartsWith("rotate") && s[1] == "based")
                {
                    RotateBasedOnPosition(ref puzzleInput, s[6][0]);
                }
                else if (s[0].StartsWith("swap") && s[1] == "position")
                {
                    SwapPosition(ref puzzleInput, Convert.ToInt32(s[2]), Convert.ToInt32(s[5]));
                }
                else if (s[0].StartsWith("move"))
                {
                    MovePositionXToY(ref puzzleInput, Convert.ToInt32(s[2]), Convert.ToInt32(s[5]));
                }
                else if (s[0].StartsWith("swap") && s[1] == "letter")
                {
                    SwapLetters(ref puzzleInput, s[2][0], s[5][0]);
                }
                else if (s[0].StartsWith("rotate"))
                {
                    Rotate(ref puzzleInput, s[1], Convert.ToInt32(s[2]));
                }
                Console.Write(puzzleInput + "\n");
            }

            //Un-scramble
            for (int i = input.Length-1; i >= 0; i--)
            {
                string str = input[i];
                Console.Write(str + ":\n");
                var s = str.Split(' ');
                if (s[0].StartsWith("reverse"))
                {
                    ReversePositions(ref puzzleInputScrambled, Convert.ToInt32(s[2]), Convert.ToInt32(s[4]));
                }
                else if (s[0].StartsWith("rotate") && s[1] == "based")
                {
                    ReversedRotateBasedOnPosition(ref puzzleInputScrambled, s[6][0]);
                }
                else if (s[0].StartsWith("swap") && s[1] == "position")
                {
                    SwapPosition(ref puzzleInputScrambled, Convert.ToInt32(s[5]), Convert.ToInt32(s[2]));
                }
                else if (s[0].StartsWith("move"))
                {
                    MovePositionXToY(ref puzzleInputScrambled, Convert.ToInt32(s[5]), Convert.ToInt32(s[2]));
                }
                else if (s[0].StartsWith("swap") && s[1] == "letter")
                {
                    SwapLetters(ref puzzleInputScrambled, s[2][0], s[5][0]);
                }
                else if (s[0].StartsWith("rotate"))
                {
                    Rotate(ref puzzleInputScrambled, s[1] == "right" ? "left" : "right", Convert.ToInt32(s[2]));
                }
                Console.Write(puzzleInputScrambled + "\n");
            }

            Console.ReadLine();
        }
    }
}
