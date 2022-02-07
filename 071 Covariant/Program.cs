using System;
using System.Collections.Generic;

namespace _071_Covariant
{
    class Program
    {
        static void Main(string[] args)
        {
            // создаём объект делегата, который принимает фрукт и возвращает void
            Action<Fruit> actFruit = fruit => fruit.Eat();
            List<Orange> oranges = new List<Orange>() { new Orange(), new Orange(), new Orange() };

            // по факту мы должны передать в метод ForEach объект делгата Action<Orange>,
            // который принимает апельсин и возвращает void, но благодаря контравариантности 
            // можем передать Action<Fruit> т.е. записываем в переменную типа Action<Orange> объект типа Action<Fruit>
            // контравариантность переварачивает порядок наследования
            // Orange : Fruit => Action<Fruit> : Action<Orange>
            // Fruit fruit = new Orange();
            // Action<Orange> action = new Action<Fruit>(fruit => fruit.Eat());
            oranges.ForEach(actFruit);

            Action<Orange> actOrange = new Action<Fruit>(fruit => fruit.Eat());


        }
    }
    class Fruit
    {
        public void Eat()
        {
            Console.WriteLine("You ate fruit!");
        }
    }
    class Orange : Fruit
    { 

    }
}
