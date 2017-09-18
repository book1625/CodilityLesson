using System;

namespace Codility
{
	/*
        https://codility.com/programmers/lessons/5-prefix_sums/count_div/

        指定一範圍，算出可以被某數整除的整數有幾個，要求O(1)

        看複雜度也知道，一定要靠數學算，這是比較麻煩的是，它有多個狀況你你要下去測
        頭尾不整除，頭尾整除，只有一個整除，指定值大到超過你的範圍等等
        而且輸入給個二十億，那運算+和* 都有雷~
    */
	public class CountDiv
    {
        /*
         * 演算法思路如下
         * 
         * 一。如果你把它列出數列來，你會發現就是 K 個一跳，算能跳幾次
         * 二。狀況又分成，第一個數可以整除與不能整除，算法看來不同
         * 三。如果你把不能整除的部份拿掉，它不就又變成有整除的
         * 四。所以就是，算出前面要拿掉幾個沒用的，然後再算出跳了幾次，再依餘數決定是否多得一個
         *
        */

		public int Solution(int A, int B, int K)
        {
            //這題有個問題是 0 到底算不算一個可以整除的數，上傳後發現是…可以的…

            int quoitent = 0;
            int remainder = 0;
            int headSkip = 0;

            //check useless haeder
            quoitent = Math.DivRem(A, K, out remainder);
            if(remainder != 0)
            {
                headSkip = K - remainder;
            }

            //
            quoitent = Math.DivRem(B - A + 1 - headSkip, K, out remainder);

            if(remainder > 0)
            {
                return quoitent + 1;
            }
            else
            {
                return quoitent;                
            }
        }
    }
}
