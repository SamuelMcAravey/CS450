using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace IrisDataset
{
    public sealed class IrisPlant : IClassifiable, IClassified<string>
    {
		public static IReadOnlyList<IrisPlant> ReadPlants()
		{
			List<IrisPlant> plants;
			using (var reader = File.OpenText("iris.data"))
			{
				var csv = new CsvReader(reader);
				csv.Configuration.RegisterClassMap<IrisPlantMap>();
				csv.Configuration.HasHeaderRecord = false;
				plants = csv.GetRecords<IrisPlant>().ToList();
			}
			return plants;
		}

        public void SetValue(string property, object value)
        {
            switch (property)
            {
                case "SepalLength":
                    this.SepalLength = (double)value;
                    break;
                case "SepalWidth":
                    this.SepalWidth = (double)value;
                    break;
                case "PetalLength":
                    this.PetalLength = (double)value;
                    break;
                case "PetalWidth":
                    this.PetalWidth = (double)value;
                    break;
                default:
                    break;
            }
        }

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
