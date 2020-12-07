using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = ReadAndParseInput("../../input.txt");

            var tickets = new List<Ticket>();
            foreach (var item in input)
                tickets.Add(new Ticket(item));

            SortedSet<int> sortedTicket = new SortedSet<int>();

            int maxID = 0;
            foreach (var item in tickets)
            {
                int id = item.GetID();
                sortedTicket.Add(id);
                if (id > maxID)
                    maxID = id;
            }

            Console.WriteLine($"Max ID: {maxID}");

            int[] ticketArray = sortedTicket.ToArray();
            for (int i = 0; i < ticketArray.Length - 1; i++)
            {
                if (ticketArray[i] + 1 == ticketArray[i + 1])
                    continue;

                Console.WriteLine($"i: {ticketArray[i]} i+1: {ticketArray[i + 1]}");
                Console.WriteLine($"My ID: {ticketArray[i] + 1}");
            }

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

    class Ticket
    {
        string _code;

        string _rightBlock;
        string _leftBlock;

        public Ticket(string code)
        {
            _code = code;
            _leftBlock = code.Substring(0, 7);
            _rightBlock = code.Substring(7, 3);
        }

        private int? _rowNumber;
        private int? _columnNumber;
        public int GetNumberRow()
        {
            if (_rowNumber != null)
                return _rowNumber.Value;

            int rowLow = 0;
            int rowHigh = 128;

            foreach (var item in _leftBlock)
            {
                if (item.Equals('F'))
                {
                    rowHigh = rowLow + (rowHigh - rowLow) / 2;
                }
                else if (item.Equals('B'))
                {
                    rowLow = rowHigh - (rowHigh - rowLow) / 2;
                }
                else
                    throw new Exception($"Неожиданный символ: {item}");
            }
            _rowNumber = rowLow;
            return _rowNumber.Value;

        }
        public int GetNumberColumn()
        {
            if (_columnNumber != null)
                return _columnNumber.Value;

            int colLow = 0;
            int colHigh = 8;

            foreach (var item in _rightBlock)
            {
                if (item.Equals('L'))
                {
                    colHigh = colLow + (colHigh - colLow) / 2;
                }
                else if (item.Equals('R'))
                {
                    colLow = colHigh - (colHigh - colLow) / 2;
                }
                else
                    throw new Exception($"Неожиданный символ: {item}");
            }
            _columnNumber = colLow;
            return _columnNumber.Value;
        }

        public int GetID()
        {
            return GetNumberRow() * 8 + GetNumberColumn();
        }

    }
}
