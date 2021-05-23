using System;
using System.Threading;
using System.Threading.Tasks;

namespace GC
{
    public static class Destructors
    {
        public static void Run()
        {
            Test();
            System.GC.Collect();
            Console.WriteLine("end");
        }

        private static void Test()
        {
            Person p = new Person();
        }

        private class Person
        {
            public string Name { get; set; }

            ~Person()
            {
                Console.Beep();
                Console.WriteLine("Disposed");
            }

            // protected override void Finalize()
            // {
            //     try
            //     {
            //         // здесь идут инструкции деструктора
            //     }
            //     finally
            //     {
            //         base.Finalize();
            //     }
            // }
        }
    }
}
