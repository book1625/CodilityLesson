using System;
using System.Collections.Generic;

namespace Codility
{
	/*
        https://codility.com/programmers/lessons/11-sieve_of_eratosthenes/count_semiprimes/

        semiprime 該元素可以由兩個質數乘出來，兩個不一定要相異，題目給你一堆範圍，問你每備範圍裡的 semiprime 有多少個

        我有成功的回想出算法，就是 prefix sum 半素表，然後利用建質數的過程，建半素
        但我忘了，半素超界問題
    */

	public class CountSemiprimes
    {
		/*
		* 演算法思路
		* 
		* 一。半質數的特色是，因數裡有質數，所以，我還是會用到質數的判斷
		* 
		* 二。半質數的判定，原則上和質數的產生過程很像，我們在建質數表時會拿質數去殺所有倍數，而這些被殺的數中
		* 如果另一個成對的因數也是質數，那這個數就是半質數，所以整個演算法抄質數生成的
		* 
		* 三。一開始，我很天真的直接在產生質數時，判定如果我的另一個因數也是質數(就是那個 jumper)
		* 那這個數就是半質數了，所以不止殺了它，也順便記下他
		* 
		* 但這樣作答案錯了… 因為我忘了，我的質數表現在正在生成，如果 jumper 超過我目前的有效範圍，則查到的也不是答案
		* 在沒有更好的想法下，我決定，先建質數表，再用一模一樣的流程，建半質數，這時查質數都是有效的
		* 
		* 然後，透過一次 prefixSum 的手法，可以計算各點的累計值，最後查表相減得到差值，就是我需要的個數
		* 
		* 但這樣又錯了…  錯在我對 prefixSum 不夠熟， prefixSum 是會計算兩個位置的變化沒錯
		* 但如果你是指定 prefix[m] - prefix[n] 則你得到的值，是從 m+1 ~ n 的所增個數
		* 但這一題是有一個狀況是 m 也是你要考量的，所以 m 的是或不是，會造成你的答案要不要+1
		* 這點先前在寫 prefixSum 是沒有注意到的，要特別小心
		* 
		* 還有，千萬不要隨便丟例外出去，因為它會造成分數很低
		* 就算你不知要丟什麼，也不要丟例外
		* 這一題，在無法判定，由於要回一個陣列，你也是要造成含0的陣列，不能直接丟空陣列
		* 這種事要特別小心
		*

	   */
		public int[] Solution(int N, int[] P, int[] Q)
        {
            int maxPrime = N;
			if (maxPrime <= 2)
			{
                return new int[]{0};
			}

            //
			bool[] primeTable = new bool[maxPrime + 1];
            bool[] semiTalbe = new bool[maxPrime + 1];
			for (int i = 2; i < primeTable.Length; i++)
			{
				primeTable[i] = true;
                semiTalbe[i] = false;
			}

			int bound = (int)Math.Floor(Math.Sqrt(primeTable.Length));
            for (int i = 2; i <= bound; i++)
            {
                if (primeTable[i] == true)
                {
                    int jumper = i;
                    int current = i * jumper;
                    while (current < primeTable.Length)
                    {
                        primeTable[current] = false;
						jumper++;
						current = i * jumper;
					}
				}
			}

			for (int i = 2; i <= bound; i++)
			{
				if (primeTable[i] == true)
				{
                    int jumper = i;
					int current = i * jumper;
					while (current < primeTable.Length)
					{
                        if(primeTable[jumper] == true)
                        {
                            semiTalbe[current] = true;
                        }

						jumper++;
						current = i * jumper;
					}
				}
			}

            int[] semiCount = new int[N+1];

            for (int i = 1; i < semiCount.Length; i++)
            {
                if(semiTalbe[i] == true)
                {
                    semiCount[i] = semiCount[i - 1] + 1;
                }
                else
                {
                    semiCount[i] = semiCount[i - 1];
                }
            }


            List<int> result = new List<int>();
            for (int i = 0; i < P.Length; i++)
            {
                //foreach P,Q 
                int temp = semiCount[Q[i]] - semiCount[P[i]];
                if (semiTalbe[P[i]] == true)
                {
                    result.Add(temp + 1);
                }
                else
                {
                    result.Add(temp);
                }
            }

            return result.ToArray();
        }
    }
}
