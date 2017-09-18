using System;

using System.Linq;
using static System.Math;

namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/15-caterpillar_method/min_abs_sum_of_two/
     * 
     * 對你一堆數，叫你任意選兩個相加，可以選自己，找所有的相加中，絕對值最小的那一個絕對值
     * 
     * 想法，這個有記得，拿絕對值排序一次，這樣可以保證與我絕對值最相近的值，就住在我隔壁，能和我產生最大程度抵消的也是他們兩
     * 所以只要檢查 自己+自已  自己+前 自己+後 記下一個最小 所有人掃一次得到最小
     * 
     * 人家下面的解法更省了，因為2倍自己，只有最小絕對值的 A[0]有資格，其它人都比它大…
     * 所以 A[0] 一開始就納入了，然後每次就只看往後，因為每個元素的往前，都是前一個元素的往後
     * 太精簡了，自己再想一次才領略到下面幾行的藝術啊~~~
     * 
    */
	public class MinAbsSumOfTwo
    {
        /*
         * 看看人家高手寫的，就這麼幾行...
         * 這是原則上，就是在找一對數字，它們的絕對值差異不大，而且一正一負互消，或是值本身小的見鬼，乘二無所謂的
         * 所以，「如果我能夠照每個值的絕對值大小把數字重排一次，則每個元素相鄰的兩元素，必具有最接近的絕對值
         * 你在找和時，只要關心他的前後就可以了…
         * 這裡可能會有一個疑問，會不會我隔壁是比我絕對值多1 但和我同號，再隔壁是比我多2，但和我異號，我應該找他加…
         * 這不是問題，因為你隔壁的同號值，比你更具資格與它相加得到更小絕對值…
         * 
         * 這個想法真的太聰明了…
        */
        public int SolutionSmart(int[] A)
        {
            Array.Sort(A, new Comparison<int>((a1, a2) => Abs(a1).CompareTo(Abs(a2))));
            int result = Abs(2 * A[0]);
            for (int i = 1; i < A.Length; i++)
            {
                result = Min(result, Abs(A[i - 1] + A[i]));
            }
            return result;
        }

        /*
         * 演算法思路
         * 
         * 一。很明顯，要有效率的找，排序是免不了的
         * 二。排完後的數列，分幾種情況考慮，有沒有0，是否全正，全負，有正有負
         * 三。題目要看清楚，它是可以同一元素選兩次的，我一開始沒看到
         * 四。只要有0 那沒什麼好說的，選它兩次
         * 五。只要全正負，那就是頭選兩次
         * 六，再來就是有正有負，我的想法是，以最小正為right，最大負為left，開始試算
         * 從 兩者和絕值，個自乘二絕對值，選一個小的為候選值
         * 然後，誰離0比較近，誰就走遠一點，重新試算，直到有人不能走，就結束，找出候選值裡的最小
        */

        public int Solution(int[] A)
        {
            Array.Sort(A);

            int len = A.Length;

            //有0的都是回0
            if(A.Where(a=>a==0).Count() >= 1)
            {
                return 0;
            }

            //全正的
            if(A[0] >=0 && A[len-1] >=0)
            {
                return 2*A[0];
            }

            //全負的
            if(A[0] <=0 && A[len-1] <=0)
            {
                return 2 * Math.Abs(A[len - 1]);
            }

            int maxNeg = int.MinValue;
            int minPos = int.MaxValue;
            int left = 0;
            int right = 0;

            for (int i = 0; i < len; i++)
            {
                if(A[i] <0 && A[i] > maxNeg)
                {
                    maxNeg = A[i];
                    left = i;
                }

                if(A[i] > 0 && A[i] < minPos)
                {
                    minPos = A[i];
                    right = i;
                }
            }
                     

            int minSum = int.MaxValue;
            while (left >= 0 && right < A.Length)
            {
                int tempL = Math.Abs(A[left]);
                int tempR = Math.Abs(A[right]);

                if(tempL == tempR)
                {
                    return 0;
                }
                else
                {
                    if(Math.Abs(A[left] + A[right]) < minSum)
                    {
                        minSum = Math.Abs(A[left] + A[right]);
                    }

                    if(tempL * 2 < minSum)
                    {
                        minSum = tempL * 2;
                    }

                    if(tempR * 2 < minSum)
                    {
                        minSum = tempR * 2;
                    }

                    if(tempL > tempR)
                    {
                        right++;
                    }
                    else
                    {
                        left--;
                    }
                }
            }

            return minSum;
        }
    }
}
