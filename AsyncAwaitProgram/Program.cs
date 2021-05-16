using System;
using System.Threading.Tasks;

namespace AsyncAwaitProgram
{
    class Program
    {
        // https://tutorial.eyehunts.com/android/android-handler-background-thread-communicate-ui-thread/
        // (!) http://hvasconcelos.github.io/articles/Offloading-work-from-the-UI-Thread
        // https://accedia.com/blog/introduction-to-asynchronous-programming-in-dot-net/

        static async Task Main(string[] args)
        {
            // Async.Run1();
            // Async.Run2();
            // Async.Run3();
            // Async.Run4();

            // AsyncReturnValue.Run();
            // MultiTasking.Run1();
            // MultiTasking.Run2();

            // AsyncExceptions.Run1();
            // AsyncExceptions.Run2();
            // AsyncExceptions.Run3();
            // AsyncExceptions.Run4();
            // AsyncExceptions.Run5();

            // AsyncStreams.Run1();
            // AsyncStreams.Run2();

            Console.ReadKey();

            // Homework
            // Create console app which sends GET request to the following resource:
            // https://api.github.com/orgs/dotnet/repos
            // and prints response to console. Use class HttpClient and async methods
            // Hints: https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
        }
    }
}
