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
        

        public Number SignedArgumentModule(Number x, Number y)
        {
            Number a = new Number(x.ToString());
            Number b = new Number(y.ToString());
            return  a.sign == 1 ? modcalculator.Mod(a,b) : modcalculator.Mod(b, a);
        }

        Number One_OrNminusOne(Number x, Number n)
        {
            return x.sign == 1 ? x : calculator.LongSub(n, one);
        }

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

            Number A_mod_Four = SignedArgumentModule(a, four);
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


        public Number JacobiSymbol(Number a, Number b)
        {
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
                if (modcalculator.Mod(b, four) == three)
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

        public ulong Xorshift(ulong word)
        {
            word ^= word << 13;
            word ^= word >> 7;
            word ^= word << 17;
            return word;
        }

        public Number RandomNumber(Number m, ulong word)
        {
            Number lucky = new Number(m.array.Length);

            lucky.array[0] = Xorshift(word);

            for (int i = 1; i < m.array.Length; i++)
            {
                lucky.array[i] = Xorshift(lucky.array[i - 1]);
            }

            int counter = 0;
            word = m.array[m.array.Length - 1];
            while ((word & 0xF000000000000000) == 0)
            {
                word <<= 4;
                lucky.array[lucky.array.Length - 1] <<= 4;
                counter++;
            }

            ulong HexLetterBorder = (word & 0xF000000000000000) >> 0x3C;
            Random rnd = new Random();
            int FirstHexLetter = rnd.Next(0, (int)HexLetterBorder);
            ulong UFHL = (ulong)FirstHexLetter;
            UFHL <<= 0x3C;
            lucky.array[lucky.array.Length - 1] &= (UFHL ^ 0x0FFFFFFFFFFFFFFF);
            lucky.array[lucky.array.Length - 1] >>= (counter << 2);

            if (calculator.LongCmp(lucky, two) == 0) { return RandomNumber(m, word + 1); }
            else { return lucky; }
        }
        
        public bool SolovayStrassenPrimalityTest(Number n, ulong k)
        {
            Number t = calculator.LongSub(n, one);

            for (ulong i = 2; i < k; i++)
            {
                var a = RandomNumber(t, i);
                var x = JacobiSymbol(a, n);
                if (calculator.LongCmp(x, zero) == 0) { return false; }

                var power = calculator.ShiftBitsToLow(calculator.LongSub(n, one), 1);
                var part1 = modcalculator.LongModPowerBarrett(a, power, n);
                var part2 = One_OrNminusOne(x, n);
                if (calculator.LongCmp(part1, part2) != 0) { return false; }
            }
            return true; 
        }
    }
}
