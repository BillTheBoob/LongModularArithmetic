﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Number r = new Number("0");
            Number c = new Number(a.array.Length);
            int k = calculator.BitLength(b);
            r = new Number(a.ToString());
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
                r.array = calculator.LongSub(r, c);
                q = calculator.SetBit(q, t - k);
            }
            return r;
        }


        public Number SteinGCD(Number z, Number x)
        {
            int shift = 0;
            Number u = new Number(z.ToString());
            Number v = new Number(x.ToString());
           
            if (calculator.LongCmp(u,zero)==0) return v;
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
                if (calculator.LongCmp(u,v)==1)
                {                  
                    var temp = v;
                    v = u;
                    u = temp;
                }
                v.array = calculator.LongSub(v,u); 
            } while (calculator.LongCmp(v,zero) != 0);
              return calculator.ShiftBitsToHigh(u, shift);           
        }
        
        public Number ConstantMu(Number n)
        {
            int k = calculator.BitLength(n);
            return calculator.LongDiv(calculator.ShiftBitsToHigh(one, k), n, out zero);
        }

        public Number BarrettReduction(Number x, Number z, int k, Number m)
        {
            Number a = new Number(x.ToString());
            Number q = new Number(1);

            q = calculator.ShiftBitsToLow(calculator.LongMull(a, m), k);
            a.array = calculator.LongSub(a, calculator.LongMull(q, z));
            if(calculator.LongCmp(z,a) <= 0)
            {
                a.array = calculator.LongSub(a,z);
            }
            return a;
        }
        
        public Number LongModPowerBarrett(Number x,Number y,Number z)
        {
            Number c = new Number("1");
            Number a = new Number(x.ToString());
            Number b = new Number(y.ToString());
            int k = 2*calculator.BitLength(a);
            Number m = calculator.LongDiv(calculator.ShiftBitsToHigh(one, k), z, out zero);

            ulong word;
            ulong bit = 0ul;

            for (int i = 0; i < b.array.Length - 1; i++)
            {
                bit = 0;
                word = b.array[i];
                for (int j = 0; j != 64; j++)
                {
                    bit = word & 1;
                    if (bit == 1)
                    {
                        c = BarrettReduction(calculator.LongMull(c, a), z, k, m);
                    }
                    a = BarrettReduction(calculator.LongMull(a, a), z, k, m);
                    word >>= 1;
                }
            }

            word = b.array[b.array.Length - 1];

            for (; word != 0; word >>= 1)
            {
                bit = word & 1;
                if (bit == 1)
                {
                    c = BarrettReduction(calculator.LongMull(c, a), z, k, m);
                }
                a = BarrettReduction(calculator.LongMull(a, a), z, k, m);
            }
            return c;
        }
    }
}

