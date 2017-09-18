using System;
namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/12-euclidean_algorithm/chocolates_by_numbers/
     * 
     * n巧克力排一圈，從 0 號開始吃，留下包裝紙，每次跳 m 個吃，如果遇到只剩包裝身就不給吃了，問你給定的 n m 你能吃幾個巧克力
     * 沒有規定 m and n 誰比較大哦~~
     * 
     * 這次的回想很有趣，發現得到一個答案和我原有的答案是等價的，但比較簡單
     * 
     * 我一樣畫圈，發現幾個 case 下來，答案都是 n / gcd ，結果我看以前我用 lcm / m 去作
     * 可是 …  n * m / gcd = lcm ，所以發現這兩個答案是等價的，但用 n / gcd 沒有溢位的危險  
     * 
    */
	public class ChocolatesByNumbers
    {
        /*
         * 演算法思路
         * 一。把圖實際畫一下，就會發現它會一直有規律的繞，如果剛好繞到某個點走過
         * 那走的次數應該是有義意的
         * 二。繞時也發現，一定會繞過或踏回起點，如果再繞下去，那次數也比 N 來的大
         * 三。如果想要再次的繞回原點，那表示次數應該是 M 的倍數的同時，也是 N 的倍數
         * 所以這時聯想，資數很可能與最小公倍數有關
         * 四。搞個最小公倍數來試試，發現答案可以用它除m來得到
         * 
         * 在測試的例子中，有測極值時測到 1, 100000000 ，這時發現答案不對
         * 所以開始測 2, 1000000000 ... 發現在我的演算法裡 1 是特例不能用除的得到
         * 
         * 另外，首次送上去時，在效能出錯，但錯誤訊息是答案錯了… 
         * 所以回頭看 code 發現，在求 lcm 時，我會把兩個 int 相乘，然後除完再丟回去
         * 但我沒考慮到 lcm 可能超過 int 可以表達的值，如果你用兩個很接現 int max 的質數
         * 那它的 lcm 就可以大到見鬼…，所以整個 lcm 其實要用 long 來處理才叫合理…
         * 還有先放大再乘，不要以為宣個大位置它就會自動放大了…它真的不理你
        */

        private int GCD(int a, int b)
		{
            //swap 寫了比較好被呼，外面不用管也沒差
            if(a < b)
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
				return GCD(b, a % b);
			}
		}

        private long LCM(int a, int b)
        {
			//最小公倍數是有公式的…
			//兩個接近 max 的 int ，最小公倍可能超過 int max
			//在相乘時就要先放大，不然乘出來也是錯的，宣個 long 來放也沒用
			long temp = (long)a * b;
            temp = temp / GCD(a, b);
            return temp;
        }

        public int Solution(int N, int M)
        {
            if(N == 1)
            {
                return 1;
            }

            long temp = LCM(M, N) / M;
            return (int)temp;
        }
    }
}
