using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    class Program
    {
        const char FREE_SEAT = 'L';
        const char LOCK_SEAT = '#';
        const char FLOOR = '.';

        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines(@"../../input.txt");

            char[,] layout = new char[input.Length, input[0].Length];

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    layout[i, j] = input[i][j];
                }
            }

            int countChange = 1;
            while (Change(layout))
            {
                Console.WriteLine($"Change: {countChange++}");
            }

            Console.WriteLine($"Count lock seats: {GetCountLockSeat(layout)}");
        }

        static bool Change(char[,] layout)
        {
            bool[,] changeState = new bool[layout.GetLength(0), layout.GetLength(1)];

            for (int i = 0; i < layout.GetLength(0); i++)
            {
                for (int j = 0; j < layout.GetLength(1); j++)
                {
                    if (layout[i, j] == LOCK_SEAT)
                    {
                        if (GetCountLockArrange(layout,i,j) >= 4)
                            changeState[i, j] = true;
                    }
                    else if (layout[i, j] == FREE_SEAT)
                    {                      
                        if (GetCountLockArrange(layout, i, j) == 0)
                            changeState[i, j] = true;
                    }
                    else if (layout[i, j] == FLOOR)
                    {
                        continue;
                    }
                    else
                    {
                        throw new Exception($"Неожиданный символ {i}/{j}: {layout[i, j]}");
                    }
                }
            }

            bool wasChange = false;
            for (int i = 0; i < layout.GetLength(0); i++)
            {
                for (int j = 0; j < layout.GetLength(1); j++)
                {
                    if (changeState[i, j])
                    {
                        wasChange = true;
                        if (layout[i, j] == LOCK_SEAT)
                            layout[i, j] = FREE_SEAT;
                        else if (layout[i, j] == FREE_SEAT)
                            layout[i, j] = LOCK_SEAT;
                        else
                            throw new Exception("Пол изменять нельзя");
                    }

                }
            }
            return wasChange;

        }

        static int GetCountLockArrange(char[,] layout, int i, int j)
        {
            int countLock = 0;
            for (int k = -1; k <= 1; k++)
            {
                for (int q = -1; q <= 1; q++)
                {
                    if (i + k < 0 ||
                        j + q < 0 ||
                        (k == 0 && q == 0) ||
                        (i + k >= layout.GetLength(0)) ||
                        (j + q >= layout.GetLength(1)))
                        continue;
                    if (layout[i + k, j + q] == LOCK_SEAT)
                        countLock++;
                }
            }
            return countLock;
        }

        static int GetCountLockSeat(char[,] layout)
        {
            int count = 0;
            for (int i = 0; i < layout.GetLength(0); i++)
            {
                for (int j = 0; j < layout.GetLength(1); j++)
                {
                    if (layout[i, j] == LOCK_SEAT)
                        count++;
                }
            }
            return count;
        }
    }
}
