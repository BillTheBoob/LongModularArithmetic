using System;
using System.Linq;

public class Number
{
    public ulong[] array = new ulong[0];
    public int sign = 1;

    public Number(string a)
    {
        if (string.IsNullOrWhiteSpace(a))
        {
            throw new ArgumentException();
        }

        string x;
        if (a != "0")
        {
            x = a.TrimStart('0');
        }
        else
        {
            x = a;
        }
        while (x.Length % 16 != 0)
        {
            x = "0" + x;
        }

        array = new ulong[x.Length / 16];

        for (int i = 0; i < x.Length; i += 16)
        {
            array[i / 16] = Convert.ToUInt64(x.Substring(i, 16), 16);
        }
        Array.Reverse(array);
    }

    public Number(int n)
    {
        array = new ulong[n];
    }

    public override string ToString()
    {
        string a = string.Concat(array.Select(chunk => chunk.ToString("X").PadLeft(sizeof(ulong) * 2, '0')).Reverse()).TrimStart('0');
        return a != "" ? a : "0";
    }
}
