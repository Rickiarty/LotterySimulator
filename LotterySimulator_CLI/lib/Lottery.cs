
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
            this.rand = rand;
        }
    }

}
