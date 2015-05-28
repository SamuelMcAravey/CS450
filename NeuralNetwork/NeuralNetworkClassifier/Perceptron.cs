using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace NeuralNetworkClassifier
{
    public sealed class PerceptronX<TItem, TClass> : Classifier<TItem, TItem, TClass> where TItem : IClassified<TClass>, IClassifiable
    {
        private readonly Func<IReadOnlyList<double>, TClass> outputConverter;
        private readonly Func<TItem, int, double> neuronExpectedOutput;
        private NeuronLayer<TItem> layer;

        public PerceptronX(
            Func<IReadOnlyList<double>, TClass> outputConverter, 
            Func<TItem, int, double> neuronExpectedOutput, 
            int neuronCount, 
            Func<TItem, string, double> propertySelector, 
            IReadOnlyCollection<string> inputNames)
        {
            this.outputConverter = outputConverter;
            this.neuronExpectedOutput = neuronExpectedOutput;
            this.layer = Neuron.CreateNumericNeuronLayer(neuronCount, propertySelector, inputNames);
        }

        protected override TClass Classify(TItem input)
        {
            var output = this.layer.LayerOutput(input);
            return this.outputConverter(output);
        }

        public override void Train(IClassifiedDataset<TItem, TClass> trainingDataset)
        {
            foreach (var item in trainingDataset)
            {
                var output = layer.LayerOutput(item);
                for (int i = 0; i < output.Count; i++)
                {
                    var expected = neuronExpectedOutput(item, i);
                    var weights = layer.NeuronInputWeights[i];
                    foreach (var weight in weights)
                    {
                        weight.Value.SetWeight(weight.Value.GetWeight() - 0.2*(output[i] - expected)*(double) item.ValueDictionary[weight.Key]);
                    }
                }
            }
        }
    }
}
