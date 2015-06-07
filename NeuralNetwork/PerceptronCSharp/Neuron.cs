using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronCSharp
{
    public sealed class Neuron
    {
        public int Index { get; private set; }
        public int InputCount { get; private set; }
        public List<double> Weights { get; private set; }
        public double Output { get; private set; }
        public double Error { get; private set; }
        public List<Neuron> PreviousNeurons { get; private set; }
        public Layer NextLayer { get; set; }

        public Neuron(int inputCount)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            this.InputCount = inputCount;
            this.Weights = Enumerable
                .Repeat(0, inputCount)
                .Select(_ => rand.NextDouble() - 0.5)
                .ToList();
        }

        public double CalculateOutput(IEnumerable<double> inputs)
        {
            var h = this.Weights.Zip(inputs, (weight, input) => weight * input).Sum();
            this.Output = Activators.eSigmoidActivator(h);
            return this.Output;
        }

        public double CalculateError(IEnumerable<double> inputs)
        {
            var h = this.Weights.Zip(inputs, (weight, input) => weight * input).Sum();
            this.Output = Activators.eSigmoidActivator(h);
            return this.Output;
        }
    }
}
