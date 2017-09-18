using System;
using System.Collections.Generic;

namespace Codility
{
	/*
     * https://codility.com/programmers/lessons/7-stacks_and_queues/brackets/
     * 
     * 解括號對稱，經典題了
    */
	public class Brackets
    {
        /*
         * 演算法思路
         * 一。這個沒想法就該打，因為這學校有學，用 stack 來對應括號
         * 二。算法是，如果看見左括就 push ，看見右括就要能 pop 到一個對稱的, 由於沒有其它字元來亂
         * 所以在 pop 時要必中，然後把這個成對的括號移除，如果過程中，有任何對不上，那就是非法
         * 如果整個輸入完，但 stack 沒有清空，這樣也是非法
         * 三。送上去時，出來分數只有一半，結果發現有程式發生例外的問題
         * 後來想到，如果沒有先檢查stack 的長度就直接偷看或 pop 是會發生例外的…
         * 另外，在它的定義裡，空字串也是合法的，因為他有定完全沒括號的 vw，而且沒說 vw 是一定有字的
         * 四。程式碼還是醜，遇到這種比字元的真的沒招嗎
         *
        */

        public int Solution(string S)
        {
            Stack<char> sk = new Stack<char>();

            Func<char,bool> check = (char c) =>
            {
                if (sk.Count > 0 && sk.Peek() == c)
                {
                    sk.Pop();
                    return true;
                }
                return false;
            };

            for (int i = 0; i < S.Length; i++)
            {
                switch(S[i])
                {
                    case '{' :
                    case '[':
                    case '(':
                        sk.Push(S[i]);
                        break;
                    
                    case '}':
                        if(!check('{'))
                        {
                            return 0;
                        }
                        break;
                    case ']':
                        if(!check('['))
                        {
                            return 0;
                        }
                        break;
                    case ')':
                        if(!check('('))
                        {
                            return 0;
                        }
                        break;    

                    default:
                        //我假設所有非括號的字元都是字串的一部份，但輸入是說，沒有其它字元
                        //sk.Push((s[i]));
                        break;
                }
            }

            if(sk.Count > 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

		/* 可以省去多的檢查，早點離開，不過碼很醜
		 * 
            Stack<char> sk = new Stack<char>(); 

            for (int i = 0; i < s.Length; i++)
            {
                switch(s[i])
                {
                    case '{' :
                    case '[':
                    case '(':
                        sk.Push(s[i]);
                        break;
                    
                    case '}':
                        if(sk.Count > 0 && sk.Peek() == '{')
                        {
                            sk.Pop();
                        }
                        else
                        {
                            return 0;
                        }
                        break;
                    case ']':
                        if (sk.Count > 0 && sk.Peek() == '[')
                        {
                            sk.Pop();
                        }
                        else
                        {
                            return 0;
                        }
                        break;
                    case ')':
                        if (sk.Count > 0 && sk.Peek() == '(')
                        {
                            sk.Pop();
                        }
                        else
                        {
                            return 0;
                        }
                        break;    

                    default:
                        //我假設所有非括號的字元都是字串的一部份，但輸入是說，沒有其它字元
                        //sk.Push((s[i]));
                        break;
                }
            }

            if(sk.Count > 0)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        */
	}
}
