using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR11
{
    class Program
    {
        static void Main(string[] args)
        {
            //----------------------------------Задание 1----------------------------------

            string[] months = new string[] {"january", "february", "march", "april", "may", "june",
                                            "jule", "august", "september", "october", "november", "december"};
            int n = 5;
            IEnumerable<string> nLengthMonth = from month in months
                                               where month.Length == n
                                               select month;
            Console.WriteLine($"Months with length = {n}");
            foreach (string month in nLengthMonth)
            {
                Console.WriteLine(month);
            }

            IEnumerable<string> summerOrWinter = months.Take(2).Concat(months.Skip(5).Take(3)).Concat(months.Skip(11).Take(1));
            Console.WriteLine("Winter and summer months");
            foreach (string month in summerOrWinter)
            {
                Console.WriteLine(month);
            }
            IEnumerable<string> containsU = from month in months
                                            where month.Contains("u") && month.Length >= 4
                                            select month;
            Console.WriteLine("Months contains u");
            foreach (string month in containsU)
            {
                Console.WriteLine(month);
            }

            //----------------------------------Задание 2, 4, 5----------------------------------

            List<Shop> shops = new List<Shop>();
            shops.Add(new Shop("evroopt", "mayakovskogo 23", 245));
            shops.Add(new Shop("sosedi", "lenina 10", 68));
            shops.Add(new Shop("evroopt", "kirova 8", 21));
            shops.Add(new Shop("brusnichka", "rokossovskogo 37", 0));
            shops.Add(new Shop("rublevskiy", "sovetskaya 120", 47));

            Location[] locations = new Location[5];
            locations[0] = new Location("mayakovskogo 23", "Minsk");
            locations[1] = new Location("lenina 10", "Gomel");
            locations[2] = new Location("kirova 8", "Minsk");
            locations[3] = new Location("rokossovskogo 37", "Brest");
            locations[4] = new Location("sovetskaya 120", "Grodno");
            //Join
            var joinResult = shops.Join(locations, //Второй набор
                                        p => p.Adress, //Селектор первого набора
                                        t => t.Address, //Селектор второга набора
                                        (p, t) => new { Name = p.Name, NumOfEmployees = p.NumberOfEmployees, City = t.City }); //Результирующий анонимный тип

            foreach(var shop in joinResult)
            {
                Console.WriteLine($"{shop.Name}\t{shop.NumOfEmployees}\t{shop.City}");
            }
            //Запрос из 5 операций
            bool queryShop = shops
                .Where(shop => shop.Name.Length > 6)
                .OrderBy(shop => shop.Adress)
                .ThenBy(shop => shop.Name)
                .Take(3)
                .Any(shop => shop.Name.StartsWith("e"));

            Console.WriteLine(queryShop);

            //----------------------------------Задание 3----------------------------------

            int[][] arrays = new int[7][];
            arrays[0] = new int[] { 1, 2, 3, 4, 5 };
            arrays[1] = new int[] { 2, 4, 6, 8 };
            arrays[2] = new int[] { 0, -2, 1, 5 };
            arrays[3] = new int[] { 3, 4 };
            arrays[4] = new int[0];
            arrays[5] = new int[] { 16, 2 };
            arrays[6] = new int[0];

            Console.WriteLine("Множества только с четными элементами: ");
            IEnumerable<int[]> even = arrays.Where(array => array.All(element => element % 2 == 0));

            foreach (int[] array in even)
            {
                foreach (int element in array)
                {
                    Console.Write(element + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Индексы пустых множеств: ");
            IEnumerable<int> empty = arrays.Select((array, index) => new { array, index }) //Генерация анонимных типов с целью получить индекс
                                           .Where(item => item.array.Length == 0)
                                           .Select(item => item.index);
            foreach(int index in empty)
            {
                Console.WriteLine(index);
            }

            Console.WriteLine("Множества, содержащие отрицательные элементы: ");
            IEnumerable<int[]> negative = arrays.Where(array => array.Any(element => element < 0));
            foreach (int[] array in negative)
            {
                foreach (int element in array)
                {
                    Console.Write(element + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Множества с длиной в диапазоне 2..4: ");
            IEnumerable<int[]> range = arrays.Where(array => array.Length >= 2 && array.Length <= 4);
            foreach (int[] array in range)
            {
                foreach (int element in array)
                {
                    Console.Write(element + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Самое длинное множество: ");
            int[] maxLengh = arrays.OrderBy(array => array.Length).Last();
            foreach (int element in maxLengh)
            {
                Console.Write(element + " ");
            }
            Console.WriteLine();

            //yield IEnumerable
            foreach(int number in A())
            {
                Console.WriteLine(number);
            }
        }
        public static IEnumerable<int> A()
        {
            for (int i = 0; i < 10; i++)
            {
                yield return i;
            }
        }
    }
    
    class Location
    {
        public string Address { get; set; }
        public string City { get; set; }
        public Location (string address, string city)
        {
            Address = address;
            City = city;
        }

    }
}
