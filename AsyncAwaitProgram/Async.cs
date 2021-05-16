using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitProgram
{
    // В заголовке метода используется модификатор async
    // Метод содержит одно или несколько выражений await
    // В качестве возвращаемого типа используется один из следующих:
    // void
    // Task
    // Task<T>
    // ValueTask<T>

    public class Async
    {
        static void Factorial()
        {
            int result = 1;
            for(int i = 1; i <= 6; i++)
            {
                result *= i;
            }

            Thread.Sleep(8000);
            Console.WriteLine($"Факториал равен {result}");
        }

        // определение асинхронного метода
        static async void FactorialAsync()
        {
            Console.WriteLine("Начало метода FactorialAsync"); // выполняется синхронно
            await Task.Run(() => Factorial()); // выполняется асинхронно
            // await возвращает управление в main
            // следующая строка выполниться после завершения факториала
            Console.WriteLine("Конец метода FactorialAsync");
        }

        public static void Run1()
        {
            FactorialAsync();   // вызов асинхронного метода

            Console.WriteLine("Введите число: ");
            int n = Int32.Parse(Console.ReadLine());
            Console.WriteLine($"Квадрат числа равен {n * n}");
            Console.WriteLine("Конец метода Main");

            Console.Read();
        }

        // --------------------------------------------------------------------
        static async void ReadWriteAsync()
        {
            Console.WriteLine("Начинаем запись в файл, поток: " + Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(2000);
            string s = "Hello world! One step at a time";

            // hello.txt - файл, который будет записываться и считываться
            using (StreamWriter writer = new StreamWriter("hello.txt", false))
            {
                await writer.WriteLineAsync(s);  // асинхронная запись в файл
                // !!! runs synchronously !!!
            }

            Console.WriteLine("Запись в файл выполнена. Поток: " + Thread.CurrentThread.ManagedThreadId);

            using (StreamReader reader = new StreamReader("hello.txt"))
            {
                string result = await reader.ReadToEndAsync();  // асинхронное чтение из файла
                Console.WriteLine("Чтение из файла выполнено, поток: " + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine(result);
            }
        }

        public static void Run2()
        {
            Console.WriteLine("Начало метода main, поток: " + Thread.CurrentThread.ManagedThreadId);

            ReadWriteAsync();

            Console.WriteLine("Метод main завершен: " + Thread.CurrentThread.ManagedThreadId);
            Console.Read();
        }

        // --------------------------------------------------------------------------------------
        static void FactorialParametrized(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }

            Thread.Sleep(5000);
            Console.WriteLine($"Факториал равен {result}");
        }

        // определение асинхронного метода
        static async void FactorialParametrizedAsync(int n)
        {
            await Task.Run(() => FactorialParametrized(n));
        }

        public static void Run3()
        {
            FactorialParametrizedAsync(5);
            FactorialParametrizedAsync(6);

            Console.WriteLine("Некоторая работа");
            Console.Read();
        }

        // --------------------------------------------------------------------------------------
        static int FactorialWithReturnValue(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }

        // определение асинхронного метода
        static async void FactorialAsync(int n)
        {
            int x = await Task.Run(() => FactorialWithReturnValue(n));
            Console.WriteLine($"Факториал равен {x}");
        }

        public static void Run4()
        {
            FactorialAsync(5);
            FactorialAsync(6);

            Console.Read();
        }
    }
}
