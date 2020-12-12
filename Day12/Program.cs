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

            Console.WriteLine(Math.Abs(ns.ShipNorth) + Math.Abs(ns.ShipEast));
        }
    }

    class NavSystem
    {
        public int ShipNorth { get; protected set; } = 0;
        public int ShipEast { get; protected set; } = 0;

        public int PointNorth { get; protected set; } = 1;
        public int PointEast { get; protected set; } = 10;

        public void SetCommand(Command cmd, int arg)
        {
            switch (cmd)
            {
                case Command.N:
                    PointNorth += arg;
                    break;
                case Command.E:
                    PointEast += arg;
                    break;
                case Command.S:
                    PointNorth -= arg;
                    break;
                case Command.W:
                    PointEast -= arg;
                    break;
                case Command.L:
                    RotatePointToRight90((360 - arg) / 90);
                    break;
                case Command.R:
                    RotatePointToRight90( arg / 90);
                    break;
                case Command.F:
                    ShipEast += arg * PointEast;
                    ShipNorth += arg * PointNorth;
                    break;
                default:
                    throw new Exception();
            }
        }

        private void RotatePointToRight90(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int temp = PointNorth;
                PointNorth = -1 * PointEast;
                PointEast = temp;
            }
        }
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
