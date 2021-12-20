using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventWaitHandle
{
    class Program
    {
        static ManualResetEvent manulalResetEvent = new ManualResetEvent(false);
        //static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        //static int sum = 0;
        static void Main(string[] args)
        {
            //Task signallingTask = Task.Factory.StartNew(() =>
            //{
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Thread.Sleep(2000); 
            //        Console.WriteLine($"Send signal: {i}");
            //        autoResetEvent.Set();
            //    }
            //});
            //Parallel.For(1, 10, (i) =>
            //{
            //    //Console.WriteLine($"Waiting task: {i}, sum: {sum}");
            //    Console.WriteLine($"Task with Id: {Task.CurrentId} waiting");
            //    autoResetEvent.WaitOne();
            //    Console.WriteLine($"Task with Id: {Task.CurrentId} receiving");
            //    sum += i;
            //});

            Task signalOffTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Network is OFF");
                    manulalResetEvent.Reset();
                }
            });
            Task signalOnTask = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    Console.WriteLine("Network is ON");
                    manulalResetEvent.Set();
                }
            });
            for (int i = 0; i < 3; i++)
            {
                Parallel.For(0, 5, (j) =>
                  {
                      Console.WriteLine($"Task id {Task.CurrentId} waiting for net");
                      manulalResetEvent.WaitOne();
                      Console.WriteLine($"Task id {Task.CurrentId} service net up");
                  });
                Thread.Sleep(2000);
            }

            Console.ReadKey();
        }
    }
}
