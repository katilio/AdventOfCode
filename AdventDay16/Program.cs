using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventDay16
{
    class Program
    {
        public static string ExtendData(string input)
        {
            StringBuilder result = new StringBuilder(input.Length);
            for (int i = input.Length - 1; i >= 0; i--)
            {
                result = result.Append(input[i] == '0' ? "1" : "0");
            }
            return result.ToString();
        }

        public static void PrintCharList(List<char> checksum, string extraInfo)
        {
            foreach (char Char in checksum)
            {
                Console.Write(Char.ToString());
            }
            Console.Write(extraInfo + "\n\n");
        }

        //public static void ExtendData(ref List<char> Input)
        //{
        //    int halfIndex = Input.Count();
        //    Input.Add('0');
        //    for (int i = halfIndex - 1; i > -1; i--)
        //    {
        //        Input.Add(Input[i]);
        //    }
        //    //Remove last 0
        //    //Input.Remove(Input[Input.Count() - 1]);
        //    ReverseData(ref Input, halfIndex);
        //}
        ////Fast but buggy
        //public static void ReverseData(ref List<char> Input, int halfIndex)
        //{
        //    for (int i = halfIndex+1; i < Input.Count(); i++)
        //    {
        //        if (Input[i] == '0')
        //        {
        //            Input[i] = '1';
        //        }
        //        else if (Input[i] == '1')
        //        {
        //            Input[i] = '0';
        //        }
        //    }
        //}

        public static List<char> CheckDoublesAndReturnChecksum(List<char> input)
        {
            List<char> checksum = input;
            for (int i = 0; i < checksum.Count()-1; i++)
            {
                if (checksum[i] == checksum[i + 1])
                {
                    checksum[i] = '1';
                    checksum.RemoveAt(i + 1);
                }
                else
                {
                    checksum[i] = '0';
                    checksum.RemoveAt(i + 1);
                }
            }
            return checksum;
        }

        static void Main(string[] args)
        {
            List<char> input = new List<char> { '1', '0', '0', '0', '0' };
            var seed = "00111101111101000";

            //List<char> checksum = new List<char>();
            int AdventInput1 = 272;
            int AdventInput2 = 35651584;

            //PrintCharList(input, " input created.");

            while (seed.Length < AdventInput2)
            {
                seed = seed + "0" + ExtendData(seed);
            }

            //Old and buggy
            //while (input.Count() < AdventInput)
            //{
            //    ExtendData(ref input);
            //    PrintCharList(input, " input extended...");
            //}

            input = seed.ToList<char>();

            while (input.Count() > AdventInput2)
            {
                input.RemoveAt(input.Count() - 1);
                //PrintCharList(input, " trimming input to 20 chars...");
            }

            Console.Write("input created\n\n");

            //Slow checksum
            //checksum = CheckDoublesAndReturnChecksum(input);

            string data = seed.Substring(0, AdventInput2);

            string checksum = data;

            //Much faster checksum
            while (checksum.Length % 2 == 0)
            {
                StringBuilder newchecksum = new StringBuilder(checksum.Length / 2 + 1);
                for (int ii = 0; ii < checksum.Length; ii += 2)
                {
                    if (checksum[ii] == checksum[ii + 1]) newchecksum.Append("1");
                    else newchecksum.Append("0");
                }
                checksum = newchecksum.ToString();
            }

            Console.Write("First checksum done \n\n");
            Console.Write(checksum + " is last checksum");
            Console.ReadLine();
        }
    }
}
