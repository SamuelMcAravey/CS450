using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using Utilities;

namespace ChessDataset
{
    public sealed class ChessEntry : IClassifiable, IClassified<string>
    {
        public static IReadOnlyList<ChessEntry> ReadChessEntries()
        {
            List<ChessEntry> entries;
            using (var reader = File.OpenText("chess.data"))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.RegisterClassMap<ChessEntryMap>();
                csv.Configuration.HasHeaderRecord = false;
                entries = csv.GetRecords<ChessEntry>().ToList();
            }
            return entries;
        }

        public void SetValue(string property, object value)
        {
            switch (property)
            {
                case "WhiteKingFile":
                    this.WhiteKingFile = (string)value;
                    break;
                case "WhiteKingRank":
                    this.WhiteKingRank = (string)value;
                    break;
                case "WhiteRookFile":
                    this.WhiteRookFile = (string)value;
                    break;
                case "WhiteRookRank":
                    this.WhiteRookRank = (string)value;
                    break;
                case "BlackKingFile":
                    this.BlackKingFile = (string)value;
                    break;
                case "BlackKingRank":
                    this.BlackKingRank = (string)value;
                    break;
                default:
                    break;
            }
        }

        public string WhiteKingFile { get; set; }
        public string WhiteKingRank { get; set; }
        public string WhiteRookFile { get; set; }
        public string WhiteRookRank { get; set; }
        public string BlackKingFile { get; set; }
        public string BlackKingRank { get; set; }

        public string Class { get; set; }

        public IEnumerable<Tuple<string, object>> Values
        {
            get
            {
                yield return Tuple.Create("WhiteKingFile", (object)this.WhiteKingFile);
                yield return Tuple.Create("WhiteKingRank", (object) this.WhiteKingRank);
                yield return Tuple.Create("WhiteRookFile", (object)this.WhiteRookFile);
                yield return Tuple.Create("WhiteRookRank", (object)this.WhiteRookRank);
                yield return Tuple.Create("BlackKingFile", (object)this.BlackKingFile);
                yield return Tuple.Create("BlackKingRank", (object)this.BlackKingRank);
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