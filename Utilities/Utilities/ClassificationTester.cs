using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class ClassificationTester
    {
        public static void Test<TClassified, TClass>(IReadOnlyList<TClassified> data, Classifier<TClassified, TClassified, TClass> classifier, double trainingSetPercentage = 0.7, bool randomize = true, int testCount = 100, bool printIndividualResults = false) where TClassified : IClassified<TClass>, IClassifiable
        {
            double totalAccuracy = 0;
            double bestAccuracy = 0;
            string bestPrintout = "Nothing to see here...";
            for (int i = 0; i < testCount; i++)
            {
                var dataset = data.CreateTestDataset<TClassified, TClass>(trainingSetPercentage, randomize);
                classifier.Train(dataset.TrainingSet);
                var classifiedDataset = classifier.Classify(dataset.TestingSet);

                if (printIndividualResults)
                    Console.WriteLine("Classified with {0}% accuracy.", classifiedDataset.Accuracy * 100);

                var accuracy = classifiedDataset.Accuracy ?? 0;
                totalAccuracy += accuracy;

                if (accuracy > bestAccuracy)
                {
                    bestAccuracy = accuracy;
                    bestPrintout = classifier.ToString();
                }
            }
            
            Console.WriteLine();
            Console.WriteLine("======================================");
            Console.WriteLine("Classified with an average accuracy of: {0}%.", totalAccuracy * 100 / testCount);
            Console.WriteLine("======================================");
            Console.WriteLine("Best Single Classification: Accuracy {0}%", bestAccuracy * 100);
            Console.WriteLine("======================================");
            Console.WriteLine();
            Console.WriteLine(bestPrintout);
            Console.WriteLine("======================================");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
