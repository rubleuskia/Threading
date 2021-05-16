using System;
using System.Threading.Tasks;

namespace AsyncAwaitProgram
{
    public class MultiTasking
    {
        static void Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }

            Console.WriteLine($"Факториал числа {n} равен {result}");
        }

        static async void FactorialAsync()
        {
            await Task.Run(() => Factorial(4));
            await Task.Run(() => Factorial(3));
            await Task.Run(() => Factorial(5));
        }

        public static void Run1()
        {
            FactorialAsync();
            Console.Read();
        }

        static async void MultiFactorialAsync()
        {
            Task t1 = Task.Run(() => Factorial(4));
            Task t2 = Task.Run(() => Factorial(3));
            Task t3 = Task.Run(() => Factorial(5));

            await Task.WhenAll(t1, t2, t3);
        }

        public static void Run2()
        {
            MultiFactorialAsync();
            Console.Read();
        }
    }
}
