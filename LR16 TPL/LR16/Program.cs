using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace LR16
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Задание 1

            Console.WriteLine("Введите максимальное число:");
            uint maxValue = 1000;
            try
            {
                maxValue = Convert.ToUInt32(Console.ReadLine());
            }
            catch(FormatException)
            {
                Console.WriteLine("Неверный ввод");
            }
            Task<uint> task = new Task<uint>(() => Erastophene(maxValue));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            task.Start();
            Timer timer = new Timer((_) => Console.WriteLine($"Статус: {task.Status}\t\tЗадача завершена: {task.IsCompleted}"), null, 0, 2000);
            Task.WaitAll(task);
            stopwatch.Stop();
            timer.Dispose();
            Console.WriteLine($"Затраченное время (мс): {stopwatch.ElapsedMilliseconds}");
            stopwatch.Reset();

            //Задание 2

            CancellationTokenSource cancellationToken = new CancellationTokenSource();

            Task task2 = new Task(() => Erastophene(maxValue), cancellationToken.Token);
            task2.Start();
            Thread.Sleep(100);
            if(!task2.IsCompleted)
            {
                cancellationToken.Cancel();
                Console.WriteLine("Задача была отменена.");
            }

            //Задание 3
            
            //Task<uint> task3 = new Task<uint>(() => Factorial(5));
            //Task<uint> task4 = new Task<uint>(() => Factorial(10));
            //task3.Start();
            //task4.Start();
            //Task.WaitAll(task3, task4);
            //Task<int> task5 = new Task<int>(() => Sum((int)task.Result, (int)task3.Result, (int)task4.Result));
            //task5.Start();
            //Task.WaitAll(task5);
            //Console.WriteLine($"Результат: {task5.Result}");

            ////Задание 4

            //Task task6 = Task.Run(() => Console.WriteLine("Выполнение 6 задачи"));
            //Task task7 = task6.ContinueWith((_) => Console.WriteLine("Продолжение 6 задачи в 7"));
            //Task.WaitAll(task6, task7);
            //Task<int> task8 = Task.Run(() => 0);
            //var awaiter = task8.GetAwaiter();
            //awaiter.OnCompleted(() => Console.WriteLine($"Инкремент задачи 8: {awaiter.GetResult() + 1}"));
            //Task.WaitAll(task8);
            
            ////Задание 5

            //stopwatch.Start();
            //ParallelLoopResult result = Parallel.For(0, 10, x => 
            //{
            //    Random random1 = new Random();
            //    int[] array = new int[1000000];
            //    for(int i = 0; i < array.Length; i++)
            //    {
            //        array[i] = random1.Next(0, 10000);
            //    }
            //});
            //if (result.IsCompleted)
            //{
            //    stopwatch.Stop();
            //    Console.WriteLine($"Время работы параллельного цикла (мс): {stopwatch.ElapsedMilliseconds}");
            //}
            //stopwatch.Reset();

            //stopwatch.Start();
            //for(int i = 0; i < 10; i++)
            //{
            //    Random random1 = new Random();
            //    int[] array = new int[1000000];
            //    for (int j = 0; j < array.Length; j++)
            //    {
            //        array[j] = random1.Next(0, 10000);
            //    }
            //}
            //stopwatch.Stop();
            //Console.WriteLine($"Время работы обычного цикла (мс): {stopwatch.ElapsedMilliseconds}");
            //stopwatch.Reset();

            ////Задание 6

            //IEnumerable<int> numbers = Enumerable.Range(1, 10000000);
            //List<int> numbers1;
            //List<int> numbers2;
            //stopwatch.Start();
            //Parallel.Invoke(() =>
            //{
            //    numbers1 = numbers.Where(num => num % 7 == 0).ToList();
            //}, () =>
            //{
            //    numbers2 = numbers.Select(num => num + numbers.First()).ToList();
            //});
            
            ////Задание 7
            
            //for (int producer = 1; producer < 6; producer++)
            //{
                
            //    Task.Run(() => Producer(producer));
            //    Thread.Sleep(10);
            //}
            //Random random = new Random();
            //for (int customer = 0; customer < 10; customer++)
            //{
            //    Thread.Sleep(10);
            //    Task.Factory.StartNew(() =>
            //    {
            //        for (int i = 0; i < 3; i++)
            //        {
            //            Thread.Sleep(150 + customer * 100);
            //            int item = random.Next(1,6);
            //            lock(locker)
            //            {
            //                if(items.TryTake(out item, 200))
            //                {
            //                    Console.WriteLine($"Посетитель купил товар {item}");
            //                }
            //                else
            //                {
            //                    Console.WriteLine("Товара не было, посетитель ушел");
            //                }
            //            }
            //        }
            //    });
            //}
            //Thread.Sleep(10000);
            
            ////Задание 8

            //ShowNumberAsync(99);
            //for(int i = 0; i < 4; i++)
            //{
            //    Console.WriteLine(i);
            //    Thread.Sleep(1000);
            //}
            //Console.Read();

        }
        static uint Erastophene(uint maxValue)
        {
            var numbers = new List<uint>();

            for (var i = 2u; i < maxValue; i++)
            {
                numbers.Add(i);
            }

            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = 2u; j < maxValue; j++)
                {

                    numbers.Remove(numbers[i] * j);
                }
            }
            foreach (uint number in numbers)
            {
                Console.WriteLine(number);
            }
            return numbers.Last();
        }

        static uint Factorial(uint x)
        {
            uint result = 1;

            for (uint i = 1; i <= x; i++)
            {
                result *= i;
            }

            return result;
        }

        static int Sum(int var1, int var2, int var3)
        {
            return var1 + var2 + var3;
        }

        static BlockingCollection<int> items = new BlockingCollection<int>();
        static object locker = new object();
        static void Producer(int producer)
        {
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500 + producer * 200);
                lock(locker)
                {
                    Console.WriteLine($"Поставщик доставил товар {producer}");
                    items.Add(producer);
                }
                
            }
        }
        static async void ShowNumberAsync(int number)
        {
            Console.WriteLine("Начало метода");
            await Task.Run(() => ShowNumber(number));
            Console.WriteLine("Конец метода");
        }
        static void ShowNumber(int number)
        {
            Thread.Sleep(5000);
            Console.WriteLine($"Ваше число {number}");
        }
    }
}
