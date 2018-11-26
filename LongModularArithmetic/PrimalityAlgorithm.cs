using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LongModArithmetic;

namespace LongModularArithmetic
{
    class PrimalityAlgorithm
    {
        Number zero = new Number(1);
        Number one = new Number("1");
        Number two = new Number("2");
        Calculator calculator = new Calculator();
        ModCalculator modcalculator = new ModCalculator();
        Random rnd = new Random();


        public void ParityRemoval_And_ReciprocityQuadraticLaw(ref Number a,ref Number b, ref Number result, Number three, Number four, Number five, Number eight)
        {
            Number t = new Number(1);
            while ((a.array[0] & 1) == 0)
            {
                t = calculator.LongAdd(t, one);
                a = calculator.ShiftBitsToLow(a, 1);
            }

            if ((t.array[0] & 1) == 1)
            {
                if (calculator.LongCmp(modcalculator.Mod(b, eight), three) == 0 || calculator.LongCmp(modcalculator.Mod(b, eight), five) == 0)
                {
                    result.sign *= -1;
                }
            }

            Number A_mod_Four = modcalculator.Mod(a, four);
            Number B_mod_Four = modcalculator.Mod(b, four);
            if (calculator.LongCmp(A_mod_Four, B_mod_Four) == 0 && calculator.LongCmp(A_mod_Four, three) == 0)
            {
                result.sign *= -1;
            }
            Number c = new Number(a.ToString());
            c.sign = a.sign;
            a = modcalculator.Mod(b, c);
            b = new Number(c.ToString());
            b.sign = c.sign;
        }


        public Number JacobiSymbol(Number x, Number y)
        {
            Number a = new Number(x.ToString());
            Number b = new Number(y.ToString());
            Number three = new Number("3");
            Number four = new Number("4");
            Number five = new Number("5");
            Number eight = new Number("8");

            if (calculator.LongCmp(modcalculator.SteinGCD(a, b), one) != 0)
            {
                return zero;
            }

            Number result = new Number("1");

            if (a.sign == -1)
            {
                a.sign = 1;
                if (calculator.LongCmp(modcalculator.Mod(b, four),three) == 0)
                {
                    result.sign *= -1;
                }
            }

            while (calculator.LongCmp(a, zero) != 0)
            {
                ParityRemoval_And_ReciprocityQuadraticLaw(ref a, ref b, ref result, three, four, five, eight);
            }
            return result;
        } 

        public int AmountOfHexLetters(Number a, out ulong word)
        {
            int hex_letters_in_ulong = 16;
            word = a.array[a.array.Length - 1];
            while ((word & 0xF000000000000000) == 0)
            {
                word <<= 4;
                hex_letters_in_ulong--;
            }
            return (a.array.Length << 4) - (16 - hex_letters_in_ulong);
        }

        public string modify(int i, string result, string original)
        {
            int integer = Convert.ToInt32(result[i+1].ToString(), 16);
            integer = rnd.Next(0, Convert.ToInt32(original[i+1].ToString(), 16) + 1);
            StringBuilder str = new StringBuilder(result);
            str[1] = Convert.ToChar(integer.ToString());
            return str.ToString();
        }

        public string GenerateHexString(Number n)
        {
            string original = n.ToString();
            var digits = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F"};
            ulong firstHexLetter = 0ul;
            int LettersAmount = AmountOfHexLetters(n, out firstHexLetter);
            string result = "";
            int num = (int)(firstHexLetter >> 0x3C);
            result += digits[rnd.Next(0, num + 1)];

            for(int j = 1; j < LettersAmount; j++)
            {
                result += digits[rnd.Next(0, 16)];
            }

            for (int i = 0; i < result.Length - 1; i++)
            {
                if ((result[i] == original[i]) && result[i + 1] > original[i + 1])
                {
                    return modify(i, result, original);
                }
                return result;
            }
            return result;
        }


        public bool SolovayStrassenPrimalityTest(Number n, int k)
        {
            Number t = calculator.LongSub(n, one);
            Number a = new Number(1);
            Number x = new Number(1);

            for (int i = 0; i < k; i++)
            {
                a = new Number(GenerateHexString(t));
                x = JacobiSymbol(a, n);
                if (calculator.LongCmp(x, zero) == 0) { return false; }
                
                var power = calculator.ShiftBitsToLow(calculator.LongSub(n, one), 1);
                var part1 = modcalculator.LongModPowerBarrett(a, power, n);
                var part2 = modcalculator.Mod(x, n);
                if (calculator.LongCmp(part1, part2) != 0) { return false; }
            }
            return true; 
        }
    }
}
