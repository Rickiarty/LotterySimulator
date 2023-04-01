using Microsoft.AspNetCore.Mvc;
using System.Text;
using LotteryLib;
using StrategiesToLottery;

namespace LotterySimulator_WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryController : ControllerBase
    {
        protected int min = 1;
        protected int max = 20000;

        // https://[host_address]:[port]/api/Lottery/GetRandomTickets/{num} 
        [HttpGet]
        [Route("[action]/{num}")]
        public string GetRandomTickets(int num)
        {
            if (num >= this.min && num <= this.max)
            {
                return this.InfoOfRandomTickets(num);
            }
            else
            {
                return $"invalid number of tickets\nvalid range:\n\t{min} ≦ number of tickets ≦ {max}";
            }
        }

        protected string InfoOfRandomTickets(int ticketsNum) 
        {
            StringBuilder sb = new StringBuilder();

            DateTime start_time = DateTime.Now; // 程式開始執行時間(大約) 

            var rand = new Random(); // 亂數產生器 

            // 大樂透 
            Lotto649 l649 = new Lotto649(rand);
            Lotto649Ticket[] l649ticket = new Lotto649Ticket[ticketsNum];
            for (int i = 0; i < l649ticket.Length; i += 1)
            {
                l649ticket[i] = new Lotto649Ticket(rand);
                if (i == l649ticket.Length - 1)
                {
                    l649ticket[i] = new Lotto649Ticket(rand, new int[6] { 1, 3, 5, 7, 9, 49 });
                }
            }
            var count_l649 = new int[9];
            foreach (var ticket in l649ticket)
            {
                var prize = l649.MatchPrize(ticket);
                sb.Append($"{prize.Name}: {prize.NTD}, 彩券內容: {ticket.Show()}\n");
                count_l649[prize.Id] += 1;
            }
            sb.Append("\n");
            sb.Append($"本期 大樂透 獎號: {l649.Show()}\n\n");
            for (int i = 0; i < count_l649.Length; i += 1)
            {
                sb.Append($"{TableLotto649.NameTable[i]}: {count_l649[i]}/{ticketsNum} ({count_l649[i] * 100.0 / ticketsNum} %)\n");
            }

            sb.Append("\n###########################\n\n");

            // 威力彩 
            SuperLotto638 sl638 = new SuperLotto638(rand);
            SuperLotto638[] sl638ticket = new SuperLotto638[ticketsNum];
            for (int i = 0; i < sl638ticket.Length; i += 1)
            {
                sl638ticket[i] = new SuperLotto638(rand);
                if (i == sl638ticket.Length - 1)
                {
                    sl638ticket[i] = new SuperLotto638(new int[6] { 2, 4, 6, 8, 10, 38 }, 6);
                }
            }

            var count_sl638 = new int[11];
            foreach (var ticket in sl638ticket)
            {
                var prize = sl638.MatchPrize(ticket);
                sb.Append($"{prize.Name}: {prize.NTD}, 彩券內容: {ticket.Show()}\n");
                count_sl638[prize.Id] += 1;
            }
            sb.Append("\n");
            sb.Append($"本期 威力彩 獎號: {sl638.Show()}\n\n");
            for (int i = 0; i < count_sl638.Length; i += 1)
            {
                sb.Append($"{TableSuperLotto638.NameTable[i]}: {count_sl638[i]}/{ticketsNum} ({count_sl638[i] * 100.0 / ticketsNum} %)\n");
            }

            sb.Append("\n###########################\n\n");

            // 今彩539  
            DailyCash539 dc539 = new DailyCash539(rand);
            DailyCash539[] dc539ticket = new DailyCash539[ticketsNum];
            for (int i = 0; i < dc539ticket.Length; i += 1)
            {
                dc539ticket[i] = new DailyCash539(rand);
                if (i == dc539ticket.Length - 1)
                {
                    dc539ticket[i] = new DailyCash539(new int[5] { 4, 7, 10, 13, 39 });
                }
            }

            var count_dc539 = new int[5];
            foreach (var ticket in dc539ticket)
            {
                var prize = dc539.MatchPrize(ticket);
                sb.Append($"{prize.Name}: {prize.NTD}, 彩券內容: {ticket.Show()}\n");
                count_dc539[prize.Id] += 1;
            }
            sb.Append("\n");
            sb.Append($"本期 今彩539 獎號: {dc539.Show()}\n\n");
            for (int i = 0; i < count_dc539.Length; i += 1)
            {
                sb.Append($"{TableDailyCash539.NameTable[i]}: {count_dc539[i]}/{ticketsNum} ({count_dc539[i] * 100.0 / ticketsNum} %)\n");
            }

            sb.Append("\n###########################\n\n");

            // 雙贏彩 
            Lotto1224 l1224 = new Lotto1224(rand);
            Lotto1224[] l1224ticket = new Lotto1224[ticketsNum];
            for (int i = 0; i < l1224ticket.Length; i += 1)
            {
                l1224ticket[i] = new Lotto1224(rand);
                if (i == l1224ticket.Length - 1)
                {
                    l1224ticket[i] = new Lotto1224(new int[12] { 1, 4, 5, 8, 9, 12, 13, 16, 17, 20, 21, 24 });
                }
            }

            var count_l1224 = new int[5];
            foreach (var ticket in l1224ticket)
            {
                var prize = l1224.MatchPrize(ticket);
                sb.Append($"{prize.Name}: {prize.NTD}, 彩券內容: {ticket.Show()}\n");
                count_l1224[prize.Id] += 1;
            }
            sb.Append("\n");
            sb.Append($"本期 雙贏彩 獎號: {l1224.Show()}\n\n");
            for (int i = 0; i < count_l1224.Length; i += 1)
            {
                sb.Append($"{TableLotto1224.NameTable[i]}: {count_l1224[i]}/{ticketsNum} ({count_l1224[i] * 100.0 / ticketsNum} %)\n");
            }

            sb.Append("\n###########################\n\n");

            // 官方app第二區包牌法 
            sb.Append("[官方app第二區包牌法 to 威力彩]\n");
            var sl638ticket1 = StrategySuperLotto638.Strategy["CommonStrategy1"](rand);
            var count1_sl638 = new int[11];
            foreach (var ticket in sl638ticket1)
            {
                var prize = sl638.MatchPrize(ticket);
                sb.Append($"{prize.Name}: {prize.NTD}, 彩券內容: {ticket.Show()}\n");
                count1_sl638[prize.Id] += 1;
            }
            sb.Append("\n");
            sb.Append($"本期 威力彩 獎號: {sl638.Show()}\n\n");
            for (int i = 0; i < count1_sl638.Length; i += 1)
            {
                sb.Append($"{TableSuperLotto638.NameTable[i]}: {count1_sl638[i]}/{sl638ticket1.Length} ({count1_sl638[i] * 100.0 / sl638ticket1.Length} %)\n");
            }

            sb.Append("\n###########################\n\n");

            // 彩券行老闆包牌法 
            sb.Append("[彩券行老闆包牌法 to 威力彩]\n");
            var sl638ticket2 = StrategySuperLotto638.Strategy["CommonStrategy2"](rand);
            var count2_sl638 = new int[11];
            foreach (var ticket in sl638ticket2)
            {
                var prize = sl638.MatchPrize(ticket);
                sb.Append($"{prize.Name}: {prize.NTD}, 彩券內容: {ticket.Show()}\n");
                count2_sl638[prize.Id] += 1;
            }
            sb.Append("\n");
            sb.Append($"本期 威力彩 獎號: {sl638.Show()}\n\n");
            for (int i = 0; i < count2_sl638.Length; i += 1)
            {
                sb.Append($"{TableSuperLotto638.NameTable[i]}: {count2_sl638[i]}/{sl638ticket2.Length} ({count2_sl638[i] * 100.0 / sl638ticket2.Length} %)\n");
            }

            sb.Append("\n###########################\n\n");

            //Thread.Sleep(3000); // in milli-second 

            DateTime end_time = DateTime.Now; // 程式結束執行時間(大約) 
            sb.Append($" started at {start_time} (time on server)\n");
            sb.Append($"finished at {end_time} (time on server)\n");
            sb.Append($"         time delta = {end_time - start_time}\n");

            return sb.ToString();
        }
    }
}
