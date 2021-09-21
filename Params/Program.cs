using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Params
{
    public sealed class Program
    {
        private static Int32 s_n = 0;

        private static void M(Int32 x = 9, String s = "A", DateTime dt = default(DateTime), Guid guid = new Guid())
        {
            Console.WriteLine("x={0}, s={1}, dt={2}, guid={3}", x, s, dt, guid);
        }

        private static void ShowType<T>(T t)
        {
            Console.WriteLine(typeof(T));
        }

        public static void Main()
        {
            //M();
            //M(8, "X");
            //M(5, guid: Guid.NewGuid(), dt: DateTime.Now);
            //M(s_n++, s_n++.ToString());
            //M(s: (s_n++).ToString(), x: s_n++);

            //var Name = "Kost"; ShowType(Name);
            //var x = (string)null; ShowType(x);
            //var nums = new Int32[] {1, 2, 3, 4, 5}; ShowType(nums);
            //var collection = new Dictionary<String, Single>() { { "Grant", 4.0f } }; ShowType(collection);
            //foreach (var item in collection)
            //{
            //    ShowType(item);
            //}
            Int32 x;
            GetValOut(out x);
            Console.WriteLine(x);
            Int32 y;
            y = 5;
            GetValRef(ref y);
            Console.WriteLine(y);

            string s1 = "str1", s2 = "str2";
            SwapObjsVal(s1, s2);
            Console.WriteLine($"{s1}:{s2}");
            s1 = "str1"; s2 = "str2";
            SwapObjsRef(ref s1, ref s2); 
            Console.WriteLine($"{s1}:{s2}");
            s1 = "str1"; s2 = "str2";
            SwapTRef(ref s1, ref s2);
            Console.WriteLine($"{s1}:{s2}");
            int i1 = 1, i2 = 2;
            SwapTRef(ref i1, ref i2);
            Console.WriteLine($"{i1}:{i2}");
            
            Console.WriteLine($"{SumPrefixP("prefffs", new int[]{1,2,3,4,5,6,7})}");
            Console.ReadKey();
            // переменные, передаваемые методу по ссылке, должны быть одного типа, объявленного в сигнатуре метода.
        }

        private static void GetValOut(out Int32 v) => v = 10;
        //private static void GetValOut(out Int32 v) => v += 10;
        private static void GetValRef(ref Int32 v) => v += 10;
        public static void SwapObjsRef(ref string obj1, ref string obj2)
        {
            string obj = obj1;
            obj1 = obj2;
            obj2 = obj;
        }
        public static void SwapObjsVal(string obj1, string obj2)
        {
            string obj = obj1;
            obj1 = obj2;
            obj2 = obj;
        }
        public static void SwapTRef<T>(ref T a, ref T b)
        {
            T t = b;
            b = a;
            a = t;
        }

        public static int SumP(params int[] values)
        {
            int ret = 0;
            foreach (var t in values) ret += t;
            return ret;
        }
        public static string SumPrefixP(string prefix, params int[] values)
        {
            int sum = 0;
            foreach (var t in values) sum += t;
            return string.Concat(prefix, " ", sum.ToString());
        }

    }
}
