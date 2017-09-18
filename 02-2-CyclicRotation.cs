using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
    public class CyclicRotation
    {
		/*
            https://codility.com/programmers/lessons/2-arrays/cyclic_rotation/

            把一個陣列轉指定次數，這是沒有管複雜度，要小心的是空陣列和轉0次

        */
		public int[] Solution(int[] A, int K)
        {
            /*
             * 演算法思路
             * 一。是的，它有可能轉0次
             * 二。轉動其實就是把「前半節沒轉到的」後面放，然後把「後半節有轉到的」後前面放
             * 三。當然，題目會給你一個很大的k，叫你轉到死，所以你要算出它叫你轉了幾圈，那些都是可以省去不作的動作，只轉剩下的次數
            */

            //the question Ask [0..100] !!!
            if(K == 0)
            {
                return A;
            }

            if(A is null || A.Count() == 0)
            {
                return A;
            }

            //prevent K larger than the count of A
            K = K % A.Length;

            List<int> result = new List<int>();

            //copy the second part (K elements) of array 
			for (int i = A.Length - K; i <= A.Length - 1; i++)
			{
                result.Add(A[i]);
			}

            //copy the first part (N-K element) of array 
            for (int i = 0; i <= A.Length - K - 1; i++)
            {
                result.Add(A[i]);
            }

            return result.ToArray();
        }
    }
}
