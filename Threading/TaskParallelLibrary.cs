using System;
using System.Threading;
using System.Threading.Tasks;

namespace Threading
{
    public class TaskParallelLibrary
    {
        public static void Run1()
        {
            Task task1 = new Task(() => Console.WriteLine("Task1 is executed"));
            task1.Start();

            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Task2 is executed"));

            Task task3 = Task.Run(() => Console.WriteLine("Task3 is executed"));

            Console.ReadLine();
        }

        public static void Run2()
        {
            Task task = new Task(Display);
            task.Start();
            Console.WriteLine("Завершение метода Main");
            Console.ReadLine();
        }

        private static void Display()
        {
            Console.WriteLine("Начало работы метода Display");
            Console.WriteLine("Завершение работы метода Display");
        }

        public static void Run3()
        {
            Task task = new Task(Display);
            task.Start();
            task.Wait();
            Console.WriteLine("Завершение метода Main");
            Console.ReadLine();
        }

        public static void Run4()
        {
            var outer = Task.Factory.StartNew(() => // внешняя задача
            {
                Console.WriteLine("Outer task starting...");
                var inner = Task.Factory.StartNew(() => // вложенная задача
                {
                    Console.WriteLine("Inner task starting...");
                    Thread.Sleep(2000);
                    Console.WriteLine("Inner task finished.");
                });
            });

            outer.Wait(); // ожидаем выполнения внешней задачи
            Console.WriteLine("End of Main");

            Console.ReadLine();
        }

        public static void Run5()
        {
            var outer = Task.Factory.StartNew(() => // внешняя задача
            {
                Console.WriteLine("Outer task starting...");
                var inner = Task.Factory.StartNew(() => // вложенная задача
                {
                    Console.WriteLine("Inner task starting...");
                    Thread.Sleep(2000);
                    Console.WriteLine("Inner task finished.");
                }, TaskCreationOptions.AttachedToParent);
            });

            outer.Wait(); // ожидаем выполнения внешней задачи
            Console.WriteLine("End of Main");

            Console.ReadLine();
        }

        public static void Run6()
        {
            Task[] tasks1 = new Task[3]
            {
                new Task(() => Console.WriteLine("First Task")),
                new Task(() => Console.WriteLine("Second Task")),
                new Task(() => Console.WriteLine("Third Task"))
            };

            foreach (var t in tasks1)
            {
                t.Start();
            }

            Task[] tasks2 = new Task[3];
            int j = 1;

            for (int i = 0; i < tasks2.Length; i++)
            {
                tasks2[i] = Task.Factory.StartNew(() => Console.WriteLine($"Task {j++}"));
            }

            Console.WriteLine("Завершение метода Main");

            Console.ReadLine();
        }

        public static void Run7()
        {
            Task[] tasks1 = new Task[3]
            {
                new Task(() => Console.WriteLine("First Task")),
                new Task(() => Console.WriteLine("Second Task")),
                new Task(() => Console.WriteLine("Third Task"))
            };

            foreach (var t in tasks1)
            {
                t.Start();
            }

            Task.WaitAny(tasks1);
            Task.WaitAll(tasks1); // ожидаем завершения задач

            Console.WriteLine("Завершение метода Main");

            Console.ReadLine();
        }

        public static void Run8()
        {
            Task<int> task1 = new Task<int>(() => Factorial(5));
            task1.Start();

            Console.WriteLine($"Факториал числа 5 равен {task1.Result}");

            Task<Book> task2 = new Task<Book>(() =>
            {
                return new Book {Title = "Война и мир", Author = "Л. Толстой"};
            });

            task2.Start();

            Book b = task2.Result; // ожидаем получение результата
            Console.WriteLine($"Название книги: {b.Title}, автор: {b.Author}");

            Console.ReadLine();
        }

        public class Book
        {
            public string Title { get; set; }
            public string Author { get; set; }
        }

        static int Factorial(int x)
        {
            int result = 1;

            for (int i = 1; i <= x; i++)
            {
                result *= i;
            }

            return result;
        }

        public static void Run9()
        {
            Task task1 = new Task(() => { Console.WriteLine($"Id задачи: {Task.CurrentId}"); });

            // задача продолжения
            Task task2 = task1.ContinueWith(Display);

            task1.Start();

            // ждем окончания второй задачи
            task2.Wait();
            Console.WriteLine("Выполняется работа метода Main");
            Console.ReadLine();
        }

        static void Display(Task t)
        {
            Console.WriteLine($"Id задачи: {Task.CurrentId}");
            Console.WriteLine($"Id предыдущей задачи: {t.Id}");
            Thread.Sleep(3000);
        }

        public static void Run10()
        {
            Task<int> task1 = new Task<int>(() => Sum(4, 5));

            // задача продолжения
            Task task2 = task1.ContinueWith(sum => Display(sum.Result));

            task1.Start();

            // ждем окончания второй задачи
            task2.Wait();
            Console.WriteLine("End of Main");
            Console.ReadLine();
        }

        static int Sum(int a, int b) => a + b;

        static void Display(int sum)
        {
            Console.WriteLine($"Sum: {sum}");
        }

        public static void Run11()
        {
            Task task1 = new Task(() => { Console.WriteLine($"Id задачи: {Task.CurrentId}"); });

            // задача продолжения
            Task task2 = task1.ContinueWith(DisplayTask);

            Task task3 = task1.ContinueWith((Task t) => { Console.WriteLine($"Id задачи: {Task.CurrentId}"); });

            Task task4 = task2.ContinueWith((Task t) => { Console.WriteLine($"Id задачи: {Task.CurrentId}"); });

            task1.Start();

            Console.ReadLine();
        }

        static void DisplayTask(Task t)
        {
            Console.WriteLine($"Id задачи: {Task.CurrentId}");
        }

        public static void Run12()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            int number = 6;

            Task task1 = new Task(() =>
            {
                int result = 1;
                for (int i = 1; i <= number; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("Операция прервана");
                        return;
                    }

                    result *= i;
                    Console.WriteLine($"Факториал числа {number} равен {result}");
                    Thread.Sleep(5000);
                }
            });

            task1.Start();

            Console.WriteLine("Введите Y для отмены операции или другой символ для ее продолжения:");
            string s = Console.ReadLine();
            if (s == "Y")
            {
                cancelTokenSource.Cancel();
            }

            Console.Read();
        }

        public static void Run13()
        {
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            Task task1 = new Task(() => Factorial(5, token));
            task1.Start();

            Console.WriteLine("Введите Y для отмены операции или любой другой символ для ее продолжения:");
            string s = Console.ReadLine();
            if (s == "Y")
            {
                cancelTokenSource.Cancel();
            }

            Console.ReadLine();
        }

        private static void Factorial(int x, CancellationToken token)
        {
            var internalTokenSource = new CancellationTokenSource();
            var linkedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(
                token, internalTokenSource.Token);

            int result = 1;
            for (int i = 1; i <= x; i++)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Операция прервана токеном");
                    return;
                }

                result *= i;
                if (result > 100)
                {
                    linkedTokenSource.Cancel();
                }

                Console.WriteLine($"Факториал числа {x} равен {result}");
                Thread.Sleep(5000);
            }
        }
    }
}
