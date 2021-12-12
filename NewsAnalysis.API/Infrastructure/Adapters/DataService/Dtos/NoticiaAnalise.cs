namespace NewsAnalysis.API.Infrastructure.Adapters.DataService.Dtos;

public class NoticiaAnalise
{
    public NoticiaAnalise(string url, string titulo, string texto, string data, int sentimento, List<string> tickers)
    {
        Url = url;
        Titulo = titulo;
        Texto = texto;
        Data = data;
        Sentimento = sentimento;
        Tickers = tickers;
    }

    public string Url { get; }
    public string Titulo { get; }
    public string Texto { get; }
    public string Data { get; }
    public int Sentimento { get; }
    public List<string> Tickers { get; }
}