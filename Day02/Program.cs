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

            int count = 0;
            foreach (string item in input)
            {
                var checker = new PasswordChecker(item);
                if (checker.IsValid())
                    count++;
            }

            Console.WriteLine($"Count correct pass: {count}");
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

    class PasswordChecker
    {
        int _min;
        int _max;
        char _symbol;

        string _password;
        public PasswordChecker(string password)
        {
            string[] temp = password.Split(':');
            if (temp.Length != 2)
                throw new ArgumentException($"Неверный формат входной строки: {password}");

            ParseLeftBlock(temp[0]);

            _password = temp[1].Trim();
        }

        public bool IsValid()
        {
            var d = _password.ToCharArray().Count(a => a.Equals(_symbol));
            return d >= _min && d <= _max;
        }

        private void ParseLeftBlock(string leftBlock)
        {
            string[] temp = leftBlock.Split(' ');
            if (temp.Length != 2)
                throw new ArgumentException($"Неверный формат политики: {leftBlock}");

            _symbol = temp[1][0];

            int[] diapason = temp[0].Split('-').Select(int.Parse).ToArray();

            _min = diapason[0];
            _max = diapason[1];
        }
    }
}
