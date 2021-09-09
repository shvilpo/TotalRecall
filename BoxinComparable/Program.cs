using System;

namespace BoxinComparable
{
    class Program
    {
        internal struct Point : IComparable
        {
            int m_x, m_y;

            public Point(int x, int y)
            {
                m_x = x;
                m_y = y;
            }

            public override string ToString()
            {   // исппользование mx_x.ToString() и mx_y.ToString() предотвращает упаковку
                return string.Format("{0}, {1}", m_x.ToString(), m_y.ToString());
            }

            public int CompareTo(object? obj)
            {
                if (GetType() != obj.GetType())
                {
                    throw new ArgumentException("obj is not Point");
                }
                return CompareTo((Point)obj);
            }
            
            // implementation of CompareTo() inherited from IComparable
            public int CompareTo(Point other)
            {
                return Math.Sign(
                    Math.Sqrt(m_x * m_x + m_y * m_y) -
                    Math.Sqrt(other.m_x * other.m_x + other.m_y * other.m_y));
            }
        }
        static void Main(string[] args)
        {
            Point p1 = new Point(10, 10);
            Point p2 = new Point(20, 20);

            // p1 НЕ пакуется для вызова ToString (виртуальный метод)
            Console.WriteLine(p1.ToString()); // "(10, 10)"
            // p1 ПАКУЕТСЯ для вызова GetType (невиртуальный метод)
            Console.WriteLine(p1.GetType()); // "Point"
            // p1 НЕ пакуется для вызова CompareTo
            // p2 НЕ пакуется, потому что вызван CompareTo(Point)
            Console.WriteLine(p1.CompareTo(p2)); // "-1"
            // p1 ПАКУЕТСЯ, а ссылка размещается в c
            IComparable c = p1;
            Console.WriteLine(c.GetType()); // "Point"
            // p1 НЕ пакуется для вызова CompareTo
            // Поскольку в CompareTo не передается переменная Point,
            // вызывается CompareTo(Object), которому нужна ссылка
            // на упакованный Point
            // c НЕ пакуется, потому что уже ссылается на упакованный Point
            Console.WriteLine(p1.CompareTo(c)); // "0"
            // c НЕ пакуется, потому что уже ссылается на упакованный Point
            // p2 ПАКУЕТСЯ, потому что вызывается CompareTo(Object)
            Console.WriteLine(c.CompareTo(p2));// "-1"
            // c пакуется, а поля копируются в p2
            p2 = (Point)c;
            // Убеждаемся, что поля скопированы в p2
            Console.WriteLine(p2.ToString());// "(10, 10)
        }
    }
}
