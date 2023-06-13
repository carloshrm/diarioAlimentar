using diarioAlimentar.Shared;

using Newtonsoft.Json;

using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace diarioAlimentar.Client.Services
{
    public class TradutorService
    {
        private readonly HttpClient _http;
        private string key;
        private static readonly string endpoint = "https://api.cognitive.microsofttranslator.com";

        private string routeEn = "/translate?api-version=3.0&from=en&to=pt-br";
        private string routePt = "/translate?api-version=3.0&from=pt-br&to=en";

        public TradutorService(HttpClient client, IConfiguration config)
        {
            _http = client;
            key = config["tlkey"];
            if (key.Length == 0)
            {
                throw new InvalidOperationException("tl key");
            }
        }

        public async Task<string> EnParaPt(string t)
        {
            object[] body = new object[] { new { Text = t } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var req = new HttpRequestMessage())
            {
                req.Method = HttpMethod.Post;
                req.RequestUri = new Uri(endpoint + routeEn);
                req.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                req.Headers.Add("Ocp-Apim-Subscription-Key", key);

                HttpResponseMessage response = await _http.SendAsync(req).ConfigureAwait(false);

                string resultado = await response.Content.ReadAsStringAsync();
                var respModel = new[] { new { translations = new[] { new { text = "", to = "" } } } };
                var json = JsonConvert.DeserializeAnonymousType(resultado, respModel);
                return json.First().translations.First().text;
            }
        }

        public async Task<string> PtParaEn(string t)
        {
            object[] body = new object[] { new { Text = t } };
            var requestBody = JsonConvert.SerializeObject(body);

            using (var req = new HttpRequestMessage())
            {
                req.Method = HttpMethod.Post;
                req.RequestUri = new Uri(endpoint + routePt);
                req.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                req.Headers.Add("Ocp-Apim-Subscription-Key", key);

                HttpResponseMessage response = await _http.SendAsync(req).ConfigureAwait(false);

                string resultado = await response.Content.ReadAsStringAsync();
                var respModel = new[] { new { translations = new[] { new { text = "", to = "" } } } };
                var json = JsonConvert.DeserializeAnonymousType(resultado, respModel);
                return json.First().translations.First().text;
            }
        }
    }
}