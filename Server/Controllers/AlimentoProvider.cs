using System.Text.Json;

namespace diarioAlimentar.Server.Controllers;

public class AlimentoProvider
{
    public IReadOnlyDictionary<int, Alimento> alimentos;

    public AlimentoProvider()
    {
        var info = File.ReadAllText("./Data/Info_Alimentos.json");
        alimentos = JsonSerializer.Deserialize<Alimento[]>(info).Where(a => a != null).ToDictionary(a => a.AlimentoID, a => a);
    }
}
