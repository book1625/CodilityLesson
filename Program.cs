using System;
using System.Collections.Generic;


namespace Codility
{
    public static class ExtendClass
    {
        public static string ToArrayString(this int[] inData)
        {
            return string.Format("[ {0} ]", string.Join(",", inData));
        }
    }

    class MainClass
    {

        public static void Main(string[] args)
        {
            DynamicProgramming.Test();

            var runCode = new NumberOfDiscIntersections();

            //11
            Console.WriteLine(runCode.Solution(new int[] { 1, 5, 2, 1, 4, 0 }));

            Console.WriteLine(runCode.Solution(new int[] { 1, 2147483647, 0 }));         
                     
        }
    }
}
