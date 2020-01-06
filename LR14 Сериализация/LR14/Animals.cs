using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
namespace LR14
{
    [XmlRoot]
    public class Container
    {
        public List<Lion> lions;
    }
    interface ITalkable
    {
        void SaySomething();
    }

    interface IBreathable
    {
        void Breathe();
    }
    [Serializable][DataContract]
    public abstract class Animal
    {
        public Animal()
        {

        }
        [DataMember]
        public string[] strings = new string[] { "a", "b", "c" };
        [DataMember]
        public string _location;
        [DataMember]
        public string _name;
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
    [Serializable]
    public abstract class Mammal : Animal
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
    [Serializable]
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
    [Serializable]
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
    [Serializable]
    public class Lion : Mammal, ITalkable, IBreathable
    {
        public Lion()
        {

        }
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
    [Serializable]
    sealed class Shark : Fish
    {
        public Shark()
        {

        }
        public Shark(string Location, string Name)
        {
            _location = Location;
            _name = Name;
        }
    }
    [Serializable]
    class Tiger : Mammal, ITalkable
    {
        public Tiger()
        {

        }
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
    [Serializable]
    class Owl : Bird, ITalkable
    {
        public Owl()
        {

        }
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
    [Serializable]
    class Parrot : Bird, ITalkable
    {
        public Parrot()
        {

        }
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

}
