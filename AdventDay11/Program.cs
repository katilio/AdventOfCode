using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventDay11
{

    class Program
    {
        public static int RegIndexFromChar(char chr)
        {
            switch (chr)
            {
                case 'a':
                    return 0;
                    break;
                case 'b':
                    return 1;
                    break;
                case 'c':
                    return 2;
                    break;
                case 'd':
                    return 3;
                    break;
            }
            return -1;
        }

        static bool isNumeric(string str)
        {
            int n;
            return int.TryParse(str, out n);
        }

        private static void PrintRegisters(object sender, System.Timers.ElapsedEventArgs e, int[] registers)
        {
            Console.Write("\r{0}%                                            ", "A: " + registers[0] + ", B: " + registers[1] + ", C: " + registers[2] + ", D: " + registers[3]);
        }

        static void Main(string[] args)
        {
            int[] registers = new int[4];
            registers[2] = 1;
            var input = Advent.Shared.getInput("Day12.txt");
            System.Timers.Timer timer = new System.Timers.Timer { Interval = 2000 };
            timer.Elapsed += (sender, e) => PrintRegisters(sender, e, registers);
            timer.Start();
            timer.AutoReset = true;
            for (int index = 0; index < input.Length; index++)
            {
                if (input[index].StartsWith("inc"))
                {
                    var s = input[index].Split(' ');
                    if (s[1] == "a") { registers[0]++; }
                    if (s[1] == "b") { registers[1]++; }
                    if (s[1] == "c") { registers[2]++; }
                    if (s[1] == "d") { registers[3]++; }
                }

                if (input[index].StartsWith("dec"))
                {
                    var s = input[index].Split(' ');
                    if (s[1] == "a") { registers[0]--; }
                    if (s[1] == "b") { registers[1]--; }
                    if (s[1] == "c") { registers[2]--; }
                    if (s[1] == "d") { registers[3]--; }
                }

                if (input[index].StartsWith("cpy"))
                {
                    var s = input[index].Split(' ');
                    int regIndex = RegIndexFromChar(s[2][0]);
                    if (isNumeric(s[1]))
                    {
                        registers[regIndex] = Convert.ToInt32(s[1]);
                    }
                    else { registers[regIndex] = registers[RegIndexFromChar(s[1][0])]; }
                }

                if (input[index].StartsWith("jnz"))
                {
                    var s = input[index].Split(' ');
                    if (isNumeric(s[1]))
                    {
                        if (Convert.ToInt32(s[1]) != 0)
                        {
                            index = (index + Convert.ToInt32(s[2])) - 1;
                        }
                    }
                    else if (registers[RegIndexFromChar(s[1][0])] != 0)
                    {
                        index = (index + Convert.ToInt32(s[2])) - 1;
                    }
                }
                //Console.Write("\r{0}%                                            ", "A: " + registers[0] + ", B: " + registers[1] + ", C: " + registers[2] + ", D: " + registers[3]);
            }
            Console.Write("Final value for Reg A = " + registers[0]);
            Console.ReadLine();
        }
    }
}
