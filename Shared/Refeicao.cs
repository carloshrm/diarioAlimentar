namespace diarioAlimentar.Shared;

public class Refeicao
{
    public Guid refeicaoID { get; set; }
    public ICollection<Porcao> alimentos { get; set; } = new List<Porcao>();
    public Periodo periodo { get; set; }

    public Refeicao()
    {

    }

    public Refeicao(Periodo periodo)
    {
        this.periodo = periodo;
    }

    public InfoNutricional GerarRelatorio()
    {
        var resultado = new InfoNutricional();
        foreach (var alm in alimentos)
            resultado += alm.alimento.informacao * alm.quantidade;
        return resultado;
    }
}
