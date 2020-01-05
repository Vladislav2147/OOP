using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.IO;
namespace LR14
{
    class Program
    {
        static void Main(string[] args)
        {
            Lion lion1 = new Lion("Africa", "lion1");
            Shark fish1 = new Shark("Bikini Bottom", "shark1");

            Console.WriteLine("Binary:");
            MyBinaryFormatter.Serialize(lion1);
            Lion lion2 = MyBinaryFormatter.Deserialize() as Lion;
            Console.WriteLine(lion2);

            MyBinaryFormatter.Serialize(fish1);
            Console.WriteLine(MyBinaryFormatter.Deserialize() as Fish);

            Console.WriteLine("Soap:");
            MySoapFormatter.Serialize(lion1);
            Console.WriteLine(MySoapFormatter.Deserialize() as Lion);

            Console.WriteLine("JSON:");
            MyJSONFormatter<Lion>.Serialize(lion1);
            Console.WriteLine(MyJSONFormatter<Lion>.Deserialize());

            Console.WriteLine("XML:");
            MyXMLFormatter<Lion>.Serialize(lion1);
            Console.WriteLine(MyXMLFormatter<Lion>.Deserialize());

            Container container = new Container();
            container.lions = new List<Lion>() { lion1, new Lion("Uganda", "lion2"), new Lion("Savannah", "Simba"), new Lion("Cartoon", "King") };
            MyXMLFormatter<Container>.Serialize(container);
            Container returned = MyXMLFormatter<Container>.Deserialize() as Container;

            Console.WriteLine("Lions from xml:");
            foreach(Lion lion in returned.lions)
            {
                Console.WriteLine(lion);
            }

            //XPath

            XmlDocument xml = new XmlDocument();
            xml.Load(@"..\..\..\xml.xml");
            XmlElement xmlRoot = xml.DocumentElement;
            Console.WriteLine($"Lion Simba by name: {xmlRoot.SelectSingleNode("//Lion[_name='Simba']").InnerText}");
            Console.WriteLine($"Second lion in xml: {xmlRoot.SelectSingleNode("//Lion[2]").InnerText}");

            //LINQ to XML

            XDocument xmldoc = XDocument.Load(@"..\..\..\xml.xml");
            var lions1 = from xe in xmldoc.Element("Container").Element("lions").Elements("Lion")
                        where xe.Element("_name").Value.StartsWith("l")
                        select new Lion
                        {
                            _location = xe.Element("_location").Value,
                            _name = xe.Element("_name").Value
                        };
            Console.WriteLine("Lions with names start with 'l'");
            foreach(Lion lion in lions1)
            {
                Console.WriteLine(lion);
            }
            var lions2 = from xe in xmldoc.Element("Container").Element("lions").Elements("Lion")
                        where xe.Element("_location").Value == "Uganda"
                        select new Lion
                        {
                            _location = xe.Element("_location").Value,
                            _name = xe.Element("_name").Value
                        };
            Console.WriteLine("Lions from Uganda:");
            foreach(Lion lion in lions2)
            {
                Console.WriteLine(lion);
            }
        }
    }

}
