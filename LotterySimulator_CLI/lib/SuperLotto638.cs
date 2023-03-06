// SuperLotto638.cs
// 威力彩.cs

using System.Text;

namespace LotteryLib
{
    class TableSuperLotto638 : Table
    {
        protected static new Dictionary<int, int> bonusTable = new Dictionary<int, int>() {
            { 0,         0},
            { 1, 200000000}, // actually 89% of total bonus
            { 2,   2000000}, // actually 11% of total bonus
            { 3,    150000},
            { 4,     20000},
            { 5,      4000},
            { 6,       800},
            { 7,       400},
            { 8,       200},
            { 9,       100},
            {10,       100},
        };
        protected static new Dictionary<int, string> nameTable = new Dictionary<int, string>() {
            { 0, "未中獎"},
            { 1, "頭獎"},
            { 2, "貳獎"},
            { 3, "參獎"},
            { 4, "肆獎"},
            { 5, "伍獎"},
            { 6, "陸獎"},
            { 7, "柒獎"},
            { 8, "捌獎"},
            { 9, "玖獎"},
            {10, "普獎"},
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

    class PrizeSuperLotto638 : Prize
    {
        public PrizeSuperLotto638(int id)
        {
            if (id > 10 || id < 0)
            {
                this.id = 0;
                this.name = TableSuperLotto638.NameTable[0];
                this.ntd = TableSuperLotto638.BonusTable[0];
            }
            else
            {
                this.id = id;
                this.name = TableSuperLotto638.NameTable[id];
                this.ntd = TableSuperLotto638.BonusTable[id];
            }
        }
        public int Id
        {
            get { return id; }
        }
        public int NTD {
            get { return ntd; }
        }
        public string Name { 
            get { return name; } 
        }
    }

    class SuperLotto638 : Lottery
    {
        protected int[] area1 = new int[6];
        protected int area2 = 0;
        public int[] Area1 
        {
            get { return this.area1; }
        }
        public int Area2
        {
            get { return this.area2; }
        }

        public SuperLotto638(Random rand) : base(rand)
        {
            for (int i = 0; i < this.area1.Length;)
            {
                var temp = this.rand.Next(1, 39); // 1~38 
                if (!this.area1.Contains(temp))
                {
                    this.area1[i] = temp;
                    i += 1;
                }
            }
            Array.Sort(this.area1);
            this.area2 = this.rand.Next(1, 9); // 1~8 
        }

        public PrizeSuperLotto638 MatchPrize(SuperLotto638 ticket) 
        {
            int c1 = 0;
            for (int i = 0; i < this.area1.Length; i++)
            {
                if (ticket.Area1.Contains(this.area1[i]))
                {
                    c1 += 1;
                }
            }
            PrizeSuperLotto638 prize = null;
            if (this.area2 == ticket.Area2)
            {
                switch (c1) 
                {
                    case 6:
                        prize = new PrizeSuperLotto638(1); break;
                    case 5:
                        prize = new PrizeSuperLotto638(3); break;
                    case 4:
                        prize = new PrizeSuperLotto638(5); break;
                    case 3:
                        prize = new PrizeSuperLotto638(7); break;
                    case 2:
                        prize = new PrizeSuperLotto638(8); break;
                    case 1:
                        prize = new PrizeSuperLotto638(10); break;
                    default:
                        prize = new PrizeSuperLotto638(0); break;
                }
            }
            else
            {
                switch (c1)
                {
                    case 6:
                        prize = new PrizeSuperLotto638(2); break;
                    case 5:
                        prize = new PrizeSuperLotto638(4); break;
                    case 4:
                        prize = new PrizeSuperLotto638(6); break;
                    case 3:
                        prize = new PrizeSuperLotto638(9); break;
                    default:
                        prize = new PrizeSuperLotto638(0); break;
                }
            }
            return prize;
        }

        public string Show() 
        {
            StringBuilder sb = new StringBuilder("{ \"第一區\": [ ");
            for (int i=0; i<this.area1.Length; i++) 
            {
                sb.Append($"{this.area1[i]}, ");
            }
            sb.Append("], \"第二區\": " + this.area2 + ", }");
            return sb.ToString();
        }

    }
}
