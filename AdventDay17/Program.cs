using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventDay14;

namespace AdventDay17
{
    class Program
    {
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

        public class coordinate
        {
            public int[] value;

            public override bool Equals(object obj)
            {
                var other = obj as coordinate;
                if (obj == null || GetType() != obj.GetType())
                    return false;
                return value[0] == other.value[0] && value[1] == other.value[1];
            }

            public override int GetHashCode()
            {
                int hash = 13;
                hash = (hash * 7) + value[0].GetHashCode();
                hash = (hash * 7) + value[1].GetHashCode();
                return hash;
            }
        }

        static public coordinate MakeCoord(int a, int b)
        {
            int[] i = new int[2];
            i[0] = a;
            i[1] = b;
            coordinate coo = new coordinate { value = i };
            return coo;
        }

        public class Node
        {
            public coordinate coordinates = new coordinate();
            public string input; //= new string('a', 10);
            

            public Node GetLeft(HashSet<coordinate> hash, string currentInput)
            {
                coordinate LeftCoord = MakeCoord(coordinates.value[0] - 1, coordinates.value[1]);
                //if (hash.Contains(LeftCoord) == false)
                //{
                    if (LeftCoord.value[0] >= 0)
                    {
                        Node LeftNode = new Node { coordinates = LeftCoord, input = currentInput + 'L' };
                        return LeftNode;
                    }
                //}
                return null;
            }

            public Node GetRight(HashSet<coordinate> hash, string currentInput)
            {
                coordinate RightCoord = MakeCoord(coordinates.value[0] + 1, coordinates.value[1]);
                //if (hash.Contains(RightCoord) == false)
                //{
                    if (RightCoord.value[0] <= 3)
                    {
                        Node RightNode = new Node { coordinates = RightCoord, input = currentInput + 'R' };
                        return RightNode;
                    }
                //}
                return null;
            }

            public Node GetUp(HashSet<coordinate> hash, string currentInput)
            {
                coordinate UpCoord = MakeCoord(coordinates.value[0], coordinates.value[1] + 1);
                //if (hash.Contains(UpCoord) == false)
                //{
                    if (UpCoord.value[1] <= 3)
                    {
                        Node UpNode = new Node { coordinates = UpCoord, input = currentInput + 'U' };
                        return UpNode;
                    }
                //}
                return null;
            }

            public Node GetDown(HashSet<coordinate> hash, string currentInput)
            {
                coordinate DownCoord = MakeCoord(coordinates.value[0], coordinates.value[1]-1);
                //if (hash.Contains(DownCoord) == false)
                //{
                    if (DownCoord.value[1] >= 0)
                    {
                        Node DownNode = new Node { coordinates = DownCoord, input = currentInput + 'D' };
                        return DownNode;
                    }
                //}
                return null;
            }
        }

        public static string GetMD5(string input)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            var md5 = new Delay.MD5Managed();
            byte[] hash = md5.ComputeHash(inputBytes);
            return ToHex(hash);
        }

        public static List<Node> GetAvailableNodes(Node CurrentNode, HashSet<coordinate> CoordsList)
        {
            List<char> openDoorChars = new List<char>{ 'B','C','D','E','F' };
            List<Node> Nodes = new List<Node>();
            if (CurrentNode.GetDown(CoordsList, CurrentNode.input) != null && openDoorChars.Contains(GetMD5(CurrentNode.input)[1]))
            {
                Nodes.Add(CurrentNode.GetDown(CoordsList, CurrentNode.input));
            }
            if (CurrentNode.GetUp(CoordsList, CurrentNode.input) != null && openDoorChars.Contains(GetMD5(CurrentNode.input)[0]))
            {
                Nodes.Add(CurrentNode.GetUp(CoordsList, CurrentNode.input));
            }
            if (CurrentNode.GetLeft(CoordsList, CurrentNode.input) != null && openDoorChars.Contains(GetMD5(CurrentNode.input)[2]))
            {
                Nodes.Add(CurrentNode.GetLeft(CoordsList, CurrentNode.input));
            }
            if (CurrentNode.GetRight(CoordsList, CurrentNode.input) != null && openDoorChars.Contains(GetMD5(CurrentNode.input)[3]))
            {
                Nodes.Add(CurrentNode.GetRight(CoordsList, CurrentNode.input));
            }
            return Nodes;
        }

        public static void AddNewNodesToHashset(ref HashSet<coordinate> CoordsList, List<Node> Nodes)
        {
            foreach (Node node in Nodes)
            {
                CoordsList.Add(node.coordinates);
            }
        }

        static void Main(string[] args)
        {
            HashSet<coordinate> CoordsList = new HashSet<coordinate>();
            var PuzzleInput = "awrkjxxr";
            coordinate goal = MakeCoord(3, 0);
            int[] goalInt = new int[2];
            goalInt[0] = 3;
            goalInt[1] = 0;
            Queue<Node> queue = new Queue<Node>();
            Node StartNode = new Node { coordinates = MakeCoord(0, 3), input = PuzzleInput };
            queue.Enqueue(StartNode);

            while (queue.Any())
            {
                Node CurrentNode = queue.Dequeue();

                List<Node> AvailableNodes = GetAvailableNodes(CurrentNode, CoordsList);
                AddNewNodesToHashset(ref CoordsList, AvailableNodes);
                foreach (Node node in AvailableNodes)
                {
                    if (node.coordinates.value[0] == goal.value[0] && node.coordinates.value[1] == goal.value[1])
                    {
                        Console.Write("Reached the goal, path length was: " + (node.input.Length - PuzzleInput.Length) + "\n");
                        //Console.ReadLine();
                    }
                    else { queue.Enqueue(node); }
                }
            }

            Console.Write("No more moves possible");
            Console.ReadLine();
        }
    }
}
