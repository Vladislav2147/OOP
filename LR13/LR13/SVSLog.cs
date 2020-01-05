using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace LR13
{
    static class SVSLog
    {
        //Количество записей в лог-файле
        public static int Count 
        {
            get
            {
                using (StreamReader sr = new StreamReader(@"..\..\..\SVSlog.txt"))
                {
                    int counter = 0;
                    while (!sr.EndOfStream)
                    {
                        sr.ReadLine();
                        counter++;
                    }
                    return counter;
                }
            }
        }
        //Вывод лог-файла на консоль
        public static void Show()
        {
            string[] fileStrings = File.ReadAllLines(@"..\..\..\SVSlog.txt");
            foreach (string str in fileStrings)
            {
                Console.WriteLine(str);
            }
        }
        //Запись действия пользователя в лог-файл
        public static void Write(string type, string operation, string name)
        {
            using (StreamWriter streamWriter = new StreamWriter(@"..\..\..\SVSlog.txt", true))
            {
                streamWriter.WriteLine($"{type} {operation}\t{name}\t{DateTime.Now}");
            }  
        }
        //Поиск записей в лог-файле по дате
        public static void FindByDate(DateTime date)
        {
            List<string> logData = new List<string>();
            using (StreamReader sr = new StreamReader(@"..\..\..\SVSlog.txt"))
            {
                while(!sr.EndOfStream)
                {
                    logData.Add(sr.ReadLine());
                }
                IEnumerable<string> collection = logData.Where(str => DateTime.Parse(str.Split('\t').Last()) >= date && 
                    DateTime.Parse(str.Split('\t').Last()) < date.AddDays(1));
                foreach(string result in collection)
                {
                    Console.WriteLine(result);
                }
            }
        }
        //Поиск записей в лог-файле по промежутку времени
        public static void FindByRange(DateTime from, DateTime to)
        {
            List<string> logData = new List<string>();
            using (StreamReader sr = new StreamReader(@"..\..\..\SVSlog.txt"))
            {
                while (!sr.EndOfStream)
                {
                    logData.Add(sr.ReadLine());
                }
                IEnumerable<string> collection = logData.Where(str => DateTime.Parse(str.Split('\t').Last()) >= from && 
                    DateTime.Parse(str.Split('\t').Last()) < to);

                foreach (string result in collection)
                {
                    Console.WriteLine(result);
                }
            }
        }
        //Удаление записей по промежутку
        public static void DeleteRange(DateTime from, DateTime to)
        {
            File.WriteAllLines(@"..\..\..\SVSlog.txt", File
                .ReadAllLines(@"..\..\..\SVSlog.txt")
                .Where(str => !(DateTime.Parse(str.Split('\t').Last()) >= from && DateTime.Parse(str.Split('\t').Last()) < to)));
        }
        //Поиск в записей лог-файле по ключевому слову
        public static void Find(string request)
        {
            List<string> logData = new List<string>();
            using (StreamReader sr = new StreamReader(@"..\..\..\SVSlog.txt"))
            {
                while (!sr.EndOfStream)
                {
                    logData.Add(sr.ReadLine());
                }
                IEnumerable<string> collection = logData.Where(str => str.Contains(request));
                foreach (string result in collection)
                {
                    Console.WriteLine(result);
                }
            }
        }
        //Вывод записей лог-файла за последний час
        public static void LastHour()
        {
            List<string> logData = new List<string>();
            using (StreamReader sr = new StreamReader(@"..\..\..\SVSlog.txt"))
            {
                while (!sr.EndOfStream)
                {
                    logData.Add(sr.ReadLine());
                }
                IEnumerable<string> collection = logData.Where(str => DateTime.Parse(str.Split('\t').Last()) >= DateTime.Now.AddHours(-1) && 
                    DateTime.Parse(str.Split('\t').Last()) < DateTime.Now);
                foreach (string result in collection)
                {
                    Console.WriteLine(result);
                }
            }
        }
    }
}
