using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClassifier
{
    public static class ObservableEx
    {
        public static IObservable<double> AddWeight(this IObservable<double> source, double weight)
        {
            return source.AddWeight(() => weight);
        }
        public static IObservable<double> AddWeight(this IObservable<double> source, IWeight weight)
        {
            return source.AddWeight(weight.GetWeight);
        }
        public static IObservable<double> AddWeight(this IObservable<double> source, Func<double> getWeight)
        {
            return source.Select(value => value * getWeight());
        }
    }
}
