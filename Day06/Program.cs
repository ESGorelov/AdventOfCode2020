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
            var currentHashSetList = new List<HashSet<char>>();
            
            int i = 0;
            while (i < input.Length)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    IEnumerable<char> intersec = currentHashSetList[0].ToList();
                    for (int j = 1; j < currentHashSetList.Count; j++)
                    {
                        intersec = intersec.Intersect(currentHashSetList[j].ToList());
                    }
                    totalQuestion += intersec.Count();
                    currentHashSetList = new List<HashSet<char>>();
                    i++;
                    continue;
                }
                var currentHashSet = new HashSet<char>();
                currentHashSetList.Add(currentHashSet);
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
