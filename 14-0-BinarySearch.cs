using System;
namespace Codility
{
    public class BinarySearch
    {
		/*
		 *
		 * https://codility.com/media/train/12-BinarySearch.pdf
		 * 
		 * binary searc O(logN)
                1 def binarySearch(A, x):
                2   n = len(A)
                3   beg = 0
                4   end = n - 1
                5   result = -1
                6   while (beg <= end):
                7       mid = (beg + end) / 2
                8       if (A[mid] <= x):
                9           beg = mid + 1
                10          result = mid
                11      else:
                12          end = mid - 1
                13 return result 
		 * 
		 * 只要資料是有序的，而且是能成功就某半都一定成功，把 beg end 設到另一頭，用這種方式逼出剛好成功的點
		 * while 迴圈只能在 beg end 未交會前才有效，交會後就天下大亂，盡可能把檢查 mid 寫成函式，這樣演算法才不會亂，移左邊還是右邊一定要搞清楚
		 * 
		 *  練習題
            Problem: You are given n binary values x0, x1, . . . , xn−1, such that xi ∈ {0, 1}. This array
            represents holes in a roof (1 is a hole). You are also given k boards of the same size. The goal
            is to choose the optimal (minimal) size of the boards that allows all the holes to be covered
            by boards.
            (它已經固定給你 k 版了，量不會變，但 size 不確定)
            
            Solution: The size of the boards can be found with a binary search. If size s is sufficient to
            cover all the holes, then we know that sizes s+ 1, s+ 2, . . . , n are also sufficient. On the other
            hand, if we know that x is not sufficient to cover all the holes, then sizes s − 1, s − 2, . . . , 1
            are also insufficient.
            (如果長度 s 可以剛好把洞都蓋，當然更長的板子也一樣可以蓋完，只是浪費了…)
            (短一點的板子，就會遇到不夠蓋住的問題，所以再短也不行，因此這題是找可以蓋住的最短板)

            板子最長就 n 一塊就可以打死，但它顯然很浪費，最小就 1 ，但你的限定數量只有 k 不夠你燒的
            所以只能在 1 ~ n 中找一個，最笨就是把所有的長度都試一次，但這是有了上面的特性，就不需要全試
  記住關鍵 -> 特性就是，你如果找到一個值是沒用的，它以下就全沒用，以上就全有用
            這個特性，讓你可以先猜個中值，如果它是有用的，那後面全有用不理，往前半找
            反之，它是沒用的，那前半全沒用，往後半找
            
            1 def boards(A, k):
            2   n = len(A)
            3   minS = 1
            4   maxS = n
            5   result = -1
            6   while (minS <= maxS):
            7       mid = ( minS + maxS) / 2
            8       if (computeNeedBoards(A, mid) <= k):  如果所需的板子數量比指定 k 小，那表示，你的板子太大塊了…
            9           maxS = mid - 1                    所以往小的 size 找
            10          result = mid                      mid 是一個暫時可以接受的 size
            11      else:
            12          minS = mid + 1                    反之，往大的方向找，mid 這個size 太燒板子數量，不適用
            13 return result                              最終總會逼到一個 size 是最合適的

            1 def computeNeedBoards(A, s):                其實就是把 s 長的板子試放一次，看看要多少塊板子
            2   n = len(A)
            3   boards = 0
            4   last = -1
            5   for i in xrange(n):
            6       if A[i] == 1 and last < i:
            7           boards += 1
            8           last = i + s - 1
            9   return boards
        */
	}
}
