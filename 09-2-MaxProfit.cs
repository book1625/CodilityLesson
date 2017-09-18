using System;
namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/9-maximum_slice_problem/max_profit/
     * 
     * 給你每天的股價，問你如果交易一次(某天進，某天出，可以當天進出)，能得到的最大利益是多少
     * 
     * 這題比上題容易多了…
    */
	public class MaxProfit
    {
        /*
         * 演算法思路
         * 我的目標是，在每一天都試著結算，所以我在每一天都要知道今天前的最低價，然後相對於今日的結果
         * 然後，從這些結果中，找一個最大的，就是最大利益
        */
        public int Solution(int[] A)
        {
            if(A.Length == 0)
            {
                return 0;
            }

            int everMin = A[0];
            int currMax = 0;

            for (int i = 1; i < A.Length; i++)
            {
                int temp = Math.Max(A[i] - everMin, currMax);

                everMin = A[i] < everMin ? A[i] : everMin;
                currMax = temp > currMax ? temp : currMax;
            }

            return currMax;
        }
    }
}
