using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;
using Utilities;

namespace DecisionTreeClassifier
{
    public class ID3DecisionTreeClassifier<TItem, TClass> : Classifier<TItem, TItem, TClass> where TItem : IClassified<TClass>, IClassifiable
    {
        public override string ToString()
        {
            return this.tree.ToString();
        }

        private Dictionary<string, IEnumerable<object>> attributeValues;
        private TreeNode tree;
        //private Tree 
        protected override TClass Classify(TItem input)
        {
            var classified = this.Classify(input, this.tree);
            return classified;
        }

        private TClass Classify(TItem input, TreeNode tree)
        {
            var value = input.ValueDictionary[tree.Attribute.ToString()];
            if (Equals(value, "???NoValue???"))
            {
                var topClass = tree.Children
                                   .Where(pair => !Equals(pair.Key, "???NoValue???"))
                                   .Select(pair =>
                                           {
                                               var c = tree.GetChild(pair.Key);
                                               if (c.Count == 0)
                                                   return (TClass) c.Attribute;
                                               return this.Classify(input, c);
                                           })
                                   .GroupBy(classification => classification)
                                   .Select(grouping => Tuple.Create(grouping.Key, grouping.Count()))
                                   .OrderBy(tuple => tuple.Item2).First();
                return topClass.Item1;
            }

            var child = tree.GetChild(value);
            if (child.Count == 0)
                return (TClass) child.Attribute;
            return this.Classify(input, child);
        }

        public override void Train(IClassifiedDataset<TItem, TClass> trainingDataset)
        {
            this.attributeValues = trainingDataset
                .SelectMany(item => item.ValueDictionary)
                .GroupBy(pair => pair.Key)
                .ToDictionary(pairs => pairs.Key, pairs => pairs.Where(pair => Equals(pair.Value, "???NoValue???"))
                .Select(pair => pair.Value)
                .Distinct());

            this.tree = this.BuildTree(trainingDataset.ToList(), trainingDataset.First().ValueDictionary.Keys.ToList());
        }

        private TreeNode BuildTree(IReadOnlyList<TItem> set, IReadOnlyCollection<string> availableAttributes)
        {
            if (availableAttributes.Count == 0)
                return new TreeNode(set.Select(i => i.Class).GroupBy(c => c).OrderBy(g => g.Count()).First().Key);
            if (set.Select(s=> s.Class).Distinct().Count() == 1)
                return new TreeNode(set.First().Class);

            var size = set.Count;
            var weights = availableAttributes.ToDictionary(
                attribute => attribute,
                attribute => set.GroupBy(item => item.ValueDictionary[attribute])
                                .ToDictionary(items => items.Key, items => Tuple.Create((double) items.Count(), this.GetEntropy(items)))
                                .Sum(pair => (pair.Value.Item1/size)*pair.Value.Item2));
            var bestAttribute = weights.OrderBy(pair => pair.Value).First();
            var remainingAttributes = availableAttributes.Except(new[] {bestAttribute.Key});
            var branches = set.GroupBy(item => item.ValueDictionary[bestAttribute.Key]).ToDictionary(items => items.Key, items => this.BuildTree(items.ToList(), remainingAttributes.ToList()));
            
            var tree = new TreeNode(bestAttribute.Key);
            
            foreach (var branch in branches)
            {
                if (Equals(branch.Key, "???NoValue???"))
                    continue;
                tree.Add(branch.Value, branch.Key);
            }

            var bestAttributeValues = this.attributeValues[bestAttribute.Key];
            var except = bestAttributeValues.Except(tree.BranchNames);
            var firstClass = set.First().Class;
            foreach (var e in except)
            {
                if (Equals(e, "???NoValue???"))
                    continue;

                tree.Add(new TreeNode(firstClass), e);
            }

            return tree;
        }

        //private double GetGain(IReadOnlyList<TItem> set, string attribute)
        //{
        //    double s = set.Count;
        //    var setEntropy = this.GetEntropy(set, s);
        //    var subsetEntropy = set
        //        .GroupBy(item => item.ValueDictionary[attribute])
        //        .Select(items => this.GetEntropy(items, set.Count)*(items.Count()/s))
        //        .Sum();
        //    return setEntropy - subsetEntropy;
        //}

        private double GetEntropy(IEnumerable<TItem> set)
        {
            var items = set as IList<TItem> ?? set.ToList();
            var groups = items.GroupBy(item => item.Class).ToList();
            if (groups.Count() == 1)
                return 0;

            double s = items.Count();
            var entropy = groups
                .Select(grouping => grouping.Count() / s)
                .Select(p => p * Math.Log(p, 2))
                .Sum() * -1;
            return entropy;
        }
    }
}
