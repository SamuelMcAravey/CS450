using CsvHelper.Configuration;

namespace ChessDataset
{
    internal sealed class ChessEntryMap : CsvClassMap<ChessEntry>
    {
        public ChessEntryMap()
        {
            Map(m => m.WhiteKingFile).Index(0);
            Map(m => m.WhiteKingRank).Index(1);
            Map(m => m.WhiteRookFile).Index(2);
            Map(m => m.WhiteRookRank).Index(3);
            Map(m => m.BlackKingFile).Index(4);
            Map(m => m.BlackKingRank).Index(5);
            Map(m => m.Class).Index(6);
        }
    }
}
