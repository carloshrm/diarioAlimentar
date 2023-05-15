using diarioAlimentar.Server.Data;
using diarioAlimentar.Server.Models;

using Duende.IdentityServer.Extensions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace diarioAlimentar.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DiarioController : Controller
{
    private readonly ApplicationDbContext _ctx;
    private UserManager<ApplicationUser> _userManager;

    public DiarioController(ApplicationDbContext diarioContext)
    {
        _ctx = diarioContext;
    }

    [HttpGet("hoje")]
    public async Task<ActionResult<Diario>> GetDiarioHoje()
    {
        var diarioHoje = await _ctx.Diarios.FirstOrDefaultAsync(d => d.usuarioID == HttpContext.User.GetSubjectId());
        if (diarioHoje == null)
        {
            return NotFound();
        }
        return Ok(diarioHoje);
    }

    [HttpGet("teste")]
    public async Task<ActionResult<Diario>> SetDiario()
    {
        var usuario = HttpContext.User;
        if (usuario == null)
            return BadRequest();
        else
            _ctx.Diarios.Add(new Diario(usuario.GetSubjectId()));
        return null;
    }
}
