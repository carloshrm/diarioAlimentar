using System.Text.Json;
namespace diarioAlimentar.Server.Controllers;

public class AlimentosJSON : IAlimentoProvider
{
    private IReadOnlyDictionary<int, Alimento> alimentos;

    public AlimentosJSON()
    {
        var info = File.ReadAllText("./Data/Info_Alimentos.json");
        alimentos = JsonSerializer.Deserialize<Alimento[]>(info).Where(a => a != null).ToDictionary(a => a.AlimentoID, a => a);
    }

    public Alimento? GetAlimentoPorID(int id)
    {
        return alimentos.GetValueOrDefault(id);
    }

    public IEnumerable<Alimento> BuscarAlimentosPorNome(string nome)
    {
        return alimentos.Values.Where(a => a.nome.ToLower().Contains(nome));
    }

    public IEnumerable<Alimento> GetTodosAlimentos()
    {
        return alimentos.Values;
    }
}
