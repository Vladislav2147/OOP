using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR12
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
            _year = Year;
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
        public void DoParrotThings(string str1, string str2)
        {
            
        }

    }

    sealed class Shark
    {
        public Shark()
        {

        }
        public void DoSharkThings(string str1, string str2)
        {
            Console.WriteLine($"{str1} {str2}");
        }
    }
}
