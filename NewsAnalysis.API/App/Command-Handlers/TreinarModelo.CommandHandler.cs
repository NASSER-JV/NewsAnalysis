using MediatR;
using NewsAnalysis.API.App.Commands;
using NewsAnalysis.API.App.Ports;
using NewsAnalysis.Schemas;

namespace NewsAnalysis.API.App.Command_Handlers;

public class TreinarModeloCommandHandler : IRequestHandler<TreinarModeloCommand>
{
    public TreinarModeloCommandHandler(IStorageService storageService, IDataService dataService)
    {
        StorageService = storageService;
        DataService = dataService;
        Trainer = new Trainer();
    }

    private IStorageService StorageService { get; }
    private IDataService DataService { get; }
    private Trainer Trainer { get; }
    private static string ModelFileName => "modelo-ml.zip";

    public async Task<Unit> Handle(TreinarModeloCommand request, CancellationToken cancellationToken)
    {
        var noticiasAnalise = await DataService.BuscarTodasNoticiasAnalise();

        if (noticiasAnalise is null) return Unit.Value;

        Trainer.TrainAndSave(
            noticiasAnalise.Select(noticiaAnalise => new NewsSchema
            {
                Title = noticiaAnalise.Titulo, Text = noticiaAnalise.Texto,
                Sentiment = noticiaAnalise.Sentimento == 1
            }).ToList(), ModelFileName);

        await StorageService.UploadFile(ModelFileName, "application/zip", ModelFileName);
        File.Delete(ModelFileName);

        return Unit.Value;
    }
}