
namespace LotteryLib
{
    abstract class Table
    {
        protected static Dictionary<int, int> bonusTable = new Dictionary<int, int>() {
            { 0, 0},
        };
        protected static Dictionary<int, string> nameTable = new Dictionary<int, string>() {
            { 0, "未中獎"},
        };

        public static Dictionary<int, string> NameTable
        {
            get { return nameTable; }
        }
        public static Dictionary<int, int> BonusTable
        {
            get { return bonusTable; }
        }
    }

    abstract class Prize
    {
        protected int id = 0;
        protected int ntd = 0;
        protected string name = "";
    }

    abstract class Lottery
    {
        protected Random rand;

        public Lottery(Random rand)
        {
            if (rand == null) 
            { 
                this.rand = new Random(); 
            }
            else
            {
                this.rand = rand;
            }
        }

        protected bool IsDuplicated(int[] intArr)
        {
            for (int i = 1; i < intArr.Length; i += 1)
            {
                for (int j = 0; j < i; j += 1)
                {
                    if (intArr[i] == intArr[j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

}
