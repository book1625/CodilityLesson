using System;
using System.Collections.Generic;

namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/8-leader/dominator/
     * 
     * 這題就是叫你找 leader，比較特別的是，它要回傳任一個 leader的索引陣列，找不到回 -1
     * 這種回傳比較特別，再來是它的值域給了全 int ，這很可怕，建 table 都是不可行，有加減也會死，絕對值也死
     * 
    */
	public class Dominator
    {
        
        /*
         * 演算法思路
         * 一。沒有思路，就是抄 EquiLeader 的作法來改
         * 二。它沒有 O(1) 的 space，字典就是多出來的
         * 三。這題要 O(1) space 的話，只能在原始陣列上動手作 stack … 不幹
        */
        public int Solution(int[] A)
        {
			Dictionary<int, int> counter = new Dictionary<int, int>();

			for (int i = 0; i < A.Length; i++)
			{
				int temp = 0;
				if (counter.TryGetValue(A[i], out temp))
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
			foreach (var kv in counter)
			{
				if (kv.Value > maxCount)
				{
					maxCount = kv.Value;
					maxKey = kv.Key;
				}
			}


			if (!(maxCount >= A.Length / 2 + 1))
			{
                //there is no leader... 
                return -1;
			}

			//now maxKey is your leader who occurs maxCount

			for (int i = 0; i < A.Length; i++)
			{
				if (A[i] == maxKey)
				{
                    //dominator occurs
                    return i;
				}
			}

            //impossible 
            return -1;
        }

		/*
         * 這是照演算法說明的寫法，這才是高手
         * 
         * 演算法思路
         * 
         * 一。得先把 Leader 的 pdf 文件先看一下，知道人家用 stack 解題
         * 二。想法是這樣，利用原有的陣列就可以作到 stack 的類能力，因為其實你只關心誰住在 stack 的最上頭
         * 那個人就是當前 stack 前半擁有最多數量的人，而你需要一個計數器來決定它還可以被不同的數字抵消多少次
         * 所以你當前的候選人，就是 pos 所指，如果又遇到相同的值，那就是這個候選人的生命值加一
         * 如果是遇到不同的值，那它的生命就減一，如果減完為 0 ，那表示目前 所在的 i 位置 含以前的所有值
         * 都被中間的某些 Leader 候選值 抵消, 如果這個數最不存在真正的 Leader(過半數)，那不管你的候選人怎停都沒有意義
         * 但如果你的數列有 Leader，那表示你在拿掉的過程中，那候選人都必須是你的 Leader，為何呢??
         * 
         * 因為… 
         * 你每次拿掉的區，都存在一個數字，它佔該區的一半，而最後一區裡，會有一個數字至少「超過」一半
         * ，請注意，最後一區不會像是 ( 1, 2, 3) 這樣的數列，因為只要相異就要抵消，所以 1, 2 也會互相抵消，留下3
         * (1 ,1, 2) 才能留下來，所以在有 Leader 的情況下，
         * 
         *
        */

		public int Solution2(int[] A)
		{
            if (A.Length == 0)
			{
				return -1;
			}

			int candidate = 0;
			int count = 0;

            for (int i = 0; i < A.Length; i++)
			{
                if(count == 0)
                {
                    candidate = A[i];
                    count++;
                }
                else
                {
                    if(A[i] == candidate)
                    {
                        count++;
                    }
                    else
                    {
                        count--;
                    }

                }
			}

			count = 0;
            int pos = -1;
            for (int i = 0; i < A.Length; i ++)
			{
                if (A[i] == candidate)
				{
					count++;

                    if(pos == -1)
                    {
                        pos = i;
                    }
				}
			}

            if (count <= A.Length / 2)
			{
				return -1;
			}

            return pos;
		}
    }
}
