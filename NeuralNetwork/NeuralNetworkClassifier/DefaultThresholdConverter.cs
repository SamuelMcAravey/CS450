using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClassifier
{
    public sealed class DefaultNeuronInputConverter : INeuronInputConverter<double>
    {
        private readonly double threshold;

        public DefaultNeuronInputConverter(double threshold = 0)
        {
            this.threshold = threshold;
        }

        public double ConvertInput(double total)
        {
            return total > this.threshold ? 1 : 0;
        }
    }
}
