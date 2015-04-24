using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Utilities;

namespace HardCodedClassifier
{
    class Program
    { 
        static void Main(string[] args)
        {
            List<IrisPlant> plants;
            using (var reader = File.OpenText("iris.data"))
            {
                var csv = new CsvReader(reader); 
                csv.Configuration.RegisterClassMap<IrisPlantMap>();
                csv.Configuration.HasHeaderRecord = false;
                plants = csv.GetRecords<IrisPlant>().ToList();
            }

            const int testCount = 100;
            double totalAccuracy = 0;
            for (int i = 0; i < testCount; i++)
            {
                var dataset = plants.CreateTestDataset<IrisPlant, string>(trainingSetPercentage: 0.7);
                var classifier = new IrisPlantClassifier();
                classifier.Train(dataset.TrainingSet);
                var classifiedDataset = classifier.Classify(dataset.TestingSet);

                Console.WriteLine("Classified with {0}% accuracy.", classifiedDataset.Accuracy);
                totalAccuracy += classifiedDataset.Accuracy ?? 0;
            }

            Console.WriteLine();
            Console.WriteLine("Classified with an average accuracy of: {0}%.", totalAccuracy/testCount);
            Console.ReadLine();
        }
    }
}
