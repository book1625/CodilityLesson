using System;
using System.Collections.Generic;

namespace Codility
{
	/*
        https://codility.com/programmers/lessons/6-sorting/triangle/

        給你一堆數字，從中間任意選三個，問你可不可以湊出三角形…

        這是一樣是數學問題，而且後面題目也有考，所以我記得

        如果他是問你有幾個，那寫法就不太一樣，要一直找到一組不是的為止

    */
	public class Triangle
    {

        /*
         
        演算法思路
        一。這是基本上也是算用猜到的，因為是目是在排序類裡，所以就往排序猜
        二。如果你有一個排好的列，則如果你找到連續三個數他們可以符合三角型，則這三個數中間的所有數都可以成為三角型
        因此，如果你發現你有連續三個數不能成為三角型，那你再往下移也沒意義了，因為你手上這三個，都是被包在後面的集合裡
        你都不能成立了，那後面的數就更不能成立了…
        三。上面的概念是用想的，但沒有數學證明，寫時只能賭一把
        四。如果上面的算法是對的那它就只有 O(n), 存在於一直找不到，確認到最後三個元素
        五。整體演算法是基於 list 的 sort …，查 msdn 是說它是平均 nLog(n), 有可能到 n*n

        */

		public int Solution(int[] A)
        {
            
            List<long> sl = new List<long>();

            for (int i = 0; i < A.Length; i++)
            {
                sl.Add(A[i]);
            }

            sl.Sort();


            for (int i = 0; i < sl.Count - 2; i++)
            {
                //check
                if( sl[i] + sl[i+1] > sl[i+2]
                    //&& sl[i+1] + sl[i + 2] > sl[i] &&    //其實這兩個檢查可以不要，因為資料有序，這兩必成立
                    //&& sl[i] + sl[i + 2] > sl[i + 1] 
                  )
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}
