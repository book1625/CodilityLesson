using System;
using System.Collections.Generic;

namespace Codility
{
	/*
        https://codility.com/programmers/lessons/6-sorting/number_of_disc_intersections/

        給你一堆數字，用索引定位置，用值定半徑，問你這堆圓，有多少個兩兩相交

        O(n*n)的話，拿所圓，去比所有圓可以達成，但是題目是 nlogn

        而且這是很賤，它的圓可以到 max int 大，這意味著你不能暴力的去統計每個位置有幾個圓，這個快取開不出來

        其實題目有說，有個「共同點」就叫有交集，所以相切也是
        We say that the J-th disc and K-th disc intersect 
        if J ≠ K and the J-th and K-th discs have at least one common point 
        (assuming that the discs contain their borders).
    */

	public class NumberOfDiscIntersections
    {
		/*
         * 演算法思路
         * 一。因為排序類，所以一開始也是往排序想，但到底排什麼卻是個問題
         * 因為，排中心或排半徑，都看不見對演算法有什麼幫助
         * 二。先想著如何判斷圓怎麼交集，試著看兩個圓交集時，他們之間因半徑而產生的上下界有什麼變化
         * 三。突然想到，如果圓照上下界去排，會發生什麼事，所以立馬畫出陣列來觀察一下
         * 四。都先照下界排，因為照兩個排很麻煩，也不確定有用
         * 五。突然發現，照下界排以後，如果我的圓的上界大過下一個圓的下界，表示有交集
         * 那就得再往下個圓的下界看去，直到我的上界沒辦法再交到任何圓的下界，就可以停了
         * 後面的圓的下界都離我更遠，可以不用考慮(這是真的省到的部份)
         * 五。實作後發現分數很低，偷看錯誤後發現有幾個問題
         *     相切也是叫有交集，雖然題目沒有講…
         *     他給的半徑很大，雖然一開始有注意到這個問題也開了 long ，但要確認中間所有過程不體有溢位的問題
         *     人家題目寫的很清楚，超過 10000000 回 -1，這也在提醒你，值會很大，要用個大傢伙在計數，也要判斷回傳，怎麼就是沒注意呢
         * 
         * 
         * 照下界排，我在雙回圈時，都會查沒多少個就停下來…，這也是變相的加速
           但我不認為這樣是 nlogn, 因為如果每個圓都超大，大到接近 n，那每一次都要查到最後一個圓
           這裡應該還有問題才對!! 我得再去看別人的解法，再寫一次 
        */

		private class Bounder
		{
			public long Lower;
			public long Upper;
		}

        public int Solution0( int[] A)
        {
            int MaxBounder = 10000000;

            List<Bounder> data = new List<Bounder>();

            for (long i = 0; i < A.Length; i++ )
            {
                data.Add( new Bounder() { Lower = i - A[i], Upper = i + A[i] });                
            }

            data.Sort((x, y) => x.Lower.CompareTo(y.Lower));
                      
            long pareCount = 0;

            for (int i = 0; i < data.Count - 1; i++)
            {
                for (int j = i + 1; j < data.Count; j++)
                {                   
                    if(data[i].Upper >= data[j].Lower)
                    {
                        pareCount++;

                        if(pareCount > MaxBounder)
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return (int)pareCount;
        }


		/*
         * 這才是真的的 golden solution!!
         * 
         * 它的想法是，如果把上下界各自排序，對於每個上界，我都可以計算出交到多少個下界
         * 由於排列的關系，當我的下界指標推動時，是不需要再回頭的，因為下個上界，肯定也全吃已經檢查過的下界了
         * 這是它能夠作到O(nLogn)以下的原因，它其實只作了 2 * O(n)
         *  
         * 真正最困難的是 count += pos - (i + 1) 到底代表了什麼，為何不是 pos 直接加
         * 
         * 首先， pos 直接加，也就是，拿每一個 a 圓去交所有可以交的圓，其中有個 b圓
         * 然後你在處理 b 圓時，因為也是對所有，所以你也「可能」看見 b 圓，而且只是「可能」
         * 因為又要看是大圓包小圓，還是單純相交… 但總之，如果直接加 pos 肯定是有問題的
         * 
         * 再來就要畫圖才能看有了，括號表示上下界
         * 
         *   
         *  c1(             c1)           
         *     
         *     c2(   c2)
         * 
         *        c3(               c3)
         *                     c4(               c4) 
         * --+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--
         * 
         * 上界排序  c2 c1 c3 c4
         * 下界排序  c1 c2 c3 c4
         * 
         * brute-forced 一次
         * 
         * c2 -> c1 c2 c3
         * c1 -> c1 c2 c3 
         * c3 -> c1 c2 c3 c4
         * c4 -> c1 c2 c3 c4
         * 
         * 可以觀察到，首先，自已一定會被選中，沒辦法，自己的下界最大也只能等於上界，所以自己一定重覆了
         * 以 c3 來看，c1 和 c2 一定出現在自己的名單裡，為何呢，因為它們倆的上界都比你小了，下界能跳的掉嗎??
         * 而它倆在處理時，也都有把 c3 算進去，所以在 c3 這裡，是重覆看見他們了
         * 同理 c4 的選單中 c1 c2 c3 也都出現了，這時可以推得一個重要的結論，就是所有上界比我小的圓加上我們己都會被重覆選上
         * 
         * 因此， pos (0~pos-1) 個可選圓，中間要扣掉重覆圓 i+1 個(0~i) 
         * 
         * 至於有沒有其它特例，我想不出來，人家演算法也都是對的
         * 
         * 我加總的點是在找完所有的有效圓後一次加，和網路上別人的解答不一樣，他們每找一個就加一次
         * 但我們都是對的，我推測是，他們每次只關心移動一圓所產生的有效圓，每次都多一點點
         * 而我是一口氣看，我認為我這樣的想法比較好理解，所以選擇這樣寫，每次都一直堆加，看起來就有種會爆的感覺
         * 
        */
		public int Solution(int[] A)
        {
            int len = A.Length;

            long[] upper = new long[len];
            long[] lower = new long[len];

            for (int i = 0; i < len; i++)
            {
                upper[i] = i + (long)A[i]; //特別注意!!! int 加法放入 long 是沒用的!!!
                lower[i] = i - (long)A[i];
            }

            Array.Sort(upper);
            Array.Sort(lower);

            long count = 0;
            int pos = 0;
            for (int i = 0; i < len; i++)
            {
                while(pos < len && upper[i] >= lower[pos])
                {
                    pos++;
                }
                //pos 會指向下一個要檢查的，也是已不符合當前有交集的
                //當前有交集的數量是 pos 個 (0~pos-1)
                //可以確認重覆的有 i+1 個 (0~i，包含自己交自己)
                count += pos - (i + 1);

                if(count > 10000000)
                {
                    return -1;
                }
            }

            return (int)count;
        }
    }
}
