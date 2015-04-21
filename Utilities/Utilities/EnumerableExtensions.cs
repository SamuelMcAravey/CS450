using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            var rnd = new Random();
            return source.OrderBy(_ => rnd.Next());
        }

        public static Dataset<T> CreateDataset<T>(this IEnumerable<T> source, double trainingSetPercentage, bool randomize = true)
        {
            if (trainingSetPercentage > 1 || trainingSetPercentage < 0)
                throw new ArgumentOutOfRangeException("trainingSetPercentage", "Must be between 0 and 1.");

            if (randomize)
                source = source.Randomize();

            var sourceList = source as IList<T> ?? source.ToList();
            var trainingSetSize = (int)(sourceList.Count() * trainingSetPercentage);

            var dataset = new Dataset<T>();
            dataset.TrainingSet = sourceList.Take(trainingSetSize).ToList();
            dataset.TestingSet = sourceList.Skip(trainingSetSize).Take(sourceList.Count - trainingSetSize).ToList();
            return dataset;
        }
    }
}
