using System.Text.Json;

using Microsoft.AspNetCore.Mvc;

namespace diarioAlimentar.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AlimentoController : ControllerBase
{
    private IReadOnlyDictionary<int, Alimento> _alimentos;

    public AlimentoController()
    {
        var info = System.IO.File.ReadAllText("./Data/Info_Alimentos.json");
        _alimentos = JsonSerializer.Deserialize<Alimento[]>(info).Where(a => a != null).ToDictionary(a => a.AlimentoID, a => a);
    }

    [HttpGet("id/{id}")]
    public async Task<ActionResult<Alimento>> GetAlimentoPorID(int id)
    {
        _alimentos.TryGetValue(id, out var resultado);
        if (resultado != null)
            return Ok(resultado);
        else
            return NotFound();
    }

    [HttpGet("nome/{nome}")]
    public async Task<ActionResult<ICollection<Alimento>>> GetAlimentoPorNome(string nome)
    {
        var resultado = _alimentos.Where(a => a.Value.nome.ToLower().Contains(nome)).Select(a => a.Value);
        return Ok(resultado);
    }
}
