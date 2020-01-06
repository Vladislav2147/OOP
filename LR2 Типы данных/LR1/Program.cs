using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR1
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Базовый уровень
            //Задание 1.1

            double dVar = 39.9;
            string sVar = "Hello, world!";

            object o1 = dVar;   //Упаковка
            object o2 = sVar;

            double dVar1 = (double)o1;  //Распаковка
            string sVar1 = (string)o2;

            //Задание 1.2

            int? num = 14;

            float f1 = num; //Неявные приведения типа
            double d1 = num;
            long l1 = num;

            float f2 = (float)num;  //Явные приведения типа
            double d2 = (double)num;
            long l2 = (long)num;

            //Задание 1.3

            string name = "Vladislav";

            string s = string.Format("My name is {0}", name); //string.Format
            Console.WriteLine(s);

            Console.WriteLine($"My name is {name}\n");  //Интерполирование строк

            //Задание 1.4

            Console.WriteLine(name.GetType().ToString()); //Методы класса Object
            Console.WriteLine(num.GetType().ToString() + "\n");

            //Задание 1.5

            if (string.Compare(sVar, name) == 0)    //Compare
            {
                Console.WriteLine($"Strings \"{sVar}\" and \"{name}\" are equal\n");
            }
            else
            {
                Console.WriteLine($"Strings \"{sVar}\" and \"{name}\" are not equal\n");
            }

            Console.WriteLine(sVar.Contains("Hello").ToString() + "\n"); //Contains

            Console.WriteLine(sVar.Substring(0, 5) + "\n"); //Substring

            Console.WriteLine(sVar.Insert(7, "incredible ") + "\n"); //Insert

            Console.WriteLine(sVar.Replace("Hello", "Goodbye") + "\n"); //Replace

            //Задание 1.6

            string emptyStr = "";
            string nullStr = null;

            Console.WriteLine("empty and null strings are null or empty? {0}, {1}.",
                string.IsNullOrEmpty(emptyStr), string.IsNullOrEmpty(nullStr)); // IsNullOrEmpty

            //Задание 1.7

            //не работает, т.к при определении переменной компилятор сам выбирает наиболее подходящий тип

            //Задание 1.8

            int? nullable = null;
            Console.WriteLine($"Value of nullable variable is {nullable}\n"); // Ничего не выводит

            Console.WriteLine(nullable.GetValueOrDefault(10)); // Если null, то по умолчанию значение 10

            #endregion

            #region Продвинутый уровень

            //Задание 2.1

            TupleOutput((5, 6));

            void TupleOutput((int a, int b) tuple)  // Передача кортежа функции в качестве аргумента
            {
                Console.WriteLine($"First variable is {tuple.a}, second is {tuple.b}\n");
            }

            //Задание 2.2

            (string, int, double) tuple1 = ("tuple", 5, 7.1);
            string str2 = tuple1.Item1; // Распаковка кортежа в строку
            (_, int num1, _) = tuple1; // Использование пустых переменных

            Console.WriteLine($"String {str2}, number {num1}\n");

            //Задание 2.3

            CheckedFun();
            UncheckedFun();

            void CheckedFun()
            {
                checked
                {
                    int a = 2147483647 + 1; //Переполнение
                    Console.WriteLine(a + "\n");
                }
            }

            void UncheckedFun()
            {
                unchecked
                {
                    int b = 2147483647 + 1; //Тоже преполнение, но не проверяется
                    Console.WriteLine(b + "\n");
                }
            }

            #endregion

            // Задание 3.1

            using (Example obj = new Example(1))
            {
                Console.WriteLine($"state: {obj.GetState()}\n");
            }


        }
    }

    class Example : IDisposable
    {
        private readonly int _state;

        public Example(int state)
        {
            _state = state;
        }

        public int GetState() => _state;

        public void Dispose()
        {

        }
    }
}
