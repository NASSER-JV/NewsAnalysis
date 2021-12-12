using System.ComponentModel;

namespace NewsAnalysis.API.Api.Dtos;

public record AnalisarNoticiasRequest(string DataInicial, string DataFinal, int EmpresaId)
{
    [DefaultValue("01/12/2021")] public string DataInicial { get; } = DataInicial;
    [DefaultValue("10/12/2021")] public string DataFinal { get; } = DataFinal;
    public int EmpresaId { get; } = EmpresaId;
}