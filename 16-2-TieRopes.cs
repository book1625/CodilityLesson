using System;
namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/16-greedy_algorithms/tie_ropes/
     * 
     * 給你一堆繩子，相鄰的可以綁一起，給你一個指定長度k，你要試著把相鄰的綁到 >=k，問最多可以綁幾條
     * 
     * 想法，brute-forced 單條超過k的都拿掉，變成很多小塊，每個小塊的單條都不超過K，總和則不一定
     * 再來對每個小塊，執行試切，這時發現它的難度和題目一樣，只是少了長的繩子
     * 對每條小繩子，往後集到夠長就放開，可以得到 m 條繩子，要從中間選出沒有交疊的，那就是 2**m 
     * 突然發現和上一題好像 XD ，如果題目本來就給全部小於 k 的繩子，那這是就回到 O(2**n)
     * 
     * 所以，和上題的推論一樣，要降到 O(n)，只能 Greedy
    */
	public class TieRopes
    {
        /*
         * 演算法思路
         * 
         * 快無言了…這樣就一次過，是來讓我們開心的嗎…
         * 
         * 由於只能相鄰的相接，所以你也不可能排序換位，只能看到就接，超長就放
         * 可以肯定答案不是最佳解…
         * 
         * 這一類題目如果出來，我該如何知道不需最佳解????
         * 我可能一直在想解法，一直卡在不可能 O(n)，這種情況該如何處理???
         * 
        */
        public int Solution(int K, int[] A)
        {
            int len = A.Length;
            int currLen = 0;
            int count = 0;

            for (int i = 0; i < len; i++)
            {
                currLen += A[i];

                if(currLen >= K)
                {
                    count++;
                    currLen = 0;
                }
            }

            return count;
        }
    }
}
