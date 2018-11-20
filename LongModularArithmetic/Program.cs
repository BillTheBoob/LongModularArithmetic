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
            var b = new Number("5555555555555555555555");
            var c = new Number("2");
            Calculator calculator = new Calculator();
            Console.WriteLine(c.array.Length);

            var u = calculator.LongMull(b, c);
            Console.WriteLine(c.array.Length);
        }
    }
}
