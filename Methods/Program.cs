using System;
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
        public Point(Int32 x, Int32 y)
        {
            m_x = x;
            m_y = y;
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            SomeClass cl = new SomeClass();
            Console.WriteLine(cl.ToString());
        }
    }
    
}
