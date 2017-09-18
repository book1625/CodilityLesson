using System;
namespace Codility
{
    public class FrogRiverOne
    {
		/*
            https://codility.com/programmers/lessons/4-counting_elements/frog_river_one/

            he frog can cross only when leaves appear at every position across the river from 1 to X
            這句話是重點，你要在最短的時間內湊到所有的葉子，要跳到 x 就要湊到  1 - x 的葉子
            只要 x  不爆掉，就有辦法開快取和統計差幾片
        */
		public int Solution(int X, int[] A)
        {
            /*
                演算法思路
                
                一。我需要一個陣列來記錄那個位置已經有葉子了
                二。我不能每次都去掃所有的葉子來確定我集滿了沒，所以在新葉子來的時候要增加計數
                三。看清楚題目，有用的葉子才是你該考慮的，而資料輸入的順序就是時間軸
                四。有可能跳不到，所以如果你沒處理就不會全對
            */

            bool[] leafCache = new bool[X];

            int currLeafCount = 0;

            for (int i = 0; i < A.Length; i++)
            {
                //葉子要是我們需要的範圍內才有用
                if (A[i] <= X)
                {
                    //確認葉子有沒有出現過，目標是湊到 X 片葉子
                    if (leafCache[A[i]-1] == false)
                    {
                        //新來的就增加計數
                        currLeafCount++;
                        leafCache[A[i]-1] = true;

                        //如果湊滿的話…就表示可以到達了
                        if (currLeafCount == X)
                        {
                            return i;
                        }
                    }
                }
            }

            //never jump
            return -1;
        }
    }
}
