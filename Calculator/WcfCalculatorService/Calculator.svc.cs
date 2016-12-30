using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfCalculatorService
{
   
    public class Calculator : ICalculator
    {
        private const string NanOrInfinityInputErrorMessage = "Input numbers can't be NaN or Infinity.";

        private const string DivisionByZeroErrorName = "division by zero";
        private const string OverflowErrorName = "overflow";

        private const string AdditionOperationName = "addition";
        private const string DivisionOperationName = "division";
        private const string MultiplicationOperationName = "multiplication";
        private const string SquaringOperationName = "squaring";
        private const string SubtractionOperationName = "subtraction";

        public double Add(double number1, double number2)
        {
            CheckInput(number1);
            CheckInput(number2);
            double result = number1 + number2;
            CheckResultForOverflow(result, AdditionOperationName);
            return result;
        }

        public double Divide(double number1, double number2)
        {
            CheckInput(number1);
            CheckInput(number2);

            if (number2 == 0)
            {
                MathFault mf = new MathFault();
                mf.Operation = DivisionOperationName;
                mf.ProblemType = DivisionByZeroErrorName;
                throw new FaultException<MathFault>(mf, new FaultReason("Error of " + DivisionOperationName + ": " + DivisionByZeroErrorName + "."));
            }

            return number1 / number2;
        }

        public double Multiply(double number1, double number2)
        {
            CheckInput(number1);
            CheckInput(number2);
            double result = number1 * number2;
            CheckResultForOverflow(result, MultiplicationOperationName);
            return result;
        }

        public double Sqr(double number)
        {
            CheckInput(number);
            double result = Multiply(number, number);
            CheckResultForOverflow(result, SquaringOperationName);
            return result;
        }

        public double Subtract(double number1, double number2)
        {
            CheckInput(number1);
            CheckInput(number2);
            double result = number1 - number2;
            CheckResultForOverflow(result, SubtractionOperationName);
            return result;
        }

        private void CheckInput(double number)
        {
            if (!DoubleIsValid(number))
            {
                throw new ArgumentException(NanOrInfinityInputErrorMessage);
            }
        }

        private void CheckResultForOverflow(double result, string operationName)
        {
            if (!DoubleIsValid(result))
            {
                MathFault mf = new MathFault();
                mf.Operation = operationName;
                mf.ProblemType = OverflowErrorName;
                throw new FaultException<MathFault>(mf, new FaultReason("Error of " + operationName + ": " + OverflowErrorName + "."));
            }
        }

        private bool DoubleIsValid(double number)
        {
            return ((double.IsNaN(number)) || (double.IsInfinity(number))) ? false : true;
        }

    }
}
