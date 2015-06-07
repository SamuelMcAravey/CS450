//using Perceptron;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using PerceptronCSharp;
using Utilities;

namespace NeuralNetworkClassifier
{
    public sealed class PerceptronClassifier<TItem, TClass> : Classifier<TItem, TItem, TClass> where TItem : IClassified<TClass>, IClassifiable
    {
        private readonly Func<IReadOnlyList<double>, TClass> outputConverter;
        private readonly Func<TClass, IEnumerable<double>> classToExpectedOutputConverter;
        private readonly Func<TItem, int, double> neuronExpectedOutput;
        private NeuronLayer<TItem> layer;
        private MLP mlp;

        public PerceptronClassifier(Func<IReadOnlyList<double>, TClass> outputConverter, int inputCount, int[] layerSizes, Func<TClass, IEnumerable<double>> classToExpectedOutputConverter)
        {
            this.outputConverter = outputConverter;
            this.classToExpectedOutputConverter = classToExpectedOutputConverter;
            this.mlp = new MLP();

            this.mlp.CreateLayers(inputCount, layerSizes);
        }

        protected override TClass Classify(TItem input)
        {
            var output = this.mlp.CalculateAll(input.ValueDictionary.Values.Cast<double>());
            return this.outputConverter(output);
        }

        public override void Train(IClassifiedDataset<TItem, TClass> trainingDataset)
        {
            int index = 0;
            //Directory.Delete("Dot", true);
            if (File.Exists("Dot\\Results.csv"))
                File.Delete("Dot\\Results.csv");

            Directory.CreateDirectory("Dot");
            File.WriteAllText("Dot\\convert.cmd", "forfiles /S /m *.dot /c \"cmd.exe /c C:\\Develop\\Graphviz2.38\\bin\\dot.exe @file -T png -o @fname.png\"");
            foreach (var item in trainingDataset)
            {
                this.mlp.CalculateAll(item.ValueDictionary.Values.Cast<double>());
                var expected = this.classToExpectedOutputConverter(item.Class).ToList();
                this.mlp.UpdateWeights(expected);
                bool success = Equals(this.outputConverter(this.mlp.Outputs), item.Class);
                File.AppendAllText("Dot\\Results.csv", (success ? 1 : -1) + "\n");
                File.WriteAllText(string.Format("Dot\\MLP{0}.dot",index++) , this.mlp.ToString());
            }

            //var process = Process.Start("Dot\\convert.cmd");
            //process?.WaitForExit();
        }
    }
}
