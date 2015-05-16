using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDataset;
using IrisDataset;
using LensesDataset;
using Utilities;

namespace DecisionTreeClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestIrisPlantDataset();
            //TestCarDataset();
            TestLensesDataset();
            Console.ReadLine();
        }

        private static void TestCarDataset()
        {
            var cars = Car.ReadCars();
            var tester = new ClassificationTester<Car, CarClass>();
            tester.Test(cars, new ID3DecisionTreeClassifier<Car, CarClass>());
        }

        private static void TestIrisPlantDataset()
        {
            var plants = IrisPlantDiscrete.ReadPlants();
            var tester = new ClassificationTester<IrisPlantDiscrete, string>();
            tester.Test(plants, new ID3DecisionTreeClassifier<IrisPlantDiscrete, string>());
        }

        private static void TestLensesDataset()
        {
            var patients = Patient.ReadPatients();
            var tester = new ClassificationTester<Patient, PatientLenseClass>();
            tester.Test(patients, new ID3DecisionTreeClassifier<Patient, PatientLenseClass>());
        }
    }
}
