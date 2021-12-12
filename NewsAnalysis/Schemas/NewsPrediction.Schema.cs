using Microsoft.ML.Data;

namespace NewsAnalysis.Schemas;

public class NewsPredictionSchema
{
    [ColumnName("PredictedLabel")] public bool Prediction { get; set; }
    public float Score { get; set; }
}