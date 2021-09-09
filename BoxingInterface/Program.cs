using System;

namespace BoxingInterface
{
    using System;

    internal struct Point
    {
        private Int32 m_x, m_y;

        public Point(Int32 x, Int32 y)
        {
            m_x = x;
            m_y = y;
        }

        public void Change(Int32 x, Int32 y)
        {
            m_x = x;
            m_y = y;
        }

        public override String ToString()
        {
            return String.Format("({0}, {1})", m_x.ToString(), m_y.ToString());
        }
    }
    internal interface IChangeBoxedPointP
    {
        void Change(int x, int y);
    }
    internal struct PointP : IChangeBoxedPointP
    {
        int m_x, m_y;

        public PointP(int x, int y)
        {
            m_x = x;
            m_y = y;
        }
        public void Change(int x, int y)
        {
            m_x = x;
            m_y = y;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}", m_x.ToString(), m_y.ToString());
        }
    }
    public sealed class Program
    {
        public static void Main()
        {
            Point p = new Point(1, 1);
            Console.WriteLine(p);
            p.Change(2, 2);
            Console.WriteLine(p);
            Object o = p;
            Console.WriteLine(o);
            ((Point)o).Change(3, 3);
            Console.WriteLine(o);
            /*
Обращаюсь к методу Change для изменения полей в упакованном объекте типа Point. 
Между тем Object (тип переменной o) ничего не «знает» о методе Change, так что сначала нужно привести o к Point. 

При таком приведении 
((Point)o).Change(3, 3);
типа o распаковывается, и поля упакованного объекта типа Point
(!!!!!) копируются во временный объект типа Point в стеке потока. Поля m_x и m_y этого
временного объекта устанавливаются равными 3, 
(!!!!!) но это обращение к Change невлияет на упакованный объект Point. 
При обращении к WriteLine снова выводится (2, 2). 
 */
            PointP p1 = new PointP(1, 1);
            Console.WriteLine(p1);

            p.Change(2,2);
            Console.WriteLine(p1);

            Object o1 = p1;
            Console.WriteLine(o1);

            // o1 распаковывается во временную значимую переменную типа Point, временная переменная изменяется на (3, 3)
            // на печать выводится неизменившаяся o1 (2,2)
            ((Point)o1).Change(3,3);
            Console.WriteLine(o1);

            // создается временный упакованный объект p1, кОторый изменяется методом Change(4,4) и удаляется после выполнения операции,
            // на перчать выводится неизменившийся  p1
            ((IChangeBoxedPointP)p1).Change(4,4);
            Console.WriteLine(p1);

            // при выполнении операции не производится упаковок/распоковок
            ((IChangeBoxedPointP)o1).Change(5,5);
            Console.WriteLine(o1);
        }
    }
}
