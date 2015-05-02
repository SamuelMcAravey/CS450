using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace kNNClassifier
{
    public interface IDistanceCalculator<TItem> where TItem : IClassifiable
    {
        double CalculateDistance(TItem source, TItem destination);
    }

    public sealed class DistanceCalculator<TItem> : IDistanceCalculator<TItem> where TItem : IClassifiable
    {
        public double CalculateDistance(TItem source, TItem destination)
        {
            List<double> distances = new List<double>();
            foreach (var pair in source.ValueDictionary)
            {
                var d = destination.ValueDictionary[pair.Key];
                distances.Add(Math.Pow(CalculateDifference(pair.Value, d), 2));
            }
            //var distances = from s in source.ValueDictionary
            //                join d in destination.ValueDictionary on s.Key equals d.Key
            //                select Math.Pow(CalculateDistance(s.Value, d.Value), 2);

            var distanceSum = distances.Sum();

            return Math.Sqrt(distanceSum);
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
