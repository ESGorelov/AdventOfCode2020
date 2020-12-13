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
                if(waitTime > curWait)
                {
                    busNum = busN;
                    waitTime = curWait;
                }
            }

            Console.WriteLine($"Answer = {busNum * waitTime}");
        }


    }
}
