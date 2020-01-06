using System;
using System.IO;
using System.Reflection;

namespace LR13
{
    static class SVSDirInfo
    {
        //Вывод на консоль информации о директории
        public static void DirInfo(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            try
            {
                Console.WriteLine($"Directory name: {directory.Name}");
                Console.WriteLine($"Amount of files: {directory.GetFiles().Length}");
                Console.WriteLine($"Time of creation: {directory.CreationTime}");
                Console.WriteLine($"Parent directory: {directory.Parent}");
                SVSLog.Write(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, path);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Directory not found in method {MethodInfo.GetCurrentMethod().Name}!");
            }
            catch (Exception)
            {
                Console.WriteLine($"Error in method {MethodInfo.GetCurrentMethod().Name}!");
            }
        }
    }
}
