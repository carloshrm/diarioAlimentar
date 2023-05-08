using diarioAlimentar.Server.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace diarioAlimentar.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DiarioController : Controller
{
    private readonly ApplicationDbContext _ctx;
    public DiarioController(ApplicationDbContext diarioContext)
    {
        _ctx = diarioContext;
    }

    [HttpGet]
    public async Task<ActionResult<Diario>> GetDiarioHoje()
    {
        var diarioHoje = await _ctx.diarios.FirstOrDefaultAsync();
        if (diarioHoje == null)
        {
            return NotFound();
        }
        return Ok(diarioHoje);
    }
}
