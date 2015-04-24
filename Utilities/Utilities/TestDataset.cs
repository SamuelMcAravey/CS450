using System.Collections;
using System.Collections.Generic;

namespace Utilities
{
    public sealed class TestDataset<T, TClass> where T : IClassified<TClass>
    {
        public IClassifiedDataset<T, TClass> TrainingSet { get; set; }
        public IClassifiedDataset<T, TClass> TestingSet { get; set; }
    }
}
