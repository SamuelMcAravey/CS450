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
            var tester = new ClassificationTester<IrisPlant, string>();
            tester.Test(plants, new IrisPlantClassifier());
            Console.ReadLine();
        }
    }
}
