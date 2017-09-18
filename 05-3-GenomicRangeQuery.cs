using System;
using System.Collections.Generic;

namespace Codility
{
	/*
        https://codility.com/programmers/lessons/5-prefix_sums/genomic_range_query/

        將 DNA 中的 ACGT，轉成 1234 impact factor，給你一串 DNA 要你找出指定區段中，最小的 impact factor

        兇一點，就開四個 prefix sum 來記 ACGT 的出現變化，查的時候就查四次 (ya~~ 回想時有直接解出)
    */
	public class GenomicRangeQuery
    {
        /*
         * 演算法思路
         * 一。這一題基本上就算往 prefixSum 方向想，也很難想到手段，會一直在那笨笨的把 ACTG 加來加去
         * 二。我一開始是希望造出可以查 ACTG 的各查詢集合，想要建一次後可以問很快
         * 後來發現這樣的思路沒有跳出原本在 ACTG 上作業的框架* 
         * 三。然後，忍不住看了別人的解釋，看到所謂 right-most array …
         * 才發現，利用 prefixSum 出作 right-most array 後… 你可以靠「出現次數」去比對指定區塊頭尾間，有那些元素有變化
         * 這才是這個演算法的真締… 有了它，每組輸入都變成只要把 ACTG 在頭尾的差算一次來找出多了誰，然後記得比對P本身，因為相減會把 P 的存在給減掉
         * 這樣就可以達到 o(n+m)
         * 
         * 在寫的過程中，發現 ACTG 的原資料有點找麻煩，所以將它數字化，也用來作二維陣列索引，但這樣可讀性會往下降
         * 而且最後的資料也得再加回來才會是對的…
         * 
        */

		public int[] Solution(string S, int[] P, int[] Q)
        {
            int[] impactor = new int[S.Length];

			for (int i = 0; i < S.Length; i++)
			{
				switch (S[i])
				{
					case 'A':
                        impactor[i] = 0;
						break;
					case 'C':
                        impactor[i] = 1;
						break;
					case 'G':
                        impactor[i] = 2;
						break;
					case 'T':
                        impactor[i] = 3;
						break;
					default:
						//do nothing...
						break;
				}
			}

            int[,] prefixSum = new int[impactor.Length, 4];

            prefixSum[0, impactor[0]] = 1;

            for (int i = 1; i < impactor.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    prefixSum[i, j] += prefixSum[i - 1, j];
                }

                prefixSum[i, impactor[i]]++;
            }


            List<int> result = new List<int>();
            for (int index = 0; index < P.Length; index++)
            {
                int tempImpact = int.MaxValue;

                for (int i = 0; i < 4; i++)
                {
                    if(prefixSum[ Q[index], i ] - prefixSum[ P[index], i ] > 0 )
                    {
                        if (i < tempImpact)
                        {
                            tempImpact = i;
                        }
                    }
                }

				//this is the answer of a (P,Q)
				result.Add(Math.Min(tempImpact, impactor[P[index]]) + 1);			
            }

            return result.ToArray();

        }
    }
}
