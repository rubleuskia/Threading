using System;
using System.Threading;

namespace Threading
{
    public class Threads
    {
        public static void Run1()
        {
            // получаем текущий поток
            Thread t = Thread.CurrentThread;

            //получаем имя потока
            Console.WriteLine($"Имя потока: {t.Name}");
            t.Name = "Метод Main";
            Console.WriteLine($"Имя потока: {t.Name}");

            Console.WriteLine($"Запущен ли поток: {t.IsAlive}");
            Console.WriteLine($"Приоритет потока: {t.Priority}");
            Console.WriteLine($"Статус потока: {t.ThreadState}");

            // получаем домен приложения
            Console.WriteLine($"Домен приложения: {Thread.GetDomain().FriendlyName}");

            Console.ReadLine();
        }

        public static void Run2()
        {
            // создаем новый поток
            Thread myThread = new Thread(new ThreadStart(Count));
            myThread.Start(); // запускаем поток
 
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Главный поток:");
                Console.WriteLine(i * i);
                Thread.Sleep(300);
            }
 
            Console.ReadLine();
        }

        public static void Run3()
        {
            int number = 4;
            // создаем новый поток
            Thread myThread = new Thread(new ParameterizedThreadStart(Count));
            myThread.Start(number);

            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Главный поток:");
                Console.WriteLine(i * i);
                Thread.Sleep(300);
            }

            Console.ReadLine();
        }

        public static void Run4()
        {
            Counter counter = new Counter();
            counter.x = 4;
            counter.y = 5;

            Thread myThread = new Thread(new ParameterizedThreadStart(CountClassParam));
            myThread.Start(counter);

            Console.ReadLine();
        }

        public static void Count()
        {
            for (int i = 1; i < 9; i++)
            {
                Console.WriteLine("Второй поток:");
                Console.WriteLine(i * i);
                Thread.Sleep(400);
            }
        }

        public static void Count(object x)
        {
            for (int i = 1; i < 9; i++)
            {
                int n = (int)x;

                Console.WriteLine("Второй поток:");
                Console.WriteLine(i*n);
                Thread.Sleep(400);
            }
        }

        public static void CountClassParam(object obj)
        {
            for (int i = 1; i < 9; i++)
            {
                Counter c = (Counter)obj;

                Console.WriteLine("Второй поток:");
                Console.WriteLine(i * c.x * c.y);
            }
        }
    }

    public class Counter
    {
        public int x;
        public int y;
    }
}
