using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace CarDataset
{
    public sealed class Car : IClassifiable, IClassified<CarClass>
    {
		public static IReadOnlyList<Car> ReadCars()
		{
			List<Car> plants;
			using (var reader = File.OpenText("car.data"))
			{
				var csv = new CsvReader(reader);
				csv.Configuration.RegisterClassMap<CarMap>();
				csv.Configuration.HasHeaderRecord = false;
				plants = csv.GetRecords<Car>().ToList();
			}
			return plants;
		}

        public void SetValue(string property, object value)
        {
            switch (property)
            {
                case "Buying":
                    this.Buying = (string)value;
                    break;
                case "Maintenance":
                    this.Maintenance = (string)value;
                    break;
                case "Doors":
                    this.Doors = (string)value;
                    break;
                case "Persons":
                    this.Persons = (string)value;
                    break;
                case "BootSize":
                    this.BootSize = (string)value;
                    break;
                case "Safety":
                    this.Safety = (string)value;
                    break;
                default:
                    break;
            }
        }

        public string Buying { get; set; }
        public string Maintenance { get; set; }
        public string Doors { get; set; }
        public string Persons { get; set; }
        public string BootSize { get; set; }
        public string Safety { get; set; }
        public CarClass Class { get; set; }

        private IEnumerable<Tuple<string, object>> values = null;
        public IEnumerable<Tuple<string, object>> Values
        {
            get
            {
                if (values == null)
                {
                    values = new List<Tuple<string, object>>
                    {
                        Tuple.Create("Buying", (object)this.Buying),
                        Tuple.Create("Maintenance", (object)this.Maintenance),
                        Tuple.Create("Doors", (object)this.Doors),
                        Tuple.Create("Persons", (object)this.Persons),
                        Tuple.Create("BootSize", (object)this.BootSize),
                        Tuple.Create("Safety", (object)this.Safety)
                    };
                }
                return values;
            }
        }

        private IReadOnlyDictionary<string, object> valueDictionary;
        public IReadOnlyDictionary<string, object> ValueDictionary
        {
            get
            {
                if (valueDictionary == null)
                {
                    this.valueDictionary = this.Values.ToDictionary(val => val.Item1, val => val.Item2);
                }
                return valueDictionary;
            }
        }
    }
}
