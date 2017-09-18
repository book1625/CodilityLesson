using System;
using System.Collections.Generic;

namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/15-caterpillar_method/count_distinct_slices/
     * 
     * 給你一堆數字，問你有多少個具不重覆元素的 slice，看他的例子比較好了解 slice
     * 
     * 想法， slice 是連續的，所以也不可能跳找，找重覆靠 hashse 也沒問題
     * 如果找到一沒不重覆，那它能組幾，試畫一下會發現它有規律，n 元素就是  1+2+..n 個
     * 利用梯型公式秒算可得，結果這個想法中標了，死在下面第一個解法的點
     * 
     * 我忘了有這樣的例子 3,4,5,3,5,2 ..
     * 所以要乖乖的用每次加入新元素，考慮可以生成的量，每個元素都有自已上次出現的點，以它來計算可以追加多少個 slice 
     * 
    */
	public class CountDistinctSlices
    {
        /*
         * 演算法思路
         * 就想說一路找，找到一個造成沖突的，那不要它，我手上就是一個大集合
         * 然後接下來就往下切出一個新的集合
         * 但這個想法是錯的，用例子就看的懂
         * 3,4,5,3,5,2
         * 當遇到 3 時，會決定 3,4,5 一組，然後就從 3 開始往下切
         * 但是… 4,5,3 也可以是一組…，這個算法找不到 ....
         * 
         * 就算在遇到 3 時往前再湊集合，答案也是不夠的，用例子看
         * 3,4,5,6,3,5,2
         * 遇到 3 時切集合，並往前湊 4,5,6,3 然後就以新的 3 開始往下
         * 這樣 中間還有個 6,3,5,2 又被跳開…
         * 
        */
        public int Solution0(int M, int[] A)
        {
            HashSet<int> hs = new HashSet<int>();
            int beg = 0;
            int end = 0;

            long count = 0;

            while(end < A.Length)
            {
                if( ! hs.Contains(A[end]) ) 
                {
                    hs.Add(A[end]);
                    end++;
                }
                else
                {
                    beg = end;

                    count += (1 + hs.Count) * hs.Count / 2;
                    hs = new HashSet<int>();

					//這個覆元素可以往前多少元素去湊一個新集合，這個集合中
					//可以以新元素為基點，產生多個小集合，這些集合都要含有新元素
					//不含的部份在上面公式有算過了…
					//if (lastMap[A[end]] >= 0)
					//{
					//	count += end - lastMap[A[end]] - 1;
					//}
                }  
            }

            if(hs.Count != 0)
            {
                count += (1 + hs.Count) * hs.Count / 2;
            }



            if(count > 1000000000)
            {
                return 1000000000;
            }
            else
            {
                return (int)count;
            }
        }

		/*
         * 演算法思路
         * 用這個 3,4,5,3,5,2 例子，才會比較好想
         * 一。其實如果要 O(n) 算出任意組合的數量，它基本上一定有公式，或是「可以用累加的找出來」
         * 二。每一個元素在加入集合時，你其實已經算過它的前半，你只要考慮這個元素所追加的可能
         * 三。所以元素進來時，如果前面都完全相異，你其實可以為這第 i 元素，追加 i 種集合
         * 四。然而，事情沒那麼簡單… 你一定會遇到中間有重覆元素，這時，你就生不出 i 個集合了
         * 你往前組集合時，最怕的就是遇到和自己一樣，或是遇到其它元素重覆
         * 五。這時你可以想像，如果我知道當前有一個 danger point ，那我最多不能碰到它,產生 i-danger 個集合
         * 然後我會決定接下來的 danger 給下個元素用
         * 六。你會發現 danger 其實就是先前的 danger 與 你上次出現的點 取個近的(大的)
         * 
         * 這裡有一件事也要記得，題目提醒你，如果超過某值就直接傳定值，那通常這個值都大的會溢位!!!!
        */
		public int Solution(int M, int[] A)
		{
			int[] lastMap = new int[M + 1];
			for (int i = 0; i < lastMap.Length; i++)
			{
				lastMap[i] = -1;
			}

            long count = 0;
            int danger = -1;
            for (int i = 0; i < A.Length; i++)
            {
                danger = Math.Max(danger, lastMap[A[i]]);
                count += i - danger;

				//記錄某值出現在索引i
				lastMap[A[i]] = i;
            }

			if (count > 1000000000)
			{
				return 1000000000;
			}
			else
			{
				return (int)count;
			}
		}
    }
}
