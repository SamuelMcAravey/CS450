using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronCSharp
{
    public sealed class Layer
    {
        public List<Neuron> Neurons { get; private set; }
        public IEnumerable<double> Outputs
        {
            get { return Neurons.Select(neuron => neuron.Output); }
        }

        public Layer(int inputCount, Layer nextLayer)
        {
            
        }

        public void Calculate(IEnumerable<double> inputs)
        {
            var inputList = inputs as IList<double> ?? inputs.ToList();
            foreach (var neuron in Neurons)
            {
                neuron.CalculateOutput(inputList);
            }
        }
    }
}
