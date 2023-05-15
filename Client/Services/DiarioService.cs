using System.Net.Http.Json;

using diarioAlimentar.Shared;

namespace diarioAlimentar.Client.Services
{
    public class DiarioService
    {
        private readonly HttpClient _http;

        public DiarioService(HttpClient client)
        {
            _http = client;
        }

        public async Task<string?> GetDiarioHoje()
        {
            var diarioRequest = await _http.GetAsync($"/diario/hoje");
            //if (diarioRequest.IsSuccessStatusCode)
                return "eita";
            //if (diarioRequest != null)
            //    return diarioRequest;
            //else
            //    return null;
        }

        public async Task<Diario?> GetDiarioPorData(DateTime data)
        {

            return null;
        }

        public async Task<Diario> SetDiario()
        {
            var diarioRequest = await _http.GetAsync($"/diario/teste");
            return null;
        }
    }
}
