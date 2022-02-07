using System;
using System.Collections;
using System.Collections.Generic;
// упрощенный синтаксис для ссылки на обобщенный закрытый тип
using DateTimeList = System.Collections.Generic.List<System.DateTime>;

namespace _07_Generics
{
    // Поддерживается обобщенные ссылочные и значимые типы
    // НЕ поддерживается обобщенные перечисляемые типы
    // Поддерживаются обобщенные интерфейсы и делегаты
    // Поддердиваются обобщенные методы в ссылочном, значимом типе, интерфейсе
    // .NET FCL (Framework Class Library) или BCL (Base Class Library) - стандартная библиотека классов платформы .NET Framework

    // Для generic-типов (типов с обобщенными параметрами-типами) CLR создает открытые (open types) объекты-типы (type objects)
    // В CLR запрещено конструировать экземпляров открытых типов и экземпляров интерфейсных типов
    // Если всем параметрам типа передаются действительные типы данных, то он становится закрытым типом (closed type)
    // В CLR разрешено создание экземпляров закрытых типов
    internal sealed class DateTimeList : List<DateTime>
    {
        // Здесь никакой код добавлять не нужно!
    }


    // Generic Interfaces
    internal sealed class Triangle<Point> : IEnumerator<Point>
    {
        private Point[] m_vertices;
        public Point Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }

    internal sealed class ArrayEnumerator<T> : IEnumerator<T>
    {
        private T[] m_array;

        public T Current => throw new NotImplementedException();

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
    class Program
    {
        public delegate TReturn CallMe<TReturn, TKey, TValue>(TKey key, TValue value);
        // Параметры типов обобщенного делегата должен быть помечен как инвариантный или контравариантный
        // Контрвариантный (in) - параметр-тип можеть быть преобразован к производному калассу, может быть только во входной позиции, напр. в качестве аргумента метода
        // Ковариантный (out) - параметр-тип может быть преобразован к базовому, может быть только в выходной позиции, напремер, возвращаемым значение метода

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
