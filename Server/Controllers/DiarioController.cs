using System.Security.Claims;

using diarioAlimentar.Server.Data;
using diarioAlimentar.Server.Models;

using Duende.IdentityServer.Extensions;

using IdentityModel;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

    [HttpGet("hoje")]
    public async Task<ActionResult<Diario>> GetDiarioHoje()
    {
        if (HttpContext.User == null)
            return BadRequest();

        var idUsuarioRequest = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var diarioHoje = _ctx.Diarios.FirstOrDefault(d => d.usuarioID == idUsuarioRequest);
        if (diarioHoje == null)
        {
            var novoDiario = new Diario(idUsuarioRequest);
            _ctx.Diarios.Add(novoDiario);
            await _ctx.SaveChangesAsync();
            return Ok(novoDiario);
        }
         else
        {
            return Ok(diarioHoje);
        }
    }

    [HttpPost("set")]
    public async Task<ActionResult<Diario>> SetDiario(Diario diario)
    {
        var usuario = HttpContext.User;
        if (usuario != null)
        {
            var diarioAnterior = await _ctx.Diarios.FirstAsync(d => d == diario);
            _ctx.Diarios.Entry(diarioAnterior).CurrentValues.SetValues(diario);
            foreach (var r in diario.refeicoes)
            {
                var refeicaoAnterior = diarioAnterior.refeicoes.FirstOrDefault(refe => refe.refeicaoID == r.refeicaoID);
                if (refeicaoAnterior == null)
                {
                    diarioAnterior.refeicoes.Add(r);
                }
                else
                {
                    _ctx.Entry(refeicaoAnterior).CurrentValues.SetValues(r);
                    foreach (var porcAlm in r.alimentos)
                    {
                        var porcaoAnterior = refeicaoAnterior.alimentos.FirstOrDefault(po => po.porcaoID == porcAlm.porcaoID);
                        if (porcaoAnterior == null)
                            refeicaoAnterior.alimentos.Add(porcAlm);
                        else
                        {
                            _ctx.Entry(porcaoAnterior).CurrentValues.SetValues(porcAlm);
                        }
                    }
                }
            }
            await _ctx.SaveChangesAsync();
            return Ok(diario);
        }
        else
            return BadRequest();
    }

    [HttpGet("data/{data}")]
    public async Task<ActionResult<Diario>> GetDiarioHoje(DateTime data)
    {
        if (HttpContext.User == null)
            return BadRequest();

        var idUsuarioRequest = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var diarioPorData = await _ctx.Diarios.FirstOrDefaultAsync(d => d.usuarioID == idUsuarioRequest && d.data.Date == data.Date);
        if (diarioPorData != null)
            return Ok(diarioPorData);
        else
            return NotFound();
    }
}
