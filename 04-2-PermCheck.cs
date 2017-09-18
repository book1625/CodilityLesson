using System;
using System.Collections.Generic;

namespace Codility
{
	/*
        https://codility.com/programmers/lessons/4-counting_elements/perm_check/
        
        A permutation is a sequence containing each element from 1 to N once, and only once.

        這題就是要判斷給的陣列是不是 permutation(排列)，只能掃一次

    */
	public class PermCheck
    {
        public int Solution(int[] A)
        {
            /*
                演算法思路
                一。判斷有沒有從1開始一路連下去，排序當然是別想了
                二。如果我能有一個集合，它是所有不重覆的資料，那我就可以透過幾件事來判斷
                集合的最小值，最大值，元素的數量，只要這三件事就可以判斷是否有湊到 1..N 一個不少
                而這個集合如果和 A 等長，那表示沒有重覆元素
                三。所以 … hashset
            */

			int maxVal = 0;
            int minVal = int.MaxValue;
			HashSet<int> pureValues = new HashSet<int>();

			//try to define max min value and create a hashset
			for (int i = 0; i < A.Length; i++)
			{
				if (A[i] > 0)
				{
					if (A[i] > maxVal)
					{
						maxVal = A[i];
					}

					if (A[i] < minVal)
					{
						minVal = A[i];
					}

					pureValues.Add(A[i]);
				}
			}

            if ( minVal == 1 && maxVal == A.Length && pureValues.Count == A.Length)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
