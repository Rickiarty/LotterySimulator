// Lotto649.cs
// 威力彩.cs

using System;
using System.Text;

namespace LotteryLib
{
    class TableLotto649 : Table
    {
        protected static new Dictionary<int, double> bonusTable = new Dictionary<int, double>() {
            { 0,         0},
            { 1,         0.82 },
            { 2,         0.065},
            { 3,         0.07 },
            { 4,         0.045},
            { 5,      2000},
            { 6,      1000},
            { 7,       400},
            { 8,       400},
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
            { 8, "普獎"},
        };

        public static new Dictionary<int, string> NameTable
        {
            get { return nameTable; }
        }
        public static new Dictionary<int, double> BonusTable
        {
            get { return bonusTable; }
        }
    }

    class PrizeLotto649 : Prize
    {
        protected new double ntd = 0;
        public PrizeLotto649(int id)
        {
            if (id > 10 || id < 0)
            {
                this.id = 0;
                this.name = TableLotto649.NameTable[0];
                this.ntd = TableLotto649.BonusTable[0];
            }
            else
            {
                this.id = id;
                this.name = TableLotto649.NameTable[id];
                this.ntd = TableLotto649.BonusTable[id];
            }
        }
        public int Id
        {
            get { return id; }
        }
        public double NTD
        {
            get { return ntd; }
        }
        public string Name
        {
            get { return name; }
        }
    }

    abstract class Lottery649 : Lottery
    {
        protected int[] numbers = new int[6];
        public int[] Numbers
        {
            get { return this.numbers; }
        }

        public Lottery649(Random rand) : base(rand)
        {
            for (int i = 0; i < this.numbers.Length;)
            {
                var temp = this.rand.Next(1, 50); // 1~49 
                if (!this.numbers.Contains(temp))
                {
                    this.numbers[i] = temp;
                    i += 1;
                }
            }
            Array.Sort(this.numbers);
        }
        public Lottery649(Random rand, int[] nums) : base(rand)
        {
            if (nums.Length != 6) { throw new ArgumentException("number of array parameter 'nums' is not valid", nameof(nums)); }
            for (int i = 0; i < this.numbers.Length; i += 1)
            {
                if (nums[i] > 49 || nums[i] < 1)
                {
                    throw new ArgumentException("each value of array parameter 'nums' must be between 1 and 49", nameof(nums));
                }
                this.numbers[i] = nums[i];
            }
            if (this.IsDuplicated()) { throw new ArgumentException("duplicated values in array parameter 'nums'", nameof(nums)); }
            Array.Sort(this.numbers);
        }

        protected bool IsDuplicated()
        {
            for (int i=1; i<this.numbers.Length; i+=1) 
            {
                for (int j=0; j<i; j+=1) 
                {
                    if (this.numbers[i] == this.numbers[j]) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    class Lotto649 : Lottery649
    {
        int specialNumber = 0;
        public int SpecialNumber
        {
            get { return this.specialNumber; }
        }

        public Lotto649(Random rand) : base(rand)
        {
            this.GenerateSpecialNumber();
        }
        public Lotto649(Random rand, int[] nums) : base(rand, nums)
        {
            this.GenerateSpecialNumber();
        }

        protected void GenerateSpecialNumber()
        {
            var temp = this.rand.Next(1, 50); // 1~49 
            while (this.numbers.Contains(temp))
            {
                temp = this.rand.Next(1, 50); // 1~49 
            }
            this.specialNumber = temp;
        }

        public PrizeLotto649 MatchPrize(Lotto649Ticket ticket)
        {
            int c1 = 0;
            for (int i = 0; i < this.numbers.Length; i++)
            {
                if (ticket.Numbers.Contains(this.numbers[i]))
                {
                    c1 += 1;
                }
            }
            PrizeLotto649 prize = null;
            switch (c1)
            {
                case 6:
                    prize = new PrizeLotto649(1); break;
                case 5:
                    if (ticket.Numbers.Contains(this.specialNumber))
                    {
                        prize = new PrizeLotto649(2);
                    }
                    else
                    {
                        prize = new PrizeLotto649(3);
                    }
                    break;
                case 4:
                    if (ticket.Numbers.Contains(this.specialNumber))
                    {
                        prize = new PrizeLotto649(4);
                    }
                    else
                    {
                        prize = new PrizeLotto649(5);
                    }
                    break;
                case 3:
                    if (ticket.Numbers.Contains(this.specialNumber))
                    {
                        prize = new PrizeLotto649(6);
                    }
                    else
                    {
                        prize = new PrizeLotto649(8);
                    }
                    break;
                case 2:
                    if (ticket.Numbers.Contains(this.specialNumber))
                    {
                        prize = new PrizeLotto649(7);
                    }
                    else
                    {
                        prize = new PrizeLotto649(0);
                    }
                    break;
                default:
                    prize = new PrizeLotto649(0); break;
            }
            return prize;
        }

        public string Show()
        {
            StringBuilder sb = new StringBuilder("{ \"本期獎號\": [ ");
            for (int i = 0; i < this.numbers.Length; i++)
            {
                sb.Append($"{this.numbers[i]}, ");
            }
            sb.Append("], \"特別號\": " + this.specialNumber + ", }");
            return sb.ToString();
        }

    }

    class Lotto649Ticket : Lottery649
    {
        public Lotto649Ticket(Random rand) : base(rand)
        {
            ;
        }
        public Lotto649Ticket(Random rand, int[] nums) : base(rand, nums)
        {
            ;
        }

        public string Show()
        {
            StringBuilder sb = new StringBuilder("{ \"彩卷內容\": [ ");
            for (int i = 0; i < this.numbers.Length; i++)
            {
                sb.Append($"{this.numbers[i]}, ");
            }
            sb.Append("], }");
            return sb.ToString();
        }
    }
}
