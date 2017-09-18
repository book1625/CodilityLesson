using System;
namespace Codility
{
    public class FrogJmp
    {
		/*
            https://codility.com/programmers/lessons/3-time_complexity/frog_jmp/

            青蛙要從 x 跳到 y, 而且是可以超過 y 的，當然有限制最多可以跳 d 步

        */

		public int Solution(int X, int Y, int D)
        {
            /*
             * 演算法思路
             * 一。別被題目嚇到了，其實在考英文
             * 二。可以不用跳的，為啥要跳
             * 三。就看跳幾次可以到，除在餘數那一下而已
             *
            */

            if( X == Y)
            {
                return 0;
            }

            int quor = (Y - X) / D;
            int rest = (Y - X) % D;

            if(rest > 0)
            {
                return quor + 1;
            }
            else
            {
                return quor;
            }
        }
    }
}
