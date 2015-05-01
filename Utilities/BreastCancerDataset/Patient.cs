using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Utilities;

namespace BreastCancerDataset
{
    public sealed class Patient : IClassifiable, IClassified<string>
    {
        public static IReadOnlyList<Patient> ReadPatients()
        {
            List<Patient> patientsd;
            using (var reader = File.OpenText("breastcancer.data"))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.RegisterClassMap<PatientMap>();
                csv.Configuration.HasHeaderRecord = false;
                patients = csv.GetRecords<Patient>().ToList();
            }
            return patients;
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
    }
}
