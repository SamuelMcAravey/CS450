using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Utilities;

namespace CarDataset
{
    internal sealed class CarMap : CsvClassMap<Car>
    {
        public CarMap()
        {
            Map(m => m.Buying).Index(0);
            Map(m => m.Maintenance).Index(1);
            Map(m => m.Doors).Index(2);
            Map(m => m.Persons).Index(3);
            Map(m => m.BootSize).Index(4);
            Map(m => m.Safety).Index(5);
            Map(m => m.Class).Index(6).TypeConverter(new EnumConverter(typeof(CarClass)));
        }
    }
}
