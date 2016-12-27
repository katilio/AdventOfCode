using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventDay10
{
    class Program
    {
        //private void MakeBotDatatable()
        //{
        //    DataTable BotData = new DataTable("BotData");
        //    DataColumn column;
        //    DataRow row;

        //    column = new DataColumn();
        //    column.ColumnName = "ID";
        //    column.DataType = System.Type.GetType("System.Int32");
        //    column.Unique = true;
        //    BotData.Columns.Add(column);

        //    column = new DataColumn();
        //    column.ColumnName = "GivesLowTo";
        //    column.DataType = System.Type.GetType("System.Int32");
        //    column.Unique = false;
        //    BotData.Columns.Add(column);

        //    column = new DataColumn();
        //    column.ColumnName = "GivesHighTo";
        //    column.DataType = System.Type.GetType("System.Int32");
        //    column.Unique = false;
        //    BotData.Columns.Add(column);

        //    column = new DataColumn();
        //    column.ColumnName = "Chips";
        //    column.DataType = System.Type.GetType("System.Int32");
        //    column.Unique = true;
        //    BotData.Columns.Add(column);
        //}
        public class Bot
        {
            public Int32 ID;
            public String GivesLowTo;
            public String GivesHighTo;
            public List<Int32> Chips;

            public void DistributeChips(ref List<Bot> BotList, ref List<Int32> OutputList, ref int count)
            {
                var HighChip = Chips.Max();
                var LowChip = Chips.Min();
                var HighDestination = GivesHighTo.Split(' ');
                var LowDestination = GivesLowTo.Split(' ');

                //Distribute HighChip
                if (GivesHighTo.StartsWith("output"))
                {
                    OutputList[Convert.ToInt32(HighDestination[1])] = HighChip;
                    count++;
                }
                else
                {
                    BotList[Convert.ToInt32(HighDestination[1])].Chips.Add(HighChip);
                }

                //Distribute LowChip
                if (GivesLowTo.StartsWith("output"))
                {
                    OutputList[Convert.ToInt32(LowDestination[1])] = LowChip;
                    count++;
                }
                else
                {
                    BotList[Convert.ToInt32(LowDestination[1])].Chips.Add(LowChip);
                }

                //Remove delivered chips from this bot
                Chips.Clear();
                
                //If any receiving bot now has 2 chips, distribute them as well
                if (BotList[Convert.ToInt32(HighDestination[1])].Chips.Count > 1)
                {
                    if (BotList[Convert.ToInt32(HighDestination[1])].HasDesiredChips() == false)
                    { 
                        BotList[Convert.ToInt32(HighDestination[1])].DistributeChips(ref BotList, ref OutputList, ref count);
                    }
                    else { Console.Write("The bot " + BotList[Convert.ToInt32(HighDestination[1])].ID + " has the desired chips!"); }
                }
                if (BotList[Convert.ToInt32(LowDestination[1])].Chips.Count > 1)
                {
                    if (BotList[Convert.ToInt32(LowDestination[1])].HasDesiredChips() == false)
                    {
                        BotList[Convert.ToInt32(LowDestination[1])].DistributeChips(ref BotList, ref OutputList, ref count);
                    }
                    else { Console.Write("The bot " + BotList[Convert.ToInt32(LowDestination[1])].ID + " has the desired chips!"); }
                }
            }

            public bool HasDesiredChips()
            {
                if ((Chips[0] == 61 || Chips[0] == 17) && (Chips[1] == 61 || Chips[1] == 17)) { return true; }
                return false;
            }
        }


        static void Main(string[] args)
        {
            List<string> input = Advent.Shared.getInputList("Day10.txt");
            DataTable BotData = new DataTable();
            BotData.Columns.Add("Number");
            //Make bot class to make list of bots 
            List<Bot> BotList = new List<Bot>();
            List<Int32> OutputList = new List<int>();
            int numberOfChipsDeliveredToOutput = 0;
            for (int i = 0; i < 21; i++) { OutputList.Add(0); }

            foreach (string str in input)
            {
                if (str.StartsWith("bot"))
                {
                    var s = str.Split(' ');
                    BotList.Add(new Bot() { ID = Convert.ToInt32(s[1]), GivesHighTo = s[10] + ' ' + s[11], GivesLowTo = s[5] + ' ' + s[6], Chips = new List<int>() });
                }
            }
            List<Bot> SortedBotList = BotList.OrderBy(x => x.ID).ToList<Bot>();

            foreach (string str in input)
            {
                if (str.StartsWith("value"))
                {
                    var s = str.Split(' ');
                    SortedBotList[Convert.ToInt32(s[5])].Chips.Add(Convert.ToInt32(s[1]));
                    if (SortedBotList[Convert.ToInt32(s[5])].Chips.Count() > 1) { SortedBotList[Convert.ToInt32(s[5])].DistributeChips(ref SortedBotList, ref OutputList, ref numberOfChipsDeliveredToOutput); }
                }
            }

            while (numberOfChipsDeliveredToOutput < 21)
            {
                foreach (Bot bot in SortedBotList)
                {
                    if (bot.Chips.Count() == 2) { bot.DistributeChips(ref SortedBotList, ref OutputList, ref numberOfChipsDeliveredToOutput); }
                    //Console.Write("Bot ID: " + bot.ID + " gives High to: " + bot.GivesHighTo + " and gives Low to: " + bot.GivesLowTo + "\n");
                    if (bot.Chips.Count() == 0) { Console.Write("Bot has 0 chips \n\n"); }
                    if (bot.Chips.Count() == 1) { Console.Write("Bot has 1 chip \n\n"); }
                }
            }

            for (int i = 0; i < 21; i++)
            {
                Console.Write("Output box: " + i.ToString() + " has chip " + OutputList[i].ToString() + "\n\n");
            }
            Console.ReadLine();
        }
    }
}
