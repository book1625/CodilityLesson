using System;
using System.Linq;
namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/17-dynamic_programming/min_abs_sum/
     * 
     * 給你一個陣列，你可以為每個元素配上任意的正負變化，目的是讓變化後的陣列，所有元素加起來的總和最小，請問這個總和是多少
     * 
     * 想法：brute-forced 每個元素都可以變正負，然後算一次總和，所以是 O(2**n * n)，太大了，又是 greedy or dp
     * greedy 就是盡可能找絕對值近的來搭，絕對值排序我也會，但是，如果遇到 1,1,.....1,1,1,1,1,-100 ....，
     * 我怎麼知道很久以後我有個-100，所以不能把前面的1先消掉
     * 同樣的，用左手右手法，合適的先出也是一樣，我猜不到後面有合適的
     * 
     * 所以 greedy 看起來能作，但肯定不怎麼正確，轉 dp 試試
     * 一樣，如果我有張表，可以查最小和，那索引應該是資料的索引，每次資料進來就要再引發最小和
     * 這時遇到問題了，如果我先前的最小和已定，再來考慮我的影響，但其實先前如果大一點或小一點，加上我會更好
     * 所以這個表，不具有往前推的特性，得換個元素來索引看看
     * 我可以肯定的是，這個和的最大值，是把每個人都變正加起來，最小值有機會是0，所以和在這裡面跑，而且達到最小就不再有效
     * 那很像二分法，所以試想一下，猜一個值，檢查這個值能不能組出來… xd 有困難，不知怎麼組起，和2**n一樣都要暴力試
     * 那如果是拿這個值去索引呢???? 如果我要求 dp[n] 而且已知 dp[n-1] 那我確定，他們兩個就只差一個1
     * 這有 dp 的味道了，但總覺得是不足的，我去那裡搞一個1出來用…
     * 後來開始試著把以 sum 為主的表畫看看，每個元素進來就標記有出現的 sum，但我還是沒想到怎用
     * 後來看了下方的 slow solution 才想到，如果各種出現過的 sum 都知道，那接下來就是去評估這些 sum 如果扮演 left 
     * allsum - left = right  ，我們想要 | left - right | 最小，所以改成  | 2left - sum | 最小 或  |sum - 2left| 最小
     * 這樣就至少可以得到一個 O(n * sum(n)) 的解，我認為，考試時至少要能想出這個解法來，沒滿分至少有答案
     * 
     * 下面的進階版，我覺得很難臨場發揮，最多就是背答案
     * 
     * 
     * 
     * 
    */
	public class MinAbsSum
    {
		/*
         * 演算法思路
         * 這個演算法只有45分
         * 想法是，把值用絕對值排序，找到一半的地方，最小差值會出現在那裡的兩側
         * 不用管值原來的正負，反正可以配任意的正負號給它
         * 但，有個例子打死這個想法 [1, 5, -2, 5, 2, 3]
         * 中間點兩側分別是 8 和 10 差 2
         * 但我其實可以把1 調過去另一邊，讓差值變小，雖然我可以再加碼，再試圖讓最小值移到另一邊去試解決這個狀態
         * 但我也不禁懷疑，是有在左右兩邊是否有任意選選取的組合，可以達到最小
         *
        */

		public int Solution0(int[] A)
        {
            int len = A.Length;
            for (int i = 0; i < len; i++)
            {
                A[i] = Math.Abs(A[i]);
            }

            Array.Sort(A);

            if(len == 0)
            {
                return 0;
            }
            else if(len == 1)
            {
                return A[0];
            }

            int sum = A.Sum();

            int leftSum = 0;
            int ind = 0;

            for (ind = 0; ind < len; ind++)
            {
                if (leftSum + A[ind] <= sum / 2)
                {
                    leftSum += A[ind];
                }
                else
                {
                    break;
                }
            }

            return Math.Min(Math.Abs(sum - 2 * leftSum), Math.Abs(sum - 2 * (leftSum + A[ind])));
        }

        /* 
         * 這時有另一種想法，是從大的分過來，那半部少就分那半，但這是 greedy，不是 dp
         * 結果有62分,死在[3, 3, 3, 4, 5] 這例子上，很明顯，它可以為 0
         * 我發現 greedy 果然是部份解，效率也夠，但在最後一個因子是超大時，它就不靈了
		 *
		 * 結果，我突發奇想的，把兩個解法的答案，再選一個小的當答案，就算是 greedy 到一個不要臉了
		 * 結果… 90分，答案正確，效率八成… Orz....

		 */
		public int Solution1(int[] A)
        {
            int len = A.Length;
            for (int i = 0; i < len; i++)
            {
                A[i] = Math.Abs(A[i]);
            }

            Array.Sort(A);

            if (len == 0)
            {
                return 0;
            }
            else if (len == 1)
            {
                return A[0];
            }

            int left = 0;
            int right = 0;

            for (int i = len - 1; i >= 0; i--)
            {
                if (left > right)
                {
                    right += A[i];
                }
                else
                {
                    left += A[i];
                }
            }

            return Math.Abs(left - right);
        }

		/*
         * 照 https://codility.com/media/train/solution-min-abs-sum.pdf
         * 進行 slow solution 實作，正確100，效率40
         * 
         * 演算法思路
         * 轉正值，排序，這些想法和上面一面，它最神奇的是那個dp
         * 它很暴力的建一張dp表，索引就是有出現過的sum值全部一把記，而且是在每個元素進來時
         * 就把所有它可能出現的 sum 就一直打上標記，其中最難理解的是，要反過來打
         * 你實際上在除錯就會發現，如果從前頭打過來，你利用加法所追打的標記，都又被再看見一次
         * 由於標記是往後打，所以從後掃回才不會重覆
         * 
         * 再來就是對 dp 表掃一次，目的就是找某個總值可以造成 |left - right| 最小
         * 這裡有點小數學
         * 
         * sum = left + right
         * diff = right - left (假設 left < right
         * => diff = sum - left - left => sum -2 * left
         * 
         * 這裡的 left 就是我們 dp 表的前半，所以只要把 dp 表用上面的公式掃一次就可以得到一個最小差值
         * 文件是寫掃到一半多一個就好
         * 我是覺得掃一次，但不掃那種不合理的 (left 超過 sum 的一半)，視覺上比較好理解，但多作了半輪…
        */
		public int Solution2(int[] A)
		{
			int len = A.Length;
			for (int i = 0; i < len; i++)
			{
				A[i] = Math.Abs(A[i]);
			}

			Array.Sort(A);

			if (len == 0)
			{
				return 0;
			}

			int max = A.Max();
			int sum = A.Sum();

			bool[] dp = new bool[sum + 1];
			dp[0] = true;

			for (int i = 0; i < len; i++)
			{
				for (int j = dp.Length - 1; j >= 0; j--)
				{
					if (dp[j] == true && j + A[i] <= sum)
					{
						dp[j + A[i]] = true;
					}
				}
			}

			int minResult = int.MaxValue;
			for (int i = 0; i < dp.Length; i++)
			{
				if (dp[i] == true && sum >= 2 * i)
				{
					minResult = Math.Min(minResult, sum - 2 * i);
				}
			}

			return minResult;
		}

        /* 
         * 我現在將是題想像成，給你一堆正整數，你幫我分兩堆，讓兩邊的總和相差最小，這點我的想法和文件的提示一樣
         * 然後開始讀文件的 golden solution...
         * 
         * 該演算法的細節，我寫在碼中，比較容易看懂
         * 前面一開始，都換正數排序也沒問題
         * 它主要的精神是，統計每個元素出現的次數，利用它來規劃 dp 表
         * 加上題目限定能出現的元素不到200個，自然它的成本就整個低下來
         * 如果你這時想，那我就把兩個同元素抵消，剩下的再來互消不就可以… 那你就想一下 1,1,1,1,4 會怎麼死
         * 
         * dp 表其實就是一直在記兩件事
         * 一。這個總和有沒有出現過
         * 二。當前的元素下在這個總和底下，還有沒有剩餘數量可以用
         * 
         * 所以，對於已組出的總和，元素就全剩下來，對於沒組過的總和，就看前面比我少這個元素的總和，是不是有量可以借
         * 就這樣一路借下去到沒得借，借以開拓出更多可以成立的總和
         * 
         * 很了不起，真的 !!
         * 
         * 寫到這裡，總算完成17個 lesson 全攻略，特此記念 ~~~ 2017/09/13
         * 
		 * 
         */ 
        public int Solution(int[] A)
        {
			int len = A.Length;
			for (int i = 0; i < len; i++)
			{
				A[i] = Math.Abs(A[i]);
			}

			Array.Sort(A);

			if (len == 0)
			{
				return 0;
			}

			int max = A.Max();
			int sum = A.Sum();

            //元素出現統計表
			int[] counter = new int[max + 1];
			for (int i = 0; i < len; i++)
			{
				counter[A[i]] += 1;
			}

            //動態表，它代表當前所有的各種總和的狀態
            //-1 代表這個總和還沒出現過，預設總和 0 是會出現的
			int[] dp = new int[sum + 1];
            for (int i = 1; i < dp.Length; i++)
            {
                dp[i] = -1;
            }

            for (int i = 0; i < counter.Length; i++)
            {
                //處理每個有出現的元素
                if(counter[i] > 0)
                {
                    //每個元素，都要考慮它與總和的所有可能
                    for (int j = 0; j < dp.Length; j++)
                    {
                        if(dp[j] >= 0)
                        {
                            //這裡表示，這個總和早出現過了，不需要我的元素
                            //所以它的格子裡留下我所有的數量，等後面的總和來借走
                            dp[j] = counter[i];
                        }
                        else if (j >= i && dp[j - i] >= 0)
                        {
                            //別忘了，能進到這，至少dp[j] 是小於0了
                            //這裡表示，這個總和還沒出現過，我是不是能用手上的元素去組出它
                            //手上的元素是 i，所以這個 總和-i 變成我們要考慮借的人
                            //如果那個人手上有我這個元素可以借，那就借過來，自己吃一個，其它的再留給後人借
                            //先前把總和留下來，就是為了這個，所有已組成的總合，都把這個量留下來給後人借
                            //這也解釋了上面那個很難理解的 dp 格直接填 counter

                            dp[j] = dp[j - i] - 1;
                        }
                    }
                    
                }
            }


			int minResult = int.MaxValue;
			for (int i = 0; i < dp.Length / 2 + 1; i++)
			{
                //這裡就照演算法用 /2+1 但其實會有錯的，它會過頭，所以還是得保證 i 不能過半
                if (dp[i] >= 0 && sum - 2*i >= 0)
                {
                    minResult = Math.Min(minResult, sum - 2 * i);
                }
			}

			return minResult;
        }
	}
}
