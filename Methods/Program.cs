using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Methods
{
    internal sealed class SomeClass
    {
        int a = 1;

        public SomeClass()
        {
            a = 2;
        }

        public override string ToString()
        {
            return a.ToString();
        }
    }
    internal struct Point
    {
        public Int32 m_x, m_y;

        //public Point()
        //{
        //    m_x = m_y = 5;
        //}
        public Point(Int32 x, Int32 y)
        {
            m_x = x;
            m_y = y;
        }
        public Point(Int32 x)
        {
            // экземпляру значимого типа this можно приписать значение нового экземпляра хначимого типа
            // в конструкторах ссылочного типа указатель this - только для чтения и присваивать значения ему нельзя
            this = new Point();
            m_x = x;
            m_y = 0;
        }

        public override string ToString()
        {
            return $"({m_x}, {m_y})";
        }
    }
    internal sealed class Rectangle
    {
        public Point m_topLeft, m_bottomRight;

        public Rectangle()
        {
            // В C# оператор new, использованный для создания экземпляра значимого
            // типа, вызывает конструктор для инициализации полей значимого типа
            m_topLeft = new Point(1, 2);
            m_bottomRight = new Point(100, 200);
        }
        public override string ToString()
        {
            return $"m_topLeft: {m_topLeft.ToString()} - bottomright: {m_bottomRight.ToString()}";
        }
    }
    internal sealed class SomeTypeRef
    {
        static Int32 s_x = 5;
        public override string ToString()
        {
            return $"s_x: {s_x.ToString()}";
        }
    }
    internal struct SomeTypeVal
    {
        private static Int32 s_x = 5;
        Int32 s_y;
        public override string ToString()
        {
            return $"s_x: {s_x.ToString()}";
        }
    }
    #region Extension Methods

    #endregion
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            //Rectangle rec = new Rectangle();
            //Console.WriteLine(rec.ToString());
            //Point poi = new Point();
            //Console.WriteLine(poi);

            //SomeTypeVal val = new SomeTypeVal();
            //SomeTypeRef reff = new SomeTypeRef();
            //Console.WriteLine(val.ToString());
            //Console.WriteLine(reff .ToString());
            //Console.ReadKey();
            StringBuilder sb = new StringBuilder("jehk*fwfklwebfk");
            var i = sb.Replace("*", "!").IndexOf('!');
            Console.WriteLine(i);
            "Grant".ShowItems();
            List<string> list = new List<string> {"Январь", "Февраль", "Март", "Апрель"};
            list.ShowItems();
            Action a = "Kost".ShowItems;
            a();
            Console.ReadKey();
        }
        //public static void ShowItems<T>(this IEnumerable<T> collection)
        //{
        //    foreach (var item in collection) Console.WriteLine(item); 
        //}
    }

    static class ClassUniExt
    {
        public static void ShowItems<T>(this IEnumerable<T> collection)
        {
            foreach (var item in collection)
                Console.WriteLine(item);
        }

        /// <summary>
        /// Returns индекс вхождения символа в строке
        /// </summary>
        public static Int32 IndexOf(this StringBuilder sb, char value)
        {
            for (int i = 0; i < sb.Length; i++)
                if (sb[i] == value) return i;
            return -1;

        }
    }
}