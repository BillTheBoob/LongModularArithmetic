
namespace LongModArithmetic
{
    class ModCalculator
    {
        Calculator calculator = new Calculator();
        Number zero = new Number(1);
        Number one = new Number("1");


        public Number GCD(Number a, Number b)
        {
            if (calculator.LongCmp(b, zero) == 0)
            {
                return a;
            }
            return GCD(b, Mod(a, b));
        }


        public Number Mod(Number a, Number b)
        {
            Number r = new Number(1);
            if (a.sign == -1)
            {
                r = calculator.LongMul(calculator.LongSub(b, one), a);
            }
            else
            {
                r = new Number(a.ToString());
            }
            Number c = new Number(a.array.Length);
            int k = calculator.BitLength(b);
            var q = new Number(a.array.Length);
            while (calculator.LongCmp(r, b) >= 0)
            {
                int t = calculator.BitLength(r);
                c = calculator.ShiftBitsToHigh(b, t - k);
                if (calculator.LongCmp(r, c) == -1)
                {
                    t--;
                    c = calculator.ShiftBitsToHigh(b, t - k);
                }
                r = calculator.LongSub(r, c);
                q = calculator.SetBit(q, t - k);
            }
            return r;
        }


        public Number SteinGCD(Number z, Number x)
        {
            int shift = 0;
            string tupo = z.ToString();
            Number u = new Number(z.ToString());
            Number v = new Number(x.ToString());

            if (calculator.LongCmp(u, zero) == 0) return v;
            if (calculator.LongCmp(v, zero) == 0) return u;

            while (((u.array[0] & 1) == 0) && ((v.array[0] & 1) == 0))
            {
                u = calculator.ShiftBitsToLow(u, 1);
                v = calculator.ShiftBitsToLow(v, 1);
                shift++;
            }
            while (((u.array[0] & 1) == 0))
            {
                u = calculator.ShiftBitsToLow(u, 1);
            }

            do
            {
                while ((v.array[0] & 1) == 0)
                {
                    v = calculator.ShiftBitsToLow(v, 1);
                }
                if (calculator.LongCmp(u, v) == 1)
                {
                    var temp = v;
                    v = u;
                    u = temp;
                }
                v = calculator.LongSub(v, u);
            } while (calculator.LongCmp(v, zero) != 0);
            return calculator.ShiftBitsToHigh(u, shift);
        }


        public Number BarrettReduction(Number x, Number z, int k, Number m)
        {
            Number a = new Number(x.ToString());
            Number q = new Number(1);

            q = calculator.ShiftBitsToLow(calculator.LongMul(a, m), k);
            a = calculator.LongSub(a, calculator.LongMul(q, z));
            if (calculator.LongCmp(z, a) <= 0)
            {
                a = calculator.LongSub(a, z);
            }
            return a;
        }


        public void MultiplyAndTakeModule(ref ulong word, ref Number result, ref Number a, ref Number module, ref int bitsize, ref Number mu)
        {
            ulong bit = word & 1;
            if (bit == 1)
            {
                result = BarrettReduction(calculator.LongMul(result, a), module, bitsize, mu);
            }
            a = BarrettReduction(calculator.LongMul(a, a), module, bitsize, mu);
        }


        public Number LongModPowerBarrett(Number x, Number y, Number module)
        {
            Number result = new Number("1");
            Number a = new Number(x.ToString());
            Number b = new Number(y.ToString());
            int bit_size = calculator.BitLength(a)<< 1;
            Number mu = calculator.LongDiv(calculator.ShiftBitsToHigh(one, bit_size), module, out zero);
            ulong word;

            for (int i = 0; i < b.array.Length - 1; i++)
            {
                word = b.array[i];
                for (int j = 0; j != 64; j++, word >>= 1)
                {
                    MultiplyAndTakeModule(ref word, ref result, ref a, ref module, ref bit_size, ref mu);
                }
            }

            word = b.array[b.array.Length - 1];
            for (; word != 0; word >>= 1)
            {
                MultiplyAndTakeModule(ref word, ref result, ref a, ref module, ref bit_size, ref mu);
            }
            return result;
        }
    }
}

