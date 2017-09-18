using System;
using System.Collections.Generic;

namespace Codility
{
	/*
        https://codility.com/programmers/lessons/6-sorting/max_product_of_three/

        給一堆數字，任意挑三個算乘積，要找最大的

        好險我記得，排完後，只有頭尾的六個數字有影響力，怕就把這六個數字拿來組合
        看答案就知道，只有兩種情況，可以直殺
    */

	public class MaxProductOfThree
    {
        /*
         * 演算法思路
         * 一。一開始因為是排序類，所以會想到先排好再來處理
         * 二。想了一下覺得，最大值應該是排好後的前三與後三這數組成
         * 三。沒想到第一次想的太簡單，直接比前三與後三，都沒想過我可以只挑它們中間的任三
         * 所以這個北七版上去也是死，一堆錯
         * 四。然後寫好取前三後三送上去，答案是對了但效率0分
         * 結果發現算剩下的個數要取前後時取錯了… remander 最大只能設到 3 要限定的,結果我幾乎全送上去,改掉就過關
         * 但程式碼不好看…
         * 
        */

		public int Solution(int[] A)
        {
            List<int> sortA = new List<int>(A);

            sortA.Sort();

            //北七
            //return Math.Max( data[0] * data[1] * data[2] , data[data.Count - 1] * data[data.Count - 2] * data[data.Count - 3]);

            List<int> data = new List<int>();

            for (int i = 0; i < 3; i++)
            {
                data.Add(sortA[i]);
            }

            int remander = sortA.Count >= 6 ? 3 : sortA.Count - 3;
            for (int i = sortA.Count - remander ; i < sortA.Count; i ++ )
            {
                data.Add(sortA[i]);
            }

            int maxProduct = int.MinValue;

            for (int i = 0; i < data.Count - 2; i++)
            {
                for (int j = i + 1; j < data.Count - 1; j++)
                {
                    for (int k = j + 1; k < data.Count - 0; k ++)
                    {
                        int temp = data[i] * data[j] * data[k];

                        //Console.WriteLine("{0},{1},{2} = {3}", i, j, k, temp);

                        if(temp > maxProduct)
                        {
                            maxProduct = temp;
                        }
                    }
                }
            }

            return maxProduct;
        }


        /*
         * 很漂亮的解法
         * 首先，他勇敢的用 array 的 sort, 我看了一下 msdn 發現，它和 list 的一樣，在夠大時採 quicksort
         * 所以… 以後我也要用 XD
         * 再來，我怕前後取六個有沒比到的情況，所以還是來了個三層迴圈而且取六個還寫錯
         * 但再想一下發現，其實情況只有 前取二負後取一 或是後取三
         * 前取一後取二不可能，因為要有二個負一個正，那最後一個必正，倒數二必負，那到我這來全是負，而且負很大該選前而不是選後
         * 我真的很弱…
        */
        private int OtherAnser(int[] A)
        {
            Array.Sort(A);

            int firstTwoAndLast = A[0] * A[1] * A[A.Length - 1];
            int lastThree = A[A.Length - 1] * A[A.Length - 2] * A[A.Length - 3];

            return (int)Math.Max(firstTwoAndLast, lastThree);
        }

    }
}
