//using System.Threading;
using LotteryLib;

DateTime start_time = DateTime.Now; // 程式開始執行時間(大約) 

var rand = new Random(); // 亂數產生器 

// 大樂透 
Lotto649 l649 = new Lotto649(rand);
Lotto649Ticket[] l649arr = new Lotto649Ticket[100];
for (int i = 0; i < l649arr.Length; i += 1)
{
    l649arr[i] = new Lotto649Ticket(rand);
}

foreach (var ticket in l649arr)
{
    var prize = l649.MatchPrize(ticket);
    Console.Write($"{prize.Name}: {prize.NTD}, 彩卷內容: {ticket.Show()}\n");
}
Console.WriteLine();
Console.WriteLine($"本期 大樂透 獎號: {l649.Show()}\n");

Console.WriteLine("###########################\n");

// 威力彩 
SuperLotto638 sl638 = new SuperLotto638(rand);
SuperLotto638[] sl638arr = new SuperLotto638[100];
for (int i=0; i<sl638arr.Length; i+=1) 
{
    sl638arr[i] = new SuperLotto638(rand);
}

foreach (var ticket in sl638arr)
{
    var prize = sl638.MatchPrize(ticket);
    Console.Write($"{prize.Name}: {prize.NTD}, 彩卷內容: {ticket.Show()}\n");
}
Console.WriteLine();
Console.WriteLine($"本期 威力彩 獎號: {sl638.Show()}\n");

Console.WriteLine("###########################\n");

DateTime end_time = DateTime.Now; // 程式結束執行時間(大約) 
Console.WriteLine($"started at {start_time}");
Console.WriteLine($"finished at {end_time}");
Console.WriteLine($"time delta = {end_time - start_time} sec");
