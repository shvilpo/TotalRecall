using System;
using System.Collections.Generic;
using System.IO;
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
            // переменные, передаваемые методу по ссылке, должны быть одного типа, объявленного в сигнатуре метода

            Console.WriteLine($"{SumPrefixP("prefffs", new int[]{1,2,3,4,5,6,7})}");

            var varAnon1 = new {Name = "Ann", BirthDate = new DateTime(1980, 12, 27)};
            //varAnon1.Name = "Ann2";
            Console.WriteLine($"{varAnon1.Name}, {varAnon1.BirthDate:dd.MM.yyyy}");
            //Console.WriteLine($"{varAnon1.ToString()}, {varAnon1.GetHashCode()}");
            string NName = "Kate";
            DateTime BBirtDate = DateTime.Now.Date;
            var varAnon2 = new {NName, BBirtDate};
            Console.WriteLine($"{varAnon1.ToString()}, {varAnon1.GetHashCode()}");
            Console.WriteLine($"{varAnon2.ToString()}, {varAnon2.GetHashCode()}");

            

            var AnonPool0 = new {Name = "Alex", Age = 23, Sex = Sex.Male};
            var AnonPool1 = new { Name = "Mila", Age = 22, Sex = Sex.Female }; 
            var AnonPool2 = new { Name = "Bob", Age = 27, Sex = Sex.Male }; 
            var AnonPool3 = new { Name = "Alex", Age = 23, Sex = Sex.Male };
            var AnonPool = new[] {AnonPool0, AnonPool1, AnonPool2, AnonPool3};
            Console.WriteLine($"before assignment:{AnonPool2 == AnonPool[2]}");
            AnonPool[2]= new { Name = "Bobby", Age = 27, Sex = Sex.Male };
            foreach (var anon in AnonPool) Console.WriteLine(anon);
            Console.WriteLine($"{AnonPool0}, {AnonPool1}, {AnonPool2}, {AnonPool3}");
            Console.WriteLine($"after assignment: {AnonPool2==AnonPool[2]}");

            //AnonPool[0].Name = "Alexa"; //error - properety on anonimous type is immutable
            // AnonPool[0]= new { Name1 = "Alex", Age = 23, Sex = Sex.Male }; //error - new anonimous type object is not some type
            //Console.WriteLine($"{varAnon2.NName}, {varAnon2.BBirtDate:dd.MM.yyyy}");
            //Console.WriteLine($"{AnonPool1==AnonPool4}, {AnonPool1.Equals(AnonPool4)}");
            String myDocuments =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var query =
                from pathname in Directory.GetFiles(myDocuments)
                let LastWriteTime = File.GetLastWriteTime(pathname)
                where LastWriteTime > (DateTime.Now - TimeSpan.FromDays(7))
                orderby LastWriteTime
                select new { Path = pathname, LastWriteTime };
            foreach (var file in query)
                Console.WriteLine("LastWriteTime={0}, Path={1}", file.LastWriteTime, file.Path);
            Console.ReadKey();
            
        }

        internal enum Sex
        {
            Male,
            Female
        };
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
