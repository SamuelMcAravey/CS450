using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class ClassificationTester<TClassified, TClass> where TClassified : IClassified<TClass>, IClassifiable
    {
        public void Test(IReadOnlyList<TClassified> data, Classifier<TClassified, TClassified, TClass> classifier, double trainingSetPercentage = 0.7, bool randomize = true, int testCount = 100)
        {
            double totalAccuracy = 0;
            for (int i = 0; i < testCount; i++)
            {
                var dataset = data.CreateTestDataset<TClassified, TClass>(trainingSetPercentage, randomize);
                classifier.Train(dataset.TrainingSet);
                var classifiedDataset = classifier.Classify(dataset.TestingSet);

                Console.WriteLine("Classified with {0}% accuracy.", classifiedDataset.Accuracy * 100);
                totalAccuracy += classifiedDataset.Accuracy ?? 0;
            }

            Console.WriteLine();
            Console.WriteLine("======================================");
            Console.WriteLine("Classified with an average accuracy of: {0}%.", totalAccuracy * 100 / testCount);
            Console.WriteLine("======================================");
            Console.WriteLine();
        }
    }
}
