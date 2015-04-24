using System.Collections;
using System.Collections.Generic;

namespace Utilities
{
    public interface IClassifiedDataset<out T, TClass> : IEnumerable<T> where T : IClassified<TClass>
    {
    }

    internal class ClassifiedDataset<T, TClass> : IClassifiedDataset<T, TClass> where T : IClassified<TClass>
    {
        private readonly IEnumerable<T> data;

        public ClassifiedDataset(IEnumerable<T> data)
        {
            this.data = data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}