using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
	/*
	 * https://codility.com/programmers/lessons/15-caterpillar_method/count_triangles/
	 * 
	 * 給你一堆數字，問你任意抽三個來組三角形，可以組多少個
	 * 這是和 6-2 算有沒有三角形，可以說是兄弟
	 * 
	 * 想法不易忘了，因為被他打的太痛…
	 * 三角形成立，中間所有值都成立，所以暴力法的第三層，可以推過的都算成立，不要回頭算
	 * 
     * 這題的解法，在教學文件裡就有講了
     * https://codility.com/media/train/13-CaterpillarMethod.pdf
    */
	public class CountTriangles
    {
       

        /*
         * 演算法思路
         * 一開始實在想不出O(n**2)的解法，所以只好硬幹 O(n**3)
         * 只是靠資料的 sort，我可以盡可能減少第三層的成本
         * 果然，答案是對的，但效率不足，總分72
         * 
         * 資料有序，就可以知道有用的元素是集中在中間，但難的是，中間元素在左右兩端各有部份可以成生三角形的元素
         * 這時要排除就變的困難，因為有些元素刪了，會造成另一邊的某些元素無效
         * 後來想一想，這個想法也不對，因為能成三角形的元素，可能自成兩三群，各自集中，這樣也沒辦法鎖定
         * 
         * 最終還是去看了別人的答案，而且一開始還一直認為自已的三層迴圈和別人是一樣的，這些讓我心情很沮喪
         * 我花了少時間，去改不要呼函式，不要呼屬性，換結構來排序，但就是沒有辦法動到演算法的主體，像個新手一樣…ß
         * 
         * 有一件事，先前我們有作過(6-2)，就是如果在一個有序的數列上，我們能框出一個範圍，則在這個範圍內，任意的中間值都可以與頭尾形成三角形
         * 這題我們的基礎演算法是每次都定義了一個頭與中間，去試所有的尾，所以是 N**3，但是如果套用上面的原理，對同一個尾而言，中間值基本上是都是任意的
         * 從下面的迴圈來看，如果限定某個 k 它在 j 等於某值時可以保證 j+1 ~ k 間的任何元素都可以現成 (i, j, j+1~k) 個三角型
         * 當下個 j 開始進行時，對上面這個結論而言，也不過就是在 k 之前，j+1 移到了 j+2，保證依然可以成立一堆三角形 (i, j+1, j+2~k)
         * 這個推論，讓我們發現，如果 K 已經往後推開，其實就不用回頭重試了，因為 j~k 間都已經獲得保證了，所以每個 j 都只是努力的把同一隻k指標往後推
         * 演算法的複雜度來自 i 的 O(n) 與 k 的 O(n) => O(n**2)
        */

        public int Solution(int[] A)
        {
            if(A.Length < 3)
            {
                return 0;
            }

            int[] data = A;

            Array.Sort(data);
            int len = data.Length;

            int count = 0;
            for (int i = 0; i < len -2; i++)
            {
				// version 2
				int k = i + 2;

                for (int j = i + 1; j < len - 1; j++)
                {
                    while ( k < len 
                            && data[i] + data[j] > data[k] 
					        //&& data[i] + data[k] > data[j]  因資料有序，這兩個檢查是多餘的
					        //&& data[j] + data[k] > data[i]
                          )
					{
                        k++;
                    }
                    count += k - j - 1;
                }
            }

            return count;
        }
    }
}
