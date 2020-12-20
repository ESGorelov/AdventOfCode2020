using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines(@"../../input.txt");

            long res = 0;
            foreach (var item in input)
            {
                res += Solve(item).Item1;
            }

            Console.WriteLine(res);
        }

        static Tuple<long, int> Solve(string expression, int curIndex = 0)
        {
            long? left = null;
            long? right = null;
            char? operation = null;
            int i = curIndex;

            for (; i < expression.Length && expression[i] != ')'; i++)
            {
                if (char.IsWhiteSpace(expression[i]))
                    continue;

                if (char.IsDigit(expression[i]))
                {
                    if (left == null)
                        left = Convert.ToInt32(expression[i].ToString(), 10);
                    else if (right == null)
                    {
                        right = Convert.ToInt32(expression[i].ToString(), 10);

                        switch (operation)
                        {
                            case '+':
                                left += right;
                                break;
                            case '*':
                                left *= right;
                                break;
                            default:
                                throw new Exception("Продолжение не верно");
                        }

                        right = null;
                        operation = null;
                    }
                    else
                        throw new Exception("Продолжение не верно");

                }
                else if (expression[i] == '(')
                {
                    Tuple<long, int> res = Solve(expression, i + 1);


                    if (left == null)
                        left = res.Item1;
                    else if (right == null)
                    {
                        right = res.Item1;

                        switch (operation)
                        {
                            case '+':
                                left += right;
                                break;
                            case '*':
                                left *= right;
                                break;
                            default:
                                throw new Exception("Продолжение не верно");
                        }

                        right = null;
                        operation = null;
                    }
                    else
                        throw new Exception("Продолжение не верно");

                    i = res.Item2;
                } else
                {
                    operation = expression[i];
                }

            }
            return new Tuple<long, int>(left.Value, i);
        }
    }
}
