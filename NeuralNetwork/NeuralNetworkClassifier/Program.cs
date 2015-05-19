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
            var neuron1 = new NumericNeuron();
            var neuron1Weight = new MutableWeight(0.25);
            var neuron2 = new NumericNeuron();
            neuron1.AddConnection(Observable.Return(2.0).AddWeight(neuron1Weight));
            neuron2.AddConnection(neuron1.AddWeight(0.25));
            neuron2.Subscribe(Console.WriteLine);
            Console.ReadLine();
        }
    }
}
