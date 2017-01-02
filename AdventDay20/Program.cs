using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventDay20
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get input into a list of int[] (start-end of banned range)
            List<uint[]> IPList = new List<uint[]>();
            var input = File.ReadAllLines("input.txt");
            foreach (string Line in input)
            {
                var s = Line.Split('-');
                var num1 = Convert.ToUInt32(s[0]);
                var num2 = Convert.ToUInt32(s[1]);
                var numArr = new uint[2] { num1, num2 };
                IPList.Add(numArr);
            }

            List<uint[]> IPRanges = new List<uint[]>();

            List<uint[]> SortedIPList = IPList.OrderBy(x => x[0]).ToList();

            //As long as we have elements in the sorted list of IPs, 
            // find the next non-banned range, add it to the IPRanges list,
            // then find the index in the list for the next immediate banned range and remove the previous ones.
            while (SortedIPList.Any())
            {
                bool foundIndex = false;
                int ReferenceIndex = 0;
                while (!foundIndex)
                {
                    foundIndex = true;
                    for (int i = 1; i < SortedIPList.Count(); i++)
                    {
                        if ((SortedIPList[i][0] < SortedIPList[ReferenceIndex][1] || SortedIPList[i][0] == SortedIPList[ReferenceIndex][1] + 1) &&
                            SortedIPList[i][1] > SortedIPList[ReferenceIndex][1])
                        {
                            ReferenceIndex = i;
                            foundIndex = false;
                        }
                    }
                }
                uint IPRangeStart = SortedIPList[ReferenceIndex][1];
                uint IPRangeEnd;
                int purgingIndex;
                var NextBannedRange = SortedIPList.FirstOrDefault(x => x[0] > SortedIPList[ReferenceIndex][1]);
                if (NextBannedRange != null)
                {
                    IPRangeEnd = NextBannedRange[0];
                    IPRanges.Add(new uint[] { IPRangeStart, IPRangeEnd });
                    purgingIndex = SortedIPList.FindIndex(x => x[0] > SortedIPList[ReferenceIndex][1]);
                    SortedIPList.RemoveRange(0, purgingIndex);
                }
                else
                {
                    IPRangeEnd = uint.MaxValue;
                    IPRanges.Add(new uint[] { IPRangeStart, IPRangeEnd });
                    SortedIPList.RemoveRange(0, SortedIPList.Count());
                }
            }

            uint FreeIPs = 0;
            foreach (uint[] uintArr in IPRanges)
            {
                FreeIPs += (uintArr[1] - uintArr[0] -1); //-1 because banned IPs are inclusive
            }
            Console.Write(FreeIPs+1); //+1 because last IPRange
            Console.ReadLine();
        }
    }
}
