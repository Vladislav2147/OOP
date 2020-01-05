using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR11
{
    partial class Shop //Поля и свойства
    {
        private static int counter; //Статическое поле

        public readonly string country = "Belarus";

        public static int NumberOfShops() //Статический метод
        {
            return counter;
        }

        private string _name;
        private string _adress;
        private int _numberOfEmployees;


        public string City //Автоматическое свойство с параметром
        {
            get;
            set;
        } = "Minsk";
        public string Name //Get only свойство
        {
            get
            {
                return _name;
            }
        }
        public string Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
            }
        }
        public int NumberOfEmployees
        {
            get
            {
                return _numberOfEmployees;
            }
            set
            {
                if (value >= 0)
                {
                    _numberOfEmployees = value;
                }
                else
                {
                    Console.WriteLine("Invalid value.");
                }
            }
        }




    }

    partial class Shop //Конструкторы и методы
    {
        public Shop() //Конструктор по умолчанию
        {
            _name = "default";
            _adress = "default";
            _numberOfEmployees = 0;
            counter++;
        }
        public Shop(string name, string adress, int numberOfEmployees) //Конструктор с параметрами
        {
            _name = name;
            _adress = adress;
            if (numberOfEmployees >= 0)
            {
                _numberOfEmployees = numberOfEmployees;
            }
            else
            {
                Console.WriteLine("Invalid value.");
            }
            counter++;
        }
        static Shop() //Статический конструктор
        {
            counter = 0;
            Console.WriteLine("Static Constructor");
        }
        public Shop(Shop CopyObject) //Конструктор копирования
        {
            _name = CopyObject.Name;
            _adress = CopyObject.Adress;
            _numberOfEmployees = CopyObject.NumberOfEmployees;
            counter++;
        }

        public override string ToString() //Переопределение ToString
        {
            return "Type: " + base.ToString() + " " + _name + " " + _adress + " " + _numberOfEmployees;
        }

        public override bool Equals(object obj) //Переопределение Equals
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            Shop shop = (Shop)obj;
            return (this.Name == shop.Name && this.Adress == shop.Adress && this.NumberOfEmployees == shop.NumberOfEmployees);
        }

        public override int GetHashCode() //Переопределение GetHashCode
        {
            // 269 или 47 простые
            int hash;
            hash = string.IsNullOrEmpty(Name) ? 0 : Name.GetHashCode();
            hash = (hash * 47) + NumberOfEmployees.GetHashCode();
            return hash;
        }

        ~Shop() //Деструктор
        {
            Console.WriteLine("Shop has been destroyed.");
        }
    }

}
