using System.ComponentModel.DataAnnotations;

namespace diarioAlimentar.Shared;

public enum Sexo
{
    [Display(Name = "Masculino")]
    masculino = 0,

    [Display(Name = "Feminino")]
    feminino = 1,
}
