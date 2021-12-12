using MediatR;

namespace NewsAnalysis.API.App.Commands;

public class AnalisarNoticiasCommand : IRequest
{
    public AnalisarNoticiasCommand(DateOnly dataInicial, DateOnly dataFinal, int empresaId)
    {
        DataFinal = dataFinal;
        DataInicial = dataInicial;
        EmpresaId = empresaId;
    }

    public DateOnly DataInicial { get; }
    public DateOnly DataFinal { get; }
    public int EmpresaId { get; }
}