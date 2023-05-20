using System.Security.Cryptography;

using diarioAlimentar.Server.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace diarioAlimentar.Server.Controllers
{
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

        [HttpPost("update")]
        public async Task<ActionResult<Porcao>> UpdatePorcao(Porcao p)
        {
            var porcaoDB = _ctx.Porcoes.First(r => r.porcaoID == p.porcaoID);
            _ctx.Porcoes.Update(porcaoDB);
            await _ctx.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("del/{pID}")]
        public async Task<ActionResult<Diario>> RemovePorcao(Guid pID)
        {
            var porcaoDB = _ctx.Porcoes.First(r => r.porcaoID == pID);
            _ctx.Porcoes.Remove(porcaoDB);
            await _ctx.SaveChangesAsync();
            return Ok();
        }
    }
}
