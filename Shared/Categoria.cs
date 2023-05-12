using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace diarioAlimentar.Shared;

public enum Categoria
{
    [Display(Name = "Cereais e derivados")]
    Cereais,

    [Display(Name = "Verduras, hortaliças e derivados")]
    Verduras,

    [Display(Name = "Frutas e derivados")]
    Frutas,

    [Display(Name = "Gorduras e óleos")]
    Gorduras,

    [Display(Name = "Pescados e frutos do mar")]
    Pescados,

    [Display(Name = "Carnes e derivados")]
    Carnes,

    [Display(Name = "Leite e derivados")]
    Leite,

    [Display(Name = "Bebidas (alcoólicas e não alcoólicas)")]
    Bebidas,

    [Display(Name = "Ovos e derivados")]
    Ovos,

    [Display(Name = "Produtos açucarados")]
    Acucarados,

    [Display(Name = "Miscelâneas")]
    Misc,

    [Display(Name = "Alimentos industrializados")]
    Industrializados,

    [Display(Name = "Alimentos preparados")]
    Preparados,

    [Display(Name = "Leguminosas e derivados")]
    Leguminosos,

    [Display(Name = "Nozes e sementes")]
    Nozes,
};

public static class CatHelper
{
    public static string GetNomeCategoria(this Categoria c)
    {
        return c.GetType().GetMember(c.ToString()).First().GetCustomAttribute<DisplayAttribute>()?.GetName();
    }
}