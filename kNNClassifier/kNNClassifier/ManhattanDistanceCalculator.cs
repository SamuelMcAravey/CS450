using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace kNNClassifier
{
    public sealed class ManhattanDistanceCalculator<TItem> : IDistanceCalculator<TItem> where TItem : IClassifiable
    {
        public double CalculateDistance(TItem source, TItem destination)
        {
            return source.ValueDictionary.Select(pair => CalculateDifference(pair.Value, destination.ValueDictionary[pair.Key])).Select(Math.Abs).Sum();
        }

        private static double CalculateDifference(object source, object destination)
        {
            if (source is double)
                return (double)source - (double)destination;

            if (Equals(source, destination))
                return 0;
            else
                return 1;
        }
    }
}
