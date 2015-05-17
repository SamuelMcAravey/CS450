using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDataset;
using ChessDataset;
using IrisDataset;
using LensesDataset;
using Utilities;
using VotingDataset;

namespace DecisionTreeClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var stream = File.OpenWrite("output.txt"))
            using (TextWriter writer = new StreamWriter(stream))
            {
                Console.SetOut(writer);
                TestIrisPlantDataset();
                TestCarDataset();
                TestLensesDataset();
                TestVoterDataset();
                TestChessDataset();
            }
            Console.SetOut(Console.Out);
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        private static void TestCarDataset()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Starting Car Testing");
            var cars = Car.ReadCars();
            ClassificationTester.Test(cars, new ID3DecisionTreeClassifier<Car, CarClass>(), testCount: 50, printIndividualResults: true);
        }

        private static void TestIrisPlantDataset()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Starting Iris Plant Testing");
            var plants = IrisPlantDiscrete.ReadPlants();
            ClassificationTester.Test(plants, new ID3DecisionTreeClassifier<IrisPlantDiscrete, string>(), testCount: 50, printIndividualResults: true);
        }

        private static void TestLensesDataset()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Starting Lenses Testing");
            var patients = Patient.ReadPatients();
            ClassificationTester.Test(patients, new ID3DecisionTreeClassifier<Patient, PatientLenseClass>(), testCount: 50, printIndividualResults: true);
        }

        private static void TestVoterDataset()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Starting Voter Testing");
            var voters = Voter.ReadVoters();
            ClassificationTester.Test(voters, new ID3DecisionTreeClassifier<Voter, string>(), testCount: 50, printIndividualResults: true);
        }

        private static void TestChessDataset()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Starting Chess Testing");
            var voters = ChessEntry.ReadChessEntries();
            ClassificationTester.Test(voters, new ID3DecisionTreeClassifier<ChessEntry, string>(), testCount: 5, printIndividualResults: true);
        }
    }
}
