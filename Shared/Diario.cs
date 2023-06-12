using System.ComponentModel.DataAnnotations;

namespace diarioAlimentar.Shared;

public class Diario
{
    [Key]
    public Guid diarioID { get; set; } = Guid.NewGuid();

    public DateTime data { get; set; } = DateTime.Now.ToUniversalTime();
    public string usuarioID { get; set; }

    public ICollection<Refeicao> refeicoes { get; set; } = new List<Refeicao>();

    public Diario()
    {
        
    }

    public void AdicionarRefeicao(Refeicao refeicao)
    {
        refeicao.diarioID = diarioID;
        refeicoes.Add(refeicao);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Diario))
            return false;
        else
            return (diarioID == ((Diario)obj).diarioID) && (usuarioID == ((Diario)obj).usuarioID);
    }

    public override string ToString()
    {
        return $"Diário de {data.ToShortDateString()}:" + string.Join("", refeicoes);
    }
}
