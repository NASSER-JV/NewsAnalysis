using MediatR;
using NewsAnalysis.API.App.Commands;
using NewsAnalysis.API.App.Ports;
using NewsAnalysis.API.Infrastructure.Adapters.DataService.Dtos;
using NewsAnalysis.Schemas;

namespace NewsAnalysis.API.App.Command_Handlers;

public class AnalisarNoticiasCommandHandler : IRequestHandler<AnalisarNoticiasCommand>
{
    public AnalisarNoticiasCommandHandler(IStorageService storageService, IDataService dataService)
    {
        DataService = dataService;

        var modelStream = storageService.GetFileStream("modelo-ml.zip").Result;
        Predictor = new Predictor(modelStream);
    }

    private IDataService DataService { get; }
    private Predictor Predictor { get; }

    public async Task<Unit> Handle(AnalisarNoticiasCommand request, CancellationToken cancellationToken)
    {
        var noticias =
            await DataService.BuscarNoticiasPorPeriodoECodigo(request.DataInicial, request.DataFinal,
                request.EmpresaId);

        if (noticias is null) return Unit.Value;

        AnalisarNoticias(ref noticias);
        CalcularPeso(ref noticias);

        await Task.WhenAll(noticias.Select(DataService.SalvarNoticia));

        return Unit.Value;
    }

    private void AnalisarNoticias(ref List<Noticia> noticias)
    {
        foreach (var noticia in noticias)
        {
            var newsSchema = new NewsSchema
            {
                Title = noticia.Titulo,
                Text = noticia.Texto
            };

            var newsPrediction = Predictor.Predict(newsSchema);

            noticia.Analise = newsPrediction.Prediction ? 1 : -1;
            noticia.Peso = 1;
        }
    }

    private void CalcularPeso(ref List<Noticia> noticias)
    {
        var noticiasAgrupadasPorEvento = noticias.GroupBy(noticia => noticia.EventoId).ToList();

        foreach (var grouping in noticiasAgrupadasPorEvento.Where(grupo => grupo.Key is not null))
        {
            var modificadorPeso = decimal.Divide(grouping.Count(), noticias.Count);

            foreach (var noticia in grouping) noticia.Peso += modificadorPeso;
        }
    }
}