using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
    public class MissingInteger
    {
		/*
            https://codility.com/programmers/lessons/4-counting_elements/missing_integer/

            從指定的陣列中，找出一個最小而且不存在陣列的中的正整數，只能是O(n)
        */

		public int Solution(int[] A)
        {
            /*
             * 演算法思路
             * 一。如果可以鎖定最大和最小值，我至少可以鎖小我要確認的範圍
             * 二。如果開頭就不是 1，那答案也只能是 1
             * 三。負數就來亂的，濾掉它
             * 四。為了快速在 1..最大值間找到缺口，我需要一個資料集合協助，所以我使用 hashset O(1)
             * 來幫我快速確認該數值有沒有出現過
             * 五。先排序就不用想了，沒有 O(n) 的排序法可以用
            */

            int maxVal = 1;
            int minVal = int.MaxValue;
            HashSet<int> pureValues = new HashSet<int>();

            //try to define max min value and create a hashset
            for (int i = 0; i < A.Length; i++)
            {
                if(A[i] > 0)
                {
                    if(A[i] > maxVal)
                    {
                        maxVal = A[i];
                    }

                    if(A[i] < minVal)
                    {
                        minVal = A[i];
                    }

                    pureValues.Add(A[i]);
                }
            }

            if(minVal != 1)
            {
                return 1;
            }
         
            for (int i = minVal; i <= maxVal + 1; i++)
            {
                if (pureValues.Contains((i)) == false)
                {
                    return i;
                }
            }

            //impossible
            return 1;
        }
    }
}
