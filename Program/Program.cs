using System;
public sealed class Program
{
    public Int32 GetFive() { return 5; }
    public static void Main()
    {
        Program p = null;
        Int32 x = p.GetFive(); // В C# выдается NullReferenceException
    }
}
