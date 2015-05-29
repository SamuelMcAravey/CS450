using Perceptron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace NeuralNetworkClassifier
{
    public sealed class PerceptronClassifier<TItem, TClass> : Classifier<TItem, TItem, TClass> where TItem : IClassified<TClass>, IClassifiable
    {
        private readonly Func<IReadOnlyList<double>, TClass> outputConverter;
        private readonly Func<TItem, int, double> neuronExpectedOutput;
        private NeuronLayer<TItem> layer;
        private MultiLayerPerceptron mlp;

        public PerceptronClassifier(Func<IReadOnlyList<double>, TClass> outputConverter, int inputCount, int[] layerSizes)
        {
            this.outputConverter = outputConverter;
            this.mlp = MultiLayerPerceptron.CreateMLP(inputCount, layerSizes, Activators.tanhSigmoidNeuronEvaluator);
        }

        protected override TClass Classify(TItem input)
        {
            var output = this.mlp.Evaluate.Invoke(input.ValueDictionary.Values.Cast<double>().ToArray());
            return this.outputConverter(output);
        }

        public override void Train(IClassifiedDataset<TItem, TClass> trainingDataset)
        {
            return;
        }
    }
}
