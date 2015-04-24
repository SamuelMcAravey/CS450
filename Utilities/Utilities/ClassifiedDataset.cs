using System.Collections.Generic;

namespace Utilities
{
    public sealed class ClassifiedData<T, TClass>
    {
        public double? Accuracy { get; set; }
        public IDictionary<TClass, IReadOnlyList<T>> Classified { get; set; }
    }
}
