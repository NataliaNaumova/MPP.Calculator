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
            //CalculatorClient calculatorClient = new CalculatorClient();

            //Console.WriteLine("Calculator work demonsration:" + Environment.NewLine);

            //Console.WriteLine("3 + 5 = {0}", calculatorClient.Add(3, 5));
            //Console.WriteLine("7 - 2 = {0}", calculatorClient.Subtract(7, 2));
            //Console.WriteLine("15 / 7 = {0}", calculatorClient.Divide(15, 7));
            //Console.WriteLine("2 * 3 = {0}", calculatorClient.Multiply(2, 3));
            //Console.WriteLine();

            //Console.WriteLine("Fault demonstration:" + Environment.NewLine);

            //Console.WriteLine("5 / 0 :");
            //FaultTest(() => calculatorClient.Divide(5, 0));

            //Console.WriteLine("<double.MaxValue> + <double.MaxValue>:");
            //FaultTest(() => calculatorClient.Add(double.MaxValue, double.MaxValue));

            //Console.WriteLine("<double.MaxValue> * <double.MaxValue>:");
            //FaultTest(() => calculatorClient.Multiply(double.MaxValue, double.MaxValue));

            //Console.WriteLine("<double.MaxValue>^2:");
            //FaultTest(() => calculatorClient.Sqr(double.MaxValue));

            //Console.WriteLine("(<double.MinValue> - <double.MaxValue>:");
            //FaultTest(() => calculatorClient.Subtract(double.MinValue, double.MaxValue));

            //calculatorClient.Close();

            //Console.ReadKey();


            CalculatorClient calculatorClient = new CalculatorClient();
            while(true)
            {
                try
                {
                    Console.WriteLine("Calculator is ready!");
                    ExecuteOperation(calculatorClient);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid operand");
                }

                Console.WriteLine();
            }
        }

        private static void FaultTest(Func<double> operation)
        {
            try
            {
                double result = operation();
                Console.WriteLine("Result: {0}", result);
            }
            catch(FaultException<MathFault> e)
            {
                Console.WriteLine(e.Reason + Environment.NewLine);
            }
        }

        private static void ExecuteOperation(CalculatorClient calculatorClient)
        {
            Console.WriteLine("Enter the first operand:");
            double operand1 = GetNumberFromConsole();
            Console.WriteLine("Enter the operation type:");
            Operation operation = GetOperationFromConsole();

            if (operation == Operation.InvalidOperation)
            {
                Console.WriteLine("Invalid operation");
                return;
            }

            if (operation == Operation.Square)
            {
                FaultTest(() => calculatorClient.Sqr(operand1));
            }
            else
            {
                Console.WriteLine("Enter the second operand:");
                double operand2 = GetNumberFromConsole();

                switch (operation)
                {
                    case Operation.Add : 
                        FaultTest(() => calculatorClient.Add(operand1, operand2));
                        break;
                    case Operation.Subtract:
                        FaultTest(() => calculatorClient.Subtract(operand1, operand2));
                        break;
                    case Operation.Multiply:
                        FaultTest(() => calculatorClient.Multiply(operand1, operand2));
                        break;
                    case Operation.Divide:
                        FaultTest(() => calculatorClient.Divide(operand1, operand2));
                        break;
                }
            }
        }

        private static double GetNumberFromConsole()
        {
                string readLine = Console.ReadLine();
                return Double.Parse(readLine);
        }

        private static Operation GetOperationFromConsole()
        {
            string readLine = Console.ReadLine();
            switch (readLine)
            {
                case "+": return Operation.Add;
                case "-": return Operation.Subtract;
                case "*": return Operation.Multiply;
                case "/": return Operation.Divide;
                case "sqr": return Operation.Square;
                default: return Operation.InvalidOperation;
            }
        }

        public enum Operation
        {
            Add,
            Subtract,
            Multiply,
            Divide,
            Square,
            InvalidOperation
        }
    }
}
