using System.Collections.Generic;

namespace Utilities
{
    public class Dataset<T>
    {
        public IReadOnlyList<T> TrainingSet { get; set; }
        public IReadOnlyList<T> TestingSet { get; set; }
    }
}
