using System;


namespace Boxing
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Boxing equals
            bool a = (object)1 == (object)1;
            bool a1 = (object)1 == (object)2;
            bool b = ((object)1).Equals((object)1);
            bool b1 = ((object)1).Equals((object)2);
            Console.WriteLine($"a = (object)1 == (object)1: {a}");
            Console.WriteLine($"a1 = (object)1 == (object)2: {a1}");
            Console.WriteLine($"b = ((object)1).Equals((object)1): {b}");
            Console.WriteLine($"b1 = ((object)1).Equals((object)2): {b1}");
            #endregion
        }
    }
}
