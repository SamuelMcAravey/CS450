using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace HardCodedClassifier
{
    public sealed class IrisPlant : IClassifiable, IClassified<string>
    {
        public double SepalLength { get; set; }
        public double SepalWidth { get; set; }
        public double PetalLength { get; set; }
        public double PetalWidth { get; set; }
        public string Class { get; set; }

        public IEnumerable<Tuple<string, object>> Values
        {
            get
            {
                yield return Tuple.Create("SepalLength", (object)this.SepalLength);
                yield return Tuple.Create("SepalWidth", (object)this.SepalWidth);
                yield return Tuple.Create("PetalLength", (object)this.PetalLength);
                yield return Tuple.Create("PetalWidth", (object)this.PetalWidth);
            }
        }
    }
}
