using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day09
{
    class Program
    {
        static void Main(string[] args)
        {

            long[] input = ReadInput("../../input.txt");
            int preambleSize = 25;

            var preamble = new long[preambleSize];

            for (int i = 0; i < preambleSize; i++)
            {
                preamble[i] = input[i];
            }

            var xmasCoder = new Xmas(preamble);
            long wrongNumber = -1;
            for (int i = preambleSize; i < input.Length; i++)
            {
                if (!xmasCoder.CheckAndAdd(input[i]))
                {
                    wrongNumber = input[i];
                    Console.WriteLine($"Wrong Number: {input[i]}");
                    break;
                }
            }

            var xmasCoder2 = new Xmas2(wrongNumber);
            
            int j = 0;
            long res;
            do
            {
                res = xmasCoder2.AddAndCheck(input[j++]);
            }
            while (res == -1);
            

            Console.WriteLine($"Number: {res}");
        }
        private static long[] ReadInput(string filePath)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                return sr.ReadToEnd().Split('\n').Select(s => s.Trim()).Select(long.Parse).ToArray();
            }
        }
    }
    class Xmas
    {
        int _preambleLength;
        List<long> _preamble;

        public Xmas(long[] preamble)
        {
            _preamble = new List<long>(preamble);
            _preambleLength = preamble.Length;
        }

        public bool CheckAndAdd(long number)
        {
            var resCheck = CheckNumber(number);
            if (resCheck)
            {
                _preamble.RemoveAt(0);
                _preamble.Add(number);
            }
            return resCheck;
        }

        private bool CheckNumber(long number)
        {
            for (int i = 0; i < _preambleLength - 1; i++)
            {
                for (int j = i + 1; j < _preambleLength; j++)
                {
                    if (_preamble[i] + _preamble[j] == number)
                        return true;
                }
            }
            return false;
        }
    }

    class Xmas2
    {
        List<long> _preamble;

        long _destNumber;
        public Xmas2(long destNumber)
        {
            _preamble = new List<long>();
            _destNumber = destNumber;
        }

        long preSum = 0;
        public long AddAndCheck(long number)
        {
            _preamble.Add(number);
            preSum += number;
            if (_preamble.Count < 2)
                return -1;

            if (preSum < _destNumber)
                return -1;

            if(preSum > _destNumber)
            {
                while(preSum > _destNumber && _preamble.Count > 1)
                {
                    preSum -= _preamble[0];
                    _preamble.RemoveAt(0);
                }
            }

            if(preSum == _destNumber)
            {
                return _preamble.Min() + _preamble.Max();
            }
            return -1;
        }
    }
}
