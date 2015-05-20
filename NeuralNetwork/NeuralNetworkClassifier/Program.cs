using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            var neuron1Weight = new MutableWeight(0.25);
            var neuron1 = Neuron.CreateNumericNeuron(() => 5 * neuron1Weight.GetWeight());

            var neuron2Weight = new MutableWeight(0.25);
            var neuron2 = Neuron.CreateNumericNeuron(() => neuron1() * neuron2Weight.GetWeight());
            Console.WriteLine(neuron1());
            Console.ReadLine();
        }
    }
}
