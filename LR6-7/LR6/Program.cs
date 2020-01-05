using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR5
{
    class Program
    {
        static void Main(string[] args)
        {
            Zoo animals1 = new Zoo();
            animals1.CreatorInfo();

            Lion lion1 = new Lion("Africa", "lion1", 91, 1999);
            animals1.Add(lion1);

            Lion lion2 = new Lion("Africa", "lion2", 120, 1995);
            animals1.Add(lion2);

            Tiger tiger1 = new Tiger("Asia", "tiger1", 69, 2000);
            animals1.Add(tiger1);

            Owl owl1 = new Owl("Europe", "owl1", 2, 2010);
            animals1.Add(owl1);
            try
            {
                Parrot parrot1 = new Parrot("Jungle", "parrot1", 0.5, 2020);
                animals1.Add(parrot1);
            }
            catch(YearException exception)
            {
                Console.WriteLine(exception.Message + exception.Value + '\n' + exception.StackTrace);
            }
            catch(Exception exception) //Универсальный обработчик
            {
                Console.WriteLine("Some exception happens " + '\n' + exception.ToString());
            }
            finally
            {
                Console.WriteLine("---finally block---");
            }
            Shark shark1 = new Shark("Ocean", "shark1", 200, 2007);
            animals1.Add(shark1);
            try
            {
                Console.WriteLine("Average weight: " + ZooController.AverageWeight(animals1));
            }
            catch(DivideByZeroException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch(ChoiceOutOfRange exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception) //Универсальный обработчик
            {
                Console.WriteLine("Some exception happens " + '\n' + exception.ToString());
            }
            finally
            {
                Console.WriteLine("---finally block---");
            }
            Console.WriteLine("Amount of predatory birds: " + ZooController.AmountOfPredatoryBirds(animals1));
            ZooController.ShowSortingByYear(animals1);
        }
    }

    class YearException: ArgumentException
    {
        public int Value { get; }
        public YearException(string message, int value) : base(message)
        {
            Value = value;
        }
    }

    class DivideByZeroException: Exception
    {
        public DivideByZeroException(string message) : base(message)
        {
            
        }
    }

    class ChoiceOutOfRange : Exception
    {
        public ChoiceOutOfRange(string message) : base(message)
        {

        }
    }
}

