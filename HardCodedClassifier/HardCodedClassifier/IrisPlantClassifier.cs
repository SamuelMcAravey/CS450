using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace HardCodedClassifier
{
    public sealed class IrisPlantClassifier : Classifier<IrisPlant>
    {
        private string plantClass = null;
        protected override void Train(IrisPlant observation)
        {
            if (string.IsNullOrWhiteSpace(this.plantClass))
                this.plantClass = observation.Class;
        }

        protected override string Classify(IrisPlant observation)
        {
            return observation.Class;
        }

        protected override string Predict(IrisPlant observation)
        {
            return this.plantClass;
        }
    }
}
