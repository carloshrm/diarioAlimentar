using System.ComponentModel.DataAnnotations;

namespace diarioAlimentar.Shared;

public class Refeicao
{
    [Key]
    public Guid refeicaoID { get; set; } = Guid.NewGuid();

    public IList<Porcao> alimentos { get; set; } = new List<Porcao>();
    public Periodo periodo { get; set; }

    public Refeicao()
    {

    }

    public InfoNutricional GerarRelatorio()
    {
        var resultado = new InfoNutricional();
        foreach (var alm in alimentos)
            resultado += alm.alimento.informacao * alm.quantidade;
        return resultado;
    }
}
