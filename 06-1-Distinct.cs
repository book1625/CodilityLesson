using System;
using System.Collections.Generic;

namespace Codility
{
	/*
        https://codility.com/programmers/lessons/6-sorting/distinct/

        給個陣列，叫你找所相異元素的個數
    */
	public class Distinct
    {
        /*
         * 這真的不是我的功勞… 但它就是 100 分
         */

        public int SolutionGood(int[] A)
        {
            HashSet<int> hs = new HashSet<int>(A);
            return hs.Count;
        }

		/*
		 * 如果要我自己的話… 由於是 O(nLog(n)), 我會想到需要 BST 一類的樹狀
		 * 或是自已搞個 hash 來試試
		 * 用 array 存 int list ，然後以 hashcode 為 index
		 * 
		 * 這個答案可以拿 91，完全就是大數量的時候效率不夠
		 * 但如果把 list 再換成別的結構，那就失去作己做的羲意了…，用 hashset就好了
		*/
		public int Solution(int[] A)
        {
            int hashWide = 1000000;
            List<int>[] hashTable = new List<int>[hashWide];
            for (int i = 0; i < hashWide; i++)
            {
                hashTable[i] = new List<int>();
            }

            int result = 0;
            for (int i = 0; i < A.Length; i++)
            {
                int tmpIndex = Math.Abs(A[i] % hashWide);

                if (!hashTable[tmpIndex].Contains(A[i]))
                {
                    //here is new value
                    hashTable[tmpIndex].Add(A[i]);
                    result++;
                }
            }

            return result;
        }
    }
}
