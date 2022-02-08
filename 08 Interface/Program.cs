//#define POINT
//#define SEALED
//#define IMETHODS
#define GENERICS
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;


// Интерфейс, в сущности - именованный набор сигнатур методов, средство назначения имени набору сигнатур методов
// Интерфейс не содержит реализаций методов
// Класс наследует интерфейс через указание его имени, при этом класс должен явно содердать реализацию интерфейсных методов
// Компилятор C# и CLR позволяют классу наследовать несколько интерфейсов, при этом класс должен реализовать все унаследованные методы
// При наследовании допускается подставлять экземпляры произодных классов в любые контексты, в которых выступают экземпляры базового класса
// Наследование от интерфейся позволяет подставлять экземпляры типа, реализующего интерфейс во все контексты, где требуется экземпляру указанного интерфейсного типа

// В интерфейсе можно определять события, свойства (свойство с параметром - индексатор)
// В интерфейса нельза определять конструкторы и экземплярные поля, а также статические члены
// CLR поддерживает обобщенные интерфейсы и интерфейсные методы
// Интерфейсы могут включать контракты других интерфейсов, т.н. "наследование" инетерфейса интерфейсом

// Компилятор требует, чтобы методы, реализующие интерфейс, были public
// CLR требует, чтобы методы, реализующие интерфейс, были виртуальными, если не отметить virtual, CLR сдлеает его virtual sealed
//   если отметить virtual, то selaed не будет
// Производный класс не в состоянии переопределить sealed методы, но может повторно унаследовать этот же интерфейс, предоставить собственную реализацию метода,
//   в этом случае при вызове переопределенного метода будет вызвана реализация метода, связянная с типом самого объекта

// Преимущества обобщенных интерфейсов:
// 1. Обеспечивают безопасность типов на стадии компиляции
// 2. При раболте со значимыми типами требуется меньше операций упаковки
// 3. Класс может реализовывать один интерфейс многократно используя параметры разного типа

namespace _08_Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            #region POINT
#if Point
            Point[] points = new Point[] { new Point(2, 3), new Point(3, 4) };
            // Вызов метода CompareTo интерфейса IComparable<T> объекта Point
            if (points[0].CompareTo(points[1]) > 0)
            {
                Point tempPoint = points[0];
                points[0] = points[1];
                points[1] = tempPoint;
            }
            Console.WriteLine("Points from closest to (0, 0) to farthest:");
            foreach (Point p in points)
                Console.WriteLine(p);
#endif
            #endregion
            #region SEALED
#if Sealed
            // *********** 1
            Base b = new Base();
            // Вызов реализации Dispose в типе b: "Dispose класса Base"
            b.Dispose();
            // Вызов реализации Dispose в типе объекта b: "Dispose класса Base"
            ((IDisposable)b).Dispose();
            // *********** 2
            Derived d = new Derived();
            // Вызов реализации Dispose в типе d: "Dispose класса Derived"
            d.Dispose();
            // Вызов реализации Dispose в типе объекта d: "Dispose класса Derived"
            ((IDisposable)d).Dispose();
            // ***********  3
            b = new Derived();
            // Вызов реализации Dispose в типе b: "Dispose класса Base"
            b.Dispose();
            // Вызов реализации Dispose в типе объекта b: "Dispose класса Derived"
            ((IDisposable)b).Dispose();
#endif
            #endregion
            #region Interface Methods
#if IMETHODS
            // string : ICloneable, IComparable, IEnumerable
            string s = "!";
            
            // могу вызывать методы интерфейса типатекущей переменной
            ICloneable s_cl = s;
            s.Clone();
            
            IComparable s_comp = s;
            var comp = s_comp.CompareTo("asdf2");

            // могу приводить к типу любого интерфейса, от которого совместно унаследован базовый тип
            IEnumerable s_num = (IEnumerable)s_comp;

            SimpleType st = new SimpleType();
            st.Dispose();
            IDisposable ds = st;
            ds.Dispose();


#endif
            #endregion
            #region Generics
#if GENERICS

#endif
            #endregion

        }

    }



    internal sealed class SimpleType : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Dispose()");
        }
        
        // Explicit Interface Method Implementation (EIMI) - явная реализация интерфейсного метода
        // нельзя указывать уровень доступа, всегда закрытый -> запрещает любому коду явно использовать экземпляр класса простым вызовом интерфейсного метода,
        //   единственный способ вызвать интерфейсный метод - обратиться через переменную этогог интерфейсного типа
        // EIMI метод не может быть виртуальным, его нельзя переопределить
        void IDisposable.Dispose()
        {
            Console.WriteLine("IDisposable.Dispose()");
        }
    }

    public sealed class Point : IComparable<Point>
    {
        private Int32 m_x, m_y;
        public Point(Int32 x, Int32 y)
        {
            m_x = x;
            m_y = y;
        }
        public int CompareTo(Point other)
        {
            return Math.Sign(Math.Sqrt(m_x * m_x + m_y * m_y) - Math.Sqrt(other.m_x * other.m_x + other.m_y * other.m_y));
        }
        public override string ToString()
        {
            return $"{m_x}, {m_y}";
        }
    }

    internal class Base : IDisposable
    {
        // implicid sealed method  (неявно запечатанный)
        public void Dispose()
        {
            Console.WriteLine("Base's Dispose()");
        }
    }
    internal class Derived : Base, IDisposable
    {
        new public void Dispose()
        {
            Console.WriteLine("Derived's Dispose()");
            // for Base dispose call:
            // base.Dispose();
        }
    }

    public interface IWindow {
        public Object GetMenu();
    }
    public interface IRestaurant {
        Object GetMenu();
    }
    public sealed class MarioPizzeria : IWindow, IRestaurant
    {
        object IRestaurant.GetMenu()
        {
            throw new NotImplementedException();
        }

        object IWindow.GetMenu()
        {
            return new object();
        }
    }
}
