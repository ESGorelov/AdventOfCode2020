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

            #region Part 2
            var size = input.Max() + 1;
            bool[,] grafMatrix = new bool[size, size];

            var listInput = new List<int>(input);
            listInput.Insert(0, 0);

            for (int i = 0; i < listInput.Count; i++)
            {
                curValue = listInput[i];

                foreach (var item in listInput.Where(n => n > curValue && n <= curValue + 3))
                {
                    grafMatrix[curValue, item] = true;
                }
            }

            var peakIndex = new Dictionary<int, long>() { { 0, 1 } };
            for (int k = 0; k < listInput.Count; k++)
            {
                int i = listInput[k];

                int j = i + 1;
                while (j <= i + 3 && j < grafMatrix.GetLength(1))
                {
                    if (grafMatrix[i, j])
                    {
                        if (peakIndex.ContainsKey(j))
                        {
                            peakIndex[j] += peakIndex[i];
                        }
                        else
                        {
                            peakIndex.Add(j, peakIndex[i]);
                        }
                    }
                    j++;
                }

            }

            Console.WriteLine($"Count Ways: {peakIndex[peakIndex.Keys.Max()]}");
            #endregion
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
