using System;
namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/9-maximum_slice_problem/max_double_slice_sum/
     * 
    */
	public class MaxSliceSum
    {
		/*
         * Kadane's Algorithm (高手必背)
         * 
         * 一。這裡不要直接抄 pdf 上的演算法，出來也是錯的，因為他每次都用 0 去比，這樣很奇怪
         * 二。接下來所有的數字，都是陣列的索引，把前三個數字的可能組合畫出來
         * 
         * 表示法： [1 .. 3] 表示 索引1 加到 索引 3 的和
         * 
         * 開始列出所有可能
         * index 0 : [0 .. 0]
         *       1 : [0 .. 0] or [0 .. 1] or [1 .. 1]
         *       2 : [0 .. 0] or [0 .. 1] or [0 .. 2] or [1 .. 1] or [1 .. 2] or [2 .. 2]
         * 以此類推…
         * 
         * 如果你一直畫下去，你會發現，每次新元素進來時，你就要考量「前一個元素的所有範圍」加上 「所有新元素以前的元素與新元素的組合」
         * 
         * 所以前一個元素的所有範圍是
         * [0 .. 0] or [0 .. 1] or [1 .. 1] 
         * 
         * 新元素組合是
         * [0 .. 2] or [1 .. 2] or [2 .. 2]
         * 
         * 然後，我們先假設，前元素的範圍已經算過了，
         * 問題在於新元素的部份怎麼算…
         * 
         * 新元素      前元素的某一組合    新元素
         * [0 .. 2] = [0 .. 1]       + [2]       
         * [1 .. 2] = [1 .. 1]       + [2]
         * [2 .. 2] = [X]            + [2]
         * 
         * 新元素的部份，除了新元素本身外，它都是和前一個元素的所有範圍裡的內容，進行相加得到的
         * 所以 元素2 的「所有可能選項」改寫成
         * [0 .. 0] 
         * [0 .. 1] 
         * [1 .. 1] 
         * [0 .. 1] + [2 .. 2]           
         * [1 .. 1] + [2 .. 2]   
         * [2 .. 2]         
         * 
         * 所以，答案其實是 「曾經發生過的最大和」、「曾經發生過的最大和 + 新元素」、「新元素」
         * 這三個區塊在角逐的，所以只要記下曾經的最大和，每次在三個數字中選一個最大的重新更新，最後就可以得到
         * 
        */
		public int Solution2(int[] A)
        {
			long maxSlice = int.MinValue;
			long slice = maxSlice;

			foreach (var a in A)
			{
				slice = Math.Max(a, a + slice);
				maxSlice = Math.Max(maxSlice, slice);
			}

			return (int)maxSlice;
        }


        /*
         * 演算法思路：
         * 一。我在累加的過程中，假設 max slice 在中間，然後我一路累加過去
         * 二。我的累計總和會一直上上下下，但當我通過 max slice 的下界時，我增經發生過的最大總和就不能再有變化
         * 三。每個新元素加進總和時，你可以把總和看成 前段和 + max slice + 新元素，所以如果新元素可以增加過去的最大和，那它就該是 max slice 的一部份才對
         * 四。所以我試著找到一個最後一次可以「增大過去總和」的點，作為 max slice 的終點
         * 五。有了這個終點，我就可以試著反加回去，看看能加到的最大是多少，它就是我要的值
         * 
         * 
         * 這個算法，正確度只有87 ，總分只有76
         * (-2,-2,1) 答案錯, 因為我包含不到這一種最大 slice 就是最後一段元素的情況
         * 當這些元素沒辦法反轉長期累計下來的負總和時，它就沒辦法對抗早期殘留下來近於0的小負值
         * 以上列為例，曾經最大是 -2，而到了最後一個 1 時，它被中間的-2所拖累，沒辦法超過曾經最大值
         * 也就是，我的演算法假設在我已經跨過 max slice 區段，這時我找到的下界才有義意…
         * 如果我還沒脫離 max slice 之前，我的下界應該都是到最後一個元素才對，問題是，我怎麼知道我已經超過 max slice 了
         * 
         * 由於我不知道如何確認是否超過 max slice ，但我知道發動一次 O(n) 從最後面加回來，也是可以得到一個值
         * 如果我真的遇到沒有超過的例子，則後來這個和應該會大過我原本的和，果然實驗後也是如此，可以將成績拿到 100
         * 
         * 另外在 (-1, 0, -1) 的測試裡也發現，如果值是 0，會造成總值沒變大，而不考慮的話，那你選的 slice 在這個例子就會是錯的
         * 
         * 但這樣的解法比之 Kadane's Algorithm 實在醜多了
         * 
        */
		public int Solution(int[] A)
        {
			
			int makeMeBig = 0;
			long total = 0;
			long everMax = long.MinValue;

			for (int i = 0; i < A.Length; i++)
			{
				total += A[i];

				if (total >= everMax)
				{
					everMax = total;
					makeMeBig = i;
				}
			}

			//here i got the endPoint of maxSlice

			total = 0;
			everMax = long.MinValue;
			for (int i = makeMeBig; i >= 0; i--)
			{
				total += A[i];

				if (total > everMax)
				{
					everMax = total;
				}
			}

			total = 0;
            //everMax = long.MinValue;
            for (int i = A.Length - 1; i >= 0; i--)
			{
				total += A[i];

				if (total > everMax)
				{
					everMax = total;
				}
			}

			//by question, this value was bounded by int
			return (int)everMax;
        }
    }
}
