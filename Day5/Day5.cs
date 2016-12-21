using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Day5
{
    class Day5
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

        public static string CalculateMD5Hash(string input)
        {
            //    MD5 md5Hasher = MD5.Create();
            //    byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            //    return BitConverter.ToString(data);
            //}
            //{
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < hash.Length; i++)
            //{
            //    sb.Append(hash[i].ToString("X2"));
            //}
            if (hash.ElementAt(0).Equals(00000000) && (hash.ElementAt(1).Equals(00000000)))
            {
                return ToHex(hash);
            }
            else return " ";
        }

        private static void checkHash(int index, ref char[] password, string inputString, ref int numberFound)
        {
            string hash = CalculateMD5Hash(inputString + index);
            if (hash.StartsWith("00000"))
            {
                int hashIndex = Convert.ToInt32(hash[5]) - 48;
                if (hashIndex < 8 && password[hashIndex] == '#')
                {
                    Console.Write(hash);
                    //if (hashIndex < 8 && password[hashIndex] == '#')
                    //{
                    password[hashIndex] = hash[6];
                    numberFound++;
                    Console.Write(password);
                }
            }
            if (numberFound > 7)
            {
                Console.Write(password);
                Console.WriteLine();
                Console.ReadLine();
            }
        }


        static void Main(string[] args)
        {
            string inputString = "abbhdwsy";
            char[] password = { '#', '#', '#', '#', '#', '#', '#', '#' };
            //StringBuilder password = new StringBuilder(8);
            bool passwordFound = false;
            int numberFound = 0;
            int index = 0;

            while (numberFound < 8)
            {
                checkHash(index, ref password, inputString, ref numberFound);
                //Task task2 = Task.Factory.StartNew(() => checkHash(index+1, ref password, inputString, ref numberFound));
                //Task task3 = Task.Factory.StartNew(() => checkHash(index+2, ref password, inputString, ref numberFound));
                //Task task4 = Task.Factory.StartNew(() => checkHash(index+3, ref password, inputString, ref numberFound));
                //string hash = CalculateMD5Hash(inputString + index);
                //if (hash.StartsWith("00000"))
                //{
                //    int hashIndex = Convert.ToInt32(hash[5]) - 48;
                //    if (hashIndex < 8 && password[hashIndex] == '#')
                //    {
                //        Console.Write(hash);
                //    //if (hashIndex < 8 && password[hashIndex] == '#')
                //    //{
                //        password[hashIndex] = hash[6];
                //        numberFound++;
                //        Console.Write(password);
                //    }
                //}
                //if (numberFound > 7)
                //{
                //    Console.Write(password);
                //    Console.WriteLine();
                //    Console.ReadLine();
                //}
                index++;
            }

            //Part 1
            //while (!passwordFound)
            //{
            //    string hash = CalculateMD5Hash(inputString + index);
            //    if (hash.StartsWith("00000")
            //    {
            //        password.Append(hash[5]);
            //    }
            //    if (password.Length > 8)
            //    {
            //        passwordFound = true;
            //        Console.Write(password);
            //        Console.WriteLine();
            //        Console.ReadLine();
            //    }
            //    index++;
            //}
        }
    }
}
