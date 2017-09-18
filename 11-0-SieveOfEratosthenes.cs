using System;
using System.Collections.Generic;
namespace Codility
{
    public class SieveOfEratosthenes
    {
		/*
         * https://codility.com/media/train/9-Sieve.pdf
         * 
         * Sieve of Eratosthenes 演算法的基本精神是
         * 一。如果你要找某個數是不是質數，你最多只要判斷到平方根就好，如果在平方根內沒有任何一個數可以整除你，那你基本上是質數
         * 所以反過來，如果你把陣列 x (x <= n)以內所有元素的倍數都標記為不是質數(都已經是倍數了還質什麼)，
         * 則你可以保證在 x 平方以內的陣列元素， 如果還沒有被標記到，那它基本上就是質數了
         * 
         * 二。所以，我們從 2 開始作，目標是往下走把每個見到的倍數都打掉，直到打到陣列長度的平方根個，來保證所有的陣列元素無需再打
         * 三。打的過程中，如果發現某個數k已經被打掉，那必然在先前處理過某個比k小的數字m，然後m的某倍數等於k
         * 在打掉 m 的過程中，其實就順手把 k 的倍數全打掉了，所以其實我們只要打掉活著的數字的倍數就好
         * 四。其實那些活著的，都一定是質數，因為他們都活在保證區段裡了…(如果你打到 x 則 保証區段是 x**2)
         * 
         * 以上演算法建立在有限的陣列裡，可以用 O( sqrt n * log log n ) 處理完
         * 
         * 文件上有給了一個改良，它的精神是，如果你現在處理到 x 你其實可以不用從 下一個 x 的倍數開始標，而且直接從 x**2 開始一路標下去
         * 就下面的演算法而言，jumper 不是設為 2 ，而是一次就 jump 到 i 次
         * 
         * 這個可以把每個數的關系畫出來，比較好理解，假設演算法目前正要處理 x ，所以能保證安全的是 (x-1)*(x-1)
         * 
         * .... x-1,  x, .........  (x-1)(x-1) ..... x*x
         * 
         * 我們假想處理 x 時所作的跳動，在快到接近 x*x 前是 x*(x-1) x*(x-2)
         * 我們可以發現  x*(x-2) = x**2 -2x 它是小於 (x-1)(x-1) 的，所以它其實被保證安全了
         * 有問題的，只剩下 x*(x-1)，但，你將它反過來看，它不就是 x-1 的 x 倍嗎，所以它剛才在 x-1 的處理，也必被修正過
         * 所以真正要處理，是從 x*x 開始的數字，因此可以一口氣直接 jump 到 x*x 開始就好
         * 
        */

		public static bool[] CreatePrimeTable(int maxPrime)
        {
            if(maxPrime < 2)
            {
                throw new ArgumentException();
            }

            bool[] primeTable = new bool[maxPrime+1];
            for (int i = 2; i < primeTable.Length;i++)
            {
                primeTable[i] = true;
            }

            int bound = (int)Math.Floor(Math.Sqrt(primeTable.Length));
            for (int i = 2; i <= bound ; i++)
            {
                if(primeTable[i] == true)
                {
                    int jumper = 2; //這裡其實可以直接寫 i 來加速
                    int current = i * jumper;
                    while(current < primeTable.Length)
                    {
                        primeTable[current] = false;
                        jumper++;
                        current = i * jumper;
                    }
                }
            }

            return primeTable;
        }

		/*
         * Factorization 質因數分解
         * 
         * 它的概念是利用上面的 sieve 手法，對每個元素標記最小的除數，別忘了，你在除時，拿的都是質數，而且從小開始拿
         * 所以，它可以在指定的 n 間每個元素，都標上一個可以除它的最小質數，再來這張表就可以讓你發動連續的除法
         * 從你本身 n 開始，查到一個最小質數可除，然後除了之後，得 m 再拿 m 去查表得另一個質數再除，一直到查表時沒有質數可以用
         * 表示手上這個數也是質數，這個過程你可以得到所有的質因數使用次數(除幾次得幾個)
         *
         * 我認為這個演算法最大的問題在空間，當 N 很大時，那個計算 factor 的陣列就會的大到開不出來
         * 如果不怕時間複雜度，那還是 brute-forced 比較有機會算出答案
         * 先找出 divisor，打掉 2 和 3 的倍數，剩下的一一判定質數
         * 然後得到一個純質數的集合，由小到大，每個都拿來除到不能除，得到質因數分解
        */

        public static int[] Factorize(int N)
        {
            int[] factor = new int[N + 1];

            int i = 2;
            while(i*i <= N)
            {
                if (factor[i] == 0)
                {
                    int curr = i * i;
                    while (curr < factor.Length)
                    {
                        factor[curr] = i;
                        curr += i;
                    }
                }
                i++;
            }

            List<int> primes = new List<int>();
            int temp = N;
            while (factor[temp] > 0)
            {
                primes.Add(factor[temp]);
                temp = temp / factor[temp];
            }
            primes.Add(temp);
            primes.Sort();
            return primes.ToArray();
        }
	}


}
