using CsvHelper.Configuration;

namespace BreastCancerDataset
{
    internal sealed class PatientMap : CsvClassMap<IrisPlant>
    {
        public PatientMap()
        {
            Map(m => m.SepalLength).Index(0);
            Map(m => m.SepalWidth).Index(1);
            Map(m => m.PetalLength).Index(2);
            Map(m => m.PetalWidth).Index(3);
            Map(m => m.Class).Index(4);
        }
    }
}
