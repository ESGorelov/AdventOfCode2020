using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = ReadAndParseInput("../../input.txt");

            var downhill = new Downhill(input);

            Console.WriteLine($"Count tree: {downhill.GetCountTree(3, 1)}");
            Console.WriteLine();
            
            Console.WriteLine("--Part 2--");

            long multiply = 1;

            Tuple<int, int>[] hill =
            {
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(3, 1),
                new Tuple<int, int>(5, 1),
                new Tuple<int, int>(7, 1),
                new Tuple<int, int>(1, 2),
            };

            foreach (var item in hill)
            {
                Console.Write($"Hill: right - {item.Item1} down - {item.Item2}\t");

                int countTree = downhill.GetCountTree(item.Item1, item.Item2);
                Console.WriteLine($"Count tree: {countTree}");

                multiply *= countTree;
            }
            Console.WriteLine($"Answer: {multiply}");
            Console.ReadKey();
        }

        private static string[] ReadAndParseInput(string filePath)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                return sr.ReadToEnd().Split('\n').Select(s => s.Trim()).ToArray();
            }
        }
    }

    class Downhill
    {
        private int _widthOneBlock;

        private string[] _input;

        public Downhill(string[] input)
        {
            _input = input;
            _widthOneBlock = input[0].Length;
        }

        public int GetCountTree(int right, int down)
        {
            int countTree = 0;
            int i = 0;
            int j = 0;

            while (i < _input.Length)
            {
                if (_input[i][j].Equals('#'))
                    countTree++;
                i += down;
                j = (j += right) % _widthOneBlock;
            }
            return countTree;
        }
    }
}
