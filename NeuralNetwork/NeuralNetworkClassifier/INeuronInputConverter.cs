using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetworkClassifier
{
    public interface INeuronInputConverter<out T>
    {
        T ConvertInput(double total);
    }
}
