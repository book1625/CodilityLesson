using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/13-fibonacci_numbers/fib_frog/
     * 
     * 給你一堆水面上葉子的位置，叫你跳，只能選費式數列的值來跳，要從 -1 的位置跳到 N，問你可以跳最少次是幾次，只能 O(NlogN)
     * 
     * 回想，每次就照所有的 fib 看看有沒有能跳的，反正fib很少，有能跳的就記下來，下次查它，完成這一跳，如果剛好有人跳到 n 打完收工
     * 能打完收工時，就看跳了幾次，它就是最少次
     * ya ~ 成功回想 
     * 
    */
	public class FibFrog
    {

        private HashSet<int> fibonacciSet;

        private void initFibionacciSet(int n)
        {
            long preFib = 0;
            long currFib = 1;
            fibonacciSet = new HashSet<int>();

            while (currFib <= n)
            {   
                fibonacciSet.Add((int)currFib);
                long temp = preFib + currFib;
                preFib = currFib;
                currFib = temp;
            }
        }

		public bool IsFibonacci(int n)
		{
			return fibonacciSet.Contains(n);
		}

		/*
         * 演算法思路
         * 一。這個算法，答案對，但效率不好 總分只有 58
         * 二。想法是我得有一堆 fib 可以查，我在決定怎麼跳時要一直查的 
         * 三。理解題目到底在說啥，題目告訴你，它要照 fib 的數字跳，從 -1 跳到 n
         * 這裡第一個不確定的是，剛好到 n 嗎，由於題目有提到 position n，所以先假設要剛剛好到n 
         * 我在掃陣列時沒有什麼好想法，從題目的例子來看，也不是找大的 fib 就一定好，所以我打算先用 brute-force
         * 四。從位置 0 開始，進行第一輪跳，所有可以靠 fib 到達的葉子都列入下次的考慮不論遠近
         *     如果過程中有看到跳到最後一葉，也就是 position n ，則成功了，大家不要再跳了
         *     再來進行第二輪，對所有剛才的一跳所選的位置，檢查所有下次可以跳的葉子記下來，行為同上面的第一輪
         *     如果這個過程中，發現都沒有可以往下跳的位置，則葉子不好，跳不到
         *
        */

        public int Solution0(int[] A)
        {
          
            initFibionacciSet(A.Length+1);

            //加上 n position 讓演算法好寫點
			int[] temp = new int[A.Length + 1];
			Array.Copy(A, temp, A.Length);
			temp[temp.Length - 1] = 1;
			A = temp;


            //進行多輪的試跳
            int loop = 0;
            HashSet<int> currPosition = new HashSet<int>();
            currPosition.Add(-1);
            bool isGetTarget = false;

            do
            {
                loop++;
                var loopTargets = currPosition.ToArray();
                currPosition = new HashSet<int>();

                //對所有手上的當前位罝，試算出所有這一跳能用的位置
                foreach (var tar in loopTargets)
                {
                    for (int i = tar + 1; i < A.Length; i++)
                    {
                        
                        //有一片有效的葉子…
                        if ( A[i] == 1 && IsFibonacci(i - tar))
                        {
                            currPosition.Add(i);

                            //剛好跳到 n position，可以結束
                            if (i == A.Length - 1)
                            {
                                isGetTarget = true;
                                break;
                            }
                        }

                    }

                    if(isGetTarget)
                    {
                        break;
                    }
                }

            } while (isGetTarget == false && currPosition.Count() > 0);


            if(isGetTarget)
            {
                return loop;
            }
            else
            {
                return -1;
            }
        }

		/*
         * 演算法思路
         * 一。小看了人家的算法後發現，手段和我很像，就是一直找到所有可行的下一跳，直到中 n position
         * 二。我和他最大的差異，就是我笨死了…人家是用 fib 數列來檢查有沒有葉子，我是去掃所有的葉子，合不合 fib 
         * 這兩個的 n 差很大啊 !!!! fib 長到不超過 max int 也不過只有 4X 個, 而 n 動不動就幾萬個
         * 就改這麼幾行，效率就過關了… 
         * 
         * 其實最難想通的是那個每次大家跳一輪，看誰已經到終點，這件事我一開始就準備了…唉…
        */

		public int Solution(int[] A)
		{
			initFibionacciSet(A.Length + 1);

			//加上 n position 讓演算法好寫點
			int[] temp = new int[A.Length + 1];
			Array.Copy(A, temp, A.Length);
			temp[temp.Length - 1] = 1;
			A = temp;


			//進行多輪的試跳
			int loop = 0;
			HashSet<int> currPosition = new HashSet<int>();
			currPosition.Add(-1);
			bool isGetTarget = false;

			do
			{
				loop++;
				var loopTargets = currPosition.ToArray();
				currPosition = new HashSet<int>();

				//對所有手上的當前位罝，試算出所有這一跳能用的位置
				foreach (var tar in loopTargets)
				{
					foreach (int fib in fibonacciSet)
					{
						int nextLeaf = tar + fib;
						if (nextLeaf == A.Length - 1)
						{
							isGetTarget = true;
							break;
						}
						else if (nextLeaf < A.Length - 1 && A[nextLeaf] == 1)
						{
							currPosition.Add(nextLeaf);
						}
					}

					if (isGetTarget)
					{
						break;
					}
				}

			} while (isGetTarget == false && currPosition.Count() > 0);


			if (isGetTarget)
			{
				return loop;
			}
			else
			{
				return -1;
			}
		}
    }
}
