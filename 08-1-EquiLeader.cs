using System;
using System.Collections.Generic;

namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/8-leader/equi_leader/
     * 
     * 給你一堆數字，從中間任意切半，如果兩半的 leader 相同叫有 equi leader
     * 問你到底有多少個 equi leader(能切幾個點是兩半相等的)
     * 
     * ya .. 回想成功，有記得演算法
    */

	public class EquiLeader
    {
        /*
        * 演算法思路
        * 一。我的想法是，如果能在合理時間內知道 Leader 是誰，那其實刷陣列找 Leader 就可能可以
        * 二。由於看了他的說明，教你用 stack 找 Leader，我是覺得用 Dictionary 比較直覺，而且還可以順手算數量
        * 三。有了 Leader 和數量，再來就是刷陣列如何進行，我的想法是，如果 Leader 的數是超過整個陣列的一半
        * 而我找到一個切點，它的前半與後半的 Leader 還要一樣，那這個 Leader 就不可能是別人了，前面的大半+後面的大半 肯定大過總數的一半
        * 所以我就拿了該 Leader ，計算前半目前有幾次，後半有幾次，只要它們各持保持超過該半段的一半，切點就成立
        */

		public int Solution(int[] A)
        {
            Dictionary<int, int> counter = new Dictionary<int, int>();

            for (int i = 0; i < A.Length; i++)
            {
                int temp = 0;
                if(counter.TryGetValue(A[i], out temp))
                {
                    counter[A[i]] = temp + 1;
                }
                else
                {
                    counter.Add(A[i], 1);
                }
            }

            int maxCount = 0;
            int maxKey = 0;
            foreach(var kv in counter)
            {
                if(kv.Value > maxCount)
                {
                    maxCount = kv.Value;
                    maxKey = kv.Key;
                }
            }


            if(! (maxCount >= A.Length / 2 + 1 ))
            {
                //there is no leader... 
                return 0;
            }

            //now maxKey is your leader who occurs maxCount

            int preCount = 0;
            int postCount = maxCount;
            int equiLeader = 0;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == maxKey)
                {
                    preCount++;
                    postCount--;
                }

                //check both leader
                if ((preCount >= (i + 1) / 2 + 1) &&
                    (postCount >= (A.Length - i - 1) / 2 + 1))
                {
                    equiLeader++;
                }
            }

            return equiLeader;
        }
    }
}
