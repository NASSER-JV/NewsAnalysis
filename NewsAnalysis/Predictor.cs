using Microsoft.ML;
using NewsAnalysis.Schemas;

namespace NewsAnalysis;

public class Predictor
{
    public Predictor(Stream modelStream)
    {
        var model = Context.MlContext.Model.Load(modelStream, out _);
        PredictionEngine = Context.MlContext.Model.CreatePredictionEngine<NewsSchema, NewsPredictionSchema>(model);
    }

    private PredictionEngine<NewsSchema, NewsPredictionSchema> PredictionEngine { get; }

    public NewsPredictionSchema Predict(NewsSchema news)
    {
        return PredictionEngine.Predict(news);
    }
}