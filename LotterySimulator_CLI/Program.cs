//using System.Threading;
using LotteryLib;

DateTime start_time = DateTime.Now; // 程式開始執行時間(大約) 

var rand = new Random(); // 亂數產生器 
var ticketsNum = 20000;

// 大樂透 
Lotto649 l649 = new Lotto649(rand);
Lotto649Ticket[] l649ticket = new Lotto649Ticket[ticketsNum];
for (int i = 0; i < l649ticket.Length; i += 1)
{
    l649ticket[i] = new Lotto649Ticket(rand);
}
var count_l649 = new int[9];
foreach (var ticket in l649ticket)
{
    var prize = l649.MatchPrize(ticket);
    Console.Write($"{prize.Name}: {prize.NTD}, 彩卷內容: {ticket.Show()}\n");
    count_l649[prize.Id] += 1;
}
Console.WriteLine();
Console.WriteLine($"本期 大樂透 獎號: {l649.Show()}\n");
for (int i= 0; i< count_l649.Length; i+=1)
{
    Console.WriteLine($"{TableLotto649.NameTable[i]}: {count_l649[i]}/{ticketsNum} ({ count_l649[i] * 100.0 / ticketsNum } %)");
}

Console.WriteLine("\n###########################\n");

// 威力彩 
SuperLotto638 sl638 = new SuperLotto638(rand);
SuperLotto638[] sl638ticket = new SuperLotto638[ticketsNum];
for (int i=0; i<sl638ticket.Length; i+=1) 
{
    sl638ticket[i] = new SuperLotto638(rand);
}

var count_sl638 = new int[11];
foreach (var ticket in sl638ticket)
{
    var prize = sl638.MatchPrize(ticket);
    Console.Write($"{prize.Name}: {prize.NTD}, 彩卷內容: {ticket.Show()}\n");
    count_sl638[prize.Id] += 1;
}
Console.WriteLine();
Console.WriteLine($"本期 威力彩 獎號: {sl638.Show()}\n");
for (int i = 0; i < count_sl638.Length; i += 1)
{
    Console.WriteLine($"{TableSuperLotto638.NameTable[i]}: {count_sl638[i]}/{ticketsNum} ({count_sl638[i] * 100.0 / ticketsNum} %)");
}

Console.WriteLine("\n###########################\n");

//Thread.Sleep(3000); // in milli-second 

DateTime end_time = DateTime.Now; // 程式結束執行時間(大約) 
Console.WriteLine($"started at {start_time}");
Console.WriteLine($"finished at {end_time}");
Console.WriteLine($"time delta = {end_time - start_time} sec");
