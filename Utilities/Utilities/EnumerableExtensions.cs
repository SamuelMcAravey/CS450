using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            return source.OrderBy(_ => rnd.Next());
        }

        public static TestDataset<T, TClass> CreateTestDataset<T, TClass>(this IEnumerable<T> source, double trainingSetPercentage, bool randomize = true) where T : IClassified<TClass>
        {
            if (trainingSetPercentage > 1 || trainingSetPercentage < 0)
                throw new ArgumentOutOfRangeException("trainingSetPercentage", "Must be between 0 and 1.");

            if (randomize)
                source = source.Randomize();

            var sourceList = source as IList<T> ?? source.ToList();
            var trainingSetSize = (int)(sourceList.Count() * trainingSetPercentage);

            var dataset = new TestDataset<T, TClass>();
            dataset.TrainingSet = sourceList.Take(trainingSetSize).CreateClassifiedDataset<T, TClass>();
            dataset.TestingSet = sourceList.Skip(trainingSetSize).Take(sourceList.Count - trainingSetSize).CreateClassifiedDataset<T, TClass>();
            return dataset;
        }


        public static IClassifiedDataset<T, TClass> CreateClassifiedDataset<T, TClass>(this IEnumerable<T> source) where T : IClassified<TClass>
        {
            return new ClassifiedDataset<T, TClass>(source);
        }
    }
}
