using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
	/*
        https://codility.com/programmers/lessons/11-sieve_of_eratosthenes/count_non_divisible/

        題目給你一堆數，問你所有元素與其它元素的整除關系，其它元素有個多少元素可以整除你，重覆也要算
        回答一個陣列來表示這個關系

        我的回想答案與我原來的初版是一致的，我也清楚我沒有避開 O(n**2)

        再次看完答案後，我重新作了思路來幫助記憶
        一。所有不能整除我的數，如果代換成 全部 - 可以整除我的 這樣是否比較好作
        二。針對單一個我，檢查所有的 divisor 是我的值開根號就可以掃完，這題裡，值是有限定在 2N 
        所以我用 sqrt(N) 應該沒有超過原題的限制
        三。我要如何得知這些元素出現，總不能掃陣列，所以花 O(N) 建一張出現表，可以避免
        四。在 sqrt(N) 次下，如果整除就去查 除數 與 商數 的出現次數，它們就是可以除我的元素個數

        整個算法的複雜度 是 O(n) + 2sqrt(2N) => O(n)
        
    */
	public class CountNonDivisible
    {


        /*
         * 演算法思路
         * 一。只有四十分，雖然答案對了80，但效率很差
         * 二。想法是將資料排序，比我大的自然就不能除我
         * 比我小的，則如果是質數就只算非1到我的數量 加上比我大
         * 如果不是質數，就得一個個除，再加上比我大的
         * 
         * 請注意建立質數時的平方根條件，要有等號，不然在剛好是平方根時會被跳過
         * 修正這件事以後，可以得到答案正確但效率沒分…
         * 
        */
        public int[] Solution0(int[] A)
        {
            //建2n質數表
            int N = A.Length * 2;

            bool[] primeTable = new bool[N + 1];

            for (int i = 2; i < primeTable.Length; i++)
            {
                primeTable[i] = true;
            }

            for (int i = 2; i <= Math.Sqrt(N) ; i++)
            {
                if(primeTable[i] == true)
                {
                    int jumper = 2;
                    int currInd = i * jumper;
                    while(currInd < primeTable.Length)
                    {
                        primeTable[currInd] = false;

                        jumper++;
                        currInd = jumper * i;
                    }
                }
            }


            //排序資料
            List<int> data = new List<int>(A);
            data.Sort();

            List<int> result = new List<int>();

            for (int i = 0; i < A.Length; i++)
            {
                int target = A[i];

				//取得所有大過我的值的總數
				int biggerCount = data.Where(x => x > target).Count();


				int smallerCount = 0;
				if (primeTable[target] == true)
				{
                    smallerCount = data.Where(x => x < target && x != 1).Count();
				}
				else
				{
					//取得所有比我小，而且「不可以」整除我的值的總數
                    smallerCount = data.Where( x => x < target && target % x != 0).Count();
				}

                result.Add(smallerCount+ biggerCount);
            }
            return result.ToArray();

        }

        public int[] Solution(int[] A)
        {
            /*
             * 演算法思路
             * 一。這個解法是看別人的，真的很聰明，我開始被題目引去作質數就死一大半了
             * 二。他的想法很簡單，只要能快速查出現次數，他就不用一直掃 array 
             * 而且能整除的對像是被限制在平方根裡，所以原則上不會多到比掃陣列過份
             * 三。他就是掃一次所有可以整除自己的數，這些數都不是答案所以不要
             * 就拿全部把它們減掉就好了…
             * 
             * 真正要小心的是，你在取因數的過程會遇到剛好平方根相同，如果這時你再，就會多減一次
             * 
             * 基本上，演算法到這裡，答案已經正確了，但是，效率最後一題我卻死了…
             * 比對別人的算法，看起來也沒有什麼差別，只有一點是，我把求方根直接寫在 for loop 裡
             * 所以不信邪的改了一下再上去… 結果就過了…
             * 
             * 這裡的推論是，如果你用一個函式去放在迴圈變數上，它應該有某些動作要重新執行，除非有被編譯器最佳化
             * 我試過把它一接出來，但換成浮點數去接，效率一樣過關，所以問題不在浮點數的比較計算
             * 這些只是推論，沒有進一步測試
             *
             * 
            */

            int[] occurCount = new int[A.Length * 2 + 1 ];
            for (int i = 0; i < A.Length; i++)
            {
                occurCount[A[i]]++;
            }

            List<int> result = new List<int>();
            foreach (int inValue in A)
            {
                int tempCount = 0;
                int boundery = (int)Math.Sqrt(inValue);
                for (int i = 1; i <= boundery; i++)
                {
                    if (inValue % i == 0)
                    {
                        tempCount += occurCount[i];

                        //另一個商數不能是平方根(會算兩次)
                        int qour = inValue / i;
                        if (qour != i)
                        {
                            tempCount += occurCount[qour];
                        }
                    }
                }

                result.Add(A.Length - tempCount);
            }

            return result.ToArray();
        }
    }
}
