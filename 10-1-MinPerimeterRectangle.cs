using System;
using System.Collections.Generic;

namespace Codility
{

	/*
        https://codility.com/programmers/lessons/10-prime_and_composite_numbers/min_perimeter_rectangle/

        給定一個 N 問你一堆面積為N的長方型，誰的週長最短

        這是數學問題，同面積下，正方形週長最短
        所以，目標是找所有的面積組合，找最接近平方根的那組，會是剛好平方根
        找 divisor 這時就派上了，每組 divisor 都是面積 N
    */
	public class MinPerimeterRectangle
    {

        /*
            演算法思路

            一。就是先看文件，學會和質數有關的事
            二。那個翻硬幣的題，很值得想一下
            三。其實找質數，就要要學會先找因數，然後再靠因數只有1和本身而決定自己是質數
            四。會找，剩下就只是找的過程，把那些看到的記下來，如果剛好平方根，要補處理，這樣比較好寫，可以明確決定是多一個還是兩個因數

        */

        private class Pair
        {
            public int X;
            public int Y;
        }

        public int Solution( int N )
        {
            List<Pair> divisors = new List<Pair>();
             
			//O(n ^ 1/2) 
			int i = 1;
			int divCount = 0;
			while (i * i < N)
			{
				if (N % i == 0)
				{
					divCount += 2;
                    divisors.Add(new Pair() { X = i, Y = N / i });
				}
				i++;
			}

			if (i * i == N)
			{
				divCount++;
                divisors.Add(new Pair() { X = i, Y = i });
			}


			//其實這裡可以勇敢的用最後一組也行
			//愈接近根號值的和就愈小
			int minPerimeter = int.MaxValue;
            foreach(Pair p in divisors)
            {
                int temp = (p.X + p.Y) * 2;

                if(temp < minPerimeter)
                {
                    minPerimeter = temp;
                }
            }

            return minPerimeter ;
        }
    }
}
