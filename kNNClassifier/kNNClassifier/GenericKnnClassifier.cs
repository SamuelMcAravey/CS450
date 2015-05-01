using IrisDataset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace kNNClassifier
{
	public sealed class GenericKnnClassifier<TItem, TClass> : Classifier<TItem, TItem, TClass> where TItem : IClassified<TClass>, IClassifiable
    {
		IClassifiedDataset<TItem, TClass> dataset;
		private int k;

		public GenericKnnClassifier(int k)
		{
			this.k = k;
		}

        public override void Train(IClassifiedDataset<TItem, TClass> trainingDataset)
		{
			this.dataset = trainingDataset;
		}

		protected override TClass Classify(TItem input)
		{
			var measured = this.dataset.Select(i => new
			{
				Item = i,
				Distance = this.CalculateDistance(input, i)
			}).OrderBy(i => i.Distance).Take(k).GroupBy(i => i.Item.Class).OrderBy(g => g.Count()).First().First();
			return measured.Item.Class;
		}

		private double CalculateDistance(TItem source, TItem destination)
        {
            List<double> distances = new List<double>();
            foreach (var pair in source.ValueDictionary)
            {
                var d = destination.ValueDictionary[pair.Key];
                distances.Add(Math.Pow(CalculateDistance(pair.Value, d), 2));
            }
            //var distances = from s in source.ValueDictionary
            //                join d in destination.ValueDictionary on s.Key equals d.Key
            //                select Math.Pow(CalculateDistance(s.Value, d.Value), 2);

            var distanceSum = distances.Sum();

            return Math.Sqrt(distanceSum);
        }

        private static double CalculateDistance(object source, object destination)
        {
            if (source.IsNumber())
                return (double)source - (double)destination;

            if (Equals(source, destination))
                return 0;
            else
                return 1;
        }
	}
}
