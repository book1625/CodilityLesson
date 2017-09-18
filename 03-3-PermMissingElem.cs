using System;
namespace Codility
{
    public class PermMissingElem
    {
		/*
            https://codility.com/programmers/lessons/3-time_complexity/perm_missing_elem/

            一個陣列裡，有n個元素，但卻包含了1..n+1 的值，所以很明顯，必少了一個數，要找出來，只能O(N)

            如果不在乎那個 O(1) 的空間使用，那就是開個陣列標記，題目給的值不大
            如果不要的話…不知道，沒有想
		 */

		public int Solution(int[] A)
        {
            /*
             * 演算法思路
             * 一。如果我能掃一次，並在自己的資料結構上標記誰有出現過，那最後我會知道誰不見了
             * 二。由於資料是正整數，所以剛好拿 array 來標記是可行的
             * 三。注意 N+1 的存在，為了程式好讀，array 的首位是沒有使用的，因為輸入是從 1 開始
             * 
            */
            
            //no data is meaning that we lost the 1
            if(A == null || A.Length == 0)
            {
                return 1;
            }

            //the zero position is useless，the last position is for n+1
            //however, this is O(n) of space
            bool[] status = new bool[A.Length+2];

            //mark all value if it exists...
            for (int i = 0; i < A.Length; i++)
            {
                status[A[i]] = true;
            }

            for (int i = 1; i < status.Length; i++)
            {
                if (status[i] == false)
                {
                    return i;
                }
            }

            //this is impossible...
            return -1;
        }
    }
}
