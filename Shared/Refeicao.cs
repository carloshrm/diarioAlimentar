using System.ComponentModel.DataAnnotations;

namespace diarioAlimentar.Shared;

public class Refeicao
{
    [Key]
    public Guid refeicaoID { get; set; } = Guid.NewGuid();

    public ICollection<Porcao> Porcoes { get; set; } = new List<Porcao>();
    public Periodo periodo { get; set; }

    public Guid diarioID { get; set; }

    public Refeicao()
    {

    }

    public void AdicionarPorcao(Porcao porcao)
    {
        porcao.refeicaoID = refeicaoID;
        Porcoes.Add(porcao);
    }

    public InfoNutricional GerarRelatorio()
    {
        var resultado = new InfoNutricional();
        foreach (var alm in Porcoes)
            resultado += alm.Alimento.informacao * alm.quantidade;
        return resultado;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Refeicao)) 
            return false;
        else
            return refeicaoID == ((Refeicao)obj).refeicaoID;
    }
}
