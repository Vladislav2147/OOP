using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR12
{
    class Program
    {
        static void Main(string[] args)
        {
            Reflector.GetPropertiesAndFields("LR12.Parrot");
            Reflector.GetPublicMethods("LR12.Parrot");
            Reflector.GetInterfaces("LR12.Parrot");
            Reflector.GetMethodsWithParameter("LR12.Parrot", "System.String");
            Reflector.ToFile("LR12.Parrot");
            Reflector.CallMethod("LR12.Shark", "DoSharkThings");

            Reflector.GetPropertiesAndFields("LR12.Shop");
            Reflector.GetPublicMethods("LR12.Shop");
            Reflector.GetInterfaces("LR12.Shop");
            Reflector.GetMethodsWithParameter("LR12.Shop", "System.String");
            Reflector.ToFile("LR12.Shop");
        }

        static class Reflector
        {
            public static void ToFile(string className)
            {
                Type type = Type.GetType(className);
                if (type != null)
                {
                    using (StreamWriter sw = new StreamWriter(@"F:\2 курс\ООП\LR12\classInfo.txt", true))
                    {
                        sw.WriteLine($"{className}\n");
                        foreach (MemberInfo member in type.GetMembers())
                        {
                            sw.WriteLine(member);
                        }
                        sw.WriteLine("------------------------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine($"There is no types with name {className}");
                }
            }
            public static void CallMethod(string className, string methodName)
            {
                Type type = Type.GetType(className);
                try
                {
                    object obj = Activator.CreateInstance(type);
                    string[] _params = File.ReadAllLines(@"F:\2 курс\ООП\LR12\Parameters.txt");
                    MethodInfo method = type.GetMethod(methodName);
                    Console.WriteLine("Result of execution of method:");
                    Console.WriteLine(method.Invoke(obj, _params));
                }
                catch(MissingMethodException)
                {
                    Console.WriteLine("Missing constructor without parameters");
                }
                catch(ArgumentNullException)
                {
                    Console.WriteLine($"Missing type with name {className}");
                }
                catch(NullReferenceException)
                {
                    Console.WriteLine($"Missing method with name {methodName}");
                }
                catch(TargetParameterCountException)
                {
                    Console.WriteLine($"Amount of parameters in the file does't match amount of the method's parameters");
                }
            }
            public static void GetPropertiesAndFields(string className)
            {
                Type type = Type.GetType(className);
                if(type != null)
                {
                    Console.WriteLine("\nProperties:");
                    if (type.GetProperties().Length == 0)
                    {
                        Console.WriteLine("Empty");
                    }
                    foreach (PropertyInfo property in type.GetProperties())
                    {
                        Console.WriteLine(property);
                    }

                    Console.WriteLine("\nFields:");
                    if (type.GetFields().Length == 0)
                    {
                        Console.WriteLine("Empty");
                    }
                    foreach (FieldInfo field in type.GetFields())
                    {
                        Console.WriteLine(field);
                    }
                }
                else
                {
                    Console.WriteLine($"There is no types with name {className}");
                }
            }
            public static void GetPublicMethods(string className)
            {
                Type type = Type.GetType(className);
                if(type != null)
                {
                    Console.WriteLine("\nPublic methods:");
                    if (type.GetMethods().Length == 0)
                    {
                        Console.WriteLine("Empty");
                    }
                    foreach (MethodInfo method in type.GetMethods())
                    {
                        Console.WriteLine(method);
                    }
                }
                else
                {
                    Console.WriteLine($"There is no types with name {className}");
                }
            }
            public static void GetInterfaces(string className)
            {
                Type type = Type.GetType(className);
                if(type != null)
                {
                    Console.WriteLine("\nInterfaces: ");
                    if (type.GetInterfaces().Length == 0)
                    {
                        Console.WriteLine("Empty");
                    }
                    foreach (Type _interface in type.GetInterfaces())
                    {
                        Console.WriteLine(_interface);
                    }
                }
                else
                {
                    Console.WriteLine($"There is no types with name {className}");
                }
            }
            public static void GetMethodsWithParameter(string className, string parameter)
            {
                Type type = Type.GetType(className);
                if (type != null)
                {
                    Console.WriteLine($"\nMethods with {parameter} paremeter: ");
                    foreach (MethodInfo method in type.GetMethods())
                    {
                        foreach (ParameterInfo parameterInfo in method.GetParameters())
                        {
                            if (parameterInfo.ParameterType.ToString() == parameter)
                            {
                                Console.WriteLine(method);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"There is no types with name {className}");
                }
            }
        }
    }
}
