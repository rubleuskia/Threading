using System;

namespace GC1
{
    public class Disposables
    {
        public static void Run1()
        {
            Person p = null;
            try
            {
                p = new Person();
            }
            finally
            {
                p?.Dispose();
            }

            Console.ReadLine();
        }

        public static void Run2()
        {
            using (Person p = new Person { Name = "Tom" })
            {
                Console.WriteLine($"Некоторые действия с объектом Person. Получим его имя: {p.Name}");
            }

            Console.WriteLine("Конец метода Test");
        }

        private static void Run3()
        {
            using Person p = new Person { Name = "Tom" };
            Console.WriteLine($"Некоторые действия с объектом Person. Получим его имя: {p.Name}");

            Console.WriteLine("Конец метода Test");
        }

        private static void Run4()
        {
            using (Person tom = new Person { Name = "Tom" })
            {
                using(Person bob = new Person { Name = "Bob" })
                {
                    Console.WriteLine($"Person1: {tom.Name}    Person2: {bob.Name}");
                }
            }

            Console.WriteLine("Конец метода Test");
        }
    }

    public class Person : IDisposable
    {
        public string Name { get; set; }

        public void Dispose()
        {
            Console.Beep();
            Console.WriteLine("Disposed");
        }
    }
}
