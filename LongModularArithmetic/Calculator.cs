using System;

public class Calculator
{
    const ulong One = 1;
    const ulong Zero = 0;
    public ulong carry = 0;

    public void LengthControl(Number a, Number b)
    {
        var requiredlenght = Math.Max(a.array.Length, b.array.Length);
        Array.Resize(ref a.array, requiredlenght);
        Array.Resize(ref b.array, requiredlenght);
    }


    public int LongCmp(Number z, Number x)
    {
        LengthControl(z, x);
        for (int i = z.array.Length - 1; i > -1; i--)
        {
            if (z.array[i] > x.array[i]) return 1;
            if (z.array[i] < x.array[i]) return -1;
        }
        return 0;
    }


    public int HighNotZeroIndex(ulong[] a)
    {
        for (var i = a.Length - 1; i >= 0; i--)
        {
            if (a[i] > 0) { return i; }
        }
        return 0;
    }


    public int BitLength(Number b)
    {
        var bits = 0;
        var index = HighNotZeroIndex(b.array);
        var temp = b.array[index];
        while (temp > 0)
        {
            temp >>= 1;
            bits++;
        }
        return bits + sizeof(ulong) * 8 * index;
    }


    public Number ShiftBitsToHigh(Number b, int shift_num)
    {
        if (shift_num == 0) return b;
        Number c = new Number(b.array.Length);
        ulong[] surrogate = new ulong[b.array.Length];
        Array.Copy(b.array, surrogate, b.array.Length);
        int shift;
        while (shift_num > 0)
        {
            c.array = new ulong[surrogate.Length + 1];
            var carriedBits = 0UL;
            if (shift_num < 64) { shift = shift_num; }
            else { shift = 63; }
            int i = 0;
            for (; i < surrogate.Length; i++)
            {
                var temp = surrogate[i];
                c.array[i] = (temp << shift) | carriedBits;
                carriedBits = temp >> (64 - shift);
            }
            c.array[i] = surrogate[i - 1] >> (64 - shift);
            shift_num -= 63;
            surrogate = c.array;
        }
        return c;
    }


    public Number ShiftBitsToLow(Number b, int shift_num)
    {
        Number c = new Number(b.array.Length);
        ulong[] surrogate = new ulong[b.array.Length];
        Array.Copy(b.array, surrogate, b.array.Length);
        int shift;
        c.array = new ulong[b.array.Length];
        while (shift_num > 0)
        {
            var carriedBits = 0UL;
            if (shift_num < 64) { shift = shift_num; }
            else { shift = 63; }
            int i = b.array.Length - 1;
            for (; i >= 0; i--)
            {
                var temp = surrogate[i];
                c.array[i] = (temp >> shift) | carriedBits;
                carriedBits = temp << (64 - shift);
            }
            shift_num -= 63;
            surrogate = c.array;
        }
        return c;
    }


    public Number SetBit(Number a, int position)
    {
        var temp = new Number(a.array.Length);
        temp.array[0] = 1;
        temp = ShiftBitsToHigh(temp, position);
        return LongAdd(a, temp);
    }


    public ulong[] Bisected(ulong[] array)
    {
        ulong[] bisected = new ulong[2 * array.Length];
        int j = 0;
        for (int i = 0; i < array.Length; i++, j += 2)
        {
            bisected[j] = array[i] & (0xFFFFFFFF);
            bisected[j + 1] = array[i] >> 32;
        }
        return bisected;
    }


    public ulong[] United(ulong[] array)
    {
        ulong[] united = new ulong[array.Length / 2];
        int j = 0;
        for (int i = 0; i < united.Length; i++, j += 2)
        {
            united[i] = (array[j + 1] << 32) | (array[j]);
        }
        int index = HighNotZeroIndex(united)+1;
        ulong[] copy = new ulong[index];
        Array.Copy(united,copy,index);
        return copy;
    }


    public Number LongAdd(Number z, Number x)
    {
        LengthControl(z, x);
        var c = new Number(z.array.Length);
        for (int i = 0; i < z.array.Length; i++)
        {
            ulong temp = unchecked(z.array[i] + x.array[i] + carry);
            c.array[i] = temp;
            carry = temp < z.array[i] ? One : Zero;
        }
        return c;
    }


    public Number LongSub(Number a, Number b)
    {
        Number z = new Number(a.ToString());
        Number x = new Number(b.ToString());
        LengthControl(z, x);
        Number c = new Number(z.array.Length);
        if (LongCmp(z, x) == 0) { return c; }
        var borrow = 0UL;
        for (int i = 0; i < c.array.Length; i++)
        {
            c.array[i] = z.array[i] - x.array[i] - borrow;
            borrow = c.array[i] > z.array[i] ? 1UL : 0UL;
        }
        return c;
    }


    public Number LongMul(Number x, Number y)
    {
        Number a = new Number(x.ToString());
        Number b = new Number(y.ToString());
        LengthControl(a, b);
        var bisectedA = Bisected(a.array);
        var bisectedB = Bisected(b.array);
        ulong carry;
        var c = new Number(bisectedA.Length + bisectedB.Length);
        for (int i = 0; i < bisectedA.Length; i++)
        {
            carry = 0;
            for (int j = 0; j < bisectedB.Length; j++)
            {
                ulong temp = c.array[i + j] + bisectedA[j] * bisectedB[i] + carry;
                c.array[i + j] = temp & 0xFFFFFFFF;
                carry = temp >> 32;
            }
            c.array[i + bisectedA.Length] = carry;
        }
        c.array = United(c.array);
        return c;
    }


    public Number LongDiv(Number a, Number b, out Number r)
    {
        Number c = new Number(a.array.Length);
        int k = BitLength(b);
        r = new Number(a.ToString());
        var q = new Number(a.array.Length);
        while (LongCmp(r, b) >= 0)
        {
            int t = BitLength(r);
            c = ShiftBitsToHigh(b, t - k);
            if (LongCmp(r, c) == -1)
            {
                t--;
                c = ShiftBitsToHigh(b, t - k);
            }
            r = LongSub(r, c);
            q = SetBit(q, t - k);
        }
        return q;
    }

    public void FiveMultiplications( ref Number result, ref Number[] D, ref ulong word, ref int i)
    {
        result = LongMul(result, D[(word) >> 0x3C]);
        if (i != 0)
        {
            for (int j = 1; j <= 4; j++)
            {
                result = LongMul(result, result);
            }
        }
    }

    public Number Gorner(Number a, Number b)
    {
        Number zero = new Number(1);
        var result = new Number("1");
        if (LongCmp(a, zero) == 0) { return zero; }
        if (LongCmp(b, zero) == 0) { return result; }
        Number[] D = new Number[1 << 4];
        D[0] = new Number("1");
        D[1] = a;

        for (int w = 2; w < D.Length; w++)
        {
            D[w] = LongMul(D[w - 1], a);
        }

        int hex_letters_in_ulong = 16;
        ulong word = b.array[b.array.Length - 1];

        while ((word & 0xF000000000000000) == 0)
        {
            word <<= 4;
            hex_letters_in_ulong--;
        }

        int amount_of_all_letters = (b.array.Length << 4) - (16 - hex_letters_in_ulong); 
        int i = amount_of_all_letters - 1;

        while (hex_letters_in_ulong != 0)
        {
            FiveMultiplications(ref result, ref D, ref word, ref i);
            i--;
            hex_letters_in_ulong--;
            word <<= 4;
        }
        if (b.array.Length == 1) { return result;}


        for (int v = b.array.Length - 2; v >= 0; v--)
        {
            word = b.array[v];
            hex_letters_in_ulong = 16;
            while (hex_letters_in_ulong != 0)
            {
                FiveMultiplications(ref result, ref D, ref word, ref i);
                i--;
                hex_letters_in_ulong--;
                word <<= 4;
            }
        }
        return result;
    }
}