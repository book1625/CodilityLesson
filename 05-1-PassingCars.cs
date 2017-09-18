using System;
namespace Codility
{
    public class PassingCars
    {/*
        https://codility.com/programmers/lessons/5-prefix_sums/passing_cars/

        陣列中的 0和1表示往東和往西，基於下面的交會定義，要算出陣列到底有多少次交會，要O(N)
        
        注意 !!!
        We say that a pair of cars (P, Q), where 0 ≤ P < Q < N, is passing 
        when P is traveling to the east and Q is traveling to the west.

        這裡很容易錯的是，要「前面」的車往東 對上 「後面」的車往西，才叫作交會，如果反過來，這兩台是永不交會的

        一路掃下去，每個點都知道目前有多少往東，每遇一個往西就統一次
        
    */
		public int Solution(int[] A)
        {
			/*
                演算法思路
                一。一列的 0 和 1 後面出現的 1 就會和前面所有的 0 交會
                所以只要一路往下找 1，並記下看過幾個 0，就可以知道會碰面幾次
                二。注意題目的界限值其實很大的，所以碰面次數是會加到暴的… 粗估 10萬*10萬 = 100 億(當然是沒那麼多)
                所以用 long 來計算會面次數
                三。題目有陷阱，它說超過 1000000000 就回 -1, 我一開始是沒看見，後來是寫 >=，可是人家題目說「超過」
                四。還有，不要自以為是的以 1 與基礎去找 0，理由是你英文不好
                人家說 0 往東 1 往西， 所以 0 先向東會碰的到往西的 1， 但如果是 往西的出發，他永遠也碰不到往東的…
                我一開始因為畫狀況圖，就把 0 開頭和 1 開頭視為兩種不同的狀況處理，反而沒有把題目看清楚
            */

			int baseCount = 0;
            long passCount = 0;  
            int maxBound = 1000000000;

            int baseValue = 0;

            for (int i = 0; i < A.Length; i++)
            {
                if(A[i] == baseValue)
                {
                    baseCount += 1;
                }
                else
                {
                    passCount += baseCount;                   
                }
            }

            if(passCount > maxBound)
            {
                return -1;
            }
            else
            {
                return (int)passCount;
            }
        }
    }
}
