using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
    public class OddOccurrencesInArray
    {
		/*
            https://codility.com/programmers/lessons/2-arrays/odd_occurrences_in_array/

            在陣列中，找出不成對的數字，只能掃一次 O(N)

        */
		public int Solution(int[] A)
        {
            /*
             * 演算法思路
             * 一。我只能掃一次就得知道我出現了偶數次，但偶多少無所謂
             * 所以我就需要一個集合，在我發現不存在時加入，存在時移除，這樣留下來的就是出現奇數次的值
             * 二。這個集合要快進快出，所以選 hashset ，反正我也只要存在與否而已
             * 
             * 這個解法，空間不算 O(1)
            */

            // O(1) for check duplicate value
            // but space cannot control
            HashSet<int> hs = new HashSet<int>();

            //O(N) for all elements checking
            foreach(int val in A)
            {
                if(hs.Contains(val))
                {
                    hs.Remove(val);
                }
                else
                {
                    hs.Add((val));
                }
            }

            //by the question, there is impossilbe for zero elements 
            return hs.First();
        }
    }
}
