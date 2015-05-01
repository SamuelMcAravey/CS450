using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace CarDataset
{
    internal class NameDoubleConverter : DefaultTypeConverter
    {
        Dictionary<string, double> nameValues;
        public NameDoubleConverter(Dictionary<string,double> nameValues)
        {
            this.nameValues = nameValues;
        }
        public override bool CanConvertFrom(Type type)
        {
            return true;
        }
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            return this.nameValues[text];
        }
    }
    internal sealed class CarMap : CsvClassMap<Car>
    {
        public CarMap()
        {
            Map(m => m.Buying).Index(0).TypeConverter(new NameDoubleConverter(new Dictionary<string, double>
            {
                { "vhigh",3 },
                { "high",2 },
                { "med",1 },
                { "low",0 }
            }));
            Map(m => m.Maintenance).Index(1).TypeConverter(new NameDoubleConverter(new Dictionary<string, double>
            {
                { "vhigh",3 },
                { "high",2 },
                { "med",1 },
                { "low",0 }
            }));
            Map(m => m.Doors).Index(2).TypeConverter(new NameDoubleConverter(new Dictionary<string, double>
            {
                { "5more",5 },
                { "4",4 },
                { "3",3 },
                { "2",2 }
            }));
            Map(m => m.Persons).Index(3).TypeConverter(new NameDoubleConverter(new Dictionary<string, double>
            {
                { "more",2 },
                { "4",1 },
                { "2",0 }
            }));
            Map(m => m.BootSize).Index(4).TypeConverter(new NameDoubleConverter(new Dictionary<string, double>
            {
                { "big",2 },
                { "med",1 },
                { "small",0 }
            }));
            Map(m => m.Safety).Index(5).TypeConverter(new NameDoubleConverter(new Dictionary<string, double>
            {
                { "high",2 },
                { "med",1 },
                { "low",0 }
            }));
            Map(m => m.Class).Index(6).TypeConverter(new EnumConverter(typeof(CarClass)));
        }
    }
}
