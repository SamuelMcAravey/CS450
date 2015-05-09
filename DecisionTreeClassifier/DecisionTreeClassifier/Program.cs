using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDataset;
using IrisDataset;
using Utilities;

namespace DecisionTreeClassifier
{
    class Program
    {
        static void Main(string[] args)
        {


            //TestIrisPlantDataset();
            TestCarDataset();
            Console.ReadLine();
        }

        private static void TestCarDataset()
        {
            var cars = Car.ReadCars();
            var tester = new ClassificationTester<Car, CarClass>();
            tester.Test(cars, new ID3DecisionTreeClassifier<Car, CarClass>(), testCount: 1000);
        }

        private static void TestIrisPlantDataset()
        {
            var plants = IrisPlant.ReadPlants();
            var tester = new ClassificationTester<IrisPlant, string>();
            //tester.Test(plants, new DecisionTreeClassifier(5, new ManhattanDistanceCalculator<IrisPlant>()));
        }
    }
}
