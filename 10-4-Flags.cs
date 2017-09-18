using System;
namespace Codility
{
        /*
        https://codility.com/programmers/lessons/10-prime_and_composite_numbers/flags/

        就 peak 的定義不變，題目要你在 peak 上插旗，插的規則如下面的描述，如果你帶k隻旗，每隻旗的位置間距不得小於 K
        你要盡可能插更多的旗，O(n)就要作完

        Flags can only be set on peaks. What's more, if you take K flags, 
        then the distance between any two flags should be greater than or equal to K. 
        The distance between indices P and Q is the absolute value |P − Q|.

        先試想 brute-forced ， k 愈多，分割的塊就愈多，但距離就會相對變小，但它有規定不能比 k 小的
        所以你可以想像 k 其實最多就是平方根，這時有聞到因數的味道…
        對 k <= sqrt(N) ，掃點插旗(第一peak 一定插，再來就一超過k就插，能插完就成功)，能成功就表示找到
        這樣複雜度是 sqrt(N) * N 還不夠…

        我雖然成功的回想了，但忘記在頭尾插旗可以多一這件事，而且，sqrt(N)我認為和真的正O(1)應該有差的，
        只是這裡的根號值不大，而且答案應該接近值比較大的一邊，所以效率上才被接受

        這裡我選擇不再向下強化，因為有的時候，能寫出來比較重要，面試就是…

        */
	public class Flags
    {
		/*
         * 演算法思路
         * 一。找 peak 照抄
         * 二。數學問題，如果照他給的題目，有k隻旗插，兩旗相距至少k，那 k 很小時很好插
         * 所以 k 應該有個上界…  
         * 
         * 試想，如果有 k 隻旗，理論上把陣列分成 k + 1 塊，但運氣最好的是頭尾 peak 剛好在 1 and n-2 的位置
         * 那其實是分成 k - 1 份，每一份的元素是 N /(k-1)，然後這個值又要 >=k，在全均分的狀態下，有一塊多一點，就有一塊不合格
         * 所以可以得到  N/(k-1) >= k  -->  N >= k(k-1) 這樣可以觀察到，k 其實是在 n 的平方根附近
         * 
         * 所以一開始是用平方根取floor 的整數作為限界，開始試著插旗
         * 這裡要真的了解題意，它是說，任兩旗距離有限制，旗子要插完，但它沒有規要插的多平均
         * 所以，你一開始隨便插，只要插的點合法，他就不能說你錯，就算你開始多久就插完，也就是一開始說的，k小時很好插…
         * 所以我們要從大的k一路找回來，看看k能不能合法的插完
         * 一開始的第一座山當然是插，他只拿來和後面的山比，再來每座山只要能達到距離限制就立馬插下去，爭取盡早插完旗
         * 最後只要手上沒有旗就是全插上，是有效 k
         * 
         * 有一個要注意的地方是，我一開始插完的檢查寫在外圍，也就是每一組 k 我都照規距插完所有 peak ，所以我的旗可能早就插完變負的了
         * 但我檢查條件寫等於0 ，這件事在試 case 時有試到
         * 
         * 以上的演算法，可以拿到85分
         * 
         * 最的大問題在於，那個方根的限界…
         * 用 方根q 去分是把陣列分成完整 q 分，但實際上由於頭尾靠邊，所以是 q+1 隻旗
         * 這點在第一次寫時，有想過，又覺得好像不可能超過方根，結果送上去就錯了
         * 但如果寫時直接把限制多加一，又好像不知其所以然的感覺，雖然多1只要多跑一次沒差…但不喜歡不知道在寫啥…
         * 
        */

		public int Solution(int[] A)
        {
			//get peaks
			bool[] peaks = new bool[A.Length];
			for (int i = 1; i < A.Length - 1; i++)
			{
				if (A[i] > A[i - 1] && A[i] > A[i + 1])
				{
					peaks[i] = true;
				}
			}

            //以方根值為界
            int boundery = (int)Math.Floor(Math.Sqrt(A.Length)) + 1;

            for (int part = boundery; part > 0; part--)
            {
                //對每個可能的切割值進行試插旗的動作

                int preFlag = -400000;
                int restFlags = part;
                for (int i = 0; i < peaks.Length; i++)
                {
                    //如果我是 peak 而且 與前旗的距離>=part 表示又可以插旗了
                    if (peaks[i] == true && i - preFlag >= part)
                    {
                        preFlag = i;
                        restFlags--;

						if (restFlags == 0)
						{
							return part;
						}
                    }
                }
            }

            return 0;
        }
    }
}
