using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongModArithmetic;

namespace LongModularArithmetic
{
    class Program
    {
      


        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            ModCalculator modCalculator = new ModCalculator();
            var i = new Number("2BDC8D2");
            Console.WriteLine(calculator.BitLength(i));
            
        }
    }
}
