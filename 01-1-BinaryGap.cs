using System;
using System.Collections.Generic;


namespace Codility
{
	/*
        https://codility.com/programmers/lessons/1-iterations/binary_gap/

       binary gap 是連續的0，被1包圍，沒包的不算
       N  被二元化，所以是 log(N) 
    */

	public class BinaryGap
	{
		public int Solution(int N)
        {
			List<int> binData = new List<int>();
			int rest = 0;
			int quor = N;

            //translate N into binary format
			while (quor > 0)
			{
				rest = quor % 2;
				quor = quor / 2;

				binData.Add(rest);
			}


			/*
			 * 演算法思路
			 * 一。當然，先得轉二元陣列
			 * 二。一堆 0 夾在 1 之間，所以關鍵是 1 在那裡，有地址，數量只是用減的得到
			 * 三。把一堆的間距算一次，就得到最大間距
			 * 四。有陷井，一定要被1包圍的0才有算
			 * 
			 * 其實可以掃一次，看到 1 記起點，開始累計，看到1記終點，判斷累計，這樣不就不掃兩次
            */

            List<int> tarIndex = new List<int>();

            //search for all ones
			for (int i = 0; i < binData.Count; i++)
			{
				if (binData[i] == 1)
				{
					tarIndex.Add(i);
				}
			}

            //compute all ones distance (zero counts)
			int maxCount = 0;
			for (int i = 1; i < tarIndex.Count; i++)
			{
				int temp = tarIndex[i] - tarIndex[i - 1] - 1;

				if (temp > maxCount)
				{

					maxCount = temp;
				}
			}

			return maxCount;
		}
	}
}
