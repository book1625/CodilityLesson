using System;
namespace Codility
{
	/*
        https://codility.com/programmers/lessons/5-prefix_sums/min_avg_two_slice/

        指定任意的陣列連續區段，可以得到這個片段的平均值，要找出這個陣列中，具有「最小片段平均值」的片段起點，只能 O(N)

        這個問題很鬼，是要找片段的「起點」，再來，片段至少要兩個元素，不會在同一個位上，不然就可以作弊找 min 就好

        試想，如果是O(n*n) 那就是對每個元素都假裝它是起點，往後掃每個元素來算片段，可以找到最小平均值
        有沒有辦法對第二個  n 作手腳...

        到這裡我還是沒想起那個賺錢不超過三個人分，人多就沒利益了…的神奇道理，果然寫過忘記。

    */
	public class MinAvgTwoSlice
    {
		/*
         *  演算法思路
         * 一。一開始還是沒辦法想到 O(n) 的解法，所以想說還是寫 O(n*n)，至少先能有答案
         * 二。為了避免作合的成本，作出一個累計的轉換陣列來算值，減少統計次數
         * 
         * 很可惜，這個方法 答案全對，但效率只有20分，總成績只有 60
         * 
         * 而且從現實來看，轉換成累計陣列只是減少加法，但問題的根本沒有變，統計陣列只是原陣列的變型
         * 在上面找 P Q ，難度和在原陣列上是一樣的…
         * 
         * 後來看完人家的答案就噴了… 
         * 理由是，你只要考慮最長3個元素的片段就好了，這是數學問題…
         * 
         * 別人的說明是這樣的…
         * 
            ex. [-10, 3, 4, -20]

            avg(0,3) = -23 / 4 = -5.75 // entire array is -5.75 average
            avg(0,1) = -7 / 2 = -3.5 // subslice (0,1)
            avg(2,3) = -16 / 2 = -8 // subslice (2,3)

            Notice that (avg(0,1) + avg(2,3)) / 2 = avg(0,3)
            Therefore, if avg(0,1) != avg(2,3) then one of them must be smaller than the other.

            我是這麼解釋的…  4個元素的平均，其實等於 兩個兩元素的和平均，這個等號一定成立，那如果這三個元素都不相等
            則，兩個相加的元素，一定有一個總平均大，一個比總平均小，也就是說，答案只會在前面的相加區，而不會在後面的總平均
            這個法則，可以推定到 任意 n 個不定長的連續片段加總平均下得到 m ，則保證 m 不會大於 n 裡的任何一個
            那 超大區 = 幾個中區平均 ，而小的必在中區，每個中區又是幾個小區的片均，而小區又必能比中區，所以推得答案應該在最小區裡
            因為最小區是 2 ，但寫不出 3區的保證式(上面規則只能推到4區)，所以是把 2 與 3 都跑一次

            以上，得到 O(n) 解法，太神奇…
             * 
            */

		public int Solution(int[] A)
        {

			
			int[] forwardCount = new int[A.Length];

            //計算累計值快取
            forwardCount[0] = A[0];
            for (int i = 1; i < A.Length; i++)
            {
                forwardCount[i] = forwardCount[i - 1] + A[i];
            }


            double currMinAvg = double.MaxValue;
            int currPIndex = 0;

            for (int p = 0; p < A.Length - 1; p++)
            {             
                for (int q = p + 1; q <= A.Length ; q++)
                {
                    //magic trick !!!!! 
                    // p and q 最多差到 2，或是 q 也沒得作就不玩
                    //所以這迴圈，每 p 最多只跑兩個 q
                    if (q - p > 2 || q == A.Length)
                    { 
                        break; 
                    }

                    double temp = (forwardCount[q] - forwardCount[p] + A[p]) / (q - p + 1.0);

                    //利用 < 避開再次發生的記錄
                    if(temp < currMinAvg)
                    {
                        currMinAvg = temp;
                        currPIndex = p;

                        //for debug
                        //Console.WriteLine("P={0} Q={1} Min={2}", p, q, currMinAvg);
                    }
                }
            }

            return currPIndex;
        }
	}
}
