using System;
using System.Globalization;

namespace _09_MainTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            String st = "Швилпо Константин Александрович";
            CultureInfo ci = CultureInfo.CurrentCulture;
            foreach (var ch in st)
            {
                var ch1 = char.ToUpper(ch, ci);
                Console.Write(ch1);
            }
            Console.WriteLine();
            if (st.ToUpperInvariant().Substring(10, 21).EndsWith("EXE"))
            { }
        }
    }
}
