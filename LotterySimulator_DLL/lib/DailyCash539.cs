// DailyCash539.cs
// 今彩539.cs

using System.Text;

namespace LotteryLib
{
    public class TableDailyCash539 : Table
    {
        protected static new Dictionary<int, int> bonusTable = new Dictionary<int, int>() {
            { 0,       0},
            { 1, 8000000},
            { 2,   20000},
            { 3,     300},
            { 4,      50},
        };
        protected static new Dictionary<int, string> nameTable = new Dictionary<int, string>() {
            { 0, "未中獎"},
            { 1, "頭獎"},
            { 2, "貳獎"},
            { 3, "參獎"},
            { 4, "肆獎"},
        };

        public static new Dictionary<int, string> NameTable
        {
            get { return nameTable; }
        }
        public static new Dictionary<int, int> BonusTable
        {
            get { return bonusTable; }
        }
    }

    public class PrizeDailyCash539 : Prize
    {
        public PrizeDailyCash539(int id)
        {
            if (id > 4 || id < 0)
            {
                this.id = 0;
                this.name = TableDailyCash539.NameTable[0];
                this.ntd = TableDailyCash539.BonusTable[0];
            }
            else
            {
                this.id = id;
                this.name = TableDailyCash539.NameTable[id];
                this.ntd = TableDailyCash539.BonusTable[id];
            }
        }
        public int Id
        {
            get { return id; }
        }
        public int NTD
        {
            get { return ntd; }
        }
        public string Name
        {
            get { return name; }
        }
    }

    public class DailyCash539 : Lottery
    {
        protected int[] number = new int[5];
        public int[] Number
        {
            get { return this.number; }
        }

        public DailyCash539(Random rand) : base(rand)
        {
            for (int i = 0; i < this.number.Length;)
            {
                var temp = this.rand.Next(1, 40); // 1~39 
                if (!this.number.Contains(temp))
                {
                    this.number[i] = temp;
                    i += 1;
                }
            }
            Array.Sort(this.number); 
        }
        public DailyCash539(int[] num) : base(new Random())
        {
            if (num.Length != 5) { throw new ArgumentException("length of array parameter 'num' is not valid", nameof(num)); }
            for (int i = 0; i < this.number.Length; i += 1)
            {
                if (num[i] > 39 || num[i] < 1)
                {
                    throw new ArgumentException("each value of array parameter 'num' must be between 1 and 39", nameof(num));
                }
                this.number[i] = num[i];
            }
            if (this.IsDuplicated()) { throw new ArgumentException("duplicated values in array parameter 'num'", nameof(num)); }
            Array.Sort(this.number);
        }

        public PrizeDailyCash539 MatchPrize(DailyCash539 ticket)
        {
            int c1 = 0;
            for (int i = 0; i < this.number.Length; i++)
            {
                if (ticket.Number.Contains(this.Number[i]))
                {
                    c1 += 1;
                }
            }
            PrizeDailyCash539 prize = null; 
            switch (c1)
            {
                case 5:
                    prize = new PrizeDailyCash539(1); break;
                case 4:
                    prize = new PrizeDailyCash539(2); break;
                case 3:
                    prize = new PrizeDailyCash539(3); break;
                case 2:
                    prize = new PrizeDailyCash539(4); break;
                default:
                    prize = new PrizeDailyCash539(0); break;
            }
            return prize;
        }

        public string Show()
        {
            StringBuilder sb = new StringBuilder("{ \"今彩539\": [ ");
            for (int i = 0; i < this.number.Length; i++)
            {
                sb.Append($"{this.number[i]}, ");
            }
            sb.Append("], }");
            return sb.ToString();
        }

        protected bool IsDuplicated()
        {
            for (int i = 1; i < this.number.Length; i += 1)
            {
                for (int j = 0; j < i; j += 1)
                {
                    if (this.number[i] == this.number[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
