using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text;

namespace diarioAlimentar.Shared;

public class Refeicao
{
    [Key]
    public Guid refeicaoID { get; set; } = Guid.NewGuid();

    public ICollection<Porcao> porcoes { get; set; } = new List<Porcao>();
    public Periodo periodo { get; set; }
    public TimeOnly horario { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

    public Guid diarioID { get; set; }

    public Refeicao()
    {

    }

    public void AdicionarPorcao(Porcao porcao)
    {
        porcao.refeicaoID = refeicaoID;
        porcoes.Add(porcao);
    }

    public InfoNutricional GerarRelatorio()
    {
        var resultado = new InfoNutricional();
        foreach (var alm in porcoes)
            resultado += alm.alimento.informacao * alm.quantidade;
        return resultado;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Refeicao)) 
            return false;
        else
            return refeicaoID == ((Refeicao)obj).refeicaoID;
    }

    public override string ToString()
    {
        if (porcoes.Any())
            return $"\n {periodo}, {horario.ToShortTimeString()} :" + string.Join("", porcoes);
        else
            return "";
    }
}
