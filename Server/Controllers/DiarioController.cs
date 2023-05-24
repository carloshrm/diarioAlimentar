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
public class DiarioController : ControllerBase
{
    private readonly ApplicationDbContext _ctx;
    private readonly AlimentoProvider _provider;

    public DiarioController(ApplicationDbContext diarioContext, AlimentoProvider alimentoProvider)
    {
        _ctx = diarioContext;
        _provider = alimentoProvider;
    }

    [HttpGet("hoje")]
    public async Task<ActionResult<Diario>> GetDiarioHoje()
    {
        var idUsuarioRequest = HttpContext.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var diarioHoje = _ctx.Diarios
            .FirstOrDefault(d => d.usuarioID == idUsuarioRequest && d.data.Date == DateTime.Now.ToUniversalTime().Date);

        if (diarioHoje == null)
        {
            var novoDiario = new Diario() { usuarioID = idUsuarioRequest };
            novoDiario.AdicionarRefeicao(new Refeicao());
            _ctx.Diarios.Add(novoDiario);
            await _ctx.SaveChangesAsync();
            return Ok(novoDiario);
        }
        else
        {
            foreach (var dbRefeicao in _ctx.Refeicoes
                .Where(r => r.diarioID == diarioHoje.diarioID)
                .ToList())
                _ctx.Porcoes
                    .Where(p => p.refeicaoID == dbRefeicao.refeicaoID)
                    .ToList()
                    .ForEach(p => p.Alimento = _provider.alimentos.GetValueOrDefault(p.alimentoID));

            return Ok(diarioHoje);
        }
    }

    [HttpPost("set")]
    public async Task<ActionResult<Diario>> SetDiario(Diario diario)
    {
        var diarioAnterior = _ctx.Diarios.FirstOrDefault(d => d == diario);
        foreach (var r in diario.Refeicoes)
        {
            var refeicaoAnterior = _ctx.Refeicoes.FirstOrDefault(refe => refe.refeicaoID == r.refeicaoID);
            if (refeicaoAnterior == null)
            {
                _ctx.Refeicoes.Add(r);
                foreach (var p in r.Porcoes)
                    _ctx.Porcoes.Add(p);
            }
            else
            {
                _ctx.Entry(refeicaoAnterior).CurrentValues.SetValues(r);
                foreach (var p in r.Porcoes)
                {
                    var porcaoAnterior = _ctx.Porcoes.FirstOrDefault(po => po.porcaoID == p.porcaoID);
                    if (porcaoAnterior == null)
                    {
                        _ctx.Porcoes.Add(p);
                    } 
                    else
                        _ctx.Entry(porcaoAnterior).CurrentValues.SetValues(p);
                }
            }
        }
        _ctx.Diarios.Entry(diarioAnterior).CurrentValues.SetValues(diario);
        await _ctx.SaveChangesAsync();
        return Ok(diarioAnterior);
    }

    [HttpGet("data/{data}")]
    public async Task<ActionResult<Diario>> GetDiarioPorData(DateTime data)
    {
        var idUsuarioRequest = HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var diarioPorData = await _ctx.Diarios.FirstOrDefaultAsync(d => d.usuarioID == idUsuarioRequest && d.data.Date == data.Date);
        if (diarioPorData != null)
            return Ok(diarioPorData);
        else
            return NotFound();
    }
}
