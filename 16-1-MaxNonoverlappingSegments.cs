using System;
namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/16-greedy_algorithms/max_nonoverlapping_segments/
     * 
     * 給你一堆板子，給你 non-overlap 的定義，問你所有的 non-overlap 集合中，最多可以有多少板子
     * 
     * 想法，首先暴力法，由於沒有規定 non-overlap 集合一定蓋到什麼程度，所以連選一塊板子也是可以的
     * 這時，宇集合是 2**N 個組合，然後每個組合都要去掃版子有沒有交集，兩兩檢查是 n**2
     * 所以在絕對暴力下，複雜度是 O(2**n * n**2)，這看也知道，寫的出來才有鬼…
     * 
     * 這時就開始猜想，這種複雜度，再怎麼降階，也不太容易從 2**n 拉下來到 n 把，更別說後面還有 n**2
     * 所以，這時得到一個可能的訊號「這種不思議的降階，只有 Greedy 作的到」
     *
     * 題目不可能無理取鬧，所以把解法轉向 greedy
     * 那就變成，看一塊板子能放，就放一塊，直到放完，看看有幾塊，就當它是最大了 XD
     * 了不起再反向作回來，看看有沒有變大，選一個答案用 XDD 
     *
    */

	public class MaxNonoverlappingSegments
    {
        
        /*
         * 演算法思路：
         * 說實在的…這題這樣解實在讓我不太能接受… 
         * 這就只是依序算出可以存在的區，沒有其它想法，而且資料本身有序了
        */

        public int Solution(int[] A, int[] B)
        {
            if (A.Length == 0)
            {
                return 0;
            }

            int len = A.Length;
            int currOverLap = B[0];
            int count = 1;

            for (int i = 1; i < len; i++)
            {
                if(A[i] > currOverLap)
                {
                    count++;
                    currOverLap = B[i];
                }
            }

            return count;        
        }

        /*
         * 由於上面的解法不太有趣，我先記下原本的想法
         * 
         * 一。先掃一次大家的長度，目標是決定一個好長度可以作分界，再爛也要用平均數或中位數
         * 二。先用該數掃一次把能放的都放上
         * 三。再把剩下的掃一次，能放上的就放上ß
         * 
         * 寫到這裡發現一個問題… 決定能放上這件事，本身不是 O(1)
         * 這樣的想法行不通…
         * 
         * 我猜他評測的標準，就是你是 O(n) , 而且答案在他可接受的標準
         * 
        */
    }
}
