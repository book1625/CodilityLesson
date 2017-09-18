using System;

using System.Collections.Generic;

namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/7-stacks_and_queues/fish/
     * 
     * 給你一排魚，從上游到下游，相遇時，大吃小，可以一路吃下去，問最後剩下多少魚
     * 
    */

	public class Fish
    {

        /*
         * 演算法思路
         * 一。這題應該是不難才對，有點是我自己寫到亂掉
         * 二。一開始當然是把魚畫出來，而且這次有考慮到方向不會再有分道的也在算
         * 三。當然 stack 也是要的，所以試著把魚畫到 stack 裡
         * 四。發現其實可以把魚的重量和方向集合成一個值，用一個 stack 就好了
         * 五。畫完會發現只要沒魚，或是往下走的魚，都是直接放不考慮
         * 六。如果要放時發現裡面的魚和我同方向，那也是直接放
         * 七。最關鍵的應該是，要放時，發現我往上，他往下時，這是就要比大小了
         * 八。如果比輸了，當然我也死了，不用 push 
         * 九。最關鍵是比勝了，除了把裡面的它拿掉以外，會有幾種情況要考慮
         *     裡面沒魚我直入
         *     裡面魚和我同向，我跟著住
         *     裡面的魚和我反向，再次比大小，我可能吃它或被吃
         *     而且上述的情況在成功吃掉別人時，又要再來一次，這幾乎是把個整個演算法再寫一次進來…
         * 一開始我這裡並沒有判斷那麼多可能，所以成績最多50分
         * 後來發現了，就卡在良心問題，到底要不要動到步進變數 i (我一開始 for loop)，畢竟 i-- 可以創造迴圈重來一次，自然有上面的功能
         * 最後，還是把它改成寫 while 的版本，但覺得也沒有很美…
         * 
         * 
        */
		public int Solution(int[] A, int[] B)
        {
            Stack<int> sk = new Stack<int>();

            int i = 0;
            while ( i < A.Length)
            {
                int temp = B[i] == 0 ? -A[i] : A[i];

                if(temp > 0)
                {
                    //向下魚都是直接放
                    sk.Push(temp);
                    i++;
                }
                else
                {
                    if(sk.Count > 0)
                    {
                        if(sk.Peek() < 0)
                        {
                            //同向魚，我負你負
                            sk.Push(temp);
                            i++;
                        }
                        else if (Math.Abs(temp) > Math.Abs(sk.Peek()))
						{
                            //正沖魚，我負你正，比重量，吃到庫存了
                            sk.Pop();

                            //這裡有幾個問題，沒吃別人，可能被吃…，太大隻一直往前再吃，最後才被吃
							//最後決定，這條魚得重來一次，一直到它被吃掉，或是遇到同向魚被push，或是沒魚直接 push
						}
                        else
                        {
                            //比不過，死魚一條
                            i++;
                        }
                    }
                    else
                    {
                        //沒魚可吃也是直接放
                        sk.Push(temp);
                        i++;
                    }
                }
            }

            return sk.Count;
        }
    }
}
