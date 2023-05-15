using IdentityModel;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace diarioAlimentar.Server.Models
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }
        protected async override Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            ClaimsIdentity claims = await base.GenerateClaimsAsync(user);
            claims.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.dataNascimento.ToString()));
            claims.AddClaim(new Claim("peso", user.peso.ToString()));
            claims.AddClaim(new Claim("altura", user.altura.ToString()));
            claims.AddClaim(new Claim("atividade", user.nivelAtividade.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Gender, user.sexo.ToString()));
            return claims;
        }
    }
}
