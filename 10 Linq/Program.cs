using System;
using System.Collections.Generic;
using System.Linq;


namespace _10_Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            var selected = new List<string>();
            var names = Names.GetNames().ToArray();
            var selectedn = from n in names
                            where n.ToUpper().StartsWith("A")
                            orderby n
                            select n;
            foreach (var s in selectedn) Console.Write($"{s}  ");
        }

    }
    internal static class Names {
        internal const int NUM_NAMES = 10000;
        
        internal static List<string> GetNames()
        {
            List<string> ret = new();
            Random rnd = new Random();
            for (int i = 0; i < NUM_NAMES; i++)
            {
                string st=string.Empty;
                int len = rnd.Next(10) + 4;
                for (int j = 0; j < len; j++)
                { 
                    char ch = (char)(65 + rnd.Next(25));
                    st += (j == 0) ? ch : char.ToLower(ch);
                }
                ret.Add(st);
                //Console.Write($"{st} ");
            }
            //Console.WriteLine($"");
            return ret;
        }
    }
}
