using Microsoft.ML.Data;

namespace NewsAnalysis.Schemas;

public class NewsSchema
{
    [ColumnName("Text")] public string Text { get; set; } = null!;
    public string Title { get; set; } = null!;
    [ColumnName("Sentiment")] public bool Sentiment { get; set; }
}