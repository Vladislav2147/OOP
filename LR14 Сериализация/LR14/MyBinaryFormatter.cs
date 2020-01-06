using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace LR14
{
    static class MyBinaryFormatter
    {
        private static string pathToFile = @"..\..\..\bin.bin";
        public static void Serialize(object obj)
        {
            
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(pathToFile, FileMode.Create))
            {
                try
                {
                    formatter.Serialize(fs, obj);
                }
                catch(SerializationException)
                {
                    Console.WriteLine($"Object {obj} is not serializable!");
                }
                catch(Exception)
                {
                    Console.WriteLine("Error");
                }
            }
        }
        public static object Deserialize()
        {
            using (FileStream fs = new FileStream(pathToFile, FileMode.Open))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(fs);
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
