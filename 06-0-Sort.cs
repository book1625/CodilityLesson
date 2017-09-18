using System;
using System.Collections.Generic;
namespace Codility
{
    public class Sort
    {
		/*
         * https://codility.com/media/train/4-Sorting.pdf
         * 
         * 這裡有提幾個 sort ，也應該都要自己寫一次，雖然我們解題用內建的 nLogn 用的很爽
         * 
        */

		

		//selectio sort

		//bobble sort

		//counting sort

		//merge sort

		//quick sort


		//自己幹個 HashTable，不要說一直用別人的，自己都不會寫
		public List<int>[] DemoHashTable(int[] A, int hashWide)
		{		
			List<int>[] hashTable = new List<int>[hashWide];
			for (int i = 0; i < hashWide; i++)
			{
				hashTable[i] = new List<int>();
			}
            			
			for (int i = 0; i < A.Length; i++)
			{
				int tmpIndex = Math.Abs(A[i] % hashWide);

				if (!hashTable[tmpIndex].Contains(A[i]))
				{
					//here is new value
					hashTable[tmpIndex].Add(A[i]);
				}
			}

            return hashTable;
		}

    }
}
