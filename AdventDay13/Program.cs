using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
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

        static public bool IsSpace(coordinate coordinates)
        {
            int x = coordinates.value[0];
            int y = coordinates.value[1];
            int result = ((x * x) + (3 * x) + (2 * x * y) + y + (y * y));
            result += 1350;
            string BinResult = Convert.ToString(result, 2);
            if ((BinResult.Count(o => o == '1') % 2) == 0) { return true; }
            else { return false; }
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
            public int distance;

            public Node GetLeft(HashSet<coordinate> hash)
            {
                coordinate LeftCoord = MakeCoord(coordinates.value[0] - 1, coordinates.value[1]);
                if (hash.Contains(LeftCoord) == false && IsSpace(LeftCoord) == true)
                {
                    if (LeftCoord.value[0] >= 0)
                    {
                        Node LeftNode = new Node { coordinates = LeftCoord, distance = distance + 1 };
                        return LeftNode;
                    }
                }
                return null;
            }

            public Node GetRight(HashSet<coordinate> hash)
            {
                coordinate RightCoord = MakeCoord(coordinates.value[0] + 1, coordinates.value[1]);
                if (hash.Contains(RightCoord) == false)
                {
                    if (IsSpace(RightCoord) == true)
                    {
                        Node RightNode = new Node { coordinates = RightCoord, distance = distance + 1 };
                        return RightNode;
                    }
                }
                return null;
            }

            public Node GetUp(HashSet<coordinate> hash)
            {
                coordinate UpCoord = MakeCoord(coordinates.value[0], coordinates.value[1] + 1);
                if (hash.Contains(UpCoord) == false && IsSpace(UpCoord) == true)
                {
                    Node UpNode = new Node { coordinates = UpCoord, distance = distance + 1 };
                    return UpNode;
                }
                return null;
            }

            public Node GetDown(HashSet<coordinate> hash)
            {
                coordinate DownCoord = MakeCoord(coordinates.value[0], coordinates.value[1] - 1);
                if (hash.Contains(DownCoord) == false && IsSpace(DownCoord) == true)
                {
                    if (DownCoord.value[1] >= 0)
                    {
                        Node DownNode = new Node { coordinates = DownCoord, distance = distance + 1 };
                        return DownNode;
                    }
                }
                return null;
            }
        }

        public static string[,] InitializeHashSet(ref HashSet<coordinate> Hash) //Initalizes the visited HashSet with all walls, also adds them to a visual map and returns it
        {
            string[,] map = new string[50, 50];
            for (int x = 0; x < 50; x++)
            {
                for (int y = 0; y < 50; y++)
                {
                    int[] coordinates = new int[2];
                    coordinates[0] = x;
                    coordinates[1] = y;
                    coordinate coord = new coordinate { value = coordinates };
                    if (IsSpace(coord) == true)
                    {
                        map[x, y] = "   ";
                        if (coord.value[0] == 31 && coord.value[1] == 39) { map[x, y] = "GGG"; }

                    }
                    else if (coord.value[0] == 31 && coord.value[1] == 39) { Console.Write("GOAL IS A WALL"); }
                    else
                    {
                        map[x, y] = "HHH";
                        Hash.Add(coord);
                    }
                }
            }
            return map;
        }


        static void Main(string[] args)
        {
            //Create HashSet of all visited nodes
            HashSet<coordinate> visited = new HashSet<coordinate>();
            int[] startInt = new int[2];
            startInt[0] = 1;
            startInt[1] = 1;
            coordinate start = new coordinate { value = startInt };
            int[] goalInt = new int[2];
            goalInt[0] = 31;
            goalInt[1] = 39;
            coordinate goal = new coordinate { value = goalInt };
            Node StartNode = new Node { coordinates = start, distance = 0 };
            Queue<Node> q = new Queue<Node>();
            List<Node> NodesAt50 = new List<Node>(); //List of nodes under 50 moves
            NodesAt50.Add(StartNode);
            q.Enqueue(StartNode);
            //Draw walls and spaces in a map, also adds them to the visited list so that we never check walls
            string[,] map = InitializeHashSet(ref visited);
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
            int nodeCount = 0;

            //Main BFS loop
            while (q.Any()/* && !Over50Distance*/)
            {
                Node currentNode = q.Dequeue();
                nodeCount++;
                visited.Add(currentNode.coordinates);
                map[currentNode.coordinates.value[0], currentNode.coordinates.value[1]] = currentNode.distance.ToString();
                Console.Write(currentNode.coordinates.value[0].ToString() + "/" + currentNode.coordinates.value[1].ToString() + " distance: " + currentNode.distance + " nodecount: " + nodeCount + "\n");
                if (currentNode.coordinates.value[0] == goal.value[0] && currentNode.coordinates.value[1] == goal.value[1])
                {
                    Console.Write("Got to goal, distance from start = " + currentNode.distance);
                }
                Node down = currentNode.GetDown(visited);
                Node up = currentNode.GetUp(visited);
                Node left = currentNode.GetLeft(visited);
                Node right = currentNode.GetRight(visited);

                //Get adjacent nodes, and if they are not walls or haven't been visited yet. Then add them to visited HashSet.
                if (down != null && visited.Contains(down.coordinates) == false)
                {
                    if(down.coordinates == goal) {
                        Console.Write("Reached goal, distance: " + down.distance); }
                    else q.Enqueue(down);
                    visited.Add(down.coordinates);
                    if (down.distance < 51) { NodesAt50.Add(down); }
                }
                if (up != null && visited.Contains(up.coordinates) == false)
                {
                    if (up.coordinates == goal) {
                        Console.Write("Reached goal, distance: " + up.distance); }
                    else q.Enqueue(up);
                    visited.Add(up.coordinates);
                    if (up.distance < 51) { NodesAt50.Add(up); }

                }
                if (left != null && visited.Contains(left.coordinates) == false)
                {
                    if (left.coordinates == goal) {
                        Console.Write("Reached goal, distance: " + left.distance); }
                    else q.Enqueue(left);
                    visited.Add(left.coordinates);
                    if (left.distance < 51) { NodesAt50.Add(left); }

                }
                if (right != null && visited.Contains(right.coordinates) == false)
                {
                    if (right.coordinates == goal) {
                        Console.Write("Reached goal, distance: " + right.distance); }
                    else q.Enqueue(right);
                    visited.Add(right.coordinates);
                    if (right.distance < 51) { NodesAt50.Add(right); }
                }
            }

            //Write map again to see all movements
            for (int y = 0; y < 50; y++)
            {
                for (int x = 0; x < 50; x++)
                {
                    Console.Write(map[x, y]);
                }
                Console.Write("\n");
            }
            Console.Write(visited.Contains(goal) + "\n");
            Console.ReadLine();
        }
    }
}
