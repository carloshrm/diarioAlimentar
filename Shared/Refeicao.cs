namespace diarioAlimentar.Shared;

public class Refeicao
{
    public Guid refeicaoID { get; set; }
    public ICollection<Alimento> alimentos { get; set; }
    public Periodo periodo { get; set; }

    public Refeicao()
    {

    }

    public Refeicao(Periodo p)
    {
        periodo = p;
    }
}