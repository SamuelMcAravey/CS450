using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace HardCodedClassifier
{
    internal sealed class IrisPlantMap : CsvClassMap<IrisPlant>
    {
        public IrisPlantMap()
        {
            Map(m => m.SepalLength).Index(0);
            Map(m => m.SepalWidth).Index(1);
            Map(m => m.PetalLength).Index(2);
            Map(m => m.PetalWidth).Index(3);
            Map(m => m.Class).Index(4);
        }
    }
}
