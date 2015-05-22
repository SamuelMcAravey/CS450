using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace IrisDataset
{
    internal sealed class IrisPlantMap : CsvClassMap<IrisPlant>
    {
        public IrisPlantMap()
        {
            Map(m => m.SepalLength).Index(0);
            Map(m => m.SepalWidth).Index(1);
            Map(m => m.PetalLength).Index(2);
            Map(m => m.PetalWidth).Index(3);
            Map(m => m.Class).Index(4).TypeConverter<IrisClassTypeConverter>();
        }
    }

    class IrisClassTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(Type type) => true;

        public bool CanConvertTo(Type type) => true;

        public object ConvertFromString(TypeConverterOptions options, string text)
        {
            switch (text)
            {
                case "Iris-setosa":
                    return IrisPlantClass.Setosa;
                case "Iris-versicolor":
                    return IrisPlantClass.Versicolor;
                case "Iris-virginica":
                    return IrisPlantClass.Virginica;
                default:
                    return null;
            }
        }

        public string ConvertToString(TypeConverterOptions options, object value) => value.ToString();
    }
}
