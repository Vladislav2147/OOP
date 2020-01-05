using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR10
{
    interface ITalkable
    {
        void SaySomething();
    }

    interface IBreathable
    {
        void Breathe();
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

    sealed class Shark : Fish
    {
        public Shark(string Location, string Name)
        {
            _location = Location;
            _name = Name;
        }
    }
}
