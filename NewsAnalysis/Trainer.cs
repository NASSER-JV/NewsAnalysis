using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using NewsAnalysis.Schemas;

namespace NewsAnalysis;

public class Trainer
{
    public void TrainAndSave(List<NewsSchema> news, string modelFileName)
    {
        var data = LoadDataAndSplit(news);
        var estimator = CreateEstimator();

        var model = estimator.Fit(data.TrainSet);
        OutputPredictions(model, data.TestSet);

        SaveModel(model, data.TrainSet.Schema, modelFileName);
    }

    private void SaveModel(ITransformer model, DataViewSchema schema, string filePath)
    {
        Context.MlContext.Model.Save(model, schema, filePath);
    }

    private DataOperationsCatalog.TrainTestData LoadDataAndSplit(List<NewsSchema> news)
    {
        var dataView = Context.MlContext.Data.LoadFromEnumerable(news);

        return Context.MlContext.Data.TrainTestSplit(dataView, 0.2);
    }

    private EstimatorChain<BinaryPredictionTransformer<LinearBinaryModelParameters>> CreateEstimator(
        int iterations = 500)
    {
        return Context.MlContext.Transforms.Concatenate("Features", nameof(NewsSchema.Title), nameof(NewsSchema.Text))
            .Append(Context.MlContext.Transforms.Text.FeaturizeText("Features"))
            .Append(Context.MlContext.BinaryClassification.Trainers.AveragedPerceptron(nameof(NewsSchema.Sentiment),
                numberOfIterations: iterations))
            .AppendCacheCheckpoint(Context.MlContext);
    }

    private void OutputPredictions(ITransformer model,
        IDataView testSet)
    {
        var predictions = model.Transform(testSet);
        var metrics =
            Context.MlContext.BinaryClassification.EvaluateNonCalibrated(predictions, nameof(NewsSchema.Sentiment));

        Console.WriteLine("Model quality metrics evaluation");
        Console.WriteLine("--------------------------------");
        Console.WriteLine($"Accuracy: {metrics.Accuracy:P2}");
        Console.WriteLine($"Auc: {metrics.AreaUnderRocCurve:P2}");
        Console.WriteLine($"F1Score: {metrics.F1Score:P2}");
        Console.WriteLine("=============== End of model evaluation ===============");
    }
}