using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace NeuralNetworkClassifier
{
    class InputDefinition
    {
         
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

        public static Func<TInput, List<double>> CreateNeuronLayer<TInput>(int neuronCount, Func<TInput, string, double> propertySelector, List<string> inputNames)
        {
            for (int i = 0; i < neuronCount; i++)
            {
                Dictionary<string, MutableWeight> propertyValueGetters = new Dictionary<string, MutableWeight>();
                List<Func<TInput, double>> propertyValues = new List<Func<TInput, double>>();
                foreach (var inputName in inputNames)
                {
                    var weight = new MutableWeight(0.1);
                    Func<TInput, double> propertyValue = (TInput input) => propertySelector(input, inputName) * weight.GetWeight();
                    propertyValueGetters.Add(inputName, weight);
                    propertyValues.Add(propertyValue);
                }
                CreateNeuron(new DefaultNeuronInputConverter(), propertyValues.ToArray());
            }
        }
    }
}