using System;
namespace Codility
{
    public class MaxDoubleSliceSum
    {
		/*
         * https://codility.com/programmers/lessons/9-maximum_slice_problem/max_slice_sum/
         * 
         * 給定一個數列和三個相異的的索引 x > y > z， x可以是陣列頭，z可以是陣列尾 
         * left = sum(x+1 ... y-1)
         * right = sum(y+1 ... z-1)
         * 求最大的 left + right 值
         * 麻煩的是 x , y , z 本身都沒算到，而且 left and right 可以為空集合
         * 
         * 而且，只能 O(n)算完
         * 
         * 這是我再次看還是沒想起解法，這就是看人家解答的後遺症了…
        */
		public int SolutionOther(int[] A)
        {

            /*
             * 這是抄高手的答案
             * 直觀來看就是
             * 從左邊算一次以 i 之前的最大和
             * 從右邊笡一次以 i 之後的最大和
             * 然後答案等於在每個取 左+右 的最大和… 
             * 
             * 他就是把它同時寫，而且他都用 0 來比還正確，不理解為何，因為輸入是可以有負
            */

			var N = A.Length - 2;

			var leftSums = new int[N];
			var rightSums = new int[N];

			for (int i = 0, length = N - 1; i < length; i++)
			{
				leftSums[i + 1] = Math.Max(0, leftSums[i] + A[i + 1]);
				rightSums[length - i - 1] = Math.Max(0, rightSums[length - i] + A[length - i + 1]);
			}

			var maxSum = int.MinValue;

			for (int i = 0; i < N; i++)
			{
				maxSum = Math.Max(maxSum, leftSums[i] + rightSums[i]);

                Console.WriteLine("( {0}, {1} )", leftSums[i] , rightSums[i]);
			}

			return maxSum;
        }

        public int Solution(int[] A)
        {
			/*
             * 演算法思路
             * 
             * 一。這不是我自己想出來的，是一直在看別人到底在寫啥，主要就是看上面那一份
             * 二。不同於 max slice 在過程中記下了過去曾經發生過的最大片段，並拿來和當前比較
             * 這個演算法主旨在在評估當下這個元素進來時，到底是會幫助增大，還是造成縮小
             * 它的精神，最重要的是那個 0 … 題目是接受你選空集合的，所以0其實代表的就是不選了，前面定型
             * 試想 如果在 i 點上(包含i)
             * 你可以預見你的總值，是前一個元素的最大總值+加上你自己，但如果前一個元素的總值只是來幫你變小，那是不是不如不要加它…
             * 如果我看見往前的總值已經沒有加的價值(變負了)，那還不如東山再起，重新累計一個總值到我目前的 i 的總值
             * 這個總值如果還是正值，就還有留下來加下去的價值，因為我們要找一個最大值，而這個區段還有長大的機會
             * 如果你輸入一個連續負值，就可以很明顯的觀察到，你一點也不想往前加，只想用當下這個元素…
             * 所以，這個動作，其實就是在找，以 i 往前連加，可以得到的最大正值，並把它記錄下來…
             * 
             * (以下才是最容易被忘記的…)
             * 如果懂了，那再來就簡單了，因為同樣的事情，你也可以把它從後面一路反算回來 
             * 靠這兩組資料，你可以開始在所有的 i 上，試算往左和往右兩個值的和，來選出一個最好的 i
             * 
             * 要小心的是，輸入的資料少，你就可能會死索引，切割元素本身是不會列入計算，尾也是，而元素可以切在第一個頭上
             * 所以每次算出來的 i 結果，其實是切在 i+1 時要計算的資料(從前面算來的話)
            */

			int[] leftSum = new int[A.Length];
            for (int i = 1; i < A.Length -1; i++)
            {
                int temp = Math.Max( 0, leftSum[i] + A[i]);

                //真正存是在於 i+1 ，因為 i+1 是切點時，計算到 i 位
                leftSum[i + 1] = temp;
            }


            int[] rightSum = new int[A.Length];
            for (int i = A.Length - 2; i > 0; i--)
            {
                int temp = Math.Max(0, rightSum[i] + A[i]);

                //從右邊切回來，以 i-1 為切點，計算從尾到 i 的結果
                rightSum[i - 1] = temp;
            }


            //頭尾不會是切點，不要它，找出那個切點是具有最大和
            int result = 0;
            for (int i = 1; i < leftSum.Length - 1; i++)
            {
                if (leftSum[i] + rightSum[i] > result)
                {
                    result = leftSum[i] + rightSum[i];
                }
            }

            return result;
        }
    }
}
