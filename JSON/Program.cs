using System;
using System.Threading.Tasks;

namespace JSON
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // JsonService.Run1();
            await JsonService.Run2();

            Console.ReadKey();
        }
    }
}
