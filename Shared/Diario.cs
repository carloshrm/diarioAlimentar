namespace diarioAlimentar.Shared;

public class Diario
{
    public Guid diarioID { get; set; }
    public DateTime data { get; set; }
    public ICollection<Refeicao> refeicoes { get; set; }
    public string userID { get; set; }

    public Diario()
    {
        refeicoes = new List<Refeicao>();
        data = DateTime.Now;
    }
}
