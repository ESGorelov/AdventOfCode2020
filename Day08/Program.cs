using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day08
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] input = ReadInput("../../input.txt");

            var cmd = new List<IComand>();

            foreach (var item in input)
                cmd.Add(new CommandWithChangeFlag(new Command(item)));

            var cdHandler = new CommandHandler();
            cdHandler.CommandsList = cmd;

            do
            {
                NextChange(cmd);
            }
            while (!cdHandler.Handle());

        }
        private static void NextChange(List<IComand> cmd)
        {
            foreach (var item in cmd)
            {
                if (((CommandWithChangeFlag)item).Change())
                    return;
            }
        }

        private static string[] ReadInput(string filePath)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                return sr.ReadToEnd().Split('\n').Select(s => s.Trim()).ToArray();
            }
        }
    }

    public interface IComand
    {
        string CommandText { get;}
        int Argument { get; }
    }

    public class CommandWithChangeFlag : IComand
    {
        IComand _cmd;
        public CommandWithChangeFlag(IComand cmd)
        {
            _cmd = cmd;
        }
        public string _overrideCommandText;
        public string CommandText => _overrideCommandText ?? _cmd.CommandText;
        public int Argument => _cmd.Argument;

        bool _wasChanged;
        public bool Change()
        {
            if (_wasChanged)
                return false;
            if (_overrideCommandText != null)
            {
                _wasChanged = true;
                _overrideCommandText = null;
                return false;
            }
            switch (_cmd.CommandText)
            {
                case "nop":
                    _overrideCommandText = "jmp";
                    break;
                case "acc":
                    return false;
                case "jmp":
                    _overrideCommandText = "nop";
                    break;
                default:
                    throw new ArgumentException($"Неожиданная команда: {_cmd.CommandText}");
            }
            return true;
        }
    }

    public class Command : IComand
    {
        public Command(string comandLine)
        {
            string[] t = comandLine.Split(' ');
            CommandText = t[0];
            Argument = int.Parse(t[1]);
        }
        public string CommandText { get; protected set; }
        public int Argument { get; protected set; }
    }

    public class CommandHandler
    {
        public List<IComand> CommandsList { get; set; }

        public bool Handle()
        {
            int cmdLine = 0;
            int Acc = 0;

            bool[] used = new bool[CommandsList.Count];

            while (cmdLine != CommandsList.Count)
            {
                if (used[cmdLine])
                    return false;

                used[cmdLine] = true;
                var command = CommandsList[cmdLine];

                switch (command.CommandText)
                {
                    case "nop":
                        cmdLine++;
                        break;
                    case "acc":
                        Acc += command.Argument;
                        cmdLine++;
                        break;
                    case "jmp":
                        cmdLine += command.Argument;
                        break;
                    default:
                        throw new ArgumentException($"Неожиданная команда: {command.CommandText}");
                }
            }
            Console.WriteLine($"Program Done: Acc = {Acc}");
            return true;
        }


    }
}
