using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR5
{
    public abstract class Animal
    {
        protected int _year;
        protected double _weight;
        protected string _location;
        protected string _name;

        public double Weight
        {
            get
            {
                return _weight;
            }
        }

        public int Year
        {
            get;
        }
        public Animal(string Location, string Name, double Weight, int Year)
        {
            _location = Location;
            _name = Name;
            _weight = Weight;
            if (Year < 1800 || Year > DateTime.Now.Year)
            {
                throw new YearException("Year is uncorrect: ", Year);
            }
            else
            {
                _year = Year;
            }
        }
        public abstract void Breathe();
        public virtual void Move()
        {
            Console.WriteLine("Move");
        }

        public override string ToString()
        {
            return "From: " + _location + "\t" + "Name: " + _name + "\t" + "Weight: " + _weight + "kg" + "\t" + "Year: " + _year;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            Animal animal = (Animal)obj;
            return (this._name == animal._name && this._name == animal._location);
        }
        public override int GetHashCode()
        {
            int hash = 269;
            hash = string.IsNullOrEmpty(_name) ? 0 : _name.GetHashCode();
            hash = (hash * 47) + _location.GetHashCode();
            return hash;
        }
    }

    interface ITalkable
    {
        void SaySomething();
    }

    abstract class Mammal : Animal
    {
        public Mammal(string Location, string Name, double Weight, int Year) : base(Location, Name, Weight, Year)
        {

        }
        public override void Move()
        {
            Console.WriteLine("Walk");
        }
        public override void Breathe()
        {
            Console.WriteLine("Breathe through nose");
        }
    }

    abstract class Bird : Animal
    {
        public Bird(string Location, string Name, double Weight, int Year) : base(Location, Name, Weight, Year)
        {

        }
        public override void Move()
        {
            Console.WriteLine("Fly");
        }
        public override void Breathe()
        {
            Console.WriteLine("Breathe through beak");
        }
    }

    abstract class Fish : Animal
    {
        public Fish(string Location, string Name, double Weight, int Year) : base(Location, Name, Weight, Year)
        {

        }
        public override void Move()
        {
            Console.WriteLine("Swim");
        }
        public override void Breathe()
        {
            Console.WriteLine("Breathe through gills");
        }
    }

    class Lion : Mammal, ITalkable
    {
        public Lion(string Location, string Name, double Weight, int Year) : base(Location, Name, Weight, Year)
        {

        }
        public void SaySomething()
        {
            Console.WriteLine("Meow");
        }
    }

    class Tiger : Mammal, ITalkable
    {
        public Tiger(string Location, string Name, double Weight, int Year) : base(Location, Name, Weight, Year)
        {

        }
        public void SaySomething()
        {
            Console.WriteLine("Meow");
        }

    }

    class Owl : Bird, ITalkable
    {
        public Owl(string Location, string Name, double Weight, int Year) : base(Location, Name, Weight, Year)
        {

        }
        public void SaySomething()
        {
            Console.WriteLine("hoo-hoo");
        }
    }

    class Parrot : Bird, ITalkable
    {
        public Parrot(string Location, string Name, double Weight, int Year) : base(Location, Name, Weight, Year)
        {

        }
        public void SaySomething()
        {
            Console.WriteLine("Chirp");
        }
    }

    sealed class Shark : Fish
    {
        public Shark(string Location, string Name, double Weight, int Year) : base(Location, Name, Weight, Year)
        {

        }
    }

    public class Zoo //Класс-контейнер 
    {
        readonly Creator creator = new Creator("Vladislav", "Shichko");
        protected List<Animal> _animals;
        public Zoo()
        {
            _animals = new List<Animal>();
        }

        public List<Animal> Animals
        {
            get
            {
                return _animals;
            }
            set
            {
                _animals = value;
            }
        }

        public void Add(Animal animal)
        {
            _animals.Add(animal);
        }

        public void Remove(Animal animal)
        {
            _animals.Remove(animal);
        }

        public void Show()
        {
            foreach (Animal animal in _animals)
            {
                Console.WriteLine(animal.ToString());
            }
        }

        public void CreatorInfo() //Информация о создателе класса
        {
            Console.WriteLine("Creator:\n" + creator.firstName + " " + creator.secondName);
        }
    }

    public static class ZooController //Класс-контроллер
    {

        enum Kinds //Перечисление 1-5
        {
            Lion = 1, Tiger, Owl, Parrot, Shark
        }

        public static double AverageWeight(Zoo zoo) //Возвращает средний вес заданного вида 
        {
            int n = 0;
            int choice;
            double totalWeight = 0;
            double averageWeight = 0;
            Console.WriteLine("Input kind of animal (1 - lion, 2 - tiger, 3 - owl, 4 - parrot, 5 - shark):");
            Debug.Assert(Int32.TryParse(Console.ReadLine(), out choice), "String is not integer number"); //Assert

            if (choice < 1 || choice > Enum.GetNames(typeof(Kinds)).Length)
            {
                throw new ChoiceOutOfRange("Invalid value");
            }

            switch (choice)
            {
                case (int)Kinds.Lion:
                    foreach (Animal animal in zoo.Animals)
                    {
                        if (animal is Lion)
                        {
                            totalWeight += animal.Weight;
                            n++;
                        }
                    }
                    break;
                case (int)Kinds.Tiger:
                    foreach (Animal animal in zoo.Animals)
                    {
                        if (animal is Tiger)
                        {
                            totalWeight += animal.Weight;
                            n++;
                        }
                    }
                    break;
                case (int)Kinds.Owl:
                    foreach (Animal animal in zoo.Animals)
                    {
                        if (animal is Owl)
                        {
                            totalWeight += animal.Weight;
                            n++;
                        }
                    }
                    break;
                case (int)Kinds.Parrot:
                    foreach (Animal animal in zoo.Animals)
                    {
                        if (animal is Parrot)
                        {
                            totalWeight += animal.Weight;
                            n++;
                        }
                    }
                    break;
                case (int)Kinds.Shark:
                    foreach (Animal animal in zoo.Animals)
                    {
                        if (animal is Shark)
                        {
                            totalWeight += animal.Weight;
                            n++;
                        }
                    }
                    break;
            }
            if (n == 0)
            {
                throw new DivideByZeroException("Zero devision");
            }

            averageWeight = totalWeight / n;
            return averageWeight;
        }

        public static int AmountOfPredatoryBirds(Zoo zoo) //Возвращает кол-во хищных птиц 
        {
            int n = 0;
            foreach (Animal animal in zoo.Animals)
            {
                if (animal is Owl)
                {
                    n++;
                }
            }
            return n;
        }

        public static void ShowSortingByYear(Zoo zoo) //Сортировка по году рождения 
        {
            AnimalComparer a = new AnimalComparer();
            zoo.Animals.Sort(a);
            foreach (Animal animal in zoo.Animals)
            {
                Console.WriteLine(animal.ToString());
            }
        }
    }

    public class AnimalComparer : IComparer<Animal> //Класс-сравниватель
    {
        public int Compare(Animal animal1, Animal animal2)
        {
            if (animal1.Year > animal2.Year)
                return 1;
            if (animal1.Year < animal2.Year)
                return -1;
            return 0;
        }
    }
    struct Creator //Структура 
    {
        public string firstName;
        public string secondName;
        public Creator(string Name1, string Name2)
        {
            firstName = Name1;
            secondName = Name2;
        }
    }
    class YearException : ArgumentException
    {
        public int Value { get; }
        public YearException(string message, int value) : base(message)
        {
            Value = value;
        }
    }

    class DivideByZeroException : Exception
    {
        public DivideByZeroException(string message) : base(message)
        {

        }
    }

    class ChoiceOutOfRange : Exception
    {
        public ChoiceOutOfRange(string message) : base(message)
        {

        }
    }
}
