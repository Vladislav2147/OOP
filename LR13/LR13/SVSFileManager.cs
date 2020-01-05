using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace LR13
{
    static class SVSFileManager
    {
        //Вывод информации о файлах и директориях диска на консоль и в файл
        public static void CheckFiles(string nameOfDisk)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(nameOfDisk);
                Directory.CreateDirectory(@"..\..\..\SVSInspect");
                using (StreamWriter sw = new StreamWriter(@"..\..\..\SVSInspect\SVSdirinfo.txt", true))
                {
                    sw.WriteLine("Directories:");
                    Console.WriteLine("Directories:");
                    foreach (DirectoryInfo dir in directory.GetDirectories())
                    {
                        sw.WriteLine(dir);
                        Console.WriteLine(dir);
                    }
                    sw.WriteLine("Files:");
                    Console.WriteLine("Files:");
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        sw.WriteLine(file);
                        Console.WriteLine(file);
                    }
                }
                File.Copy(@"..\..\..\SVSInspect\SVSdirinfo.txt", @"..\..\..\SVSInspect\SVSdirinfo1.txt", true);
                File.Delete(@"..\..\..\SVSInspect\SVSdirinfo.txt");
                SVSLog.Write(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, nameOfDisk);
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
        //Копирование всех файлов заданного расширения из указанного каталога
        public static void CopyDirectory(string path, string extension)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            Directory.CreateDirectory(@"..\..\..\SVSFiles");
            try
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    if (file.Extension == extension)
                    {
                        file.CopyTo(@"..\..\..\SVSFiles\" + file.Name, true);
                    }
                }
                DirectoryInfo directoryInfo = new DirectoryInfo(@"..\..\..\SVSFiles");
                if (Directory.Exists(@"..\..\..\SVSInspect\SVSFiles"))
                {
                    Directory.Delete(@"..\..\..\SVSInspect\SVSFiles", true);
                }
                directoryInfo.MoveTo(@"..\..\..\SVSInspect\SVSFiles");
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
        //Архивирование директории
        public static void ZipDirectory(string source, string dest)
        {
            try
            {
                if (File.Exists(@"..\..\..\files.zip"))
                {
                    File.Delete(@"..\..\..\files.zip");
                }
                ZipFile.CreateFromDirectory(source, @"..\..\..\files.zip");
                ZipFile.ExtractToDirectory(@"..\..\..\files.zip", dest);
                SVSLog.Write(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, source);
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
