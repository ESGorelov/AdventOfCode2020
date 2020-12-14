using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines(@"../../input.txt");

            int myTimeStamp = int.Parse(input[0]);

            int busNum = 0;
            int waitTime = int.MaxValue;
            foreach (Match busStr in new Regex(@"(\d+)").Matches(input[1]))
            {
                var busN = int.Parse(busStr.Value);
                var div = Math.DivRem(myTimeStamp, busN, out int rem);
                if (rem == 0)
                {
                    busNum = busN;
                    waitTime = 0;
                    break;
                }


                int curWait = ((div + 1) * busN) - myTimeStamp;
                if (waitTime > curWait)
                {
                    busNum = busN;
                    waitTime = curWait;
                }
            }

            Console.WriteLine($"Answer = {busNum * waitTime}");


            var bus = new List<Tuple<int, int>>();
            int k = 0;
            foreach (var item in input[1].Split(','))
            {
                if (item.Equals("x"))
                {
                    k++;
                    continue;
                }
                bus.Add(new Tuple<int, int>(int.Parse(item), k++));
            }

            int[] num = bus.Select(b => b.Item1).ToArray();

            int[] rem1 = bus.Select(b => b.Item2).Select(u => (u * -1)).ToArray();

            Console.WriteLine($"TimeStamp is {ChineseRemainder(num, rem1)}");
        }

        static long ChineseRemainder(int[] num, int[] rem)
        {
            if (num.Length != rem.Length)
                throw new Exception("Разная длина входных данных");

            long prod = num.Select(u => (long)u).Aggregate((q1, t1) => q1 * t1);

            long result = 0;
            for (int i = 0; i < num.Length; i++)
            {
                long pp = prod / num[i];
                result += rem[i] * Evclide_gcd(pp, num[i]) * pp;
            }

            while (result < 0)
                result += prod;

            return result % prod;

        }

        static long Evclide_gcd(long a, long b)
        {
            long m0 = b, t, q;
            long x0 = 0, x1 = 1;

            if (b == 1)
                return 0;

            while (a > 1)
            {
                t = b;
                q = Math.DivRem(a, b, out b);
                a = t;

                t = x0;
                x0 = x1 - q * x0;
                x1 = t;
            }
            while (x1 < 0)
                x1 += m0;

            return x1;
        }
    }
}
