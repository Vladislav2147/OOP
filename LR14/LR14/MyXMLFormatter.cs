using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Serialization;

namespace LR14
{
    static class MyXMLFormatter<T> where T: class, new()
    {
        private static string pathToFile = @"..\..\..\xml.xml";
        public static void Serialize(T obj)
        {
            using (FileStream fs = new FileStream(pathToFile, FileMode.Create))
            {
                try
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(T));
                    formatter.Serialize(fs, obj);
                }
                catch (SerializationException)
                {
                    Console.WriteLine($"Object {obj} is not serializable!");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
        }

        public static T Deserialize()
        {
            using (FileStream fs = new FileStream(pathToFile, FileMode.Open))
            {
                try
                {
                    XmlSerializer formatter = new XmlSerializer(typeof(T));
                    return formatter.Deserialize(fs) as T;
                }
                catch (SerializationException)
                {
                    Console.WriteLine($"Object is not serializable!");
                    return null;
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                    return null;
                }
            }
        }
    }
}
