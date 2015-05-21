using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Security.Cryptography;

namespace NeuralNetworkClassifier
{
    public sealed class NeuronLayer<TInput>
    {
        public NeuronLayer(IReadOnlyList<IReadOnlyDictionary<string, MutableWeight>> neuronInputWeights, Func<TInput, List<double>> layerOutput)
        {
            NeuronInputWeights = neuronInputWeights;
            LayerOutput = layerOutput;
        }

        public IReadOnlyList<IReadOnlyDictionary<string, MutableWeight>> NeuronInputWeights { get; }
        public Func<TInput, List<double>> LayerOutput { get; }
    }
    public static class Neuron
    {
        public static Func<T> CreateNeuron<T>(
            INeuronInputConverter<T> neuronInputConverter,
            params Func<double>[] inputs)
        {
            return () => neuronInputConverter.ConvertInput(inputs.Select(i => i()).Sum());
        }

        public static Func<double> CreateNumericNeuron(params Func<double>[] inputs)
        {
            return CreateNeuron(new DefaultNeuronInputConverter(), inputs);
        }

        public static NeuronLayer<TInput> CreateNumericNeuronLayer<TInput>(int neuronCount, Func<TInput, string, double> propertySelector, IReadOnlyCollection<string> inputNames)
        {
            Random rand = new Random(DateTime.Now.Millisecond);
            List<Action<TInput>> inputSetters = new List<Action<TInput>>();
            List<Func<double>> neurons = new List<Func<double>>();
            List<IReadOnlyDictionary<string, MutableWeight>> neuronInputWeights = new List<IReadOnlyDictionary<string, MutableWeight>>();
            for (int i = 0; i < neuronCount; i++)
            {
                Dictionary<string, MutableWeight> propertyWeights = new Dictionary<string, MutableWeight>();
                Dictionary<string, double> propertyValues = new Dictionary<string, double>();
                List<Func<double>> propertyValueGetters = new List<Func<double>>();
                foreach (var inputName in inputNames)
                {
                    var weight = new MutableWeight(rand.NextDouble() - 0.5);
                    Func<double> propertyValueGetter = () => propertyValues[inputName] * weight.GetWeight();
                    propertyWeights.Add(inputName, weight);
                    propertyValueGetters.Add(propertyValueGetter);

                    inputSetters.Add(input => propertyValues[inputName] = propertySelector(input, inputName));
                }

                neurons.Add(CreateNumericNeuron(propertyValueGetters.ToArray()));
                neuronInputWeights.Add(propertyWeights);
            }

            NeuronLayer<TInput> layer = new NeuronLayer<TInput>(neuronInputWeights, input =>
            {
                foreach (var inputSetter in inputSetters)
                {
                    inputSetter(input);
                }
                return neurons.Select(n => n()).ToList();
            });
            return layer;
        }
    }
}