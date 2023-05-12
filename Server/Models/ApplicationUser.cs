using Microsoft.AspNetCore.Identity;

namespace diarioAlimentar.Server.Models;

public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public DateTime dataNascimento { get; set; }

    [PersonalData]
    public double peso { get; set; }

    [PersonalData]
    public double altura { get; set; }

    [PersonalData]
    public NivelAtividade nivelAtividade { get; set; }

    [PersonalData]
    public Sexo sexo { get; set; }
}