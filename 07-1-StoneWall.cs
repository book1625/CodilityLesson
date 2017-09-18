using System;
using System.Collections.Generic;

namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/7-stacks_and_queues/stone_wall/
     * 
     * 給你一堆高度來指定打造一排牆，每個位置的高度都有指定，要求你把牆造出來時，牆所有的石塊數最少
     * 比如說，連續同高的可以共同成一塊，或是一堆不同高的，下面可以共用一塊，總之「塊數」最少 
     * 
    */
	public class StoneWall
    {

        /*
         * 抄別人寫的策略，因為我真的想不到演算法

        Strategy:

        Stack is initiated with first height of array H. Necessary bricks are initiated with N.

        Traveling array H from index=1 to the end:
        -If height[i] is bigger than last value in stack -> height[i] is added to stack.

        -If height[i] is equal to last value in stack -> Necessary bricks decrease 1 unit.

        -If height[i] is smaller than last value in stack -> values in stack are extracted 

        until:
        +Found one smaller than height[i] in stack.
        +Found one equal to height[i] in stack: Necessary bricks decrease 1 unit.
        +Stack is empty: stack reinitiates with height[i]

        我的理解是
        一。如果什麼都沒作，就一個位置依指定高度立一塊石頭，那最多也就是 n 塊
        二。如果遇到同高度的，那就可以省一塊，因為相鄰的它們可以共組
        三。如果遇到不同高度的，雖然他們的下方可以共組一塊，但總塊數是沒有減少的
        四。到底是高組還是低組高，我的判定是你在組合時，是以能組合區的最低去組成一塊，所以應是低組高
        五。你得由後回頭往前組，因為還沒有讀到下一組數字前，你無法預期前面有什麼，但讀過的你都可以記得
        六。即然是低組高，那回頭只要是比我高的或是一樣高的，都是可以組的，但不能低組更低，所以你只能一路組到看見比你低就停手
        七。你可以畫圖出來看就知道，回組時，遇到原本比你高的區，你回組也沒辦法發生省石頭，就算它們是回組過的，也會在比你高的區保留下數量
           只有和你一樣高的，會因為與你合併而再次節省
        八。合併過的區就被以合併的高度視為一個區了，這也是為什麼在 stack 裡，會在合併時 pop 一堆，最後才把自已 push 進去作代表
        九。盡管看懂他在作什麼，我沒辦法證明這樣就可以是最小值

        */

        public int Solution(int[] H)
        {
            Stack<int> sk = new Stack<int>();

            int useBrick = H.Length;

            for (int i = 0; i < H.Length; i++)
            {
                if(sk.Count == 0)
                {
                    sk.Push(H[i]);                   
                }
                else if( sk.Peek() == H[i])
                {
                    useBrick--;
                }
                else if(sk.Peek() < H[i])
                {
                    sk.Push(H[i]);
                }
                else
                {
                    //this mean the new brick high is lower than privous 
                    while(sk.Count > 0 && sk.Peek() >= H[i] )
                    {
                        int temp = sk.Pop();

                        if (temp == H[i])
                        {
                            useBrick--;
                        }
                    }

                    sk.Push(H[i]);
                }
            }


            return useBrick;
        }


        /*
         * 下面這個是抄裡面的一份解答
         * 
         * 在過去的歷史中，在比我大就持續移除下，直到找到與我相同或比我小
         * 
         * 如果可以找到相同，那我就不會造成使用量的增加，如果沒辦法，那我還是造成了使用的增加，並列入歷史記錄
         * 
        */

        public int Solution2(int[] H)
        {
			var blocksCount = 0;
			var blocks = new Stack<int>();

			for (int i = 0, len = H.Length; i < len; i++)
			{
				var height = H[i];
				var block = 0;

				while (blocks.Count > 0)
				{
					block = blocks.Peek();
					if (block > height)
					{
						blocks.Pop();
					}
					else
					{
						break;
					}
				}

				if (block != height)
				{
					blocks.Push(height);
					blocksCount++;
				}
			}

			return blocksCount;
        }
    }
}
