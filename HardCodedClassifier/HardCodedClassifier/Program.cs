using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Utilities;
using IrisDataset;

namespace HardCodedClassifier
{
    class Program
    { 
        static void Main(string[] args)
        {
			IReadOnlyList<IrisPlant> plants = IrisPlant.ReadPlants();

            const int testCount = 100;
            double totalAccuracy = 0;
            for (int i = 0; i < testCount; i++)
            {
                var dataset = plants.CreateTestDataset<IrisPlant, string>(trainingSetPercentage: 0.7);
                var classifier = new IrisPlantClassifier();
                classifier.Train(dataset.TrainingSet);
                var classifiedDataset = classifier.Classify(dataset.TestingSet);

                Console.WriteLine("Classified with {0}% accuracy.", classifiedDataset.Accuracy * 100);
                totalAccuracy += classifiedDataset.Accuracy ?? 0;
            }

            Console.WriteLine();
            Console.WriteLine("Classified with an average accuracy of: {0}%.", totalAccuracy * 100 / testCount);
            Console.ReadLine();
        }
    }
}
