using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using CalculatorConsole.CalculatorServiceReference;

namespace CalculatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {          
            CalculatorClient calculatorClient = new CalculatorClient();

            Console.WriteLine("Calculator work demonsration:" + Environment.NewLine);

            Console.WriteLine("3 + 5 = {0}", calculatorClient.Add(3, 5));
            Console.WriteLine("7 - 2 = {0}", calculatorClient.Subtract(7, 2));
            Console.WriteLine("15 / 7 = {0}", calculatorClient.Divide(15, 7));
            Console.WriteLine("2 * 3 = {0}", calculatorClient.Multiply(2, 3));
            Console.WriteLine();

            Console.WriteLine("Fault demonstration:" + Environment.NewLine);

            Console.WriteLine("5 / 0 :");
            FaultTest(() => calculatorClient.Divide(5, 0));

            Console.WriteLine("<double.MaxValue> + <double.MaxValue>:");
            FaultTest(() => calculatorClient.Add(double.MaxValue, double.MaxValue));

            Console.WriteLine("<double.MaxValue> * <double.MaxValue>:");
            FaultTest(() => calculatorClient.Multiply(double.MaxValue, double.MaxValue));

            Console.WriteLine("<double.MaxValue>^2:");
            FaultTest(() => calculatorClient.Sqrt(double.MaxValue));

            Console.WriteLine("(<double.MinValue> - <double.MaxValue>:");
            FaultTest(() => calculatorClient.Subtract(double.MinValue, double.MaxValue));

            calculatorClient.Close();

            Console.ReadKey();
        }

        private static void FaultTest(Action operation)
        {
            try
            {
                operation();
            }
            catch(FaultException<MathFault> e)
            {
                Console.WriteLine(e.Reason + Environment.NewLine);
            }
        }
    }
}
