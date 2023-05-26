using System.ComponentModel.DataAnnotations;

namespace diarioAlimentar.Shared;

public enum Periodo
{
    [Display(Name = "Café da Manhã")]
    Manha = 0,

    [Display(Name = "Almoço")]
    Almoco = 1,

    [Display(Name = "Jantar")]
    Janta = 2,

    [Display(Name = "Lanche")]
    Lanche = 3
};