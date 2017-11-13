using System;
using System.Numerics;

namespace Karatsuba
{
    class Program
    {
        static BigInteger Multiply(BigInteger x, BigInteger y)
        {
            if (NumberOfDigits(x) == 1 || NumberOfDigits(y) == 1)
            {
                BigInteger product = 0;
                for(var i = 1; i <= x; i++)
                {
                    product += y;
                }

                return product;
            }

            string xString = x.ToString();
            string yString = y.ToString();

            var xUpto = xString.Length / 2;
            var yUpto = yString.Length / 2;

            BigInteger a = BigInteger.Parse(xString.Substring(0, xUpto));
            BigInteger b = BigInteger.Parse(xString.Substring(xUpto));
            BigInteger c = BigInteger.Parse(yString.Substring(0, yUpto));
            BigInteger d = BigInteger.Parse(yString.Substring(yUpto));

            return (int)Math.Pow(10, xString.Length) * Multiply(a, c) 
                +  (int)Math.Pow(10, xString.Length / 2) * (Multiply(a, d) + Multiply(b, c))
                + Multiply(b, d);
        }

        static Int64 NumberOfDigits(BigInteger x)
        {
            return x.ToString().Length;
        }

        static void Main(string[] args)
        {
            BigInteger x = BigInteger.Parse(args[0]);
            BigInteger y = BigInteger.Parse(args[1]);
            Console.WriteLine(Multiply(x, y));
        }
    }
}
