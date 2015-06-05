//using Perceptron;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using PerceptronCSharp;
using Utilities;

namespace NeuralNetworkClassifier
{
    public sealed class PerceptronClassifier<TItem, TClass> : Classifier<TItem, TItem, TClass> where TItem : IClassified<TClass>, IClassifiable
    {
        private readonly Func<IReadOnlyList<double>, TClass> outputConverter;
        private readonly Func<TClass, IEnumerable<double>> classToExpectedOutputConverter;
        private readonly Func<TItem, int, double> neuronExpectedOutput;
        private NeuronLayer<TItem> layer;
        private MLP mlp;

        public PerceptronClassifier(Func<IReadOnlyList<double>, TClass> outputConverter, int inputCount, int[] layerSizes, Func<TClass, IEnumerable<double>> classToExpectedOutputConverter)
        {
            this.outputConverter = outputConverter;
            this.classToExpectedOutputConverter = classToExpectedOutputConverter;
            this.mlp = new MLP();

            this.mlp.CreateLayers(inputCount, layerSizes);
        }

        protected override TClass Classify(TItem input)
        {
            var output = this.mlp.CalculateAll(input.ValueDictionary.Values.Cast<double>());
            return this.outputConverter(output);
        }

        public override void Train(IClassifiedDataset<TItem, TClass> trainingDataset)
        {
            foreach (var item in trainingDataset)
            {
                this.mlp.CalculateAll(item.ValueDictionary.Values.Cast<double>());
                this.mlp.UpdateWeights(this.classToExpectedOutputConverter(item.Class));
            }
        }
    }
}
