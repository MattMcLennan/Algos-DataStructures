using System;
using System.Numerics;

namespace Karatsuba
{
    class Program
    {
        static BigInteger Multiply(BigInteger x, BigInteger y)
        {
            if (x < 10 || y < 10)
            {
                return x * y;
            }
            
            var n = Math.Max(x.ToString().Length, y.ToString().Length);
            var n2 = n/2;

            BigInteger a = x / BigInteger.Pow(10, n2);
            BigInteger b = x % BigInteger.Pow(10, n2);
            BigInteger c = y / BigInteger.Pow(10, n2);
            BigInteger d = y % BigInteger.Pow(10, n2);

            BigInteger ac = Multiply(a, c);
            BigInteger bd = Multiply(b, d);
            BigInteger adPlusBc = Multiply((a+b), (c+d)) - ac - bd;

            return (ac * BigInteger.Pow(10, 2 * n2)) 
                +  ((adPlusBc) * BigInteger.Pow(10, n2)) 
                + bd;
        }

        static void Main(string[] args)
        {
            BigInteger x = BigInteger.Parse(args[0]);
            BigInteger y = BigInteger.Parse(args[1]);

            Console.WriteLine(Multiply(x,y));
        }
    }
}
