using System;

namespace FiniteFieldsLib;

public static class FFMath
{
    public static bool IsPrime(int n)
    {
        if (n < 2) return false;
        if (n % 2 == 0) return n == 2;
        int root = (int)Math.Sqrt(n);
        for (int i = 3; i <= root; i += 2)
        {
            if (n % i == 0) return false;
        }

        return true;
    }

    public static int Gcd(int a, int b, out int x, out int y)
    {
        if (a == 0)
        {
            x = 0;
            y = 1;
            return b;
        }

        int d = Gcd(b % a, a, out int x1, out int y1);
        x = y1 - b / a * x1;
        y = x1;
        return d;
    }
}