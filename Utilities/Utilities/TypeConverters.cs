using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.TypeConversion;

namespace Utilities
{
    public class NameDoubleConverter : DefaultTypeConverter
    {
        Dictionary<string, double> nameValues;
        public NameDoubleConverter(Dictionary<string, double> nameValues)
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

    public class ObjectToEnumConverter<TEnum> : DefaultTypeConverter where TEnum : struct
    {
        Dictionary<string, TEnum> enumValues;
        public ObjectToEnumConverter(Dictionary<string, TEnum> enumValues)
        {
            this.enumValues = enumValues;
        }
        public override bool CanConvertFrom(Type type)
        {
            return true;
        }
        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            return this.enumValues[text];
        }
    }
}
