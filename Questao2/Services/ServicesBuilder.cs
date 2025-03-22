using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Questao2.Api;

namespace Questao2.Services
{
    public static class ServicesBuilder
    {
        private static IHost? _host;
        private static string? _baseAddress;

        public static void BuildHost(string baseAddress)
        {
            if (_host != null)
                return;

            if (string.IsNullOrWhiteSpace(baseAddress))
                throw new ArgumentException("É necessário informar a URL.");

            _baseAddress = baseAddress;

            _host = Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Warning);
                })
                .ConfigureServices(services =>
                {
                    services.AddHttpClient<IResultadoPartidasApi, ResultadoPartidasApi>(client =>
                    {
                        client.BaseAddress = new Uri(_baseAddress);
                    });
                })
                .Build();
        }

        public static T GetService<T>() where T : notnull
        {
            if (_host == null)
                throw new InvalidOperationException($"É necessário chamar o método {BuildHost} antes.");

            return _host.Services.GetRequiredService<T>();
        }
    }
}
