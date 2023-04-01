using LotteryLib;

namespace StrategiesToLottery
{
    public class StrategySuperLotto638
    {
        protected static Dictionary<string, Func<Random, SuperLotto638[]>> strategy = new Dictionary<string, Func<Random, SuperLotto638[]>>() {
            { "CommonStrategy1", StrategySuperLotto638.CommonStrategy1 },
            { "CommonStrategy2", StrategySuperLotto638.CommonStrategy2 },
        };

        public static Dictionary<string, Func<Random, SuperLotto638[]>> Strategy 
        { 
            get { return strategy; }
        }

        public static int[] Generate6DistinctNumbers(Random rand) 
        {
            if (rand == null) { rand = new Random(); }
            int[] nums = new int[6];
            for (int i = 0; i < nums.Length;)
            {
                var temp = rand.Next(1, 39); // 1~38 
                if (!nums.Contains(temp))
                {
                    nums[i] = temp;
                    i += 1;
                }
            }
            Array.Sort(nums);
            return nums;
        }

        public static SuperLotto638[] CommonStrategy1(Random rand)
        { /* a.k.a. 官方app第二區包牌法 */
            if (rand == null) { rand = new Random(); }
            SuperLotto638[] ticket = new SuperLotto638[8];
            for (int i=0; i<ticket.Length; i+=1)
            {
                int[] area1 = StrategySuperLotto638.Generate6DistinctNumbers(rand);
                ticket[i] = new SuperLotto638(area1, i+1);
            }
            return ticket;
        }

        public static SuperLotto638[] CommonStrategy2(Random rand)
        { /* a.k.a. 彩券行老闆包牌法 */
            if (rand == null) { rand = new Random(); }
            SuperLotto638[] ticket = new SuperLotto638[8];
            int[] area1 = StrategySuperLotto638.Generate6DistinctNumbers(rand);
            for (int i = 0; i < ticket.Length; i += 1)
            {
                ticket[i] = new SuperLotto638(area1, i+1);
            }
            return ticket;
        }

    }
}
