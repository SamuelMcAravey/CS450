using System.Collections.Generic;

namespace Utilities
{
    public sealed class ClassifiedDataset<T> : Dataset<T>
    {
        public double Accuracy { get; set; }
        public IDictionary<string, IReadOnlyList<T>> ClassifiedData { get; set; }
    }
}
