using diarioAlimentar.Shared;

using System.Net.Http.Json;

namespace diarioAlimentar.Client.Services
{
    public class RefeicaoService
    {
        private readonly HttpClient _http;

        public RefeicaoService(HttpClient client) 
        {
            _http = client;
        }

        public async Task AddRefeicao(Refeicao r)
        {
            var refRequest = await _http.PostAsJsonAsync($"/refeicao/add", r);
        }

        public async Task UpdateRefeicao(Refeicao r)
        {
            var refRequest = await _http.PostAsJsonAsync($"/refeicao/upd", r);
        }

        public async Task<bool> RemoveRefeicao(Refeicao r)
        {
            var refRequest = await _http.GetAsync($"/refeicao/del/{r.refeicaoID}");
            return refRequest.IsSuccessStatusCode;
        }
    }
}
