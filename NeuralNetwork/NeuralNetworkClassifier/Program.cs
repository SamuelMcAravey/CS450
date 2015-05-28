using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using IrisDataset;
using Utilities;
using Perceptron;

namespace NeuralNetworkClassifier
{
    class Program
    {
        static void Main(string[] args)
        {
            var mlp = MultiLayerPerceptron.CreateMLP(4, new[] { 5, 4, 3 }, Activators.tanhSigmoidNeuronEvaluator);
            var results = mlp.Evaluate.Invoke(new[] { 1.0, 2.0, 3.0, 4.0 });
            foreach (var r in results)
            {
                Console.WriteLine(r);
            }
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
            //TestIrisPlantDataset();
            Console.ReadLine();
        }

        private static void TestIrisPlantDataset()
        {
            Console.WriteLine("======================================");
            Console.WriteLine("Starting Iris Plant Testing");
            var plants = IrisPlant.ReadPlants();
            ClassificationTester.Test(plants, new PerceptronX<IrisPlant, IrisPlantClass>(
                GetClass, 
                GetIndexedValue, 
                3, 
                (plant, name) => (double)plant.ValueDictionary[name], 
                new[] { "SepalLength" , "SepalWidth" , "PetalLength" , "PetalWidth" }), testCount: 50, printIndividualResults: true);
        }

        private static double GetIndexedValue(IrisPlant item, int index)
        {
            switch (index)
            {
                case 0:
                    return item.Class == IrisPlantClass.Setosa ? 1 : 0;
                case 1:
                    return item.Class == IrisPlantClass.Versicolor ? 1 : 0;
                case 2:
                    return item.Class == IrisPlantClass.Virginica ? 1 : 0;
                default:
                    return 0;
            }
        }

        private static IrisPlantClass GetClass(IReadOnlyList<double> outputValues)
        {
            if (outputValues[0] == 1)
                return IrisPlantClass.Setosa;
            if (outputValues[1] == 1)
                return IrisPlantClass.Versicolor;
            if (outputValues[2] == 1)
                return IrisPlantClass.Setosa;

            throw new Exception();
        }
    }
}
