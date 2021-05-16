using System;
using System.Threading.Tasks;

namespace AsyncAwaitProgram
{
    public class AsyncExceptions
    {
        static void Factorial(int n)
        {
            Console.WriteLine($"Факториал числа {n}: ");

            if (n < 1)
            {
                throw new Exception($"{n} : число не должно быть меньше 1");
            }

            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }

            Console.WriteLine($"Факториал числа {n} равен {result}");
        }

        static async void FactorialAsync(int n)
        {
            try
            {
                await Task.Run(() => Factorial(n));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void Run1()
        {
            FactorialAsync(-4);
            FactorialAsync(6);

            Console.Read();
        }

        // --------------------------------------
        static async void FactorialDetailedAsync(int n)
        {
            Task task = null;
            try
            {
                task = Task.Run(() => Factorial(n));
                await task; // comment and check
            }
            catch (Exception ex)
            {
                Console.WriteLine(task.Exception.InnerException.Message);
                Console.WriteLine($"IsFaulted: {task.IsFaulted}");
            }
        }

        public static void Run2()
        {
            FactorialDetailedAsync(-4);
            Console.Read();
        }

        // -----------------------------------------------
        static async Task DoMultipleAsync()
        {
            Task allTasks = null;

            try
            {
                Task t1 = Task.Run(()=>Factorial(-3));
                Task t2 = Task.Run(() => Factorial(-5));
                Task t3 = Task.Run(() => Factorial(-10));

                allTasks = Task.WhenAll(t1, t2, t3);
                await allTasks;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Исключение: " + ex.Message);
                Console.WriteLine("IsFaulted: " + allTasks.IsFaulted);
                foreach (var inx in allTasks.Exception.InnerExceptions)
                {
                    Console.WriteLine("Внутреннее исключение: " + inx.Message);
                }
            }
        }

        public static void Run3()
        {
            DoMultipleAsync();
            Console.Read();
        }

        // ----------------------------------
        static async void FactorialWithCatchFinallyAsync(int n)
        {
            try
            {
                await Task.Run(() => Factorial(n));
            }
            catch (Exception ex)
            {
                await Task.Run(() => Console.WriteLine(ex.Message));
            }
            finally
            {
                await Task.Run(() => Console.WriteLine("await в блоке finally"));
            }
        }

        public static void Run4()
        {
            FactorialWithCatchFinallyAsync(-5);
            Console.Read();
        }

        // ------------------------------------------

        private async void ThrowExceptionAsync()
        {
            throw new InvalidOperationException();
        }

        // https://docs.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming
        public void Run5()
        {
            try
            {
                ThrowExceptionAsync();
            }
            catch (Exception e)
            {
                // The exception is never caught here!
                throw new Exception(e.Message);
            }
        }
    }
}
