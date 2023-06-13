using Microsoft.JSInterop;

namespace diarioAlimentar.Client.Services;

public static class FileHelper
{
    public static ValueTask<object> SalvarComo(this IJSRuntime js, string filename, byte[] data)
       => js.InvokeAsync<object>(
           "SalvarComo",
           filename,
           Convert.ToBase64String(data));
}