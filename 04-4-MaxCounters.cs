using System;
namespace Codility
{
    public class MaxCounters
    {
		/*
            https://codility.com/programmers/lessons/4-counting_elements/max_counters/

            連續的指令處理，增加 counter 或是 全員設最大，最後回傳整個 counter

            直觀照作是 O(N**2)，因為每次收到重置最大，就要全員改一次…
            除非拿掉這一動，不然成本是下不來的
        */

		public int[] Solution(int N, int[] A)
        {
            /*
             * 演算法思路
             * 
             * 一。當然，還是需要有個陣列去記一下當前的所有值
             * 二。收到計數時，要考慮它是 有效+1 或 全部重置 或 不理它
             * 三。理論上，照著流程寫完就可以拿個80分，但效率不夠
             * 四。主要的問題是，你順著每個 A, 當 A 是大量的重置時，你得一直全掃寫入最大 counter
             * 所以 若 A 是 m 個重置，你會變成  O(n*m)
             * 五。因此，關鍵是你能不能不要真的跑去寫…
             * 六。你可以發現，當重置時，關鍵是重置值是多少，它原本是多少都沒用，而且一定小於等於你的重置值
             * 所以偷步的想法是，當我真的要進去加它1時發現它比我的上次重置值小，那就表示很久沒來了，也一直沒重置它
             * 那你就把它重置再加上這次的+1就好
             * 七。所以，你得記得你上次用多少去重置的…
             * 八。最後，你當然要檢查一次全員，因為可能有人很久都沒有重置到
            */

			int[] counter = new int[N];
			int maxCount = 0;
            int preMax = 0;

            //想辦法留下「當前的最大值」但「不去真的重置」，是演算法高分的精神
			foreach (int i in A)
			{
				if (i >= 1 && i <= N)
				{
                    if (counter[i - 1] < preMax)
                    {
                        //在真正需要加 counter 時才把值也納進來
                        //反正重置後如果都沒來加過，則原位置的值必小於等重置值
                        //如果都沒來就有重置，那也是看最新一次重置值來決定
                        //但只要一次進來納入重置值後，它就一口氣變大，不再小於重置值，直到下次重置
                        counter[i - 1] = preMax + 1;
                    }
                    else
                    {
                        counter[i - 1] += 1;
                    }

					//keep temp max count
					if (counter[i - 1] > maxCount)
					{
						maxCount = counter[i - 1];
					}
				}
				else if (i == N + 1)
				{
                    preMax = maxCount;
				}
				else
				{
					//the question has no describe for it
				}
			}

            //把沒重置到的補上而已
            for (int i = 0; i < counter.Length; i++)
            {
                if(counter[i] < preMax)
                {
                    counter[i] = preMax;
                }
            }


			return counter;
        }

        /* 成績 88，被一直交插的大量 max 害 因為演算法的本質還是 O(m*n) 
         * int[] counter = new int[N];
            int maxCount = 0;

            bool maxCountInc = false;

            foreach(int i in A)
            {
                if(i >=1 && i <= N)
                {
                    //increase position i-1
                    counter[i - 1] += 1;

                    //keep temp max count
                    if(counter[i-1] > maxCount)
                    {
                        maxCount = counter[i - 1];
                        maxCountInc = true;
                    }
                }
                else if (i == N + 1)
                {
                    //this flag is for ingore duplicate all max commad 
                    if (maxCountInc)
                    {
                        //max all counter
                        for (int j = 0; j < N; j++)
                        {
                            counter[j] = maxCount;
                        }
                    }
                    else
                    {
                        maxCountInc = false;
                    }
                }
                else
                {
                    //the question has no describe for it
                }
            }

            return counter;
        */
	}
}
