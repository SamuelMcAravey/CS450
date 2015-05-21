using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisDataset;
using Utilities;

namespace NeuralNetworkClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            //var layer = Neuron.CreateNumericNeuronLayer<double>(10, (d, p) => d, new[] {"val"});
            //var output = layer.LayerOutput(5);
            //foreach (var value in output)
            //{
            //    Console.WriteLine(value);
            //}
            //Console.WriteLine();
            //output = layer.LayerOutput(0);
            //foreach (var value in output)
            //{
            //    Console.WriteLine(value);
            //}
            TestIrisPlantDataset();
            Console.ReadLine();
        }

        private static void TestIrisPlantDataset()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Starting Iris Plant Testing");
            var plants = IrisPlant.ReadPlants();
            ClassificationTester.Test(plants, new Perceptron<IrisPlant, string>(GetClass, GetIndexedValue, 3, (plant, name) => (double)plant.ValueDictionary[name], new[] { "SepalLength" , "SepalWidth" , "PetalLength" , "PetalWidth" }), testCount: 50, printIndividualResults: true);
        }

        private static double GetIndexedValue(IrisPlant item, int index)
        {
            switch (index)
            {
                case 0:
                    return string.Equals(item.Class, "Iris-setosa", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
                case 1:
                    return string.Equals(item.Class, "Iris-versicolor", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
                case 2:
                    return string.Equals(item.Class, "Iris-virginica", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
                default:
                    return 0;
            }
        }

        private static string GetClass(IReadOnlyCollection<double> outputValues)
        {
            foreach (var outputValue in outputValues)
            {
                switch ((int) outputValue)
                {
                    case 0:
                        return "Iris-setosa";
                    case 1:
                        return "Iris-versicolor";
                    case 2:
                        return "Iris-virginica";
                    default:
                        return "Error classifying";
                }
            }
            return "Error classifying";
        }
    }
}
