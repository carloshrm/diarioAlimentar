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

        public async Task<Alimento?> GetPorID(int id)
        {
            var alimentoRequest = await _http.GetFromJsonAsync<Alimento>($"/alimento/id/{id}");
            if (alimentoRequest != null)
                return alimentoRequest;
            else
                return null;
        }

        public async Task<ICollection<Alimento>?> GetPorNome(string nome)
        {
            var alimentoRequest = await _http.GetFromJsonAsync<ICollection<Alimento>>($"/alimento/nome/{nome}");
            if (alimentoRequest != null)
                return alimentoRequest;
            else
                return null;
        }
    }
}
