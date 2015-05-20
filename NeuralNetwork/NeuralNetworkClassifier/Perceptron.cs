using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClassifier
{
    public sealed class Perceptron<TInput>
    {
        private readonly Func<TInput, IReadOnlyDictionary<string, double>> getInputs;
        Dictionary<int, Tuple<NumericNeuron, MutableWeight>> neuronDictionary = new Dictionary<int, Tuple<NumericNeuron, MutableWeight>>();
        private readonly Dictionary<string, Tuple<Subject<double>, MutableWeight>> networkInputs;
        private NumericNeuron neuron = new NumericNeuron();

        public Perceptron(IEnumerable<string> inputNames, Func<TInput, IReadOnlyDictionary<string,double>> getInputs)
        {
            this.getInputs = getInputs;

            this.networkInputs = inputNames.ToDictionary(name => name, name => Tuple.Create(new Subject<double>(), new MutableWeight()));
        }

        public double CalculateOutput(TInput input)
        {
            var inputs = this.getInputs(input);

            var r = from inputPair in inputs
                join networkInput in this.networkInputs on inputPair.Key equals networkInput.Key
                select new
                {
                    Subject = networkInput.Value,
                    Value = inputPair.Value
                };

            foreach (var value in r)
            {
                value.Subject.Item1.OnNext(value.Value);
            }

            return this.neuron.Value;
        }

        private void UpdateWeights(TInput input, double desiredResult)
        {
            var inputs = this.getInputs(input);
            foreach (var networkInput in networkInputs)
            {
                var newWeight = networkInput.Value.Item2.GetWeight() + 0.3*(desiredResult - CalculateOutput(input))*inputs[networkInput.Key];
                networkInput.Value.Item2.SetWeight(newWeight);
            }
        }
    }
}
