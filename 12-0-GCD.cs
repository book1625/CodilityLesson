using System;
namespace Codility
{
    public class GCD
    {
        public static int GetBySub(int a, int b)
        {
            /* 
             * 如果理解輾轉相除法，就會發現減法只是除法的步驟分解
             * a 一直減 b 減到最後，會得到一個小於 b 的值，那其實就是 a % b 的餘數
            */
            if(a == b)
            {
                return a;
            }
            else if ( a > b )
            {
                return GetBySub(a - b, b);
            }
            else
            {
                return GetBySub(a, b - a);
            }
        }

        //呼叫者要保證 a >= b
        public static int GetByDiv(int a, int b)
        {
            /*
             * 輾轉相除法
            */

            if( a % b == 0)
            {
                return b;
            }
            else
            {
                //這裡由於 b%a 的餘數不可能大過 b ，所以可以保證
                //每次傳入時，前一個比較大
                return GetByDiv(b, a % b); 
            }
        }

		public static int GetByDiv2(int a, int b)
		{
			//swap 寫了比較好被呼，外面不用管也沒差
			if (a < b)
			{
				int temp = a;
				a = b;
				b = temp;
			}

			if (a % b == 0)
			{
				return b;
			}
			else
			{
				//這裡由於 b%a 的餘數不可能大過 b ，所以可以保證
				//每次傳入時，前一個比較大
				return GetByDiv2(b, a % b);
			}
		}

        public static int GetByBin(int a, int b, int res = 1 )
        { 
            /*
             * 這個演算法是基於
             * 一。如果兩者偶數，那把兩者都先除二降一級，再來看公因數，最終把降級乘回去就好
             * 二。如果只有單一偶數，那公因數也不可能有偶數，因為奇數的因數，只能是奇數…
             * 所以，相辦法把單一偶數降到變奇數再來作
             * 三。如果兩者都是奇數了，那就開始利用互減的方式找
            */

            if(a == b)
            {
                return a * res; 
            }
            else if ( a % 2 == 0 && b % 2 == 0)
            {
                return GetByBin(a >> 1, b >> 1, res * 2);
            }
            else if ( a % 2 == 0)
            {
                return GetByBin(a >> 1, b, res); 
            }
            else if (b % 2 == 0)
            {
                return GetByBin(a, b >> 1, res);
            }
            else if (a > b)
            {
                return GetByBin(a - b, b, res); 
            }
            else 
            {
                return GetByBin(a, b - a, res);
            }
        }

	    public static long GetLCM(int a, int b)
		{
			//最小公倍數是有公式的…
            //兩個接近 max 的 int ，最小公倍可能超過 int max
            //在相乘時就要先放大，不然乘出來也是錯的，宣個 long 來放也沒用
			long temp = (long)a * b;
            temp = temp / GetByBin(a, b);
			return temp;
		}



        //這裡假定不存在有 long 級的 lcm.....
        public static long GetLCM(int[] A)
        {
			if (A.Length == 2)
			{   
                return GetLCM(A[0], A[1]);
			}

            int[] lcmArray = A.Length % 2 == 0 ? new int[A.Length / 2] : new int[A.Length / 2 + 1];

            int i = 0;
            while(i + 1 < A.Length)
            {
                lcmArray[i] = (int)GetLCM(new int[] { A[i], A[i + 1] });
                i += 2;
            };

            if(A.Length % 2 != 0)
            {
                lcmArray[lcmArray.Length - 1] = A[A.Length - 1];
            }

            return GetLCM(lcmArray);
        }
    }
}
