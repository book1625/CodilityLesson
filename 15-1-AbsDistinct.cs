using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/15-caterpillar_method/abs_distinct/
     * 
     * 給你一堆有正負的值，問你有多少個不同的絕值，O(N) 要作完
     * 
     * 想法，即然有 O(N) 那掃一次絕對值建 hashset 再算次數
     * 要注意它輸入給了 int min max， 這是在考你知不知道 int min 的絕對值是轉不回來的
     * 所以，用 long 處理比較快
     * 
     * 發現有試寫挑戰 space O(1) 不錯~~ 狀況有點多，不太好寫對
     * 
    */

	public class AbsDistinct
    {
        /*
         * 如上面所寫的原始正解
        */
        public int Solution0(int[] A)
        {
            HashSet<uint> hs = new HashSet<uint>();

            for (int i = 0; i < A.Length; i++)
            {
                if(A[i] == int.MinValue)
                {
                    hs.Add((uint)int.MaxValue + 1);
                }
                else
                {
                    hs.Add((uint)Math.Abs(A[i]));
                }
            }

            return hs.Count();
        }

        //以下是利用毛毛蟲算法，挑戰 space O(1) ------------------

        /*
         * 演算法思路
         * 因為有序，所以從兩側逼入，那邊大就逼那邊
         * 因為用 while 寫很難理解，所以而且很難處理連續出現相同數，所以放棄
        */
        public int SolutionBad(int[] A)
        {
            long[] temp = new long[A.Length];
            A.CopyTo(temp, 0);
        

            int beg = 0;
            int end = A.Length - 1;
            int count = 0;
            while(beg < end)
            {
                while(Math.Abs(temp[beg]) > Math.Abs( temp[end]) && beg <= end)
                {
                    count++;
                    beg++;
                }

                if(beg < end && Math.Abs(temp[beg]) == Math.Abs(temp[end]))
                {
                    end--;
                }

                while(Math.Abs(temp[end]) > Math.Abs(temp[beg]) && beg <= end)
                {
                    count++;
                    end--;
                }

				if (beg < end && Math.Abs(temp[beg]) == Math.Abs(temp[end]))
				{
                   beg++;
				}

                while(beg < end && temp[beg] == temp[end])
                {
                    end--;
                }
            }

            if (beg == end)
                return count + 1;
            else
                return count;
        }

        /*
         * 演算法思路
         * 一樣從頭尾逼，但演算法改成一次只會往前逼一動，所以不是前跳就是後跳
         * 利用一個值來處理出現過的絕對值以過慮重覆出現的值
         * 
         * 最後一動要小心，同時跳會產生，同時進同一位置和剛前後錯開
         * 同時進入時，也不代表那個值就有效 ... 
        */
        public int Solution(int [] A)
        {
			long[] temp = new long[A.Length];
			A.CopyTo(temp, 0);


			int beg = 0;
			int end = A.Length - 1;
			int count = 0;

            long currAbs = long.MaxValue;
           
			while (beg < end)
            {
                if(Math.Abs(temp[beg]) > Math.Abs(temp[end]))
                {
                    if (currAbs > Math.Abs(temp[beg]))
                    {
                        count++;
                        currAbs = Math.Abs(temp[beg]);
                    }

                    beg++;
                }
                else if ( Math.Abs(temp[beg]) < Math.Abs(temp[end]) )
                {
                    if (currAbs > Math.Abs(temp[end]))
                    {
                        count++;
                        currAbs = Math.Abs(temp[end]);
                    }

                    end--;
                }
                else
                {
                    if (currAbs > Math.Abs(temp[end]))
                    {
						currAbs = Math.Abs(temp[end]);
                        count++;
                    }

					beg++;
					end--;
                }
            }

            if (beg == end && Math.Abs(temp[beg]) != currAbs)
				return count + 1;
			else
				return count;
        }
    }
}
