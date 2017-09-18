using System;
using System.Collections.Generic;
using System.Linq;
namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/13-fibonacci_numbers/ladder/
     * 
     * 給你一個指定長的樓梯，你一次只能走一步或二步，問你有多少種走上去的組合，答案要是除某個 2**p 的餘數
     * 梯子有 L 高，所以要在 O(L) 作完
     * 
     * 這題回想時有想到，也有記得要統一 mod 個數來確保放的下，也記得 mod 的相加 等於 先加再 mod 
     * 
    */
	public class Ladder
    {
        /*
         * 演算法思路
         * 一。這題基本上也不算是自己想出來的…，也是小瞄了解答
         * 二。主要的重點有兩個，你畫出 ladder 時會發現，那個值是有規律的，它就是個 fib... 
         * 但說真的，沒人講，想不到，不是這區的題，也想不到…
         * 三。它給你除那個 2**P ，就是因為你根本也算不了那麼大，除非可以先除
         * 這時又是數學問題了，因為他要的答案是餘數，所以這題才能運行，因為兩個數如果同除一個值得到餘數
         * 則兩數相加除該值的餘數，等於兩個數先除所得餘數相加… 利用這個手法，你可以把餘數一直限制在 int
         * 反正，你最後也只是要餘數而已… 
         * 四。題目最多會叫你 mod 2**30 ，所以你得留足夠多的值給它 mod ，所以我是留 2*30 來確保有值可以用
         * 兩個 2**30 相加也不會爆 int
        */
        public int[] Solution(int[] A, int[] B)
        {
            int maxInput = A.Max();

            int moder = (int)Math.Pow(2, 30);

            List<int> fib = new List<int>();
            fib.Add(1);
            fib.Add(1);

            for (int i = 2; i <= maxInput + 1; i++)
            {
                //由於除2**30，所以永逺相加不會超過 int
                fib.Add((fib[i - 2] + fib[i - 1]) % moder);
            }

            List<int> result = new List<int>();
            for (int i = 0; i < A.Length; i++)
            {
                result.Add(fib[A[i]] % (int)Math.Pow(2, B[i]));
            }

            return result.ToArray();
        }
    }
}
