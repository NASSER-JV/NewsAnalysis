using NewsAnalysis.API.Infrastructure.Adapters.DataService.Dtos;

namespace NewsAnalysis.API.App.Ports;

public interface IDataService
{
    public Task<List<NoticiaAnalise>?> BuscarTodasNoticiasAnalise();

    public Task<List<Noticia>?>
        BuscarNoticiasPorPeriodoECodigo(DateOnly dataInicial, DateOnly dataFinal, int empresaId);

    public Task SalvarNoticia(Noticia noticia);
}