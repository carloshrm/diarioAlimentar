using diarioAlimentar.Client.Services;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace diarioAlimentar.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddHttpClient("diarioAlimentar.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("diarioAlimentar.ServerAPI"));

            builder.Services.AddScoped<AlimentoService>();

            builder.Services.AddTransient<TradutorService>();
            builder.Services.AddTransient<ReceitaService>();

            builder.Services.AddScoped<DiarioService>();
            builder.Services.AddScoped<PorcaoService>();
            builder.Services.AddScoped<RefeicaoService>();
            builder.Services.AddApiAuthorization();
            await builder.Build().RunAsync();
        }
    }
}