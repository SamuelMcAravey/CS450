using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronCSharp
{
    public class NeuronLayer
    {
        public List<Neuron> Neurons { get; set; }
        public double[][] Weights { get; set; }
        public List<double> Outputs { get; set; }
        public NeuronLayer(int neuronCount, int inputCount, Func<IEnumerable<double>, double> evaluator)
        {
            this.Neurons = new List<Neuron>();
            this.Outputs = new List<double>();
            this.Weights = new double[neuronCount][];

            var rand = new Random((int) DateTime.Now.Ticks);
            for (int i = 0; i < neuronCount; i++)
            {
                this.Weights[i] = new double[inputCount];
                this.Neurons.Add(new Neuron
                {
                    Evaluator = evaluator
                });

                for (int j = 0; j < inputCount; j++)
                {
                    this.Weights[i][j] = (double) (rand.NextDouble() - 0.5);
                }
            }
        }

        public void EvaluateLayer(IEnumerable<double> inputs)
        {
            this.Outputs = this.Neurons.Select((neuron, i) =>
            {
                return neuron.Evaluator(inputs.Zip(this.Weights[i], (input, weight) => input*weight));
            }).ToList();
        }
        /*
type neuronLayer(neuronCount: int, inputCount: int, evaluator: (double[] -> double)) =
    let rand = System.Random((int)DateTime.Now.Ticks);
    let neurons = [|for i in 0 .. neuronCount - 1 -> {evaluator = evaluator; weights=[|for j in 0 .. inputCount -> rand.NextDouble() - 0.5|]} |];

    member this.Neurons = neurons;
    member this.EvaluateLayer(inputs: double[]) =
        this.Neurons 
            |> Array.map (fun neuron -> 
                inputs 
                |> Array.zip neuron.weights
                |> Array.map (fun (input, weight) -> input * weight)
                |> neuron.evaluator);
    member this.SetWeight(neuronIndex: int, weightIndex: int, value: double) = this.Neurons.[neuronIndex].weights.[weightIndex] <- value;*/
    }

    public class Neuron
    {
        public Func<IEnumerable<double>, double> Evaluator { get; set; }
    }
}
