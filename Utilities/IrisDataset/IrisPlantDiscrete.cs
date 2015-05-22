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
    public sealed class IrisPlantDiscrete : IClassifiable, IClassified<IrisPlantClass>
    {
		public static IReadOnlyList<IrisPlantDiscrete> ReadPlants()
		{
		    var irisPlants = IrisPlant.ReadPlants();
            var petalLengthDiscretizer = Discretizer.CreateDiscretizer(irisPlants, plant => plant.PetalLength);
            var petalWidthDiscretizer = Discretizer.CreateDiscretizer(irisPlants, plant => plant.PetalWidth);
            var sepalLengthDiscretizer = Discretizer.CreateDiscretizer(irisPlants, plant => plant.SepalLength);
            var sepalWidthDiscretizer = Discretizer.CreateDiscretizer(irisPlants, plant => plant.SepalWidth);
		    return irisPlants.Select(plant => new IrisPlantDiscrete
		                                      {
		                                          Class = plant.Class,
		                                          PetalLength = petalLengthDiscretizer(plant),
		                                          PetalWidth = petalWidthDiscretizer(plant),
		                                          SepalLength = sepalLengthDiscretizer(plant),
                                                  SepalWidth = sepalWidthDiscretizer(plant)
		                                      }).ToList();
		}

        public void SetValue(string property, object value)
        {
            switch (property)
            {
                case "SepalLength":
                    this.SepalLength = (int)value;
                    break;
                case "SepalWidth":
                    this.SepalWidth = (int)value;
                    break;
                case "PetalLength":
                    this.PetalLength = (int)value;
                    break;
                case "PetalWidth":
                    this.PetalWidth = (int)value;
                    break;
                default:
                    break;
            }
        }

        public int SepalLength { get; set; }
        public int SepalWidth { get; set; }
        public int PetalLength { get; set; }
        public int PetalWidth { get; set; }
        public IrisPlantClass Class { get; set; }

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
