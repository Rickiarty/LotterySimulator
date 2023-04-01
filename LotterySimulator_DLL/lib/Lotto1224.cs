// Lotto1224.cs
// 雙贏彩.cs

using System.Text;

namespace LotteryLib
{
    public class TableLotto1224 : Table
    {
        protected static new Dictionary<int, int> bonusTable = new Dictionary<int, int>() {
            { 0,        0},
            { 1, 15000000},
            { 2,   100000},
            { 3,      500},
            { 4,      100},
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

    public class PrizeLotto1224 : Prize
    {
        public PrizeLotto1224(int id)
        {
            if (id > 4 || id < 0)
            {
                this.id = 0;
                this.name = TableLotto1224.NameTable[0];
                this.ntd = TableLotto1224.BonusTable[0];
            }
            else
            {
                this.id = id;
                this.name = TableLotto1224.NameTable[id];
                this.ntd = TableLotto1224.BonusTable[id];
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

    public class Lotto1224 : Lottery
    {
        protected int[] number = new int[12];
        public int[] Number
        {
            get { return this.number; }
        }

        public Lotto1224(Random rand) : base(rand)
        {
            for (int i = 0; i < this.number.Length;)
            {
                var temp = this.rand.Next(1, 25); // 1~24 
                if (!this.number.Contains(temp))
                {
                    this.number[i] = temp;
                    i += 1;
                }
            }
            Array.Sort(this.number);
        }
        public Lotto1224(int[] num) : base(new Random())
        {
            if (num.Length != 12) { throw new ArgumentException("length of array parameter 'num' is not valid", nameof(num)); }
            for (int i = 0; i < this.number.Length; i += 1)
            {
                if (num[i] > 24 || num[i] < 1)
                {
                    throw new ArgumentException("each value of array parameter 'num' must be between 1 and 24", nameof(num));
                }
                this.number[i] = num[i];
            }
            if (this.IsDuplicated()) { throw new ArgumentException("duplicated values in array parameter 'num'", nameof(num)); }
            Array.Sort(this.number);
        }

        public PrizeLotto1224 MatchPrize(Lotto1224 ticket)
        {
            PrizeLotto1224 prize = null;
            IEnumerable<int> intersection = this.number.Intersect(ticket.Number);
            int count = intersection.Count();
            if (count == 12 || count == 0)
            {
                prize = new PrizeLotto1224(1);
            }
            else if (count == 11 || count == 1)
            {
                prize = new PrizeLotto1224(2);
            }
            else if (count == 10 || count == 2)
            {
                prize = new PrizeLotto1224(3);
            }
            else if (count == 9 || count == 3)
            {
                prize = new PrizeLotto1224(4);
            }
            else
            {
                prize = new PrizeLotto1224(0);
            }
            return prize;
        }

        public string Show()
        {
            StringBuilder sb = new StringBuilder("{ \"雙贏彩\": [ ");
            for (int i = 0; i < this.number.Length; i+=1)
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
