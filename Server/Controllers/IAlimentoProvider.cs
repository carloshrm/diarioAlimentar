namespace diarioAlimentar.Server.Controllers
{
    public interface IAlimentoProvider
    {
        Alimento? GetAlimentoPorID(int id);
        IEnumerable<Alimento> GetTodosAlimentos();
        IEnumerable<Alimento> BuscarAlimentosPorNome(string nome);
    }
}