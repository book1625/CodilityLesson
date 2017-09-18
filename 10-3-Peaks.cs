using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
	/*
        https://codility.com/programmers/lessons/10-prime_and_composite_numbers/peaks/

        給你一堆值，並且告訴你，一個比左右都大的值叫 peak，請注意，右左都要，所以頭尾是沒機會的
        然後，它希望你可以把陣列均分成 k 份，每一份至少有一個 peak，這樣 k 最多有可能切到幾份
        中間說一堆只是在講 peak 是可以在每一份的頭尾，不影響他是 peak 的身份

        試想，brute-forced 是反正最多是 n / 2 份 (每隔一人就 peak)
        所以就對所有 n/2 份檢查成不成立，演算法 O(n**2)，然後題目要 n*loglogn，嚇~小很多

        這裡回想時，和原法有小出入，我忘了是均分，可以利用 divisor ，它的數量是小的，去作檢查也不會貴
        我是為了降複雜度，想用二分法來逼 k，因為理論上 k 如果不成立，那大的 k 也不成立了
        這個想法有個不好的點是，我其實不太確定，如果切 k 成立，那 k 以下都可以成立…不知道有沒有特例??
    */
	public class Peaks
    {
		/*
         * 演算法思路
         * 一。找peak不是問題，定義都給了，所以先把 peak 標出來 O(n)
         * 二。如果要均分陣列，可能的值沒幾人，就陣列長度n的 divisor 
         * 三。拿到每個 divisor 後，其實就是開始暴力試(因為也沒有更神的想法了)
         * 想像陣列分成 k 份，每一份就有 n/k 個，然後就開始依序檢查每一份n/k個裡是否至少有隻 flag
         * 如果能安全的檢查到最後一份，那這個 k 就是我們要的
         * 理論上 k 愈小愈好成立，所以 k 到某個值就以後就不行了，所以在找時是把所有的 divisor 排序，從大的找起
         * 第一個找到的就是最大可成立的分割值
         * 
         * 找 divisor 的演算法，後有來遇到當值很大時，用兩個 i 去相乘判斷有沒有<=n 那個乘法還是基於 int執行
         * 所以會造成溢位…，然後死很久，所以這裡是採先算方根的方式來避 i*i
         * 但我有想到一點可以改進的是，查 peak 可以不要一隻隻找，該用 prefix sum 來作
         * 用二元加 prefix 不知會不會過，要來找時間試
         * 
        */
		public int Solution(int[] A)
        {
            //get peaks
            bool[] peaks = new bool[A.Length];
            for (int i = 1; i < A.Length - 1; i++)
            {
                if(A[i] > A[i-1] && A[i] > A[i+1])
                {
                    peaks[i] = true;
                }
            }

			//get divisor
			HashSet<int> divisors = new HashSet<int>();

            int N = A.Length;
            double maxInd = Math.Sqrt(A.Length);

			int ind = 1;
			while (ind < maxInd)
			{
				if (N % ind == 0)
				{
					divisors.Add(ind);
					divisors.Add(N / ind);
				}
				ind++;
			}

            long temp = ind;
            if ( temp * temp == N)
			{
				divisors.Add(ind);
			}

            var divList = divisors.ToList();
            divList.Sort();
            divList.Reverse();

            // now we got ordered dividsors...

            //start to check k useful

            foreach( int k in divList)
            {
                int partCount = N / k;

                for (int i = 0; i < k ; i++)
                {
                    bool hasPeak = false;
                    for (int j = i * partCount  ; j < (i + 1) * partCount ; j++)
                    {
                        if(peaks[j] == true)
                        {
                            hasPeak = true;
                        }                
                    }

                    if (hasPeak == false)
                    {
                        //this part has no peak, this k is useless
                        break;
                    }

                    if(i == k - 1)
                    {
                        //here is mean all part has peak
                        //this k is a max value to divide the array
                        return k;
                    }
                }
            }

            return 0;
        }
    }
}
