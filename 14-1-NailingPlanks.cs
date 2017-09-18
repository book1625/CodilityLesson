using System;
using System.Collections.Generic;
using System.Linq;

namespace Codility
{
	/*
	 * https://codility.com/programmers/lessons/14-binary_search_algorithm/nailing_planks/
	 * 
	 * 給你一堆板子，各自有佔位置，給你一堆釘子，每一隻都有指定的位置，你一隻隻打下來，打到那一隻可以完全打完
	 * 
	 * 回想，如果 brute-forced ，那就是一隻隻打，每隻去掃板子來釘，O(M*N)
	 * 
	 * 如果二元法，以 M 來切，先有 Log(M)，反正本來就是在猜一隻最佳的釘子，再少也不行
	 * 問題是每次猜一個位置，就要拿該位置前的釘子去釘版子，這樣還是 M*N，只是M小了些
	 * 要如何才能變 M+N，這時想到，要對版子要知道有沒有釘，如果可以速查就會是 O(N)
	 * 而中間有沒有釘，就想到 prefix sum 來建速查表，這時拿手上這把釘建表是 O(M)
	 * 所以這樣可以作到 O(M+N)
	 * ya ~~ 這是有想到，該最怕的是 prefix sum 用錯，因為它是頭尾釘都算數的
	 * 
    */
	public class NailingPlanks
    {
        /*
         * 演算法思路
         * 一。用二元法逼釘子的位置，基本上是必要的，這是 Log(M) 的基礎，看文件就知道
         * 二。問題在，一隻釘子打所有版本，其實很沒效率…
         * 我有考慮過，如果版子是照順序排，我可以檢查到某些版子就不查了…
         * 可是我看題目，沒有這個條件，所以我得假設它沒有順序
         * 把打過的版子拿掉，雖然可以少查，但演算法沒有降級，遇到萬個打不死版就沒救
         * 這次，版子和釘子都不少，沒有站在那一邊就賺到的可能…
         * 而且拿掉的代價也不見得少… 因此就一張張的查了… XD
         * 果然，答案對了，效率 0 分，只好再去看看有沒有線索可以用…
        */

        private class Board 
        {
            public int ST;
            public int ET;

            public bool IsNailed(int nail)
            {
                return nail >= ST && nail <= ET;
            }
        }

        /*
         * 演算法思路
         * 一。當需求變成，想要「檢查某段區是否有發生時」，這時就該聯想到 prefix sum 了
         * 二。拿誰來 sum ，拿有沒有發生來 sum，所以對釘子作 prefix sum ，這時要注意，題目是有定義釘子內的值範圍的
         * 三。拿板子來檢查時也要注意，兩個 prefix 的點相減，有一個點會被忽略，所以如果要包含，是要往前多拿一個點來減
         * 當然，這時就會有超陣列的危險
         * 四。寫這個演算法才發現自己有些例子根本就不合理，釘子的值太大了…
        */

        private bool CheckAllBoardNailed(int N, int[] C, List<Board> boards) 
        {
            int[] nailSum = new int[C.Length * 2 + 1];

            for (int i = 0; i <= N; i++)
            {
                nailSum[C[i]]++;
            }

            for (int i = 1; i < nailSum.Length; i++)
            {
                nailSum[i] += nailSum[i - 1]; 
            }

            int nailBoard = 0;
            foreach(var b in boards)
            {
                if(nailSum[b.ET] != nailSum[b.ST - 1]) //這裡要小心，要取前一個，還要怕越界
                {
                    nailBoard++;
                }
            }

            return nailBoard == boards.Count();
        }



        //這裡我有寫了一個 O(m+n)的寫法，假設板子是有序的
        //所以用 stack 去解，遇到不能吃的板子就放棄釘子，但答案是錯的
        //推論還是有無序的狀態存在
        private bool CheckAllBoardNailed0(int N, int[] C, List<Board> boards)
        {
            bool[] nailBoard = new bool[boards.Count()];

            //O(m*n)?
            for (int i = 0; i <= N; i++)
            {
                for (int bi = 0; bi < boards.Count(); bi++)
                {
                    Board b = boards[bi];

                    if(nailBoard[bi] == false && b.IsNailed(C[i]))
                    {
                        nailBoard[bi] = true;
                    }
                }
            }

            return nailBoard.Where(b => b == false).Count() == 0;
        }

        public int Solution(int[] A, int[] B, int[] C)
        {
			//建板子物件
			List<Board> boards = new List<Board>();
            for (int i = 0; i < A.Length; i++)
            {
                boards.Add(new Board(){ST = A[i], ET = B[i]});
            }
      
            //用二元法逼釘子的最佳位置
            int beg = 0;
            int end = C.Length - 1;
            int mid = -1;
            int result = -1;
            while(beg <= end)
            {
                mid = (beg + end) / 2; 

                if( CheckAllBoardNailed(mid, C, boards) )
                {
                    end = mid - 1;
                    result = mid + 1; // 我要的數目，不是索引
                }
                else
                {
                    beg = mid + 1;
                }
            }

            return result;
        }
    }
}
