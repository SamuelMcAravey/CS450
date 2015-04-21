using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public abstract class Classifier<T>
    {
        protected abstract string Classify(T observation);
        protected abstract void Train(T observation);
        protected abstract string Predict(T observation);

        public ClassifiedDataset<T> Classify(Dataset<T> dataset)
        {
            dataset.TrainingSet.ForEach(this.Train);
            var classified = (from item in dataset.TestingSet
                              select new
                                     {
                                         Item = item,
                                         Predicted = this.Predict(item),
                                         Classified = this.Classify(item)
                                     }).ToList();

            var accuracy = ((double) classified.Count(c => c.Classified == c.Predicted)) / (dataset.TestingSet.Count);

            var classifiedData = classified.GroupBy(c => c.Classified)
                                           .ToDictionary(grouping => grouping.Key,
                                                         grouping => (IReadOnlyList<T>) grouping.Select(g => g.Item).ToList());
            return new ClassifiedDataset<T>
                   {
                       Accuracy = accuracy,
                       ClassifiedData = classifiedData,
                       TestingSet = dataset.TestingSet,
                       TrainingSet = dataset.TrainingSet
                   };
        }
    }
}
