using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR4
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue1 q1 = new Queue1();
            q1 = q1 + 5 + 10 + 15;
            q1.Show();
            Console.WriteLine("Sum: " + StatisticOperation.Sum(q1));
            Console.WriteLine("Last element in queue is: " + q1.LastElement());

            Queue1 q2 = new Queue1();
            q2 = q2 + 3 + 7 + 9 + 11 + 6 + 5 + 23;
            q2.Show();
            Console.WriteLine("Delta between min & max: " + StatisticOperation.Delta(q2));
            Console.WriteLine("Amount of elements: " + StatisticOperation.Amount(q2));
            Console.WriteLine("Last element in queue is: " + q2.LastElement());
            q1 = q1 < q2;
            q1.Show();

            q1--;
            q1.Show();

            if(q1)
            {
                Console.WriteLine("Queue is not empty");
            }

            int length = q1;

            Console.WriteLine("Длина очереди q1 " + length);
        }
    }

    class Queue1
    {
        private Queue<int> queue;

        public Queue<int> Queue
        {
            get
            {
                return queue;
            }
        }

        public Owner owner = new Owner("Vladislav", "Shichko"); //Вложенный объект

        public static class Date // Вложенный класс
        {
            static int day = 16;
            static int month = 10;
            static int year = 2019;
        }

        public Queue1()
        {
            queue = new Queue<int>();
        }

        public void Show()
        {
            Console.WriteLine('\n');
            foreach(int elem in queue)
            {
                Console.WriteLine(elem);
            }
        }

        public static Queue1 operator +(Queue1 q, int element)
        {
            q.queue.Enqueue(element);
            return q;
        }

        public static Queue1 operator --(Queue1 q)
        {
            q.queue.Dequeue();
            return q;
        }

        public static bool operator true(Queue1 q)
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
        public static bool operator false(Queue1 q)
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
      
        public static Queue1 operator <(Queue1 q1, Queue1 q2)
        {
            int[] arr = new int[q2.queue.Count];
            q2.queue.CopyTo(arr, 0);
            Array.Sort(arr);
            Array.Reverse(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                q1.queue.Enqueue(arr[i]);
            }
            return q1;
        }
        public static Queue1 operator >(Queue1 q1, Queue1 q2)
        {
            int[] arr = new int[q1.queue.Count];
            q1.queue.CopyTo(arr, 0);
            Array.Sort(arr);
            Array.Reverse(arr);
            for (int i = 0; i < arr.Length; i++)
            {
                q2.queue.Enqueue(arr[i]);
            }
            return q2;
        }

        public static implicit operator int(Queue1 q)
        {
            return q.queue.Count;
        }
    }

    static class StatisticOperation
    {
        public static int Sum(Queue1 q)
        {
            int sum = 0;
            Queue<int> queue1 = new Queue<int>(q.Queue);
            for(int i = 0, counter = queue1.Count; i < counter; i++)
            {
                sum += queue1.Dequeue();
            }
            return sum;
        }

        public static int Delta(Queue1 q)
        {
            int delta;
            int min;
            int max = min = q.Queue.Peek();
            foreach(int el in q.Queue)
            {
                if(el < min)
                {
                    min = el;
                }
                if(el > max)
                {
                    max = el;
                }
            }
            delta = max - min;
            return delta;
        }

        public static int Amount(Queue1 q)
        {
            return q.Queue.Count;
        }

        public static int FirstPoint(this String str)
        {
            for(int i = 0; i < str.Length; i++)
            {
                if(str[i] == '.')
                {
                    return i;
                }
            }
            return -1;
        }

        public static int LastElement(this Queue1 q)
        {
            Queue<int> queue1 = new Queue<int>(q.Queue);
            int last = 0;
            for(int i = 0, counter = queue1.Count; i < counter; i++)
            {
                if(i == counter - 1)
                {
                    last = queue1.Dequeue();
                }
                else
                {
                    queue1.Dequeue();
                }
            }
            return last;
        }
    }

    public class Owner
    {
        public string firstName;
        public string secondName;

        public Owner(string name1,string name2)
        {
            firstName = name1;
            secondName = name2;
        }
    }
}
