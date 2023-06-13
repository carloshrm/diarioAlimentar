using diarioAlimentar.Server.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diarioAlimentar.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class RefeicaoController : ControllerBase
{
    private readonly ApplicationDbContext _ctx;

    public RefeicaoController(ApplicationDbContext diarioContext)
    {
        _ctx = diarioContext;
    }

    [HttpPost("add")]
    public async Task<ActionResult<Diario>> AddRefeicao(Refeicao rf)
    {
        var refExistente = _ctx.Refeicoes.FirstOrDefault(r => r.refeicaoID == rf.refeicaoID);
        if (refExistente != null)
            _ctx.Refeicoes.Update(rf);
        else
        {
            _ctx.Refeicoes.Add(rf);
            foreach (var p in rf.porcoes)
                _ctx.Porcoes.Add(p);
        }
        await _ctx.SaveChangesAsync();
        return Ok(rf);
    }

    [HttpPost("upd")]
    public async Task<ActionResult<Diario>> UpdateRefeicao(Refeicao rf)
    {
        var refExistente = _ctx.Refeicoes.FirstOrDefault(r => r.refeicaoID == rf.refeicaoID);
        if (refExistente == null)
            return BadRequest();
        else
        {
            _ctx.Refeicoes.Update(rf);
            _ctx.Porcoes.Where(p => p.refeicaoID == rf.refeicaoID).ToList();
        }

        await _ctx.SaveChangesAsync();
        return Ok(refExistente);
    }

    [HttpGet("del/{rID}")]
    public async Task<ActionResult<Diario>> RemoveRefeicao(Guid rID)
    {
        var refDB = _ctx.Refeicoes.First(r => r.refeicaoID == rID);
        _ctx.Refeicoes.Remove(refDB);
        await _ctx.SaveChangesAsync();
        return Ok();
    }
}
