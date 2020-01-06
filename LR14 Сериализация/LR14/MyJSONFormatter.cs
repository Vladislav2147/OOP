using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
namespace LR14
{
    class MyJSONFormatter<T> where T: class
    {
        private static string pathToFile = @"..\..\..\json.json";
        public static void Serialize(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
            using (FileStream fs = new FileStream(pathToFile, FileMode.Create))
            {
                try
                {
                    json.WriteObject(fs, obj);
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
        }
        public static T Deserialize()
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
            using (FileStream fs = new FileStream(pathToFile, FileMode.Open))
            {
                try
                {
                    return json.ReadObject(fs) as T;
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

