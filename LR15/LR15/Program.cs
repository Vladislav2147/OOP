using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Threading;
namespace LR15
{
    class Program
    {
        static object Locker = new object();
        [Obsolete]
        static void Main(string[] args)
        {
            //Задание 1
            
            //Console.WriteLine("All Processes:");
            //foreach(Process process in Process.GetProcesses())
            //{
            //    try
            //    {
            //        Console.WriteLine($"Process name: {process.ProcessName}");
            //        Console.WriteLine($"Process ID: {process.Id}");
            //        Console.WriteLine($"Priority: {process.BasePriority}");
            //        Console.WriteLine($"Start time: {process.StartTime}");
            //        Console.WriteLine($"Total time: {process.TotalProcessorTime}");
            //        Console.WriteLine($"Is Responding: {process.Responding}");
            //    }
            //    catch(Exception)
            //    {
                    
            //    }
            //    Console.WriteLine("-------------------------------------------------------------------------------");
            //}
            
            //Задание 2

            AppDomain currentDomain = AppDomain.CurrentDomain;
            Console.WriteLine("Current domain info:");
            Console.WriteLine($"Name: {currentDomain.FriendlyName}");
            Console.WriteLine($"ID: {currentDomain.Id}");
            Console.WriteLine($"Assemblies:");
            foreach (Assembly assembly in currentDomain.GetAssemblies())
            {
                Console.WriteLine(assembly.FullName);
            }

            AppDomain newDomain = AppDomain.CreateDomain("NewDomain");
            newDomain.Load("LR14");
            Console.WriteLine("Assemblies in new domain:");
            foreach (Assembly assembly in newDomain.GetAssemblies())
            {
                Console.WriteLine(assembly.FullName);
            }
            //newDomain.ExecuteAssembly("LR14.exe");
            AppDomain.Unload(newDomain);
            
            //Задание 3

            int n = 0;
            Console.WriteLine("Input max value (min 100):");
            if(!Int32.TryParse(Console.ReadLine(), out n) || n <= 100)
            {
                n = 100;
            }
            Thread thread = new Thread(new ParameterizedThreadStart(MyThread));
            thread.Name = "thread1";
            thread.Start(n);
            Thread.Sleep(1000);
            thread.Abort();
            //thread.Suspend();
            Console.WriteLine(thread.Name);
            Console.WriteLine(thread.ManagedThreadId);
            Console.WriteLine(thread.ThreadState);
            Console.WriteLine(thread.Priority);
            thread.Join();
            //thread.Resume();
            Thread.Sleep(500);

            //Задание 4

            using (FileStream fs = new FileStream(@"..\..\..\numbers1.txt", FileMode.Truncate)) { }
            using (FileStream fs = new FileStream(@"..\..\..\numbers2.txt", FileMode.Truncate)) { }

            Thread thread1 = new Thread(new ParameterizedThreadStart(MyEvenThread));
            Thread thread2 = new Thread(new ParameterizedThreadStart(MyOddThread));

            thread2.Priority = ThreadPriority.Highest;
            thread1.Start(n);
            thread2.Start(n);
            Thread.Sleep(2000);

            //Timer

            Timer timer = new Timer(new TimerCallback((_) => Console.WriteLine("Timer!")), null, 0, 200); 
            Thread thread3 = new Thread(new ParameterizedThreadStart(SynchronizedEvenThread)); 
            Thread thread4 = new Thread(new ParameterizedThreadStart(SynchronizedOddThread));
            Console.WriteLine("Numbers in order:");
            thread3.Start(n);
            thread4.Start(n);
            Thread.Sleep(10000);
            Console.WriteLine("barrier:");
            (new Thread(new ParameterizedThreadStart(BarrierEvenThread))).Start(n);
            (new Thread(new ParameterizedThreadStart(BarrierOddThread))).Start(n);
        }

        static void MyThread(object maxValue)
        {
            using (StreamWriter sw = new StreamWriter(@"..\..\..\numbers.txt"))
            {

                for (int i = 0; i < (int)maxValue; i++)
                {
                    try
                    {
                        Console.WriteLine(i);
                        sw.WriteLine(i);
                        Thread.Sleep(10);
                    }
                    catch (ThreadAbortException)
                    {
                        Thread.ResetAbort();
                        Thread.Sleep(500);
                    }
                }
            }
        }
        static void MyEvenThread(object maxValue)
        {
            lock(Locker)
            {
                using (StreamWriter sw = new StreamWriter(@"..\..\..\numbers1.txt", true))
                {

                    for (int i = 0; i < (int)maxValue; i+=2)
                    {
                        Console.WriteLine(i);
                        sw.WriteLine(i);
                        Thread.Sleep(15);
                    }
                }
            }
        }
        static void MyOddThread(object maxValue)
        {
            lock(Locker)
            {
                using (StreamWriter sw = new StreamWriter(@"..\..\..\numbers1.txt", true))
                {

                    for (int i = 1; i < (int)maxValue; i+=2)
                    {
                        Console.WriteLine(i);
                        sw.WriteLine(i);
                        Thread.Sleep(10);
                    }
                }
            }
        }

        static AutoResetEvent autoResetEvent = new AutoResetEvent(true);

        static void SynchronizedEvenThread(object maxValue)
        {
            for (int i = 0; i < (int)maxValue; i += 2)
            {
                autoResetEvent.WaitOne();
                using (StreamWriter sw = new StreamWriter(@"..\..\..\numbers2.txt", true))
                {
                    Console.WriteLine(i);
                    sw.WriteLine(i);
                    Thread.Sleep(10);
                }
                autoResetEvent.Set();
            }
        }
        static void SynchronizedOddThread(object maxValue)
        {
            for (int i = 1; i < (int)maxValue; i += 2)
            {
                autoResetEvent.WaitOne();
                using (StreamWriter sw = new StreamWriter(@"..\..\..\numbers2.txt", true))
                {
                    Console.WriteLine(i);
                    sw.WriteLine(i);
                    Thread.Sleep(100);
                }
                autoResetEvent.Set();
            }
        }
        static Barrier barrier = new Barrier(2);

        static void BarrierEvenThread(object maxValue)
        {
            for(int i = 0; i < (int)maxValue; i += 2)
            {
                Console.WriteLine(i);
                Thread.Sleep(20);
                barrier.SignalAndWait();
            }
        }
        static void BarrierOddThread(object maxValue)
        {
            for (int i = 1; i < (int)maxValue; i += 2)
            {
                Console.WriteLine(i);
                Thread.Sleep(10);
                barrier.SignalAndWait();
            }
        }
    }
}
