using System;
using System.Collections.Generic;

namespace Codility
{
	/*
        https://codility.com/programmers/lessons/10-prime_and_composite_numbers/count_factors/

        就是找因數
    */
	public class CountFactors
	{
        /*
         * 演算法思路
         * 
         * 其實就是先看了質數的寫法，把找質數這件事看了一次
         * 
         * 這裡一開始就完成了大半卻卡在最後一組測試走不完
         * 因為，照原本的寫法，比對 i*i < n 這樣其實是會去計算一組 i*i
         * 但，它給你的測試 i 可以到 Maxint...  所以一乘就爆了
         * 一爆就又到小值一直跑，一值到出現一個 0 造成例外才會停…
         * 
         * 所以 只要有 int 間的加減乘除，就要注意溢位的問題啊 !!!!
         * 
         * 一開始，我是為了不想要相乘，所以先去算方根值來避免
         * 但又遇到，會有 double 對 int 比較的誤差可能，所以改用 long 在存相乘
         * 這樣 n 在 max int 內都沒事
        */
		public int Solution(int N)
        {
            HashSet<int> divisors = new HashSet<int>();

            int i = 1;
            long temp = 1;

            while( temp <= N )
            {
                int rem;
                int qout = Math.DivRem(N, i, out rem);
                if(rem == 0)
                {
                    divisors.Add(i);
                    divisors.Add(qout);
                }
                i++;
                temp = (long)i * i;
            }

            return divisors.Count;
        }
    }
}
