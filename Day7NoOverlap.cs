using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day7
{
    class IP
    {
        public List<string> stringsInBrackets;
        public List<string> stringsOutsideBrackets;
        public bool hasAbba = false;
        public bool hasBab = false;
        public bool hasAba = false;
        public bool supportsSSL = false;
    }

    class Day7
    {
        public static bool IsStringABBA(string str)
        {
            char currentChar = ' ';
            char previousChar = ' ';
            int charIndex = 0;
            while (charIndex < str.Length)
            {
                foreach (char character in str)
                {
                    currentChar = character;
                    if (previousChar == character)
                    {
                        if (charIndex > 1 && charIndex < (str.Length - 1))
                        {
                            if (str[charIndex + 1] == str[charIndex - 2] && str[charIndex + 1] != currentChar)
                            { 
                                return true;
                            }
                        }
                    }
                    previousChar = currentChar;
                    charIndex++;
                }
            }
            return false;
        }

        public static Tuple<string, int> IsStringAba(string str, int charIndex)
        {
            if (charIndex > str.Length)
            {
                return Tuple.Create(" ", -1);
            }
            string Aba = " ";
            char currentChar = ' ';
            char lastLastChar = ' ';
            while (charIndex < str.Length)
            {
                for (int i = charIndex; i <= str.Length-1; i++)
                {
                    currentChar = str[charIndex];
                    if (charIndex > 1 && charIndex <= str.Length - 1)
                    {
                        lastLastChar = str[charIndex - 2];
                        if (lastLastChar == currentChar && str[charIndex - 1] != currentChar)
                        {
                            Aba = lastLastChar.ToString() + str[charIndex - 1].ToString() + currentChar.ToString();
                            return Tuple.Create(Aba, charIndex);
                        }
                    }
                    charIndex++;
                }
                return Tuple.Create(Aba, -1);
            }
            return Tuple.Create(Aba, -1);
        }

        public static bool IsStringBab(string str, string aba)
        {
            char B = aba[1];
            char A = aba[0];
            char currentChar = ' ';
            char lastLastChar = ' ';
            int charIndex = 0;
            while (charIndex < str.Length)
            {
                foreach (char character in str)
                {
                    currentChar = character;
                    if (charIndex > 1 && charIndex <= str.Length - 1)
                    {
                        lastLastChar = str[charIndex - 2];
                        if (lastLastChar == B && currentChar == B && str[charIndex - 1] == A && str[charIndex] == B)
                        {
                            //Console.Write("BAB found for aba " + aba + "!! IP supports SSL = " + lastLastChar.ToString() + str[charIndex - 1].ToString() + currentChar.ToString() + "\n");
                            return true;
                        }
                    }
                    charIndex++;
                }
                //Console.Write("BAB NOT found for ABA: " + aba + "\n");
                return false;
            }
            //Console.Write("BAB NOT found for ABA: " + aba + "\n");
            return false;
        }

        public static bool SupportSSL(IP address)
        {
            bool hasAba = address.hasAba;
            int abaIndex = 0;
            foreach (string str in address.stringsOutsideBrackets)
            {
                abaIndex = 0;

                while (abaIndex != -1)
                {
                    string aba = IsStringAba(str, abaIndex).Item1;
                    abaIndex = IsStringAba(str, abaIndex).Item2;
                 
                    if (aba != " ")
                    {
                        //Console.Write("ABA found!!" + "\n");
                        //Console.Write("ABA string: " + IsStringAba(str, abaIndex).Item1 + " and abaIndex: " + IsStringAba(str, abaIndex).Item2 + "\n");
                        foreach (string str2 in address.stringsInBrackets)
                        {
                            if (IsStringBab(str2, aba))
                            {
                                return true;
                            }
                        }
                        ////DERP - only check for Bab inside brackets
                        //foreach (string str3 in address.stringsOutsideBrackets)
                        //{
                        //    if (IsStringBab(str3, aba))
                        //    {
                        //        return true;
                        //    }
                        //}
                        ////DERP
                        abaIndex = IsStringAba(str, abaIndex + 1).Item2;
                    }
                }
            }
            return false;
        }


        static void Main(string[] args)
        {
            List<string> input = Advent.Shared.getInputList("Day7.txt");

            List<IP> IPList = new List<IP>();
            foreach (string line in input)
            {
                List<string> bracketedStrings = new List<string>();
                List<string> nonBracketedStrings = new List<string>();
                int startSearchIndex = 0;
                while (line.IndexOf('[', startSearchIndex) != -1)
                {
                    int startBracketIndex = line.IndexOf('[', startSearchIndex);
                    int endBracketIndex = line.IndexOf(']', startSearchIndex);
                    nonBracketedStrings.Add(line.Substring(startSearchIndex, (startBracketIndex - startSearchIndex)));
                    bracketedStrings.Add(line.Substring(startBracketIndex, (endBracketIndex - startBracketIndex)));
                    startSearchIndex = endBracketIndex + 1;
                }
                nonBracketedStrings.Add(((line.Substring(startSearchIndex, (line.Length - startSearchIndex)))));
                IPList.Add(new IP() { hasAbba = false, stringsInBrackets = bracketedStrings, stringsOutsideBrackets = nonBracketedStrings });
            }

            int IPsChecked = 0;
            int IPsWithSSL = 0;

            foreach (IP address in IPList)
            {
                //PART 2
                if (SupportSSL(address)) { IPsWithSSL++; Console.Write("Adding +1 to SSL IPs, have: " + IPsWithSSL + " out of " + (IPsChecked + 1) + "\n"); }
                bool checkFinished = false;

                //PART 1
                foreach (string str in address.stringsInBrackets)
                {
                    if (IsStringABBA(str) == true)
                    {
                        //Console.Write("ABBA in brackets!" + "\n");
                        address.hasAbba = false;
                        checkFinished = true;
                    }
                }
                if (checkFinished == false)
                {
                    foreach (string str in address.stringsOutsideBrackets)
                    {
                        if (IsStringABBA(str) == true)
                        {
                            //Console.Write("ABBA found!!" + "\n");
                            address.hasAbba = true;
                            checkFinished = true;
                        }
                    }
                }
                checkFinished = true;
                    IPsChecked++;
                
                Console.Write("IP checked... " + "Number " + IPsChecked + "\n" + "___________________\n");
            }

            Console.Write("Number of ABBA IPs " + IPList.FindAll(IP => IP.hasAbba == true).Count() +"\n");
            Console.Write("Number of IPs with SSL: " + IPsWithSSL);
            Console.ReadLine();
        }
    }
}
