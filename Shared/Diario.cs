namespace diarioAlimentar.Shared;

public class Diario
{
    public Guid diarioID { get; set; }
    public DateTime data { get; set; } = DateTime.Now.ToUniversalTime();
    public IList<Refeicao> refeicoes { get; set; } = new List<Refeicao>();
    public string usuarioID { get; set; }

    public Diario()
    {

    }

    public Diario(string usuarioID)
    {
        this.usuarioID = usuarioID;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != typeof(Diario))
            return false;
        else
            return diarioID == ((Diario)obj).diarioID;
    }
}
