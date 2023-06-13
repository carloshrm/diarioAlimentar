using System.Net.Http.Json;
using diarioAlimentar.Shared;

namespace diarioAlimentar.Client.Services;

public class DiarioService
{
    private readonly HttpClient _http;

    public DiarioService(HttpClient client)
    {
        _http = client;
    }

    public async Task<Diario> GetDiarioHoje()
    {
        var diarioRequest = await _http.GetAsync($"/diario/hoje");
        if (diarioRequest.IsSuccessStatusCode)
        {
            var resultado = await diarioRequest.Content.ReadFromJsonAsync<Diario>();
            if (resultado != null)
                return resultado;
            else
                throw new NullReferenceException("erro diario service");
        }
        else
            throw new NullReferenceException("erro diario endpoint");
    }

    public async Task<Diario?> GetDiarioPorData(DateTime data)
    {
        var diarioRequest = await _http.GetAsync($"/diario/data/{data.ToString("yyyy-MM-dd")}");
        if (diarioRequest.IsSuccessStatusCode)
            return await diarioRequest.Content.ReadFromJsonAsync<Diario>();
        else
            return null;
    }

    public async Task<IEnumerable<Diario>> GetDiariosSemana()
    {
        var diarioRequest = await _http.GetAsync($"/diario/sem/");
        if (diarioRequest.IsSuccessStatusCode)
        {
            var resultado = await diarioRequest.Content.ReadFromJsonAsync<IEnumerable<Diario>>();
            if (resultado != null)
                return resultado;
            else
                throw new NullReferenceException("erro diario service");
        }
        else
            return new List<Diario>();
    }

    public async Task<Diario?> SetDiario(Diario diario)
    {
        var diarioRequest = await _http.PostAsJsonAsync($"/diario/set", diario);
        if (diarioRequest.IsSuccessStatusCode)
            return await diarioRequest.Content.ReadFromJsonAsync<Diario>();
        else
            return null;
    }

}
