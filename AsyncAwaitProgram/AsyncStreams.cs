using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwaitProgram
{
    // public interface IAsyncEnumerable<out T>
    // {
    //     IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default);
    // }
    //
    // public interface IAsyncEnumerator<out T> : IAsyncDisposable
    // {
    //     T Current { get; }
    //     Task<bool> MoveNextAsync();
    // }
    //
    // public interface IAsyncDisposable
    // {
    //     ValueTask DisposeAsync();
    // }

    public class AsyncStreams
    {
        public static async Task Run1()
        {
            await foreach (var number in GetNumbersAsync())
            {
                Console.WriteLine(number);
            }
        }

        public static async IAsyncEnumerable<int> GetNumbersAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(100);
                yield return i;
            }
        }


        // -----------------------------------------------------------
        class Repository
        {
            string[] data = { "Tom", "Sam", "Kate", "Alice", "Bob" };
            public async IAsyncEnumerable<string> GetDataAsync()
            {
                for (int i = 0; i < data.Length; i++)
                {
                    Console.WriteLine($"Получаем {i + 1} элемент");
                    await Task.Delay(500);
                    yield return data[i];
                }
            }
        }

        public static async Task Run2()
        {
            Repository repo = new Repository();
            IAsyncEnumerable<string> data = repo.GetDataAsync();
            await foreach (var name in data)
            {
                Console.WriteLine(name);
            }
        }
    }
}
