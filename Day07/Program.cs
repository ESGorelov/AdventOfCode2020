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
        static List<IBag> AllBags;
        static void Main(string[] args)
        {

            string[] input = ReadInput("../../input.txt");

            var regexBaseBag = new Regex(@"^\w+ \w+ bags contain");
            var regexBaseBagReplace = new Regex(@" bags contain");


            var regexInsideBag = new Regex(@"\d+ \w+ \w+ bag[s,|s.|.]");

            var regexCountBag = new Regex(@"^\d+");
            var regexBagName = new Regex(@"^\w+ \w+");


            AllBags = new List<IBag>();
            foreach (var item in input)
            {
                var BagName = regexBaseBagReplace.Replace(regexBaseBag.Match(item).Value, "");

                var bag = AllUniqueBags.GetBag(BagName);

                var right = regexBaseBag.Replace(item, "").Trim();

                if (!right.Equals("no other bags."))
                {
                    var matchs = regexInsideBag.Matches(right);
                    foreach (object match in matchs)
                    {
                        var s = match.ToString();
                        if (int.TryParse(regexCountBag.Match(s).Value, out int k))
                        {
                            s = regexCountBag.Replace(s, "").Trim();

                            string name = regexBagName.Match(s).Value;
                            bag.InsideBags.Add(new BagHeaderWithCount(AllUniqueBags.GetBag(name), k));
                        }
                        else
                            throw new Exception($"Строка {s} имеет не верный формат");
                    }
                }

                AllBags.Add(bag);
            }

            Console.WriteLine(CountBag(AllUniqueBags.GetBag("shiny gold")));

        }

        private static string[] ReadInput(string filePath)
        {
            using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
            {
                return sr.ReadToEnd().Split('\n').Select(s => s.Trim()).ToArray();
            }
        }

        private static bool SearchBag(string color, IBag bag)
        {
            AllUniqueBags.ClearUsed();
            Queue<IBag> queue = new Queue<IBag>();
            bag.Used = true;
            foreach (var itemBag in bag.InsideBags)
            {
                if (!itemBag.Used)
                {
                    queue.Enqueue(itemBag);
                }
            }

            while (queue.Count > 0)
            {
                var qB = queue.Dequeue();
                if (qB.Header.Color == color)
                    return true;

                qB.Used = true;
                foreach (var itemBag in qB.InsideBags)
                {
                    if (!itemBag.Used)
                    {
                        itemBag.Used = true;
                        queue.Enqueue(itemBag);
                    }
                }
            }
            return false;
        }
        private static int CountBag(IBag bag)
        {
            int countBags = 0;
            AllUniqueBags.ClearUsed();
            var queue = new Queue<Tuple<int, IBag>>();
            foreach (var itemBag in bag.InsideBags)
            {
                if (!itemBag.Used)
                {
                    countBags += itemBag.Count;
                    queue.Enqueue(new Tuple<int, IBag>(itemBag.Count, itemBag));
                }
            }

            while (queue.Count > 0)
            {
                var qB = queue.Dequeue();
                foreach (var itemBag in qB.Item2.InsideBags)
                {
                    if (!itemBag.Used)
                    {
                        countBags += qB.Item1 * itemBag.Count;
                        queue.Enqueue(new Tuple<int, IBag>(qB.Item1 * itemBag.Count, itemBag));
                    }
                }
            }

            return countBags;
        }
    }

    static class AllUniqueBags
    {
        static Dictionary<string, int> _colors = new Dictionary<string, int>();

        static List<IBag> _allUniqueBags = new List<IBag>();
        static int internalId = 1;

        public static IBag GetBag(string bagColor)
        {
            if (_colors.TryGetValue(bagColor, out int id))
            {
                return _allUniqueBags.First(b => b.Header.Color == bagColor);
            }
            else
            {
                _colors.Add(bagColor, internalId);
                var bag = new Bag(new BagHeader(internalId, bagColor));
                _allUniqueBags.Add(bag);
                return bag;
            }
        }
        public static void ClearUsed()
        {
            foreach (var item in _allUniqueBags)
            {
                item.Used = false;
            }
        }

    }

    public interface IBagHeader
    {
        string Color { get; }
    }

    public interface IBag
    {
        IBagHeader Header { get; }
        List<IBagWithCount> InsideBags { get; }
        bool Used { get; set; }
    }

    public interface IBagWithCount : IBag
    {
        int Count { get; }
    }

    class BagHeader : IBagHeader
    {
        public BagHeader(int id, string color)
        {
            Id = id;
            Color = color;
        }
        public int Id { get; protected set; }
        public string Color { get; protected set; }
    }

    class BagHeaderWithCount : IBagWithCount
    {
        IBag _bag;
        public BagHeaderWithCount(IBag bag, int count)
        {
            _bag = bag;
            Count = count;
        }
        public int Count { get; protected set; }

        public IBagHeader Header => _bag.Header;

        public bool Used { get => _bag.Used; set => _bag.Used = value; }

        public List<IBagWithCount> InsideBags => _bag.InsideBags;
    }

    class Bag : IBag
    {
        public bool Used { get; set; }
        public Bag(IBagHeader header)
        {
            Header = header;
            InsideBags = new List<IBagWithCount>();
        }
        public IBagHeader Header { get; protected set; }
        public List<IBagWithCount> InsideBags { get; protected set; }
    }
}
