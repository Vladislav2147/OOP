using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR9
{
    class Program
    {
        static void Main(string[] args)
        {
            

            Programmer vladislav = new Programmer();
            
            ProgrammingLanguage cpp = new ProgrammingLanguage("c++");
            ProgrammingLanguage cs = new ProgrammingLanguage("c#");
            Programmer.RenameEventHandler renameEventHandler = cpp.OnChangeName;
            renameEventHandler(null, new RenameEventArgs("message"));
            cpp.Show();
            cs.Show();

            vladislav.ChangeName += cpp.OnChangeName; //Регистрация обработчика
            vladislav.Rename("c--"); //Инициирования события
            vladislav.ChangeName -= cpp.OnChangeName;

            vladislav.ChangeName += cs.OnChangeName;
            vladislav.Rename("c!");

            vladislav.AddProperty += cpp.OnAddProperty;
            vladislav.AddProperty += cs.OnAddProperty;

            vladislav.AddNewProperty("Работать");
            vladislav.AddNewProperty("Быть понятным");

            cpp.Show();
            cs.Show();

            string str = "My string";
            MyString.StringOperation(ref str, str1 => str1.ToLower());
            Console.WriteLine(str);
            MyString.StringOperation(ref str, str1 => str1.ToUpper());
            Console.WriteLine(str);
            MyString.StringOperation(ref str, str1 => str1.Substring(0, str.IndexOf(" ")));
            Console.WriteLine(str);
            str += " string";
            MyString.StringOperation(ref str, str1 => str1.Replace(" ", ""));
            Console.WriteLine(str);
            MyString.StringOperation(ref str, '1', (str1, ch) => str1 + char.ToString(ch));
            Console.WriteLine(str);

        }
    }

    class Programmer //Класс-отправитель
    {
        public delegate void RenameEventHandler(object sender, RenameEventArgs e); //Делегаты, задающие сигнатуру обработчиков событий
        public delegate void AddProperyEventHandler(object sender, PropertyEventArgs e);
        public event RenameEventHandler ChangeName; //События
        public event AddProperyEventHandler AddProperty;
        public void Rename(string newName) //Методы, инициируищее события
        {
            ChangeName?.Invoke(this, new RenameEventArgs(newName)); //nullable, чтобы не было NullReferenceException в случае отсутствия обработчиков
        }
        public void AddNewProperty(string newProperty)
        {
            AddProperty?.Invoke(this, new PropertyEventArgs(newProperty));
        }

    }
    class ProgrammingLanguage //Класс-получатель
    { 
        public string Name { get; set; }
        public List<string> properties = new List<string>();
        public ProgrammingLanguage(string name)
        {
            Name = name;
        }
        public void OnChangeName(object sender, RenameEventArgs e) //Обработчики событий (сигнатура соответствует делегату)
        {
            Name = e.NewName;
            Console.WriteLine("Произошло изменение имени");
        }
        public void OnAddProperty(object sender, PropertyEventArgs e)
        {
            properties.Add(e.NewProperty);
            Console.WriteLine("Произошло добавление свойства");
        }
        public void Show()
        {
            Console.WriteLine("Язык программирования:");
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine("Свойства:");
            foreach(string property in properties)
            {
                Console.WriteLine(property);
            }

        }
    }

    class RenameEventArgs : EventArgs //Аргументы событий (производные от EventArgs)
    {
        public string NewName { get; }
        public RenameEventArgs(string newname)
        {
            NewName = newname;
        }
    }
    class PropertyEventArgs : EventArgs
    {
        public string NewProperty { get; }
        public PropertyEventArgs(string newproperty)
        {
            NewProperty = newproperty;
        }
    }

    static class MyString
    {
        public static void StringOperation(ref string str, Func<string, string> func)
        {
            str = func(str);
        }
        public static void StringOperation(ref string str, char ch, Func<string, char, string> func)
        {
            str = func(str, ch);
        }        
    }
}
