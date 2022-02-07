//#define BOXING_EQUALS
//#define BOXING_GENERICS
//#define BOXING_UNBOXING
#define BOXING_EQUALS
#define INEFFICIENT
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
/*
Boxing:
 Для преобразования значимого типа в ссылочный служит упаковка (boxing)
 1. В управляемой куче выделяется память
 2. Поля значимого типа копируются в память, только что выделенную в куче
 3. Возвращается адрес объекта
Unboxing:
 1. Сначала извлекается адрес полей из упако­ванного объекта (непосредственно unboxing)
 2. Значения полей копируются из кучи в экземпляр значимого типа, находящийся в стеке
Упаковка применяется в слудующих случаях:
 • преобразование типа значения в объектную ссылку;
 • преобразование типа значения в ссылку на System.ValueType;
 • преобразование типа значения в ссылку на интерфейс, реализованный этим типом значения;
 • преобразование типа enum в ссылку на System.Еnum.
*/


namespace Boxing
{
    class Program
    {

        struct Point
        {
            public int x, y;
        }

        static void Main(string[] args)
        {
            #region Boxing equals
#if BOXING_EQUALS
            bool a = (object)1 == (object)1;
            bool a1 = (object)1 == (object)2;
            bool b = ((object)1).Equals((object)1);
            bool b1 = ((object)1).Equals((object)2);
            Console.WriteLine($"a = (object)1 == (object)1: {a}");
            Console.WriteLine($"a1 = (object)1 == (object)2: {a1}");
            Console.WriteLine($"b = ((object)1).Equals((object)1): {b}");
            Console.WriteLine($"b1 = ((object)1).Equals((object)2): {b1}");
#endif
            #endregion

            #region GenericNoBoxing & untypedBoxing
#if BOXING_GENERICS
            Stopwatch sw = new Stopwatch();
            const int num_iter = 50000000;
            Console.WriteLine($"Times for {num_iter} iterations:");
            sw.Start();
            ArrayList a = new ArrayList();
            Point p;                        // no heap
            for (int i = 0; i < num_iter; i++)
            {
                p.x = p.y = i;
                a.Add(p);
            }
            //PrintElements(a);
            Console.WriteLine($"With boxing in ArrayList:               {sw.Elapsed}");
            sw.Reset();
            sw.Start();
            List<Point> l = new List<Point>();
            for (int i = 0; i < num_iter; i++)
            {
                p.x = p.y = i;
                l.Add(p);
            }
            //PrintElements(a);
            Console.WriteLine($"Without boxing in generic List<>:       {sw.Elapsed}");
            sw.Reset();
            sw.Start();
            Dictionary<int, Point> d = new Dictionary<int, Point>();
            for (int i = 0; i < num_iter; i++)
            {
                p.x = p.y = i;
                d.Add(i, p);
            }
            //PrintElements(a);
            Console.WriteLine($"Without boxing in generic Dictionary<>: {sw.Elapsed}");

            void PrintElements(IEnumerable coll)
            {
                foreach (var el in coll) Console.Write($"{((Point)el).x}:{((Point)el).y} ");
                Console.WriteLine();
            }
            //Times for 50000000 iterations:
            //With boxing in ArrayList:               00:00:05.2309454
            //Without boxing in generic List<>:       00:00:01.1364339
            //Without boxing in generic Dictionary<>: 00:00:06.6429161

            //DE0006: Non-generic collections shouldn't be used: https://github.com/dotnet/platform-compat/blob/master/docs/DE0006.md
#endif
            #endregion

            #region Unboxing
#if BOXING_UNBOXING
            Int64 x = 100;
            Object o = x;
            Int32 y = (Int32)(Int64)o;

            // Изменение полей упакованной структуры с созданием нового упакованного объекта
            Point p;
            p.x = p.y = 1;
            Object o1 = p;      // boxing, o refer to packed instance of p;
            //p = (Point)o1;    // unboxing - unpack un copy to var in stack
            p.x = 2;            // change var in stack
            o1 = p;             // boxing var(struct) p to new packed instance in heap

            // Попытка изменения упакованной структуры без создания нового упакованного объекта - FAIL
            p.x = p.y = 3;
            object o3 = p;
            p.x = p.y = 4;
            Console.WriteLine($"p.x:{p.x}, o3.x:{((Point)o3).x}");
            // такая конструкция не работает, в левой части выражения должна быть переменная, а не unboxed область памяти
            //((Point)o3).x = 5; 
#endif
            #endregion
            //TODO Обобщения позволяют определить метод, принимающий любой значимый тип, не требуя при этом упаковки (см.главу 12)

            #region Boxing Manual
#if BOXING_MANUAL
            Int32 v = 5;
#if INEFFICIENT
            Stopwatch sw = new Stopwatch();
            sw.Start();
            // Происходит автоматическая упаковка три раза
            for (int i=0; i<100000; i++) Console.WriteLine("{0}, {1}, {2}", v, v, v);
            var sw1 = sw.Elapsed;
            sw.Reset();
#endif
            // Ручная упаковка один раз
            Object o = v;
            for (int i = 0; i < 100000; i++) Console.WriteLine("{0}, {1}, {2}", o, o, o);
            var sw2 = sw.Elapsed;
            Console.WriteLine($"1: {sw1}");
            Console.WriteLine($"2: {sw2}");
#endif

            #endregion
        }
    }
}
