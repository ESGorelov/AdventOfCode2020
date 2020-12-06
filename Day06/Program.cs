using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = ReadInput("../../input.txt");

            int totalQuestion = 0;
            var currentHashSet = new HashSet<char>();
            
            int i = 0;
            while (i < input.Length)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    totalQuestion += currentHashSet.Count();
                    currentHashSet = new HashSet<char>();
                    i++;
                    continue;
                }
                foreach (var item in input[i])
                {
                    currentHashSet.Add(item);
                }
                i++;
            }

            Console.WriteLine($"totalQuestions: {totalQuestion}");
        }

        private static string[] ReadInput(string filePath)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                return sr.ReadToEnd().Split('\n').Select(s => s.Trim()).ToArray();
            }
        }
    }
}
