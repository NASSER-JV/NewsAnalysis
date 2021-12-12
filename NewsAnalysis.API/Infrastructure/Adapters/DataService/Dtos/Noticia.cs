namespace NewsAnalysis.API.Infrastructure.Adapters.DataService.Dtos;

public class Noticia
{
    public Noticia(int id, string url, string titulo, string texto, string data, int sentimento, int? analise,
        decimal? peso, string? eventoId, int empresaId)
    {
        Id = id;
        Url = url;
        Titulo = titulo;
        Texto = texto;
        Data = data;
        Sentimento = sentimento;
        Analise = analise;
        Peso = peso;
        EventoId = eventoId;
        EmpresaId = empresaId;
    }

    public int Id { get; }
    public string Url { get; }
    public string Titulo { get; }
    public string Texto { get; }
    public string Data { get; }
    public int Sentimento { get; }
    public int? Analise { get; set; }
    public decimal? Peso { get; set; }
    public string? EventoId { get; }
    public int EmpresaId { get; }
}