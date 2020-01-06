using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR5
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Целочисленная очередь: ");
                MyQueue<int> q1 = new MyQueue<int>();
                q1 = q1 + 5 + 10 + 15;
                MyQueue<int> q2 = new MyQueue<int>();
                q2 = q2 + 3 + 7 + 9 + 11 + 6 + 5 + 23;
                q1 = q1 < q2;
                q1.Show();
                if (q1)
                {
                    Console.WriteLine("Queue is not empty");
                }
                int length = q1;
                Console.WriteLine("Длина очереди q1 " + length);

                Console.WriteLine("\nВещественная очередь: ");
                MyQueue<double> q3 = new MyQueue<double>();
                q3 = q3 + 0.5 + 0.7 + 0.6;
                q3--;
                q3.Show();

                Console.WriteLine("\nОчередь пользовательских объектов: ");
                Lion lion = new Lion("Africa", "lion", 80, 1990);
                Owl owl = new Owl("Europe", "owl", 2, 2005);
                Parrot parrot = new Parrot("Jungles", "parrot", 1.3, 2010);
                MyQueue<Animal> q4 = new MyQueue<Animal>();
                q4 = q4 + lion + owl + parrot;
                q4.Show();
            }
            catch(YearException e)
            {
                Console.WriteLine(e.Message + " " + e.Value);
            }
            catch(DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }
            catch(ChoiceOutOfRange e)
            {
                Console.WriteLine(e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Произошло исключение\t" + e.Message + e.Source);
            }
            finally
            {
                Console.WriteLine("Все");
            }
        }
    }
    interface IUseless<T> //Обобщенный интерфейс
    {
        void Show();
        void Add(T element);
        T Remove();
    }
    partial class MyQueue<T> : IUseless<T> //Обобщенный тип, реализующий обобщенный интерфейс
    {
        private Queue<T> queue;
        public Queue<T> Queue
        {
            get
            {
                return queue;
            }
        }

        public MyQueue()
        {
            queue = new Queue<T>();
        }
        public void Show()
        {
            foreach (T element in queue)
            {
                Console.WriteLine(element);
            }
        }

        public void Add(T element)
        {
            Queue.Enqueue(element);
        }

        public T Remove()
        {
            return Queue.Dequeue();
        }
    }

    partial class MyQueue<T> //Перегрузка операций
    {
        public static MyQueue<T> operator +(MyQueue<T> q, T element)
        {
            q.queue.Enqueue(element);
            return q;
        }

        public static MyQueue<T> operator --(MyQueue<T> q)
        {
            q.queue.Dequeue();
            return q;
        }

        public static bool operator true(MyQueue<T> q)
        {
            if (q.queue.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public static bool operator false(MyQueue<T> q)
        {
            if (q.queue.Count != 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public static MyQueue<T> operator <(MyQueue<T> q1, MyQueue<T> q2)
        {
            T[] arr = new T[q2.queue.Count];
            q2.queue.CopyTo(arr, 0);
            Array.Sort(arr);
            Array.Reverse(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                q1.queue.Enqueue(arr[i]);
            }
            return q1;
        }
        public static MyQueue<T> operator >(MyQueue<T> q1, MyQueue<T> q2)
        {
            T[] arr = new T[q1.queue.Count];
            q1.queue.CopyTo(arr, 0);
            Array.Sort(arr);
            Array.Reverse(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                q2.queue.Enqueue(arr[i]);
            }
            return q2;
        }

        public static implicit operator int(MyQueue<T> q)
        {
            return q.queue.Count;
        }
    }


}
