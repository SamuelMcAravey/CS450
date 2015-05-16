using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using Utilities;

namespace LensesDataset
{
    public sealed class Patient : IClassifiable, IClassified<PatientLenseClass>
    {
		public static IReadOnlyList<Patient> ReadPatients()
		{
			List<Patient> patients = new List<Patient>();
			using (var reader = File.OpenText("lenses.data"))
			{
			    TextFieldParser parser = new TextFieldParser(reader)
			                             {
			                                 Delimiters = new[] {"\t"}
			                             };

			    while (!parser.EndOfData)
			    {
			        var row = parser.ReadFields();
                    if (row == null)
                        continue;

                    patients.Add(new Patient
                                 {
                                     Age = (PatientAge)Convert.ToInt32(row[1]),
                                     Prescription = (PatientPrescription) Convert.ToInt32(row[2]),
                                     Astigmatic = (PatientAstigmatic) Convert.ToInt32(row[3]),
                                     TearProductionRate = (PatientTearProductionRate) Convert.ToInt32(row[4]),
                                     Class = (PatientLenseClass)Convert.ToInt32(row[5])
                                 });
			    }
			}
			return patients;
		}

        public void SetValue(string property, object value)
        {
            switch (property)
            {
                case "Age":
                    this.Age = (PatientAge) value;
                    break;
                case "Prescription":
                    this.Prescription = (PatientPrescription) value;
                    break;
                case "Astigmatic":
                    this.Astigmatic = (PatientAstigmatic) value;
                    break;
                case "TearProductionRate":
                    this.TearProductionRate = (PatientTearProductionRate) value;
                    break;
                default:
                    break;
            }
        }

        public PatientAge Age { get; set; }
        public PatientPrescription Prescription { get; set; }
        public PatientAstigmatic Astigmatic { get; set; }
        public PatientTearProductionRate TearProductionRate { get; set; }
        public PatientLenseClass Class { get; set; }

        public IEnumerable<Tuple<string, object>> Values
        {
            get
            {
                yield return Tuple.Create("Age", (object)this.Age);
                yield return Tuple.Create("Prescription", (object)this.Prescription);
                yield return Tuple.Create("Astigmatic", (object)this.Astigmatic);
                yield return Tuple.Create("TearProductionRate", (object)this.TearProductionRate);
            }
        }

        private IReadOnlyDictionary<string, object> valueDictionary;
        public IReadOnlyDictionary<string, object> ValueDictionary
        {
            get
            {
                if (this.valueDictionary == null)
                {
                    this.valueDictionary = this.Values.ToDictionary(val => val.Item1, val => val.Item2);
                }
                return this.valueDictionary;
            }
        }
    }
}
