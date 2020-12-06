using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day02
{
    class Program
    {
        readonly static List<string> _searchPattern = new List<string>
        {
            "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid", "cid",
        };

        readonly static List<Tuple<string, PassRule>> _searchPattern2 = new List<Tuple<string, PassRule>>
        {
            new Tuple<string, PassRule>("byr", new ByrRule()),
            new Tuple<string, PassRule>("iyr", new IyrRule()),
            new Tuple<string, PassRule>("eyr", new EyrRule()),
            new Tuple<string, PassRule>("hgt", new HgtRule()),
            new Tuple<string, PassRule>("hcl", new HclRule()),
            new Tuple<string, PassRule>("ecl", new EclRule()),
            new Tuple<string, PassRule>("pid", new PidRule()),
        };

        readonly static List<string> _searchPatternAsOptional = new List<string>
        {
            "cid",
        };
        static void Main(string[] args)
        {

            string[] input = ReadInput("../../input.txt");

            int countAllpass = 0;
            int countpassInvalid = 0;
            int i = 0;
            while (i < input.Length)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    i++;
                    continue;
                }
                var pass = new List<string>();

                while (i < input.Length && !string.IsNullOrEmpty(input[i]))
                {
                    pass.AddRange(input[i].Split(' '));
                    i++;
                }
                countAllpass++;

                #region Part 1
                //foreach (var item in _searchPattern)
                //{
                //    if (!pass.Any(s => s.Contains(item)) && !_searchPatternAsOptional.Contains(item))
                //    {
                //        countpassInvalid++;
                //        break;
                //    }
                //}
                #endregion

                #region Part 2
                foreach (var item in _searchPattern2)
                {
                    string keyValue = pass.FirstOrDefault(s => s.Contains(item.Item1));
                    if (keyValue == null)
                    {
                        if (!_searchPatternAsOptional.Contains(item.Item1))
                        {
                            countpassInvalid++;
                            break;
                        }
                        else
                            continue;
                    }

                    if (!item.Item2.IsValid(keyValue.Split(':')[1]))
                    {
                        countpassInvalid++;
                        break;
                    }
                }
                #endregion

                i++;
            }
            Console.WriteLine($"AllPass: {countAllpass} | is valid: {countAllpass - countpassInvalid} | is invalid: {countpassInvalid}");
            Console.ReadKey();
        }

        private static string[] ReadInput(string filePath)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                return sr.ReadToEnd().Split('\n').Select(s => s.Trim()).ToArray();
            }
        }
    }

    abstract class PassRule
    {
        abstract public bool IsValid(string value);
    }

    class ByrRule : PassRule
    {
        public override bool IsValid(string value)
        {
            if (int.TryParse(value, out int v))
            {
                return v >= 1920 && v <= 2002;
            }
            return false;
        }
    }

    class IyrRule : PassRule
    {
        public override bool IsValid(string value)
        {
            if (int.TryParse(value, out int v))
            {
                return v >= 2010 && v <= 2020;
            }
            return false;
        }
    }
    class EyrRule : PassRule
    {
        public override bool IsValid(string value)
        {
            if (int.TryParse(value, out int v))
            {
                return v >= 2020 && v <= 2030;
            }
            return false;
        }
    }

    class HgtRule : PassRule
    {
        public override bool IsValid(string value)
        {
            var unitRegex = new Regex(@"(\w{2})$");

            string unit = unitRegex.Match(value).Value;

            Regex regex;
            switch (unit)
            {
                case "cm":
                    regex = new Regex(@"^\d{3}");
                    if (int.TryParse(regex.Match(value).Value, out int v))
                    {
                        return v >= 150 && v <= 193;
                    }
                    break;
                case "in":
                    regex = new Regex(@"^\d{2}");
                    if (int.TryParse(regex.Match(value).Value, out int v2))
                    {
                        return v2 >= 59 && v2 <= 76;
                    }
                    break;
            }
            return false;
        }
    }
    class HclRule : PassRule
    {
        public override bool IsValid(string value)
        {
            return new Regex(@"^#[a-f0-9]{6}$").IsMatch(value);
        }
    }
    class EclRule : PassRule
    {
        readonly static List<string> _searchPattern = new List<string>
        {
            "amb", "blu", "brn", "hgt", "gry", "grn", "hzl", "oth",
        };
        public override bool IsValid(string value)
        {
            return _searchPattern.Contains(value);
        }
    }

    class PidRule : PassRule
    {
        public override bool IsValid(string value)
        {
            return new Regex(@"^[0-9]{9}$").IsMatch(value);
        }
    }
}
