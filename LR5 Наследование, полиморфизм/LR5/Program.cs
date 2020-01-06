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
            Lion lion1 = new Lion("Africa", "lion1");
            Tiger tiger1 = new Tiger("Asia", "tiger1");
            Owl owl1 = new Owl("Europe", "owl1");
            Parrot parrot1 = new Parrot("Jungle", "parrot1");
            Shark shark1 = new Shark("Pacific Ocean", "shark1");
            lion1.Breathe(); //Вызов переопределенного метода от базового класса
            ((IBreathable)lion1).Breathe(); //Вызов метода интерфейса
            Console.WriteLine(lion1 is Animal); //true
            Console.WriteLine(lion1 is Fish); //false

            Animal lion2 = lion1 as Animal;
            Console.WriteLine(lion2.ToString());
            Animal[] animals = new Animal[5];
            animals[0] = lion1;
            animals[1] = tiger1;
            animals[2] = owl1;
            animals[3] = parrot1;
            animals[4] = shark1;

            Printer printer = new Printer();

            foreach(Animal animal in animals)
            {
                printer.IAmPrinting(animal);
            }
            
        }
    }

    interface ITalkable
    {
        void SaySomething();
    }

    interface IBreathable
    {
        void Breathe();
    }

    class Printer
    {
        public void IAmPrinting(object obj)
        {
            if(obj is Animal)
            {
                Console.WriteLine(obj.ToString());
            }
            else
            {
                Console.WriteLine("Object is not an animal");
            }
            if(obj is ITalkable)
            {
                ITalkable animal = (ITalkable)obj;
                animal.SaySomething();
            }
            else
            {
                Console.WriteLine("Object is not talkable");
            }
        }
    }

    abstract class Animal
    {
        protected string _location;
        protected string _name;
        public abstract void Breathe();
        public virtual void Move()
        {
            Console.WriteLine("Move");
        }
        public override string ToString()
        {
            return _location + " " + _name;
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

    abstract class Mammal : Animal
    {
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
        public override void Move()
        {
            Console.WriteLine("Swim");
        }
        public override void Breathe()
        {
            Console.WriteLine("Breathe through gills");
        }
    }

    class Lion : Mammal, ITalkable, IBreathable
    {
        public Lion(string Location, string Name)
        {
            _location = Location;
            _name = Name;
        }
        public void SaySomething()
        {
            Console.WriteLine("Meow");
        }
        void IBreathable.Breathe()
        {
            Console.WriteLine("Breathe through nose but interface");
        }
    }

    class Tiger : Mammal, ITalkable
    {
        public Tiger(string Location, string Name)
        {
            _location = Location;
            _name = Name;
        }
        public void SaySomething()
        {
            Console.WriteLine("Meow");
        }
        
    }

    class Owl : Bird, ITalkable
    {
        public Owl(string Location, string Name)
        {
            _location = Location;
            _name = Name;
        }
        public void SaySomething()
        {
            Console.WriteLine("hoo-hoo");
        }
    }

    class Parrot : Bird, ITalkable
    {
        public Parrot(string Location, string Name)
        {
            _location = Location;
            _name = Name;
        }
        public void SaySomething()
        {
            Console.WriteLine("Chirp");
        }
    }

    sealed class Shark: Fish
    {
        public Shark(string Location, string Name)
        {
            _location = Location;
            _name = Name;
        }
    }


}
