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

        public async Task<Porcao?> SetPorcao(Porcao p)
        {
            var porcaoRequest = await _http.PostAsJsonAsync("porcao/set", p);
            if (porcaoRequest.IsSuccessStatusCode)
                return await porcaoRequest.Content.ReadFromJsonAsync<Porcao>();
            else
                return null;
        }

        public async Task<bool> RemovePorcao(Porcao p)
        {
            var porcaoRequest = await _http.GetAsync($"porcao/del/{p.porcaoID}");
            return porcaoRequest.IsSuccessStatusCode;
        }
    }
}
