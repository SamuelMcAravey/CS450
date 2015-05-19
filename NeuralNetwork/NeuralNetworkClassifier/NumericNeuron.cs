using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace NeuralNetworkClassifier
{
    public sealed class NumericNeuron : Neuron<double>
    {
        public NumericNeuron() 
            : base(new DefaultNeuronInputConverter())
        {
            
        }
    }
}