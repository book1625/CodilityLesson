using System;
using System.Collections.Generic;

namespace Codility
{
    public class DynamicProgramming
    {
        /*
         * https://codility.com/media/train/15-DynamicProgramming.pdf
         * 
         * Dynamic programming is a method by which a solution is determined based on 
         * solving successively similar but smaller problems.
         * 
         * 文件中，用「換零錢問題」為例子，給了 dp 解法
         * 
         * 它設計了一個二維 dp 表，用來表示 總和 與 不同元素集合 間需使用的最小硬幣數
         * 先把它畫出來會比較好觀察… 總和0 當然不用硬幣，沒硬幣它設無限是為了好算先不管
         * 所有用 1 去組的總和，個數就是總和，這也沒問題，再來…
         * 
         * 總和1 你只能用 1 也沒問題
         * 總和2呢?? 由於新元素都沒法湊2，所以都是用1湊的答案往下延用
         * 總和3呢?? 由於新元素3可以湊3，所以總數由3 降到 1，新元素4幫不上忙
         * 總和4呢?? 四個1，3可以湊4變2個，4可以湊4，變1個
         * …
         * 你會發現總和 m，在新元素 x 的出現時，
         * 如果新元素可以納入總和，造成你會需要在有 x 下組 m-x 的結果+1 (你決定用它x) 或 m 在無 x 下的結果 (你決定不用x) 選一個小的
         * 如果新元素無法納入(它的面值都比總和大了)，那就放棄，直接延用原本沒有這個面值的結果(m在無x下)
         * 有了這個發現，動態規劃就成立了，因為只要完成這張表，你就有答案了…
         * 
         * 問題是，這張表怎麼想出來的…
         * 首先，我們需要的答案是，最少要幾個硬幣，所以如果有張表，格子就是最少有幾個，不是很完美
         * 那這張表的索引是什麼，當然，是總和，這是題目要問你，叫你查表的參數
         * 但這樣，只是把題目表格化，格子怎麼填還是沒個譜啊??
         * 這時就得考慮，是否加入其它的干擾了，以這題而言，如果硬幣只有1元，不就很好算，但隨著硬幣變多，組合就變的多
         * 所以，如果我可以用少一點的硬幣去去推答案呢?? 能不能在每次有新硬幣就考慮一下能不能使用過去的資料來推得
         * 這時才試畫這張表，作上面的推論… 但說實在的，想到硬幣集合變化不太容易
         * 
         * 所以我發現，dp 題目的特徵
         * 一。你得先找到，「當前」可以是從「先前」配上某些事來得到，然後它一定可以被制成表來查
         * 二。如果先前的因素不只一個，比如這題，有總和與硬幣面值兩個因素，那你的表就至少要二維
         *     所以，你可以發現，能符合上面描述一的因素，不只一個，這時 dp 的可能性大增(因為不 dp，複雜度會大到見鬼)
         * 
        */

        public static void Test()
        {
            Console.WriteLine("{0}", ChangeCoin(new int[] { 1, 3, 4 }, 9));
        }


        private static int ChangeCoin(int[] coins, int n)
        {
            int[,] dp = new int[n + 1, coins.Length];

            Action debug = () =>
            {
                for (int i = 0; i < n + 1; i++)
                {
                    Console.WriteLine("Sum = {0}", i);
                    for (int j = 0; j < coins.Length; j++)
                    {
                        Console.Write("{0},", dp[i, j]);
                    }
                    Console.WriteLine();
                }
            };

            for (int i = 0; i < coins.Length; i++)
            {
                dp[0, i] = 0;
            }

            for (int i = 1; i < n + 1; i++)
            {
                dp[i, 0] = i;
            }

            //debug();

            for (int i = 1; i < n + 1; i++)
            {
                for (int j = 1; j < coins.Length; j++)
                {
                    int coin = coins[j];
                    int sum = i;

                    if (coin > sum)
                    {
                        dp[i, j] = dp[i, j - 1];
                    }
                    else
                    {
                        int useCoin = dp[i - coin,j] + 1;
                        int noUse = dp[i, j - 1];
                        dp[i, j] = (int)Math.Min(useCoin, noUse);
                    }
                }
            }

            //debug();

            return dp[n, coins.Length - 1];
        }
    }
}
