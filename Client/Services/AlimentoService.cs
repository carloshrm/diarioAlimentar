using System.Net.Http.Json;

using diarioAlimentar.Shared;

namespace diarioAlimentar.Client.Services
{
    public class AlimentoService
    {
        private readonly HttpClient _http;

        public AlimentoService(HttpClient client)
        {
            _http = client;
        }

        public async Task<Alimento> GetAlimento(int id)
        {
            var al = await _http.GetFromJsonAsync<Alimento>($"/Alimento/${id}");
            if (al != null)
                return al;
            else
                throw new Exception("Alimento não encontrado");
        }
    }
}
