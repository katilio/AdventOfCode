using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Advent;


namespace Advent
{

    public class Day4
    {
        public class Room
        {
            public char[] checksum;
            public char[] guessedChecksum = { 'a', 'a', 'a', 'a', 'a' };
            public char[] sectorID;
            public char[] name;
            public char[] sortedName;
            public List<ocurrancesByLetter> results;


            public List<ocurrancesByLetter> checkName(Room room)
            {
                List<ocurrancesByLetter> nameResults = new List<ocurrancesByLetter>();
                nameResults = initResultsTemplate(nameResults);
                for (char character = 'a'; character < 'a' + 26; character++)
                {
                    if (this.sortedName.Contains(character) == true)
                    {
                        int ocurrances = Math.Abs((Array.IndexOf(sortedName, character) - Array.LastIndexOf(sortedName, character))) + 1;
                        nameResults[Convert.ToInt32(character) - 97].ocurrances = ocurrances;
                    }
                    else { nameResults[Convert.ToInt32(character) - 97].ocurrances = 0; }
                }
                return nameResults;
            }
        }

        public class ocurrancesByLetter
        {
            public char letter { get; set; }
            public int ocurrances { get; set; }
        }

        static int convertCharArrayToInt(char[] array)
        {
            int value = 0;
            string tempString = "";
            foreach (char c in array)
            {
                tempString = tempString + Char.GetNumericValue(c).ToString();
            }
            value = Convert.ToInt32(tempString);
            return value;
        }

        static char[] decipherName(Room room)
        {
            char[] decodedName = room.name;
            int spacesToMove = convertCharArrayToInt(room.sectorID) % 26;
            for (int i = 0; i < decodedName.Length; i++)
            {
                if (decodedName[i] == '-') { decodedName[i] = ' '; }
                else decodedName[i] = Convert.ToChar((Convert.ToInt32(decodedName[i]) + spacesToMove));
                //97-122/a-z
                if (decodedName[i] > 'z' && decodedName[i] != ' ')
                {
                    int difference = decodedName[i] - 'z';
                    int baseValue = 96;
                    decodedName[i] = Convert.ToChar((baseValue) + difference);
                }
            }
            return decodedName;
        }

        public static List<ocurrancesByLetter> initResultsTemplate(List<ocurrancesByLetter> template)
        {
            char letter = 'a';
            for (int row = 0; row < 26; row++)
            {
                template.Add(new ocurrancesByLetter { letter = 'a', ocurrances = 0 });
                template[row].letter = letter;
                letter++;
            }
            return template;
        }

        static void Main(string[] args)
        {
            string[] input = Shared.getInput("Day4.txt");
            List<int> validSectorIDs = new List<int>();
            var Rooms = new List<Room>();

            //////////////////////

            string[,] resultsTemplate = new string[26, 2];
            char letter = 'a';
            for (int row = 0; row < 26; row++)
            {
                resultsTemplate[row, 0] = letter.ToString();
                letter++;
            }

            /////////////////////

            foreach (string line in input)
            {
                char[] checksum = line.Substring(line.Length - 6, 5).ToCharArray();
                char[] sectorID = line.Substring(line.Length - 10, 3).ToCharArray();
                char[] name = line.Substring(0, line.Length - 11).ToCharArray();
                char[] sortedName = line.Substring(0, line.Length - 11).ToCharArray();
                Array.Sort(sortedName);
                Rooms.Add(new Room
                {
                    checksum = checksum,
                    sectorID = sectorID,
                    name = name,
                    sortedName = sortedName,
                });
            }

            foreach (Room room in Rooms)
            {
                room.results = room.checkName(room);
                List<ocurrancesByLetter> sortedResults = room.results.OrderByDescending(o => o.ocurrances).ToList();
                for (int i = 0; i < 5; i++)
                {
                    room.guessedChecksum[i] = sortedResults[i].letter;
                }
                if (room.guessedChecksum.SequenceEqual(room.checksum))
                {
                    room.name = decipherName(room);
                    Console.Write(new string(room.name) + " " + "| Sector ID: " + convertCharArrayToInt(room.sectorID) + "\n");
                    File.AppendAllText("log.txt", new string(room.name) + " " + "| Sector ID: " + convertCharArrayToInt(room.sectorID) + Environment.NewLine);
                    int convertedSectorID = convertCharArrayToInt(room.sectorID);
                    validSectorIDs.Add(convertedSectorID);
                }
                
            }


            Process.Start("log.txt");

            Console.Write("Sum of valid sector IDs is: " + validSectorIDs.Sum());
            Console.WriteLine();
            Console.ReadLine();

        }
    }
}