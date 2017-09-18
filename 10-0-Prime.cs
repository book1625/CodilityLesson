using System;
namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/10-prime_and_composite_numbers/min_perimeter_rectangle/
    */

	public class Prime
    {
        public static int GetDivisor(int n)
        {
            if (n <= 0)
            { 
                return 0; 
            }

            //O(n ^ 1/2) 
            int i = 1;
            int divCount = 0;
            long temp = 1;
            while( temp <= n )     
            {
                if(n % i == 0)
                {
                    divCount += 2;
                }
                i++;
                temp = (long)i * i; //這裡如果 i 太大就會爆 int，所以靠 long 來撐
                                    //而且這裡很要命的是光開 long 來存是沒有用的，兩個int只在 int內相乘，也是爆!!!!
                                    //這件事我至少死過五次，還是沒有記取教訓
			}

            return divCount;
        }

        public static bool IsPrime(int n)
        {
            //大於 1 而且只有兩個因數
            return n >= 2 && GetDivisor(n) == 2;
        }
    }
}
