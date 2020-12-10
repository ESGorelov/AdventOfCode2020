using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {

            int[] input = ReadInput("../../input.txt");

            int count1Pulse = 0;
            int count3Pulse = 0;

            int curValue = 0;

            for (int i = 0; i < input.Length; i++)
            {
                var res = input[i] - curValue;
                switch (res)
                {
                    case 1:
                        count1Pulse++;
                        break;
                    case 2:
                        break;
                    case 3:
                        count3Pulse++;
                        break;
                    default:
                        throw new Exception("Большая разница");
                }
                curValue = input[i];
            }
            count3Pulse++;
            Console.WriteLine($"Number: {count1Pulse * count3Pulse}");



            

        }
        private static int[] ReadInput(string filePath)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                return sr.ReadToEnd().Split('\n').Select(s => s.Trim()).Select(int.Parse).OrderBy(a => a).ToArray();
            }
        }
    }
}
