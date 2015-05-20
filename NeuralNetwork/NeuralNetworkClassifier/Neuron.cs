using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace NeuralNetworkClassifier
{
    public class Neuron<T> : IObservable<T>, IDisposable
    {
        private readonly List<IObservable<double>> connections = new List<IObservable<double>>();
        private readonly object gate = new object();
        private readonly BehaviorSubject<T> subject = new BehaviorSubject<T>(default(T));
        private readonly INeuronInputConverter<T> neuronInputConverter;
        public bool Running { get; private set; }
        public T Value => this.subject.Value;

        protected Neuron(INeuronInputConverter<T> neuronInputConverter)
        {
            this.neuronInputConverter = neuronInputConverter;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            var disposable = subject.Subscribe(observer);
            lock (gate)
            {
                if (!Running)
                {
                    Running = true;
                    subscriptionDisposable = connections.Zip()
                        .Select(list => list.Sum())
                        .Select(this.neuronInputConverter.ConvertInput)
                        .Subscribe(subject.OnNext);
                }
            }
            return disposable;
        }

        public void AddConnection(IObservable<double> connection)
        {
            lock (gate)
            {
                if (Running)
                    return;
            }

            connections.Add(connection);
        }

        #region IDisposable Support

        private bool disposedValue; // To detect redundant calls
        private IDisposable subscriptionDisposable;

        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    subscriptionDisposable?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Neuron() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}