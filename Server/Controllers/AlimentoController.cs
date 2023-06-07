using Microsoft.AspNetCore.Mvc;

namespace diarioAlimentar.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AlimentoController : ControllerBase
{
    private IAlimentoProvider _provider;

    public AlimentoController(IAlimentoProvider provider)
    {
        _provider = provider;
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<Alimento>> GetAlimentoPorID(int id)
    {
        var resultado = _provider.GetAlimentoPorID(id);
        if (resultado != null)
            return Ok(resultado);
        else
            return NotFound();
    }

    [HttpGet("nome/{nome}")]
    public async Task<ActionResult<ICollection<Alimento>>> GetAlimentoPorNome(string nome)
    {
        var resultado = _provider.BuscarAlimentosPorNome(nome);
        return Ok(resultado);
    }
}