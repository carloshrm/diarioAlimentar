using diarioAlimentar.Shared;

using System.Net.Http.Json;

namespace diarioAlimentar.Client.Services
{
    public class PorcaoService
    {
        private readonly HttpClient _http;
        public PorcaoService(HttpClient client)
        {
            _http = client;
        }

        public async Task RemovePorcao(Porcao p)
        {
            var porcaoRequest = await _http.GetAsync($"diario/del/{p.porcaoID}");
        }
        public async Task UpdatePorcao(Porcao p)
        {
            var porcaoRequest = await _http.PostAsJsonAsync($"/diario/set", p);
        }
    }
}
