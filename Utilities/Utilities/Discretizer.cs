using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Statistics;

namespace Utilities
{
    public static class Discretizer
    {
        public static Func<T, int> CreateDiscretizer<T>(IEnumerable<T> data, Func<T, double> attributeSelector, int bucketCount = 4, bool evenFill = true)
        {
            var items = data.OrderBy(attributeSelector).ToList();
            List<double> splitPoints = new List<double>();
            if (evenFill)
            {
                int itemsPerBucket = items.Count / bucketCount;
                int position = itemsPerBucket - 1;
                for (int i = 0; i < bucketCount - 1; i++)
                {
                    var firstPoint = attributeSelector(items[position]);
                    var secondPoint = attributeSelector(items[position + 1]);
                    var average = new[] {firstPoint, secondPoint}.Mean();
                    splitPoints.Add(average);
                    position += itemsPerBucket;
                }
            }
            else
            {
                
            }

            return item =>
                   {
                       var attributeValue = attributeSelector(item);
                       for (int i = 0; i < splitPoints.Count; i++)
                       {
                           if (attributeValue < splitPoints[i])
                               return i;
                       }
                       return splitPoints.Count;
                   };
        } 
    }
}
