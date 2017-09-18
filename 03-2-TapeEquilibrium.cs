using System;
using System.Linq;


namespace Codility
{
    public class TapeEquilibrium
    {
		/*
            https://codility.com/programmers/lessons/3-time_complexity/tape_equilibrium/

            把陣列切兩半，|left sum - ritht sum | 要最小，只能 O(n)

            在寫這個說明時，我有在第一時間就反應到，如果我知道總和，那 left 可以累計 right 可以用總和減出來

            這樣掃一次而已…  ya~ 先前太笨了才寫了下面一堆
            
        */
		public int Solution(int[] A)
        {
            /*
             * 演算法思路
             * 
             * 一。註記一下，這是去訂櫃子的路上想到的…
             * 二。由於變異的正負很大值可能出現在任何角落，所以全掃一次還是很難避免
             * 三。如果每次移動位置就全加一次，演算法就會變成 O(n*n)，算和的行為是不斷重覆的
             * 算 位置5 是 1 + ..4   6 是 1 + ..5
             * 四。如果我發動幾次 O(n) 可以解決的話…我何不把所有的和先計算一次，正向和反向各一個O(n)
             * 位置5 是 前一個和 + 5 的值 ，一路下推…
             * 五。如果有這兩個和，我就可以掃一次每個位置的選擇所具有的「和差絕對值」，找到最好的答案
             * 
            */

            int[] forwardCount = new int[A.Length];
            int[] backwardCount = new int[A.Length];

            forwardCount[0] = A[0];
            backwardCount[A.Length - 1] = A[A.Length - 1];

            //預先計算累計值，兩個方向都算
            for (int i = 1; i < A.Length; i++)
            {
                forwardCount[i] = forwardCount[i - 1] + A[i];
            }

            for (int i = A.Length - 2; i >= 0; i--)
            {
                backwardCount[i] = backwardCount[i + 1] + A[i];
            }

            //從兩個方向中，找出最小絕對差值
            int minDiff = int.MaxValue;
            for (int i = 0; i < A.Length - 1; i++)
            {
                int temp = Math.Abs(forwardCount[i] - backwardCount[i + 1]);
                if(temp < minDiff)
                {
                    minDiff = temp;
                }
            }

            return minDiff;
        }
		/* 別人的答案實在太強大，這已是數學直覺了…
         *  int diff = A.Sum();

            int minDiff = int.MaxValue;
            for (int p = 0; p < A.Length - 1; p++)
            {
                diff -= A[p] * 2;
                minDiff = Math.Min(minDiff, Math.Abs(diff));
            }
            return minDiff;
        */

		/* 成績 35
		 * 
		 * 演算法思路
		 * 一。如果有兩個計算點，一個從前面加下去，一個從後來加回來
		 * 每次都貪心的選傷害比較少的一邊前進，理論上到中間時應該傷害可以不高
		 * 
		 * 二。很明顯，可以設計前半值比較大，後半值比較小，但兩者的差沒有很多，中間夾一個負很大的值，這會造成
		 * 程式一路前吃吃後吃吃，到中間才遇到負很大，選邊吃，但其實真正的答是盡可能把前半的正也分一些給後半來抵消負的成長
		 * 
         * if (firstPart > secondPart)
                {
                    postIndex--;
                    secondPart += A[postIndex];
                }else if (firstPart < secondPart)
                {
                    preIndex++;
                    firstPart += A[preIndex];
                }else 
                {
                    //遇到兩個和剛好相等時，貪心的選個增加少的來加入
                    if(A[preIndex+1] < A[postIndex-1])
                    {
                        preIndex++;
                        firstPart += A[preIndex];
                    }
                    else
                    {
                        postIndex--;
                        secondPart += A[postIndex];
                    }
                }
        */

		/* 成績 75
         * 
         * 演算法思路
         * 
         * 這只是上面方法的改良版，因為上面用和來看根本是錯的
         * 要看得是差值的絕對值，所以要實作
         * 這裡就想，每次偷算下一動，找個好的答案選
         * 可惜，只是修正的和的錯，但精神沒變，問題也還在…
         * 
         * 
         *  int firstPart = A[0];
            int secondPart = A[A.Length-1];

            //這兩個 index 都是站在已取過的位置上
            int preIndex = 0;
            int postIndex = A.Length - 1;

            //各自從兩頭從中間加，一直到兩個相遇
            //誰的和能保持差值小，誰就繼續加下去

            while (postIndex-preIndex > 1)
            {
                //先偷算
                int tmpFirst = firstPart + A[preIndex + 1];
                int tmpSecond = secondPart + A[postIndex - 1];

                //依試算結果來找出差值小的一方
                if (Math.Abs(tmpFirst - secondPart) < Math.Abs((firstPart - tmpSecond)))
                {
                    preIndex++;
                    firstPart += A[preIndex];
                }
                else
                {
                    //相等也先選後半段來移動
                    postIndex--;
                    secondPart += A[postIndex];
                }
            }

            return Math.Abs(firstPart - secondPart);
            
        */
	}
}
