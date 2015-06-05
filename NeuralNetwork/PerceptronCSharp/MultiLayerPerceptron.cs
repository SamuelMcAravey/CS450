using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronCSharp
{
    public class MultiLayerPerceptron
    {
        public List<double> Inputs { get; private set; }

        public List<double> Outputs => this.NeuronValues.Last();
        public IEnumerable<double[][]> Weights => this.Layers.Select(layer => layer.Weights);
        public IEnumerable<List<double>> NeuronValues => this.Layers.Select(layer => layer.Outputs);

        public List<List<List<double>>> NeuronWeights = new List<List<List<double>>>();

        public MultiLayerPerceptron(IEnumerable<NeuronLayer> layers)
        {
            this.Layers = layers.ToList();
        }

        public List<NeuronLayer> Layers { get; set; }

        private void EvaluateInternal(int index, IEnumerable<double> inputs)
        {
            this.Layers[index].EvaluateLayer(inputs.Concat(new[] { -1.0 }));
            var outputs = this.Layers[index].Outputs;
            if (index < (this.Layers.Count - 1))
                this.EvaluateInternal(index + 1, outputs);
        }

        public void Evaluate(IEnumerable<double> inputs)
        {
            this.Inputs = inputs.ToList();
            this.EvaluateInternal(0, this.Inputs);
        }

        private static IEnumerable<NeuronLayer> CreateLayers(int inputCount, IEnumerable<int> layerSizes,
            Func<IEnumerable<double>, double> evaluator, IEnumerable<NeuronLayer> layers)
        {
            var sizes = layerSizes as IList<int> ?? layerSizes.ToList();
            if (sizes.Any())
            {
                var layer = new NeuronLayer(sizes.First(), inputCount, evaluator);
                return CreateLayers(layer.Neurons.Count, sizes.Skip(1), evaluator, layers.Concat(new[] { layer }));
            }
            return layers;
        }

        public static MultiLayerPerceptron CreateMLP(int inputCount, IEnumerable<int> layerSizes,
            Func<IEnumerable<double>, double> evaluator)
        {
            return new MultiLayerPerceptron(CreateLayers(inputCount, layerSizes, evaluator, Enumerable.Empty<NeuronLayer>()));
        }

        public void UpdateWeights(IEnumerable<double> expectedOutput)
        {
            var outputErrors = this.Outputs.Zip(expectedOutput, (output, expected) => output*(1 - output)*(output - expected)).ToList();
            var values = this.NeuronValues.ToList();
            var layerIndex = values.Count - 2;
            var layer = values[layerIndex];
            var weights = this.Weights.ToList();
            var layerWeights = weights[layerIndex+1];
            for (int i = 0; i < values[layerIndex].Count; i++)
            {
                var a_i = layer[i];
                for (int j = 0; j < outputErrors.Count; j++)
                {
                    this.Layers[layerIndex + 1].Weights[j][i] = layerWeights[j][i] - 0.2*outputErrors[j]*a_i;
                }
            }
        }

        public Layer GetLayer(int i)
        {
            return null;
        }

        public Neuron GetNeuron(int i, int j)
        {
            var layer = GetLayer(i);
            return layer.Neurons[j];
        }

        public double GetWeight(int i, int j, int k)
        {
            var neuron = GetNeuron(i, j);
            return NeuronWeights[i][j][k];
        }
        /*
type public MultiLayerPerceptron(layers: neuronLayer[]) =
    member this.Layers = layers;
    member private this.EvaluateInternal (index: int) (inputs:double[]) =
        match index with
        | i when i < (this.Layers.Length - 1) -> this.EvaluateInternal (index + 1) (this.Layers.[index].EvaluateLayer(Array.append inputs [|-1.0|]))
        | _ -> this.Layers.[index].EvaluateLayer(Array.append inputs [|-1.0|]);
    member public this.Evaluate = this.EvaluateInternal 0;
    member public this.SetWeight(layerIndex: int, neuronIndex: int, weightIndex: int, value: double) = 
        this.Layers.[layerIndex].SetWeight(neuronIndex, weightIndex, value);

    static member createLayers(inputCount: int, layerSizes: int[], evaluator: (double[] -> double), layers: neuronLayer[]) =
        match layerSizes.Length with
            | i when i > 0 ->
                let layer = neuronLayer(layerSizes.[0], inputCount, evaluator)
                MultiLayerPerceptron.createLayers(layer.Neurons.Length, Array.skip 1 layerSizes, evaluator, Array.concat [layers; [|neuronLayer(layerSizes.[0], inputCount, evaluator)|]])
            | _ -> layers

    static member public CreateMLP(inputCount: int, layerSizes: int[], evaluator: (double[] -> double)) =
        MultiLayerPerceptron(MultiLayerPerceptron.createLayers(inputCount, layerSizes, evaluator, Array.empty));
        */
    }
}
