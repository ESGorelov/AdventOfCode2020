using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] input = File.ReadAllLines("../../input.txt");

            var data = new Data();

            for (int i = 0; i < input.Length; i++)
            {
                data.Write(input[i]);
            }

            Console.WriteLine($"Memory sum = {data.GetSum()}");

            data = new Data();

            for (int i = 0; i < input.Length; i++)
            {
                data.Write2(input[i]);
            }

            Console.WriteLine($"v2 Memory sum = {data.GetSum()}");
            Console.ReadKey();
        }
    }

    public class Data
    {
        /// <summary>Биты на которые влияет маска</summary>
        Dictionary<int, bool> _mask;
        Dictionary<long, long> _memory = new Dictionary<long, long>();

        string _maskStr;

        Regex maskRegex = new Regex(@"^mask = (.+)$");

        private void SetMask(string mask)
        {
            _mask = new Dictionary<int, bool>();
            _maskStr = maskRegex.Match(mask).Groups[1].Value;
            for (int i = 0; i < _maskStr.Length; i++)
            {
                if (_maskStr[i] != 'X')
                    _mask.Add(_maskStr.Length - 1 - i, _maskStr[i] == '1');
            }
        }

        public void Write(string writeCmd)
        {
            if (maskRegex.IsMatch(writeCmd))
                SetMask(writeCmd);
            else
            {
                int addr;
                long value;
                GroupCollection match = new Regex(@"^mem\[(\d+)\] = (\d+)$").Match(writeCmd).Groups;
                addr = int.Parse(match[1].Value);
                value = long.Parse(match[2].Value);

                var sb = new StringBuilder(Convert.ToString(value, 2).PadLeft(36, '0'));

                foreach (var bit in _mask.Keys)
                {
                    if (_mask[bit])
                        sb[35-bit] = '1';
                    else
                        sb[35-bit] = '0';
                }

                value = Convert.ToInt64(sb.ToString(), 2);

                if (_memory.ContainsKey(addr))
                    _memory[addr] = value;
                else
                    _memory.Add(addr, value);
            }
        }

        public void Write2(string writeCmd)
        {
            if (maskRegex.IsMatch(writeCmd))
                SetMask(writeCmd);
            else
            {
                int addr;
                long value;
                GroupCollection match = new Regex(@"^mem\[(\d+)\] = (\d+)$").Match(writeCmd).Groups;
                addr = int.Parse(match[1].Value);
                value = long.Parse(match[2].Value);

                var sb = new StringBuilder(Convert.ToString(addr, 2).PadLeft(36, '0'));

                for (int i = 0; i < _maskStr.Length; i++)
                {
                    if (_maskStr[i] == '0')
                        continue;

                    sb[i] = _maskStr[i];
                }

                long[] addresses = GetAllAddress(sb.ToString());

                foreach (var item in addresses)
                {
                    if (_memory.ContainsKey(item))
                        _memory[item] = value;
                    else
                        _memory.Add(item, value);
                }
            }
        }

        private long[] GetAllAddress(string addMask)
        {
            List<long> result = new List<long>();
            Queue<string> strAddr = new Queue<string>();
            strAddr.Enqueue(addMask);

            while(strAddr.Count > 0)
            {
                var t = strAddr.Dequeue();

                int pos = t.IndexOf('X');
                if (pos == -1)
                {
                    result.Add(Convert.ToInt64(t, 2));
                    continue;
                }
                StringBuilder sb = new StringBuilder(t);
                sb.Replace('X', '0', pos, 1);
                strAddr.Enqueue(sb.ToString());
                sb.Replace('0', '1', pos, 1);
                strAddr.Enqueue(sb.ToString());
            }
            return result.ToArray();
        }

        public long GetSum() => _memory.Select(m => m.Value).Aggregate((t, t1) => t + t1);
    }
}
