using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace LR13
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SVSDiskInfo.DiskFreeSpace(@"C:\");
            SVSDiskInfo.FileSystemInfo();
            SVSDiskInfo.AllDiskInfo();
            
            string path = @"..\..\..\SVSlog.txt";
            SVSFileInfo.FileInfo(path);
            SVSFileInfo.WhereWasCreated(path);
            SVSDirInfo.DirInfo(Directory.GetCurrentDirectory());
            SVSFileManager.CheckFiles(@"C:\");
            SVSFileManager.CopyDirectory(@"F:\2 курс\ООП\LR13\LR13\obj\Debug", ".txt");
            SVSFileManager.ZipDirectory(@"..\..\..\SVSInspect\SVSFiles", @"..\..\..\SVSFiles\");
            DateTime dateTime;
            if(DateTime.TryParse(Console.ReadLine(), out dateTime))
            {
                Console.WriteLine("Found by date:");
                SVSLog.FindByDate(dateTime);
                Console.WriteLine("Found by range:");
                SVSLog.FindByRange(dateTime, DateTime.Now);
            }
            else
            {
                Console.WriteLine("Incorrect input!");
            }
            Console.WriteLine("Found by name:");
            SVSLog.Find("SVSDirInfo");
            Console.WriteLine($"Amount of strings in SVSlog.txt: {SVSLog.Count}");
            SVSLog.DeleteRange(new DateTime(2019, 12, 15), new DateTime(2019, 12, 17, 18, 35, 0));
        }
    }
}
