using System;
using System.IO;
using System.Reflection;

namespace LR13
{
    class SVSFileInfo
    {
        //Вывод на консоль абсолютного пути к файлу
        public static void fullPath(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                Console.WriteLine($"Full path to file {file.Name}: {file.DirectoryName}");
                SVSLog.Write(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, path);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found in method {MethodInfo.GetCurrentMethod().Name}!");
            }
            catch (Exception)
            {
                Console.WriteLine($"Error in method {MethodInfo.GetCurrentMethod().Name}!");
            }

        }
        //Вывод информации о файле на консоль
        public static void FileInfo(string path)
        {
            try
            {
                FileInfo file = new FileInfo(path);
                Console.WriteLine($"Name: {file.Name}");
                Console.WriteLine($"Size: {file.Length} bytes");
                Console.WriteLine($"Extension: {file.Extension}");
                SVSLog.Write(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, path);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found in method {MethodInfo.GetCurrentMethod().Name}!");
            }
            catch (Exception)
            {
                Console.WriteLine($"Error in method {MethodInfo.GetCurrentMethod().Name}!");
            }
        }
        //Вывод на консоль времени создания файла
        public static void WhereWasCreated(string path)
        {
            
            try
            {
                FileInfo file = new FileInfo(path);
                Console.WriteLine($"Creation time of file {file.Name}: {file.CreationTime}");
                SVSLog.Write(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, path);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found in method {MethodInfo.GetCurrentMethod().Name}!");
            }
            catch (Exception)
            {
                Console.WriteLine($"Error in method {MethodInfo.GetCurrentMethod().Name}!");
            }
        }
    }
}
