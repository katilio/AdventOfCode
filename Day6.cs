using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advent;


namespace Advent
{
    class Day6
    {

        public class Day6Data
        {
            public struct VerticalLine
            {
                List<char[]> line;
            } 

            public struct LineResults
            {
                List<Day4.ocurrancesByLetter> results;
            }

            public List<VerticalLine> Lines = new List<VerticalLine>();
            //public List<char> Line1 = new List<char>();
            //public List<char> Line2 = new List<char>();
            //public List<char> Line3 = new List<char>();
            //public List<char> Line4 = new List<char>();
            //public List<char> Line5 = new List<char>();
            //public List<char> Line6 = new List<char>();
            //public List<char> Line7 = new List<char>();
            //public List<char> Line8 = new List<char>();

            public List<LineResults> LinesResults = new List<LineResults>();
            //public List<Day4.ocurrancesByLetter> Line1Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line2Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line3Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line4Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line5Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line6Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line7Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line8Results = new List<Day4.ocurrancesByLetter>();

            public StringBuilder finalPassword = new StringBuilder();

            public Day6Data initializeResults(ref Day6Data data)
            {
                foreach (VerticalLine line in data.Lines)
                {
                    foreach (char character in )
                    {

                    }
                }
                return data;
            }

            public Day6Data populateLines(ref Day6Data data, string[] input)
            {
                foreach (string str in input)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        switch(i)
                        {
                            case 0:
                                data.Line1.Add(str[i]);
                                break;
                            case 1:
                                data.Line2.Add(str[i]);
                                break;
                            case 2:
                                data.Line3.Add(str[i]);
                                break;
                            case 3:
                                data.Line4.Add(str[i]);
                                break;
                            case 4:
                                data.Line5.Add(str[i]);
                                break;
                            case 5:
                                data.Line6.Add(str[i]);
                                break;
                            case 6:
                                data.Line7.Add(str[i]);
                                break;
                            case 7:
                                data.Line8.Add(str[i]);
                                break;
                        }
                    }
                    Line1.Sort();
                    Line2.Sort();
                    Line3.Sort();
                    Line4.Sort();
                    Line5.Sort();
                    Line6.Sort();
                    Line7.Sort();
                    Line8.Sort();
                }
                return data;
            }

            public Day6Data countLetters(ref Day6Data data)
            {
                foreach (char character in Line1)
                {
                    if (data.Line1.Contains(character) == true)
                    {
                        int ocurrances = Math.Abs((Array.IndexOf(sortedName, character) - Array.LastIndexOf(sortedName, character))) + 1;
                        nameResults[Convert.ToInt32(character) - 97].ocurrances = ocurrances;
                    }
                    else { nameResults[Convert.ToInt32(character) - 97].ocurrances = 0; }
                }

                    else { nameResults[Convert.ToInt32(character) - 97].ocurrances = 0; }
                }
                return data;
            }
        }



        static void Main(string[] args)
        {
            string[] input = Shared.getInput("Day6.txt");
            List<Day4.ocurrancesByLetter> verticalLetters = new List<Day4.ocurrancesByLetter>();
            verticalLetters = Day4.initResultsTemplate(verticalLetters);

            Day6Data day6data = new Day6Data();

            day6data.initializeData(ref day6data);
            day6data.populateLines(ref day6data, input);
            


            Console.Write(day6data.Line1[0]);
            Console.ReadLine();
            Console.Read();
            
        }
    }
}
