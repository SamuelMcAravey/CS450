using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics;

namespace PerceptronCSharp
{
    public static class Activators
    {
        public static readonly Func<IEnumerable<double>, double> eSigmoidNeuronEvaluator;
        public static readonly Func<IEnumerable<double>, double> tanhSigmoidNeuronEvaluator;

        static Activators()
        {
            eSigmoidNeuronEvaluator = inputs => abstractNeuronEvaluator(eSigmoidActivator, inputs);
            tanhSigmoidNeuronEvaluator = inputs => abstractNeuronEvaluator(tanhSigmoidActivator, inputs);
        }

        private static T abstractNeuronEvaluator<T>(Func<double, T> activator, IEnumerable<double> inputs)
        {
            return activator(inputs.Sum());
        }

        public static double eSigmoidActivator(double total)
        {
            return (double) (1/(1.0 + Math.Pow(Constants.E, -total)));
        }

        private static double tanhSigmoidActivator(double total)
        {
            return (double) Trig.Tanh(total);
        }
    }
}