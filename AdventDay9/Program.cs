using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventDay9
{
    class Day9
    {
        public static void AppendToOutput(ref StringBuilder output, string input, int startIndex, int length, int repetitions)
        {
            int cycles = 0;
            if (repetitions == 0) { output.Append(input[startIndex]); }
            else
            {
                while (cycles <= repetitions - 1)
                {
                    int index = startIndex;
                    for (int i = 0; i < length; i++)
                    {
                        output.Append(input[index]);
                        index++;
                    }
                    Console.Write(output + "\n\n");
                    cycles++;
                }
            }
        }

        static void Main(string[] args)
        {
            String[] input = Advent.Shared.getInput("Day9.txt");
            StringBuilder output = new StringBuilder();
            //Index to know where in the input we are
            int currentIndex = 0;
            Int64 characterCount = 0;

            List<int> charMultipliers = new List<int>();

            foreach (string line in input)
            {
                foreach (char c in line)
                {
                    charMultipliers.Add(1);
                }
            }

            foreach (string line in input)
            {
                for (int i = 0; currentIndex < line.Length; i++)
                {
                    if (line[currentIndex] == '(')
                    {
                        //Determine where the marker ends
                        int markerEndIndex = line.IndexOf(')', currentIndex);
                        //+1 and -1 to avoid parenthesis
                        string markerData = line.Substring(currentIndex + 1, (markerEndIndex - currentIndex) - 1);
                        var marker = markerData.Split('x');
                        int length = int.Parse(marker[0]);
                        int repetitions = int.Parse(marker[1]);
                        
                        for (int o = 0; o < length; o++)
                        {
                            int index = (markerEndIndex + 1) + o;
                            charMultipliers[index] *= repetitions;
                        }
                        currentIndex += (markerData.Length + 2);

                        ////Part 1
                        //AppendToOutput(ref output, line, (currentIndex + markerData.Length + 2), length, repetitions);
                        //currentIndex += length + (markerData.Length+2);
                        ////Part 1
                    }
                    else
                    {
                        characterCount += charMultipliers[currentIndex];
                        currentIndex++;
                    }
                    //Part 1
                    //else
                    //{
                    //    AppendToOutput(ref output, line, currentIndex, 1, 0);
                    //    currentIndex++;
                    //}
                }
                
            }
            //Console.Write(output.Length);
            //Part 1

            Console.Write(characterCount);
            Console.ReadLine();
        }
    }
}
