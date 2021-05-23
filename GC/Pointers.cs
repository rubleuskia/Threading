using System;

namespace GC
{
    public class Pointers
    {
        public static void Run1()
        {
            unsafe
            {

            }
        }

        private static unsafe void PointerMethod()
        {

        }

        unsafe struct State
        {

        }

        public static void Run2()
        {
            unsafe
            {
                int* x; // определение указателя
                int y = 10; // определяем переменную

                x = &y; // указатель x теперь указывает на адрес переменной y
                Console.WriteLine(*x); // 10

                y = y + 20;
                Console.WriteLine(*x);// 30

                *x = 50;
                Console.WriteLine(y); // переменная y=50
            }
        }

        public static void Run3()
        {
            unsafe
            {
                int* x; // определение указателя
                int y = 10; // определяем переменную

                x = &y; // указатель x теперь указывает на адрес переменной y

                // получим адрес переменной y
                uint addr = (uint)x;
                Console.WriteLine("Адрес переменной y: {0}", addr);
            }
        }

        public static void Run4()
        {
            unsafe
            {
                int* x; // определение указателя
                int y = 10; // определяем переменную

                x = &y; // указатель x теперь указывает на адрес переменной y
                int** z = &x; // указатель z теперь указывает на адрес, который указывает и указатель x
                **z = **z + 40; // изменение указателя z повлечет изменение переменной y
                Console.WriteLine(y); // переменная y=50
                Console.WriteLine(**z); // переменная **z=50
            }
        }

        // https://metanit.com/sharp/tutorial/8.4.php
    }
}
