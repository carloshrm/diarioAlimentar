using diarioAlimentar.Shared;

using System.Net.Http.Json;

namespace diarioAlimentar.Client.Services;

public class RefeicaoService
{
    private readonly HttpClient _http;

    public RefeicaoService(HttpClient client) 
    {
        _http = client;
    }

    public async Task<bool> AddRefeicao(Refeicao r)
    {
        var refRequest = await _http.PostAsJsonAsync($"/refeicao/add", r);
        return refRequest.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateRefeicao(Refeicao r)
    {
        var refRequest = await _http.PostAsJsonAsync($"/refeicao/upd", r);
        return refRequest.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveRefeicao(Refeicao r)
    {
        var refRequest = await _http.GetAsync($"/refeicao/del/{r.refeicaoID}");
        return refRequest.IsSuccessStatusCode;
    }
}
