using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Threading
{
    public class ThreadsSynchronization
    {
        private static int _x = 0;
        static object locker = new object();
        static Mutex _mutex = new Mutex();

        public static void Run1()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(Count);
                myThread.Name = "Поток " + i.ToString();
                myThread.Start();
            }
        }

        public static void Run2()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(CountWithLock);
                myThread.Name = "Поток " + i.ToString();
                myThread.Start();
            }

            Console.ReadLine();
        }

        public static void Run3()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(CountWithMonitor);
                myThread.Name = $"Поток {i.ToString()}";
                myThread.Start();
            }

            Console.ReadLine();
        }

        public static void Run4()
        {
            for (int i = 0; i < 5; i++)
            {
                Thread myThread = new Thread(CountWithMutex);
                myThread.Name = $"Поток {i}";
                myThread.Start();
            }

            Console.ReadLine();
        }

        public static void Count()
        {
            _x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, _x);
                _x++;
                Thread.Sleep(100);
            }
        }

        public static void CountWithLock()
        {
            lock (locker)
            {
                _x = 1;
                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine("{0}: {1}", Thread.CurrentThread.Name, _x);
                    _x++;
                    Thread.Sleep(100);
                }
            }
        }

        public static void CountWithMonitor()
        {
            bool acquiredLock = false;
            try
            {
                Monitor.Enter(locker, ref acquiredLock);
                _x = 1;
                for (int i = 1; i < 9; i++)
                {
                    Console.WriteLine($"{Thread.CurrentThread.Name}: {_x}");
                    _x++;
                    Thread.Sleep(100);
                }
            }
            finally
            {
                if (acquiredLock) Monitor.Exit(locker);
            }
        }

        public static void CountWithMutex()
        {
            _mutex.WaitOne();
            _x = 1;
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {_x}");
                _x++;
                Thread.Sleep(100);
            }
            _mutex.ReleaseMutex();
        }
    }
}
