using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Utilities;

namespace VotingDataset
{
    public sealed class Voter : IClassifiable, IClassified<string>
    {
        public static IReadOnlyList<Voter> ReadVoters()
        {
            List<Voter> voters;
            using (var reader = File.OpenText("voting.data"))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.RegisterClassMap<VoterMap>();
                csv.Configuration.HasHeaderRecord = false;
                voters = csv.GetRecords<Voter>().Do(voter => voter.Values.ForEach(pair =>
                                                                                  {
                                                                                      if (Equals(pair.Item2, "?"))
                                                                                          voter.SetValue(pair.Item1, "???NoValue???");
                                                                                  })).ToList();
            }
            return voters;
        }

        public void SetValue(string property, object value)
        {
            switch (property)
            {
                case "HandicappedInfants":
                    this.HandicappedInfants = (string)value;
                    break;
                case "WaterProjectCostSharing":
                    this.WaterProjectCostSharing = (string)value;
                    break;
                case "AdoptionOfTheBudgetResolution":
                    this.AdoptionOfTheBudgetResolution = (string)value;
                    break;
                case "PhysicianFeeFreeze":
                    this.PhysicianFeeFreeze = (string)value;
                    break;
                case "ElSalvadorAid":
                    this.ElSalvadorAid = (string)value;
                    break;
                case "ReligiousGroupsInSchools":
                    this.ReligiousGroupsInSchools = (string)value;
                    break;
                case "AntiSatelliteTestBan":
                    this.AntiSatelliteTestBan = (string)value;
                    break;
                case "AidToNicaraguanContras":
                    this.AidToNicaraguanContras = (string)value;
                    break;
                case "MxMissile":
                    this.MxMissile = (string)value;
                    break;
                case "Immigration":
                    this.Immigration = (string)value;
                    break;
                case "SynfuelsCorporationCutback":
                    this.SynfuelsCorporationCutback = (string)value;
                    break;
                case "EducationSpending":
                    this.EducationSpending = (string)value;
                    break;
                case "SuperfundRightToSue":
                    this.SuperfundRightToSue = (string)value;
                    break;
                case "Crime":
                    this.Crime = (string)value;
                    break;
                case "DutyFreeExports":
                    this.DutyFreeExports = (string)value;
                    break;
                case "ExportAdministrationActSouthAfrica":
                    this.ExportAdministrationActSouthAfrica = (string)value;
                    break;
                default:
                    break;
            }
        }

        public string HandicappedInfants { get; set; }
        public string WaterProjectCostSharing { get; set; }
        public string AdoptionOfTheBudgetResolution { get; set; }
        public string PhysicianFeeFreeze { get; set; }
        public string ElSalvadorAid { get; set; }
        public string ReligiousGroupsInSchools { get; set; }
        public string AntiSatelliteTestBan { get; set; }
        public string AidToNicaraguanContras { get; set; }
        public string MxMissile { get; set; }
        public string Immigration { get; set; }
        public string SynfuelsCorporationCutback { get; set; }
        public string EducationSpending { get; set; }
        public string SuperfundRightToSue { get; set; }
        public string Crime { get; set; }
        public string DutyFreeExports { get; set; }
        public string ExportAdministrationActSouthAfrica { get; set; }

        public string Class { get; set; }

        public IEnumerable<Tuple<string, object>> Values
        {
            get
            {
                yield return Tuple.Create("HandicappedInfants", (object)this.HandicappedInfants);
                yield return Tuple.Create("WaterProjectCostSharing", (object) this.WaterProjectCostSharing);
                yield return Tuple.Create("AdoptionOfTheBudgetResolution", (object)this.AdoptionOfTheBudgetResolution);
                yield return Tuple.Create("PhysicianFeeFreeze", (object)this.PhysicianFeeFreeze);
                yield return Tuple.Create("ElSalvadorAid", (object)this.ElSalvadorAid);
                yield return Tuple.Create("ReligiousGroupsInSchools", (object)this.ReligiousGroupsInSchools);
                yield return Tuple.Create("AntiSatelliteTestBan", (object)this.AntiSatelliteTestBan);
                yield return Tuple.Create("AidToNicaraguanContras", (object)this.AidToNicaraguanContras);
                yield return Tuple.Create("MxMissile", (object)this.MxMissile);
                yield return Tuple.Create("Immigration", (object)this.Immigration);
                yield return Tuple.Create("SynfuelsCorporationCutback", (object)this.SynfuelsCorporationCutback);
                yield return Tuple.Create("EducationSpending", (object)this.EducationSpending);
                yield return Tuple.Create("SuperfundRightToSue", (object)this.SuperfundRightToSue);
                yield return Tuple.Create("Crime", (object)this.Crime);
                yield return Tuple.Create("DutyFreeExports", (object)this.DutyFreeExports);
                yield return Tuple.Create("ExportAdministrationActSouthAfrica", (object)this.ExportAdministrationActSouthAfrica);
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