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
    private readonly IAlimentoProvider _provider;

    public DiarioController(ApplicationDbContext diarioContext, IAlimentoProvider alimentoProvider)
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
                    .ForEach(p => p.alimento = _provider.GetAlimentoPorID(p.alimentoID));

            return Ok(diarioHoje);
        }
    }

    [HttpPost("set")]
    public async Task<ActionResult<Diario>> SetDiario(Diario diario)
    {
        var diarioAnterior = _ctx.Diarios.FirstOrDefault(d => d == diario);
        foreach (var r in diario.refeicoes)
        {
            var refeicaoAnterior = _ctx.Refeicoes.FirstOrDefault(refe => refe.refeicaoID == r.refeicaoID);
            if (refeicaoAnterior == null)
            {
                _ctx.Refeicoes.Add(r);
                foreach (var p in r.porcoes)
                    _ctx.Porcoes.Add(p);
            }
            else
            {
                _ctx.Entry(refeicaoAnterior).CurrentValues.SetValues(r);
                foreach (var p in r.porcoes)
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
    public async Task<ActionResult<Diario>> GetDiarioPorData(string data)
    {
        if (!DateTime.TryParse(data, out DateTime dataParse))
            return BadRequest(data);

        var idUsuarioRequest = HttpContext.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier).Value;


        var diarioPorData = await _ctx.Diarios.FirstOrDefaultAsync(d => d.usuarioID == idUsuarioRequest && d.data.Date == dataParse.ToUniversalTime().Date);
        if (diarioPorData != null)
        {
            PreencherDiario(diarioPorData);
            return Ok(diarioPorData);
        }
        else
        {
            var novoDiario = new Diario() { usuarioID = idUsuarioRequest };
            novoDiario.data = dataParse.ToUniversalTime();
            novoDiario.AdicionarRefeicao(new Refeicao());
            _ctx.Diarios.Add(novoDiario);
            await _ctx.SaveChangesAsync();
            return Ok(novoDiario);
        }
    }

    private void PreencherDiario(Diario d)
    {
        foreach (var dbRefeicao in 
            _ctx.Refeicoes
                .Where(r => r.diarioID == d.diarioID)
                .ToList())
        {
            _ctx.Porcoes
                .Where(p => p.refeicaoID == dbRefeicao.refeicaoID)
                .ToList()
                .ForEach(p => p.alimento = _provider.GetAlimentoPorID(p.alimentoID));
        }
    }

    [HttpGet("sem")]
    public async Task<ActionResult<IEnumerable<Diario>>> GetDiariosSemana()
    {
        var idUsuarioRequest = HttpContext.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var diariosDaSemana = new List<Diario>();
        for (int i = 0; i < 7; i++)
        {
            DateTime diasAnteriores = DateTime.Now.ToUniversalTime().Subtract(TimeSpan.FromDays(i));
            var diarioGravado = await _ctx.Diarios.FirstOrDefaultAsync(d => d.usuarioID == idUsuarioRequest && d.data.Date == diasAnteriores.ToUniversalTime().Date);
            if (diarioGravado == null)
            {
                var novoDiario = new Diario() { usuarioID = idUsuarioRequest };
                novoDiario.AdicionarRefeicao(new Refeicao());
                _ctx.Diarios.Add(novoDiario);
                diariosDaSemana.Add(novoDiario);
            }
            else
            {
                PreencherDiario(diarioGravado);
                diariosDaSemana.Add(diarioGravado);
            }            
        }
        await _ctx.SaveChangesAsync();
        return Ok(diariosDaSemana);
    }
}
