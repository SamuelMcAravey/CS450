using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronCSharp
{
    public sealed class MLP
    {
        public readonly List<List<List<double>>> NeuronWeights = new List<List<List<double>>>();
        public readonly List<List<double>> NeuronOutputs = new List<List<double>>();
        public readonly List<List<double>> NeuronErrors = new List<List<double>>();

        public List<double> Outputs => this.NeuronOutputs.Last();
        private List<double> mlpInputs;

        public void CreateLayers(int inputCount, IEnumerable<int> layerSizes)
        {
            var rand = new Random((int) DateTime.Now.Ticks);
            var sizes = layerSizes as IList<int> ?? layerSizes.ToList();
            for (int i = 0; i < sizes.Count; i++)
            {
                var weightLayer = new List<List<double>>();
                var outputLayer = Enumerable.Repeat(1.0, sizes[i]).ToList();
                this.NeuronWeights.Add(weightLayer);
                this.NeuronOutputs.Add(outputLayer);
                this.NeuronErrors.Add(Enumerable.Repeat(0.0, sizes[i]).ToList());

                int count = i == 0 ? inputCount : sizes[i - 1];
                for (int j = 0; j < sizes[i]; j++)
                {
                    weightLayer.Add(Enumerable.Repeat(0, count).Select(_ => rand.NextDouble() - 0.5).ToList());
                }
            }
        }

        private double CalculateOutput(int layerIndex, int neuronIndex)
        {
            var previousOutputs = layerIndex > 0 ? NeuronOutputs[layerIndex - 1] : this.mlpInputs;
            var inputs = previousOutputs.Zip(NeuronWeights[layerIndex][neuronIndex], (input, weight) => input*weight);
            var output = Activators.eSigmoidNeuronEvaluator(inputs);
            return output;
        }

        public List<double> CalculateAll(IEnumerable<double> input)
        {
            this.mlpInputs = input.ToList();

            for (int layerIndex = 0; layerIndex < NeuronOutputs.Count; layerIndex++)
            {
                for (int neuronIndex = 0; neuronIndex < NeuronOutputs[layerIndex].Count; neuronIndex++)
                {
                    NeuronOutputs[layerIndex][neuronIndex] = CalculateOutput(layerIndex, neuronIndex);
                }
            }

            return this.Outputs;
        }

        public void UpdateWeights(IEnumerable<double> expectedOutput)
        {
            var lastLayerIndex = this.NeuronOutputs.Count - 1;
            for (int layerIndex = lastLayerIndex; layerIndex >= 0; layerIndex--)
            {
                if (layerIndex < lastLayerIndex)
                {
                    var error = CalculateHiddenError(layerIndex);
                    this.NeuronErrors[layerIndex] = error;
                }
                else
                {
                    var outputError = this.CalculateOutputError(expectedOutput);
                    this.NeuronErrors[lastLayerIndex] = outputError;
                }
            }

            for (int layerIndex = 0; layerIndex <= lastLayerIndex; layerIndex++)
            {
                var error = this.NeuronErrors[layerIndex];
                var previousLayerOutputs = layerIndex == 0 ? this.mlpInputs : this.NeuronOutputs[layerIndex - 1];
                var previousLayerWeights = this.NeuronWeights[layerIndex];
                for (int i = 0; i < previousLayerWeights.Count; i++)
                {
                    for (int j = 0; j < previousLayerOutputs.Count; j++)
                    {
                        previousLayerWeights[i][j] = previousLayerWeights[i][j] - 0.05*error[i]*previousLayerOutputs[j];
                    }
                }
            }

            //for (int i = 0; i < previousLayerWeights.Count; i++)
            //{
            //    for (int j = 0; j < outputError.Count; j++)
            //    {
            //        previousLayerWeights[i][j] = previousLayerWeights[i][j] -
            //                                     0.2 * outputError[j] * this.NeuronOutputs[lastLayerIndex - 1][i];
            //    }
            //}
        }

        private List<double> CalculateOutputError(IEnumerable<double> expectedOutput)
        {
            var outputErrors = this.Outputs.Zip(expectedOutput, (output, expected) => output * (1 - output) * (output - expected)).ToList();
            return outputErrors;
        }

        private List<double> CalculateHiddenError(int currentLayerIndex)
        {
            var currentOutputs = this.NeuronOutputs[currentLayerIndex];
            var nextLayerWeights = this.NeuronWeights[currentLayerIndex + 1];
            var nextLayerErrors = this.NeuronErrors[currentLayerIndex + 1];
            List<double> errors = new List<double>();
            for (int j = 0; j < currentOutputs.Count; j++)
            {
                var a_j = currentOutputs[j];

                double sum = 0;
                for (int k = 0; k < nextLayerWeights.Count; k++)
                {
                    sum += nextLayerWeights[k][j]*nextLayerErrors[k];
                }
                errors.Add(a_j*(1 - a_j)*sum);
            }
            return errors;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine("digraph { ");
            sb.AppendLine("rankdir=LR;");
            sb.AppendLine("nodesep=1;");
            sb.AppendLine("ranksep=3;");
            sb.AppendLine("splines=line;");
            
            sb.Append("{ rank=same; ");
            foreach (var mlpInput in mlpInputs)
                sb.Append(string.Format("\"{0}\" ", mlpInput));
            sb.AppendLine("; }");

            for (int layerIndex = 0; layerIndex < this.NeuronOutputs.Count; layerIndex++)
            {
                sb.Append("{ rank=same; ");
                //sb.AppendLine(string.Format("subgraph cluster{0} {{", layerIndex+1));
                var fromLayer = layerIndex == 0 ? mlpInputs : this.NeuronOutputs[layerIndex - 1];
                var toLayer = this.NeuronOutputs[layerIndex];
                var weights = this.NeuronWeights[layerIndex];

                List<Tuple<double, double, double>> values = new List<Tuple<double, double, double>>();
                for (int fromIndex = 0; fromIndex < fromLayer.Count; fromIndex++)
                {
                    for (int toIndex = 0; toIndex < toLayer.Count; toIndex++)
                    {
                        sb.Append(string.Format("\"{0}\" ", toLayer[toIndex]));
                        values.Add(Tuple.Create(fromLayer[fromIndex], toLayer[toIndex], weights[toIndex][fromIndex]));
                    }
                }

                sb.AppendLine("; }");

                foreach (var value in values)
                {
                    sb.AppendLine(string.Format("\"{0}\" -> \"{1}\" [label=\"{2}\" color=\"grey\" decorate=true];", value.Item1, value.Item2, value.Item3));
                }
            }

            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
