using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public abstract class Classifier<TClassified, TInput, TClass> where TClassified : IClassified<TClass> where TInput : IClassifiable
    {
        protected abstract TClass Classify(TInput input);

		public abstract void Train(IClassifiedDataset<TClassified, TClass> trainingDataset);

        public ClassifiedData<TInput, TClass> Classify(IEnumerable<TInput> dataset)
        {
            IReadOnlyList<TClass> classes = null;
            var dataList = dataset as IList<TInput> ?? dataset.ToList();
            if (typeof (TInput).GetInterface(typeof (IClassified<TClass>).Name) != null)
            {
                classes = dataList.Cast<IClassified<TClass>>().Select(c => c.Class).ToList();
            }

            var classified = (from item in dataList
                              select new
                                     {
                                         Item = item,
                                         EstimatedClass = this.Classify(item)
                                     }).ToList();

            double? accuracy = null;
            if (classes != null)
            {

                accuracy = ((double)classified.Zip(classes, (classifiedItem, actualClass) => new
                {
                    EstimatedClass = classifiedItem.EstimatedClass,
                    ActualClass = actualClass
                }).Count(c => CompareClass(c.EstimatedClass, c.ActualClass) == 0)) / (dataList.Count);
            }

            var classifiedData = classified.GroupBy(c => c.EstimatedClass)
                                           .ToDictionary(grouping => grouping.Key,
                                                         grouping => (IReadOnlyList<TInput>) grouping.Select(g => g.Item).ToList());
            return new ClassifiedData<TInput, TClass>
                   {
                       Accuracy = accuracy,
                       Classified = classifiedData
                   };
        }

        private static int CompareClass(TClass first, TClass second)
        {
            return Comparer<TClass>.Default.Compare(first, second);
        }
    }
}
