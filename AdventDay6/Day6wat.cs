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
            public List<List<char>> VerticalLines = new List<List<char>>();
            //public List<char> Line2 = new List<char>();
            //public List<char> Line3 = new List<char>();
            //public List<char> Line4 = new List<char>();
            //public List<char> Line5 = new List<char>();
            //public List<char> Line6 = new List<char>();
            //public List<char> Line7 = new List<char>();
            //public List<char> Line8 = new List<char>();

            public List<List<Day4.ocurrancesByLetter>> LineResults = new List<List<Day4.ocurrancesByLetter>>();
            //public List<Day4.ocurrancesByLetter> Line1Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line2Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line3Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line4Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line5Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line6Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line7Results = new List<Day4.ocurrancesByLetter>();
            //public List<Day4.ocurrancesByLetter> Line8Results = new List<Day4.ocurrancesByLetter>();

            public StringBuilder finalPassword = new StringBuilder();

            public Day6Data initializeData(ref Day6Data data)
            {
                for (int i = 0; i < 8; i++)
                { 
                    data.VerticalLines.Add(new List<char> { });
                    data.LineResults.Add(new List<Day4.ocurrancesByLetter> { });
                }
                foreach (List<Day4.ocurrancesByLetter> lineResult in data.LineResults)
                {
                    char letter = 'a';
                    for (int row = 0; row < 26; row++)
                    {
                        lineResult.Add(new Day4.ocurrancesByLetter { letter = 'a', ocurrances = 0 });
                        lineResult[row].letter = letter;
                        letter++;
                    }
                }
                return data;
            }

            public Day6Data populateLines(ref Day6Data data, string[] input)
            {
                foreach (string str in input)
                {
                    int index = 0;
                    foreach (char character in str)
                    {
                        data.VerticalLines[index].Add(character);
                        index++;
                    }
                }
                return data;
            }

            public Day6Data countLetters(ref Day6Data data)
            {
                int currentLine = 0;
                foreach (List<char> Line in data.VerticalLines)
                {
                    Line.Sort();
                    for (char character = 'a'; character < 'a' + 26; character++)
                        {
                        if (Line.Contains(character) == true)
                        {
                            int ocurrances = Math.Abs(Line.IndexOf(character) - Line.LastIndexOf(character)) + 1;
                            LineResults[currentLine][Convert.ToInt32(character) - 97].ocurrances = ocurrances;
                        }
                        else { LineResults[currentLine][Convert.ToInt32(character) - 97].ocurrances = 0; }
                    }
                    currentLine++;
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
            day6data.countLetters(ref day6data);
            List<List<Day4.ocurrancesByLetter>> sortedResults = new List<List<Day4.ocurrancesByLetter>>();
            for (int i = 0; i < 8; i++)
            {
                sortedResults.Add(new List<Day4.ocurrancesByLetter>(day6data.LineResults[i].OrderBy(o => o.ocurrances).ToList()));
                day6data.LineResults[i].OrderBy(o => o.ocurrances).ToList();
            }
            char[] pass = new char[12];
            int passIndex = 0;
            foreach (List<Day4.ocurrancesByLetter> lineResult in sortedResults)
            {
                pass[passIndex] = lineResult[0].letter;
                passIndex++;
            }
            Console.Write(pass);
            Console.ReadLine();
            Console.Read();
            
        }
    }
}
