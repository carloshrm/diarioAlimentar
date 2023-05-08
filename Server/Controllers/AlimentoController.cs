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

    [HttpGet("{id}")]
    public async Task<ActionResult<Alimento>> GetAlimentoPorID(int id)
    {
        _alimentos.TryGetValue(id, out var result);

        if (result != null)
            return Ok(result);
        else
            return NotFound();
    }
}
