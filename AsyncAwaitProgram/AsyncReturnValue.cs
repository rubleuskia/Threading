using System;
using System.Threading.Tasks;

namespace AsyncAwaitProgram
{
    public class AsyncReturnValue
    {
        static void Factorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }

            Console.WriteLine($"Факториал равен {result}");
        }

        static int GetFactorial(int n)
        {
            int result = 1;
            for (int i = 1; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        // VOID --------------------------------------
        static async void FactorialVoidAsync(int n)
        {
            await Task.Run(() => Factorial(n));
        }

        // Task --------------------------------------
        static async Task FactorialTaskAsync(int n)
        {
            await Task.Run(() => Factorial(n));
        }

        // Task<T> --------------------------------------
        static async Task<int> FactorialAsync(int n)
        {
            // remove await?
            return await Task.Run(() => GetFactorial(n));
        }

        static async ValueTask<int> FactorialValueTaskAsync(int n)
        {
            return await Task.Run(() => GetFactorial(n));
        }

        public static async Task Run()
        {
            FactorialVoidAsync(5);
            await FactorialTaskAsync(6);

            int n1 = await FactorialAsync(5);
            int n2 = await FactorialAsync(6);

            int n3 = await FactorialValueTaskAsync(5);
            int n4 = await FactorialValueTaskAsync(6);

            Console.Read();
        }
    }
}
