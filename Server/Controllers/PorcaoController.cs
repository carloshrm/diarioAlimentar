using System.Security.Cryptography;

using diarioAlimentar.Server.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diarioAlimentar.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PorcaoController : ControllerBase
{
    private readonly ApplicationDbContext _ctx;

    public PorcaoController(ApplicationDbContext diarioContext)
    {
        _ctx = diarioContext;
    }

    [HttpPost("set")]
    public async Task<ActionResult<Porcao>> SetPorcao(Porcao p)
    {
        var porcaoExistente = _ctx.Porcoes
            .FirstOrDefault(pc => pc.porcaoID == p.porcaoID);

        if (porcaoExistente == null)
            _ctx.Porcoes.Add(p);
        else
            _ctx.Porcoes.Update(p);

        await _ctx.SaveChangesAsync();
        return Ok(p);
    }

    [HttpGet("del/{pID}")]
    public async Task<ActionResult<Diario>> RemovePorcao(Guid pID)
    {
        var porcaoDB = _ctx.Porcoes.FirstOrDefault(r => r.porcaoID == pID);

        if (porcaoDB == null) 
            return NotFound();
        else
        {
            _ctx.Porcoes.Remove(porcaoDB);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
