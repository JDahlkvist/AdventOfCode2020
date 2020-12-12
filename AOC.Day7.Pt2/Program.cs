using AOC.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC.Day7.Pt2
{
    class Program
    {
        static List<Bag> totalBagCollection = new List<Bag>();
        static List<Bag> bagCount = new List<Bag>();

        static void Main(string[] args)
        {
            var inputText = FileReader.GetProgramInput();
            var answers = FileReader.SplitInputByNewLine(inputText).ToArray();


            foreach (var answ in answers)
            {
                var bagsInfo = answ.Split(
                    new string[] { "bags contain", "bags.", "bags", "bag", "no other" },
                    StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim(new char[] { ',', '.', ' ' }))
                    .Where(c => !string.IsNullOrEmpty(c))
                    .ToArray();

                var parentName = bagsInfo[0];
                CreateBagIfNotExist(parentName);

                if (bagsInfo.Count() == 1) continue;

                if (bagsInfo.Count() > 1)
                {
                    var bagList = new List<Bag>();
                    for (int i = 1; i < bagsInfo.Length; i++)
                    {
                        var bag = bagsInfo[i].Split(' ');
                        for (int j = 0; j < int.Parse(bag[0]); j++)
                        {
                            var bagName = $"{bag[1]} {bag[2]}";

                            var parent = totalBagCollection.Where(b => string.Equals(b.Name, parentName)).FirstOrDefault();
                            if (parent != null)
                            {
                                if (parent.Children == null) { parent.Children = new List<Bag>(); }
                                parent.Children.Add(CreateBagIfNotExist(bagName, parent));
                            }
                        }
                    }
                }
            }
            CountChildren(totalBagCollection.Where(b => b.Name == "shiny gold").FirstOrDefault());

            Console.WriteLine($"Number of bags {bagCount.Count} ");
            Console.ReadKey();
        }

        static Bag CreateBagIfNotExist(string bagName, Bag parent = default)
        {
            Bag bag = totalBagCollection.Where(bag => string.Equals(bag.Name, bagName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (bag == null)
            {
                bag = new Bag(bagName);
                totalBagCollection.Add(bag);
            }
            if (parent != default)
            {
                if (bag.Parents == null) { bag.Parents = new HashSet<Bag>(); }
                bag.Parents.Add(parent);
            }
            return bag;
        }
        static void CountChildren(Bag bag)
        {
            foreach (var child in bag.Children)
            {
                bagCount.Add(child);
                if (child.Children != null)
                {
                    CountChildren(child);
                }
            }
        }
    }
    class Bag
    {
        public Bag(string name, List<Bag> bags = null)
        {
            if (bags != null) Children = new List<Bag>(bags);
            Name = name;
        }

        public List<Bag> Children { get; set; }
        public string Name { get; set; }
        public HashSet<Bag> Parents { get; set; }
    }
}
