using IrisDataset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace kNNClassifier
{
	public sealed class KnnClassifier : Classifier<IrisPlant, IrisPlant, string>
	{
		IClassifiedDataset<IrisPlant, string> dataset;
		private int k;

		public KnnClassifier(int k)
		{
			this.k = k;
		}

        public override void Train(IClassifiedDataset<IrisPlant, string> trainingDataset)
		{
			this.dataset = trainingDataset;
		}

		protected override string Classify(IrisPlant input)
		{
			var measured = this.dataset.Select(i => new
			{
				Iris = i,
				Distance = this.CalculateDistance(input, i)
			}).OrderBy(i => i.Distance).Take(k).GroupBy(i => i.Iris.Class).OrderBy(g => g.Count()).First().First();
			return measured.Iris.Class;
		}

		private double CalculateDistance(IrisPlant source, IrisPlant destination)
		{
			return Math.Sqrt(
			Math.Pow(source.PetalLength - destination.PetalLength, 2) +
			Math.Pow(source.PetalWidth - destination.PetalWidth, 2) +
			Math.Pow(source.SepalLength - destination.SepalLength, 2) +
			Math.Pow(source.SepalWidth - destination.SepalWidth, 2));
        }
	}
}
