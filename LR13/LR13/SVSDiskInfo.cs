using System;
using System.IO;
using System.Reflection;

namespace LR13
{
    static class SVSDiskInfo
    {
        private static DriveInfo[] drives = DriveInfo.GetDrives();
        //Вывод cвободного пространства диска на консоль
        public static void DiskFreeSpace(string nameOfDisk)
        {
            foreach (DriveInfo drive in drives)
            {
                if (drive.Name == nameOfDisk)
                {
                    Console.WriteLine($"Total free space on disk {nameOfDisk}: {drive.TotalFreeSpace} bytes");
                    break;
                }
            }
            SVSLog.Write(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, nameOfDisk);
        }
        //Вывод на консоль информации о файловой системе
        public static void FileSystemInfo()
        {
            string fileSystemInfo = null;
            long totalFreeSpace = 0;
            foreach (DriveInfo drive in drives)
            {
                fileSystemInfo += $"{drive.Name} {drive.DriveType}\t";
                totalFreeSpace += drive.TotalFreeSpace;
            }
            Console.WriteLine($"Format Info: {fileSystemInfo}");
            Console.WriteLine($"Total free space: {totalFreeSpace}");
            SVSLog.Write(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, "root");
        }
        //Вывод на консоль информации о диске
        public static void AllDiskInfo()
        {
            foreach(DriveInfo drive in drives)
            {
                Console.WriteLine($"\nName: {drive.Name}");
                Console.WriteLine($"Total size: {drive.TotalSize}");
                Console.WriteLine($"Total free space: {drive.TotalFreeSpace}");
                Console.WriteLine($"Volume label: {drive.VolumeLabel}");
            }
            SVSLog.Write(MethodInfo.GetCurrentMethod().DeclaringType.Name, MethodInfo.GetCurrentMethod().Name, "root");
        }
        
    }
}
