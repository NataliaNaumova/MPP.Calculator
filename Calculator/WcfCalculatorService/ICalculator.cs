using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfCalculatorService
{
    
    [ServiceContract]
    public interface ICalculator
    {

        [OperationContract]
        [FaultContract(typeof(MathFault))]
        double Add(double number1, double number2);

        [OperationContract]
        [FaultContract(typeof(MathFault))]
        double Subtract(double number1, double number2);

        [OperationContract]
        [FaultContract(typeof(MathFault))]
        double Multiply(double number1, double number2);

        [OperationContract]
        [FaultContract(typeof(MathFault))]
        double Divide(double number1, double number2);

        [OperationContract]
        [FaultContract(typeof(MathFault))]
        double Sqr(double number);
    }
}
