using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace LR10
{
    class Program
    {
        static void Main(string[] args)
        {

            //-------------Задание 1-------------

            ArrayList myCollection = new ArrayList();
            Random random = new Random();
            for(int i = 0; i < 5; i++)
            {
                myCollection.Add(random.Next(0, 10));
            }
            myCollection.Add("String");
            myCollection.Add(new Student("Vladislav", "Shichko", 7.5));
            Console.WriteLine(myCollection.Contains("String")); //true
            myCollection.Remove("String");
            Console.WriteLine($"Количество элементов в коллекции: {myCollection.Count}");
            foreach(object obj in myCollection)
            {
                Console.WriteLine(obj);
            }

            //-------------Задание 2-------------

            HashSet<long> hashset= new HashSet<long>();
            for(int i = 0; i < 10; i++)
            {
                hashset.Add(random.Next(0, 100));
            }

            hashset.Show();
            try
            {
                hashset.DeleteLastn(5);
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            hashset.Add(21);
            Console.WriteLine();
            hashset.Show();

            SortedList<int, long> pairs = new SortedList<int, long>();
            hashset.ToSortedList(pairs);
            pairs.Show();
            Console.WriteLine(pairs.ContainsValue(21));

            //-------------Задание 3-------------

            Lion lion = new Lion("africa", "lion");
            Parrot parrot = new Parrot("jungles", "parrot");
            Tiger tiger = new Tiger("asia", "tiger");
            Owl owl = new Owl("europe", "owl");

            HashSet<Animal> animalsHashSet = new HashSet<Animal>();
            animalsHashSet.Add(lion);
            animalsHashSet.Add(parrot);
            animalsHashSet.Add(tiger);
            animalsHashSet.Add(owl);
            animalsHashSet.Show();

            SortedList<int, Animal> animalsSortedList = new SortedList<int, Animal>();
            animalsHashSet.ToSortedList(animalsSortedList);
            animalsSortedList.Show();

            //-------------Задание 4-------------

            ObservableCollection<Animal> animals = new ObservableCollection<Animal>();
            animals.CollectionChanged += Operations.OnCollectionChange;
            animals.Add(lion);
            animals.Add(parrot);
            animals.Add(tiger);
            animals.Add(owl);
            animals.RemoveAt(1);
            foreach(Animal animal in animals)
            {
                Console.WriteLine(animal);
            }
        }
    }
    static class Operations //Методы расширения
    {
        static public void ToSortedList<T>(this HashSet<T> hashset, SortedList<int, T> pairs) //Преобразование таблицы уникальных значений в сортированный список
        {
            foreach(T element in hashset)
            {
                pairs.Add(element.GetHashCode(), element);
            }
        }
        static public void Show<T>(this HashSet<T> hashset) //Вывод на консоль
        {
            foreach(T element in hashset)
            {
                Console.WriteLine(element);
            }
        }
        static public void Show<TKey, TValue>(this SortedList<TKey, TValue> pairs)
        {
            foreach(KeyValuePair<TKey, TValue> keyValuePair in pairs)
            {
                Console.WriteLine(keyValuePair.Key + "\t" + keyValuePair.Value);
            }
        }
        static public void DeleteLastn<T>(this HashSet<T> hashset, int n) //Удаление последних N элементов
        {
            if(n < 0 || n > hashset.Count)
            {
                throw new IndexOutOfRangeException("Invalid index exception");
            }
            for(int i = 0; i < n; i++)
            {
                hashset.Remove(hashset.Last());
            }
        }
        public static void OnCollectionChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Console.WriteLine("Добавлен новый объект: " + e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Console.WriteLine("Объект " + e.OldItems[0] + " удален");
                    break;

            }

        }
    }
    class Student
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public double AverageGrade { get; set; }
        public Student(string firstName, string secondName, double averageGrade)
        {
            FirstName = firstName;
            SecondName = secondName;
            AverageGrade = averageGrade;
        }
        public override string ToString()
        {
            return "Name: " + FirstName + " " + SecondName + " \tAverage grade: " + AverageGrade;
        }
    }
}
