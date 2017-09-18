using System;
using System.Linq;
namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/14-binary_search_algorithm/min_max_division/
     * 
     * 對一堆數，作 k 切割，每一切割可以從 0 到 n 個元素，空切割的和是0，每個切割都有個和，要找這些和裡的最大值
     * 並且這個切法是讓這個最大值可以最達到最小，要在 O(N*log(N+M)) 完成 (M是元素的上界)
     * 
     * 回想，即然存在一個最小，表示比它大一點的會成功，比它小的會失敗 -> binary search
     * 再來，總值全加和也沒有爆 int，演算法更是納入 N+M，但我只想到拿總和來逼，這樣沒辦法 n+m
     * 
     * 我忘了題目沒有負值，所以一個區塊的總和最小只能是 0 ，最小的最大當然是把最大元素關在一個集合裡
     * 其它的集合不要超過它，也別再拿東西去加它，最大元素必讓一個集合至少達到這個值，所以它住那，誰就變大，因此最小的可能就是把它關起來
     * 但也要能關的成功才行，所以我們可以知道，答案的下界是最大元素，上界是所有總和
     * 
     * 我發現，就算不管那個下界，我還是可以用 0 下去試切，效率也是會過，畢竟上界很大，而且用 0 切，那個切的演算法寫錯會變無窮迴圈
     * 但我還是沒有作到 nlog(n+m)，只有 nlog(sum(A))，網路上比較易讀的答案也是這麼寫，不進一步深究，畢竟 log 很威啊，再大都砍光
    */

	public class MinMaxDivision
    {

        /*
         * 演算法思路
         * 一。沒想出來，也是偷瞄的…但寫了送一次過，測試自己有找到 bug
         * 二。想法其實從數學開始，如果你把陣列作任意切割，那你的任意一個切割的和，最小是0，最大可以是整個陣列的和
         * 三。而就算有0存在，也一定有其它集合是非空，而他們的和，至少也要一個元素，所以，所有有元素的切割，裡面一定有一個切割
         * 它包含了整個陣列的最大元素，只要它和其它元素配集合，這個集合的值就更大，所以，所有集合各自的和取最大值 m 它最小的可能是
         * 把最大元素獨立成一個集合，而其它的集合的和都小於它
         * 四。由上面的定義，我們就可以判斷  Max(A) <= 最小最大和 <= Sum(A)
         * 五。有了範圍，主演算就決定了用二元下去逼
         * 六。每次要確認的條件能不能切，就是用這個值去試切，每個區塊不能大過這個值，如切的塊數不大於限制，就表示可以成功切
         * 
         * 我還未夠班啊~~~
        */

        private bool IsSplitByK(int K, int [] A, int maxSum)
        {
            //在每次都不超過 maxSum 的情況下，切陣列
            //成功條件是，切完的後，block 數不能超過 K 個

            int currSum = 0;
            int block = 1;
            int i = 0;
            while(i < A.Length)
            {
                if(currSum + A[i] <= maxSum)
                {
                    currSum += A[i];
                    i++;
                }
                else
                {
                    currSum = 0;
                    block++;
                }
            }

            return block <= K;
        }

        public int Solution(int K, int M, int[] A)
        {
            int beg = A.Max();
            int end = A.Sum();
            int mid;
            int result = 0;
            while(beg <= end)
            {
                mid = (end + beg) / 2;
                if( IsSplitByK(K, A, mid) )
                {
                    end = mid - 1;
                    result = mid;
                }
                else
                {
                    beg = mid + 1;
                }
            }

            return result;
        }
    }
}
