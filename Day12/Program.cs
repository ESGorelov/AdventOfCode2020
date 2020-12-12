using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = System.IO.File.ReadAllLines(@"../../input.txt");

            var ns = new NavSystem();

            foreach (var item in input)
            {
                ns.SetCommand((Command)item[0], int.Parse(item.Substring(1)));
            }

            Console.WriteLine(Math.Abs(ns.NorthSouth) + Math.Abs(ns.WestEast));
        }
    }

    class NavSystem
    {
        public Direction curDirection { get; protected set; } = Direction.East;
        public int NorthSouth { get; protected set; } = 0;
        public int WestEast { get; protected set; } = 0;

        public void SetCommand(Command cmd, int arg)
        {
            switch (cmd)
            {
                case Command.N:
                    NorthSouth += arg;
                    break;
                case Command.E:
                    WestEast += arg;
                    break;
                case Command.S:
                    NorthSouth -= arg;
                    break;
                case Command.W:
                    WestEast -= arg;
                    break;
                case Command.L:
                    curDirection = (Direction)(((int)curDirection + ((360-arg) / 90)) % 4);
                    break;
                case Command.R:
                    curDirection = (Direction)(((int)curDirection + (arg / 90)) % 4);
                    break;
                case Command.F:
                    switch (curDirection)
                    {
                        case Direction.East:
                            WestEast += arg;
                            break;
                        case Direction.South:
                            NorthSouth -= arg;
                            break;
                        case Direction.West:
                            WestEast -= arg;
                            break;
                        case Direction.North:
                            NorthSouth += arg;
                            break;
                        default:
                            throw new Exception();
                    }
                    break;
                default:
                    throw new Exception();
            }
        }
    }


    enum Direction
    {
        East = 0,
        South = 1,
        West = 2,
        North = 3,
    }

    enum Command
    {
        N = 'N',
        E = 'E',
        S = 'S',
        W = 'W',
        R = 'R',
        L = 'L',
        F = 'F',
    }
}
