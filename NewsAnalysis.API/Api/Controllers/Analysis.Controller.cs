using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewsAnalysis.API.Api.Dtos;
using NewsAnalysis.API.App.Commands;

namespace NewsAnalysis.API.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnalysisController : ControllerBase
{
    public AnalysisController(IMediator mediator)
    {
        Mediator = mediator;
    }

    private IMediator Mediator { get; }

    [HttpPost("treinar")]
    public async Task<ActionResult> TreinarModelo()
    {
        await Mediator.Send(new TreinarModeloCommand());

        return Ok();
    }

    [HttpPost("analisar-noticias")]
    public async Task<ActionResult> AnalisarNoticias([FromQuery] AnalisarNoticiasRequest query)
    {
        var (dataInicial, dataFinal, empresaId) = query;

        await Mediator.Send(new AnalisarNoticiasCommand(DateOnly.ParseExact(dataInicial, "dd/MM/yyyy"),
            DateOnly.ParseExact(dataFinal, "dd/MM/yyyy"),
            empresaId));

        return Ok();
    }
}