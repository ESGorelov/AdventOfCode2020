using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            int dest = 2020;
            int[] input = ReadAndParseInput("../../input.txt");
            Array.Sort(input);

            for (int i = input.Length - 1; i > 0; i--)
            {
                int j = 0;

                while (j < i)
                {
                    int sum = input[i] + input[j];

                    if (sum > dest)
                        break;

                    int k = j + 1;
                    while(k < i)
                    {
                        int sumThree = sum + input[k];

                        if (sumThree > dest)
                            break;
                        if (sumThree.Equals(dest))
                        {
                            Console.WriteLine($"number 1: {input[i]}");
                            Console.WriteLine($"number 2: {input[j]}");
                            Console.WriteLine($"number 3: {input[k]}");

                            Console.WriteLine($"multiply: {input[j] * input[i]* input[k]}");

                            i = 0;
                            break;
                        }

                        k++;
                    }

                    
                    j++;
                }
            }
            Console.ReadKey();
        }

        private static int[] ReadAndParseInput(string filePath)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                return sr.ReadToEnd().Split('\n').Select(int.Parse).ToArray();
            }
        }
    }
}
