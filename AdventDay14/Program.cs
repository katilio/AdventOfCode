using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Day5;

namespace AdventDay14
{
    class Program
    {
        private static readonly string[] HexStringTable = new string[]
        {
            "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0A", "0B", "0C", "0D", "0E", "0F",
            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "1A", "1B", "1C", "1D", "1E", "1F",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "2A", "2B", "2C", "2D", "2E", "2F",
            "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3A", "3B", "3C", "3D", "3E", "3F",
            "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "4A", "4B", "4C", "4D", "4E", "4F",
            "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "5A", "5B", "5C", "5D", "5E", "5F",
            "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "6A", "6B", "6C", "6D", "6E", "6F",
            "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "7A", "7B", "7C", "7D", "7E", "7F",
            "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "8A", "8B", "8C", "8D", "8E", "8F",
            "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "9A", "9B", "9C", "9D", "9E", "9F",
            "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "AA", "AB", "AC", "AD", "AE", "AF",
            "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "BA", "BB", "BC", "BD", "BE", "BF",
            "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "CA", "CB", "CC", "CD", "CE", "CF",
            "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "DA", "DB", "DC", "DD", "DE", "DF",
            "E0", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "EA", "EB", "EC", "ED", "EE", "EF",
            "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "FA", "FB", "FC", "FD", "FE", "FF"
        };

        public static string ToHex(byte[] value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (value != null)
            {
                foreach (byte b in value)
                {
                    stringBuilder.Append(HexStringTable[b]);
                }
            }

            return stringBuilder.ToString();
        }

        private static string StretchHash(byte[] hash)
        {
            string ConvertedHash = ToHex(hash).ToLower();
            hash = Encoding.ASCII.GetBytes(ConvertedHash);
            var md5 = new Delay.MD5Managed();
            for (int i = 0; i < 2016; i++)
            {
                hash = md5.ComputeHash(hash);
                ConvertedHash = ToHex(hash).ToLower();
                if (i < 2015)
                {
                    hash = Encoding.ASCII.GetBytes(ConvertedHash);
                }
            }
            return ToHex(hash).ToLower();
        }

        //If there is a triplet in the string, add it to the triplets list along with the index where you found it
        static void CheckAndAddTriplet(string ConvertedHash, ref List<Triplet> Triplets, int index)
        {
            char currentChar;
            for (int i = 1; i < ConvertedHash.Length; i++)
            {
                currentChar = ConvertedHash[i];

                if (ConvertedHash[i-1] == currentChar && i + 1 < ConvertedHash.Length)
                {
                    if (ConvertedHash[i + 1] == currentChar)
                    {
                        Triplets.Add(new Triplet { index = index, Char = currentChar }); 
                        return;
                    }
                }
            }
        }

        //If there is a quint in the string, add it to the quints dictionary along with the index where you found it
        static bool CheckAndAddQuintet(string ConvertedHash, ref Dictionary<char,List<int>> Quints, int index)
        {
            char currentChar;
            for (int i = 0; i < ConvertedHash.Length; i++)
            {
                currentChar = ConvertedHash[i];
                if (ConvertedHash[i] == currentChar && i + 4 < ConvertedHash.Length)
                {
                    if (ConvertedHash[i + 3] == currentChar && ConvertedHash[i + 4] == currentChar)
                    {
                        if (ConvertedHash[i + 1] == currentChar && ConvertedHash[i + 2] == currentChar)
                        {
                            if (Quints.ContainsKey(currentChar)) { Quints[currentChar].Add(index); }
                            else
                            {
                                List<int> indexList = new List<int>();
                                indexList.Add(index);
                                Quints.Add(key: currentChar, value: indexList);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        //Given a triplet, find its corresponding quint
        static bool FindQuint(Dictionary<char, List<int>> Quints, char charToFind, int index)
        {
            if (Quints.ContainsKey(charToFind))
            {
                foreach (int IndexInList in Quints[charToFind])
                {
                    if (IndexInList > index && IndexInList < index + 1000)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //Main function after all hashes have been computed. For every triplet try to find a quint in the next 1000 indices
        static void FindKey(Dictionary<char, List<int>> Quints, List<Triplet> Triplets, ref int count, ref int KeyIndex)
        {
            int index = 0;
            int lastTripleIndex = 0;
            for (int i = 0; i < Triplets.Count(); i++)
            {
                index = Triplets[i].index;
                if (index > lastTripleIndex)
                {
                    char charToFind = Triplets[i].Char;
                    lastTripleIndex = index;
                    if (FindQuint(Quints, charToFind, index))
                    {
                        count++;
                        KeyIndex = index;
                    }
                }
                if (count == 64) {
                    return;
                }
            }
        }

        //static void FindQuintuple(ref int count, ref int KeyIndex, string salt, int index, char charToFind)
        //{
        //    for (int i = index; i < index+1000; i++)
        //    {
        //        string source = salt + i;
        //        byte[] BytesSource = Encoding.ASCII.GetBytes(source);
        //        var md5 = new Delay.MD5Managed();
        //        byte[] hash = md5.ComputeHash(BytesSource);
        //        string ConvertedHash = StretchHash(hash);
        //        for (int o = 0; o < ConvertedHash.Length - 3; o++)
        //        {
        //            if (ConvertedHash[o] == charToFind && o + 4 < ConvertedHash.Length)
        //            {
        //                if (ConvertedHash[o + 3] == charToFind && ConvertedHash[o + 4] == charToFind)
        //                {
        //                    if (ConvertedHash[o + 1] == charToFind && ConvertedHash[o + 2] == charToFind)
        //                    {
        //                        count++;
        //                        KeyIndex = index;
        //                        return;
        //                    }
        //                }
        //                else o += 2;
        //            }
        //        }
        //    }
        //}

        public class Triplet
        {
            public int index;
            public char Char;
        }

        static void Main(string[] args)
        {
            string salt = "cuanljph";
            int keysFound = 0;
            int lastPossibleKeyIndex = 0;
            int lastKeyIndex = 0;
            int index = 0;
            List<Triplet> Triplets = new List<Triplet>();
            Dictionary<char, List<int>> Quints = new Dictionary<char, List<int>>();
            //Calculate first 21k hashes (solution given this input is right before 21k, otherwise use 30k if you want to be safe)
            while (index < 21000)
            {
                string HashSource = salt + index.ToString();
                byte[] BytesSource = Encoding.ASCII.GetBytes(HashSource);
                var md5 = new Delay.MD5Managed();
                byte[] hash = md5.ComputeHash(BytesSource);
                string ConvertedHash = ToHex(hash).ToLower();
                hash = Encoding.ASCII.GetBytes(ConvertedHash);
                //Same thing as StretchHash()
                for (int i = 0; i < 2016; i++)
                {
                    hash = md5.ComputeHash(hash);
                    ConvertedHash = ToHex(hash).ToLower();
                    if (i < 2015)
                    { 
                        hash = Encoding.ASCII.GetBytes(ConvertedHash);
                    }
                }
                ConvertedHash = ToHex(hash).ToLower();

                CheckAndAddQuintet(ConvertedHash, ref Quints, index);
                CheckAndAddTriplet(ConvertedHash, ref Triplets, index);

                index++;
            }

            while (keysFound < 64)
            {
                FindKey(Quints, Triplets, ref keysFound, ref lastKeyIndex);
            }
                
            Console.Write(lastKeyIndex-1);
            Console.ReadLine();
        }
    }
}
