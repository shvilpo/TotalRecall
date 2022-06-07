using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadBlockings
{
    class Program
    {
        static object _locker = new object();
        private static Mutex mutex = new();
        private static Mutex namedMutex = new Mutex(false, "KoztorezMutex");

        private static void DummyService(int i)
        {
            Thread.Sleep(1000);
        }
        private static void CallService()
        { 
            var s = DateTime.Now.Millisecond * 10;
            Console.WriteLine($"pause {s} s");
            Thread.Sleep(s);
        }
        static void Main(string[] args)
        {
            
            
            Console.WriteLine("Hello World!");
            var range = Enumerable.Range(1, 1000);
            Stopwatch watch = Stopwatch.StartNew();
            //for (int i = 0; i < range.Count(); i++)
            //{
            //    Thread.Sleep(10);
            //    File.AppendAllText("test.txt", $"{i.ToString()} ");
            //}

            #region Lock
            //range.AsParallel().AsOrdered().ForAll(i => {
            //    Thread.Sleep(10);
            //    lock (_locker)
            //    {
            //        File.AppendAllText("test.txt", $"{i.ToString()} ");
            //    }
            //});
            #endregion

            #region Monitor
            range.AsParallel().AsOrdered().ForAll(i =>
                {
                    Thread.Sleep(10);
                    Monitor.Enter(_locker);
                    try
                    {
                        File.AppendAllText("test.txt", $"{i.ToString()} ");
                    }
                    finally {
                        Monitor.Exit(_locker);
                    }
                });
            #endregion

            #region Mutex
            // Для того чтобы у нас получилось провести блокировку общих ресурсов, мы можем обратиться к блокировке на уровне ядра ОС,
            // используя класс Mutex. Подобно lock, mutex предоставляет доступ к защищенному ресурсу только для одного потока.
            // Но он также может работать между процессами, предоставляя одному потоку в каждом из них доступ к защищенному ресурсу, независимо
            // от количества выполняемых процессов.
            // Mutex бывает именованный и безымянный.Безымянный работает как блокировка, он не может работать со всеми процессами.
            //range.AsParallel().AsOrdered().ForAll(i =>
            //    {
            //        Thread.Sleep(10);
            //        mutex.WaitOne();
            //        File.AppendAllText("test.txt", $"{i.ToString()} ");
            //        mutex.ReleaseMutex();
            //    });

            // безимянный мьютекс реботает только в рамках одного процесса. В межпроцессорной блокировке необходимо использовать именованный мьютекс
            //range.AsParallel().AsOrdered().ForAll(i =>
            //{
            //    Thread.Sleep(10);
            //    namedMutex.WaitOne(3000); // блокировка с таймаутом, через указанное время блокировка снимается
            //    File.AppendAllText("test.txt", $"{i.ToString()} ");
            //    namedMutex.ReleaseMutex();
            //});
            #endregion

            #region Semaphore
            // Семафор работает, также как и Mutex, со всеми потоками, он потоконезависим
            // Реализуем параллельное выполнение задоч, но не более 3 одновременно
            // Локальный семафор работает в пределах процесса
            //var semaphore = new Semaphore(3, 3);
            // Глобальный семафор создается на уровне операционной системы и работает со всеми запущенными процессами
            var semaphore = new Semaphore(3, 3, "GlobalSemaphore");
            range.AsParallel().AsOrdered().ForAll(i =>
            {
                semaphore.WaitOne();
                Console.WriteLine($"Index {i} making service call using Task {Task.CurrentId}");
                CallService();
                Console.WriteLine($"Index {i} releasing semaphore using Task { Task.CurrentId}");
                semaphore.Release();
            });
            #endregion


            watch.Stop();
            Console.WriteLine($"Total time to write file is {watch.ElapsedMilliseconds}");

        }
    }
}
