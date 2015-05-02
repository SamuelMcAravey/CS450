using IrisDataset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using MathNet.Numerics.Statistics;

namespace kNNClassifier
{
	public sealed class GenericKnnClassifier<TItem, TClass> : Classifier<TItem, TItem, TClass> where TItem : IClassified<TClass>, IClassifiable
    {
		IClassifiedDataset<TItem, TClass> dataset;
        IDistanceCalculator<TItem> distanceCalculator;
        private int k;

		public GenericKnnClassifier(int k, IDistanceCalculator<TItem> distanceCalculator = null)
		{
			this.k = k;
            this.distanceCalculator = distanceCalculator;
            if (this.distanceCalculator == null)
                this.distanceCalculator = new EuclideanDistanceCalculator<TItem>();
        }

        public override void Train(IClassifiedDataset<TItem, TClass> trainingDataset)
		{
            this.dataset = trainingDataset;
            var first = this.dataset.FirstOrDefault();
            if (first == null)
                return;

            // Standardize the data when it's a double
            foreach (var property in first.ValueDictionary.Keys.Where(p => first.ValueDictionary[p] is double))
            {
                var values = this.dataset.Select(i => i.ValueDictionary[property]).Cast<double>().ToList();
                var meanStandardDeviation = Statistics.MeanStandardDeviation(values);
                foreach (var item in this.dataset)
                {
                    var value = ((double)item.ValueDictionary[property] - meanStandardDeviation.Item1) / meanStandardDeviation.Item2;
                    item.SetValue(property, value);
                }
            }
		}

		protected override TClass Classify(TItem input)
		{
			var measured = this.dataset.Select(i => new
			{
				Item = i,
				Distance = this.distanceCalculator.CalculateDistance(input, i)
			}).OrderBy(i => i.Distance).Take(k).GroupBy(i => i.Item.Class).OrderBy(g => g.Count()).First().First();
			return measured.Item.Class;
		}
	}
}
