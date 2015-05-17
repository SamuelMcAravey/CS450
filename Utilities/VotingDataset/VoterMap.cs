using CsvHelper.Configuration;

namespace VotingDataset
{
    internal sealed class VoterMap : CsvClassMap<Voter>
    {
        public VoterMap()
        {
            Map(m => m.Class).Index(0);
            Map(m => m.HandicappedInfants).Index(1);
            Map(m => m.WaterProjectCostSharing).Index(2);
            Map(m => m.AdoptionOfTheBudgetResolution).Index(3);
            Map(m => m.PhysicianFeeFreeze).Index(4);
            Map(m => m.ElSalvadorAid).Index(5);
            Map(m => m.ReligiousGroupsInSchools).Index(6);
            Map(m => m.AntiSatelliteTestBan).Index(7);
            Map(m => m.AidToNicaraguanContras).Index(8);
            Map(m => m.MxMissile).Index(9);
            Map(m => m.Immigration).Index(10);
            Map(m => m.SynfuelsCorporationCutback).Index(11);
            Map(m => m.EducationSpending).Index(12);
            Map(m => m.SuperfundRightToSue).Index(13);
            Map(m => m.Crime).Index(14);
            Map(m => m.DutyFreeExports).Index(15);
            Map(m => m.ExportAdministrationActSouthAfrica).Index(16);
        }
    }
}
