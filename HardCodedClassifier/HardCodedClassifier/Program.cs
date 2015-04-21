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

            var dataset = plants.CreateDataset(trainingSetPercentage: 0.7);
            var classifier = new IrisPlantClassifier();
            var classifiedDataset = classifier.Classify(dataset);

            Console.WriteLine("Classified with {0}% accuracy.", classifiedDataset.Accuracy);
            Console.ReadLine();
        }
    }
}
