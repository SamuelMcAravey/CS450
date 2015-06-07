using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisDataset;
using PerceptronCSharp;
using Utilities;
//using Perceptron;

namespace NeuralNetworkClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            MLP mlp = new MLP();
            mlp.CreateLayers(4,new[] {5,4,3});
            //for (int i = 0; i < 10; i++)
            //{
            //    var mlp = MultiLayerPerceptron.CreateMLP(4, new[] { 5, 4, 3 }, Activators.tanhSigmoidNeuronEvaluator);
            //    var results = mlp.Evaluate.Invoke(new[] { 1.0, 2.0, 3.0, 4.0 });
            //    foreach (var r in results)
            //        Console.WriteLine(r);
            //    Console.WriteLine(GetClass(results));
            //    Console.WriteLine();
            //}


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
            ClassificationTester.Test(plants, new PerceptronClassifier<IrisPlant, IrisPlantClass>(GetClass, 4, new[] { 5,3 }, ClassToExpectedOutputConverter), testCount: 1);
        }

        private static IrisPlantClass GetClass(IReadOnlyList<double> outputValues)
        {
            var max = outputValues.Select((r, idx) => Tuple.Create(idx, r)).OrderByDescending(t => t.Item2).First();

            switch (max.Item1)
            {
                case 0:
                    return IrisPlantClass.Setosa;
                case 1:
                    return IrisPlantClass.Versicolor;
                case 2:
                    return IrisPlantClass.Virginica;
            }
            throw new Exception();
        }

        private static IReadOnlyList<double> ClassToExpectedOutputConverter(IrisPlantClass plantClass)
        {
            switch (plantClass)
            {
                case IrisPlantClass.Setosa:
                    return new[] {1.0, 0.0, 0.0};
                case IrisPlantClass.Versicolor:
                    return new[] { 0.0, 1.0, 0.0 };
                case IrisPlantClass.Virginica:
                    return new[] { 0.0, 0.0, 1.0 };
                default:
                    throw new ArgumentOutOfRangeException(nameof(plantClass), plantClass, null);
            }
        }
    }
}
