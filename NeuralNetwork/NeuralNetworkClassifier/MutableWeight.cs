using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClassifier
{
    public sealed class MutableWeight : IWeight, ISubject<double>
    {
        private readonly BehaviorSubject<double> weightSubject;

        public MutableWeight(double initialWeight = 1)
        {
            this.weightSubject = new BehaviorSubject<double>(initialWeight);
        }

        public void SetWeight(double weight)
        {
            this.weightSubject.OnNext(weight);
        }

        public double GetWeight()
        {
            return weightSubject.Value;
        }

        public void OnNext(double value)
        {
            weightSubject.OnNext(value);
        }

        public void OnError(Exception error)
        {
            weightSubject.OnError(error);
        }

        public void OnCompleted()
        {
            weightSubject.OnCompleted();
        }

        public IDisposable Subscribe(IObserver<double> observer)
        {
            return weightSubject.Subscribe(observer);
        }
    }
}
