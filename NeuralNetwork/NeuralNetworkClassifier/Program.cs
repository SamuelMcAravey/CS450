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
            var neuron = new NumericNeuron();
            neuron.AddConnection(Observable.Return(2.0).AddWeight(-0.25));
            neuron.Subscribe(Console.WriteLine);
            neuron.Run();
            Console.ReadLine();
        }
    }
}
