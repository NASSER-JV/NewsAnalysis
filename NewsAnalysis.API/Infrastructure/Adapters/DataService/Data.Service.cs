using NewsAnalysis.API.App.Ports;
using NewsAnalysis.API.Infrastructure.Adapters.DataService.Dtos;

namespace NewsAnalysis.API.Infrastructure.Adapters.DataService;

public class DataService : IDataService
{
    public DataService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        HttpClient = httpClientFactory.CreateClient();
        HttpClient.BaseAddress = new Uri(configuration["DataServiceHost"]);
    }

    private HttpClient HttpClient { get; }

    public async Task<List<NoticiaAnalise>?> BuscarTodasNoticiasAnalise()
    {
        return await HttpClient.GetFromJsonAsync<List<NoticiaAnalise>>("api/noticias-analise");
    }

    public async Task<List<Noticia>?> BuscarNoticiasPorPeriodoECodigo(DateOnly dataInicial, DateOnly dataFinal,
        int empresaId)
    {
        return await HttpClient.GetFromJsonAsync<List<Noticia>>(
            $"/api/noticias?empresaId={empresaId}&dataInicial={dataInicial:dd/MM/yyyy}&dataFinal={dataFinal:dd/MM/yyyy}");
    }

    public async Task SalvarNoticia(Noticia noticia)
    {
        await HttpClient.PostAsJsonAsync("/api/noticias", noticia);
    }
}