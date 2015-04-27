using IrisDataset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace HardCodedClassifier
{
    public sealed class IrisPlantClassifier : Classifier<IrisPlant, IrisPlant, string>
    {
        private string plantClass = null;

		public override void Train(IClassifiedDataset<IrisPlant, string> trainingDataset)
		{
		}

		protected override string Classify(IrisPlant input)
        {
            if (string.IsNullOrWhiteSpace(this.plantClass))
                this.plantClass = input.Class;
            return this.plantClass;
        }
    }
}
