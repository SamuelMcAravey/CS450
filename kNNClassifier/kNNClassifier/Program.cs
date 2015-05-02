using CarDataset;
using IrisDataset;
using kNNClassifier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace kNNClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            TestIrisPlantDataset();
            TestCarDataset();
            Console.ReadLine();
        }

        private static void TestCarDataset()
        {
            var cars = Car.ReadCars();
            var tester = new ClassificationTester<Car, CarClass>();
            tester.Test(cars, new GenericKnnClassifier<Car, CarClass>(5, new ManhattanDistanceCalculator<Car>()), testCount: 5); // Takes too long
        }

        private static void TestIrisPlantDataset()
        {
            var plants = IrisPlant.ReadPlants();
            var tester = new ClassificationTester<IrisPlant, string>();
            tester.Test(plants, new GenericKnnClassifier<IrisPlant, string>(5, new ManhattanDistanceCalculator<IrisPlant>()));
        }
    }
}
