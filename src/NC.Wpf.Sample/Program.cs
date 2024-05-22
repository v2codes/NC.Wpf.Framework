using Microsoft.Extensions.Hosting;
using Serilog.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace NC.Wpf.Sample
{
    static class Program
    {
        private static IHost? _host;

        async static Task<int> Main(string[] args)
        {
            // 窗口化应用需要在 STA 模式下运行，设置当前
            if (!Thread.CurrentThread.TrySetApartmentState(ApartmentState.STA))
            {
                Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
                Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            }

            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt", rollingInterval: RollingInterval.Day))
                .CreateLogger();

            try
            {
                Log.Information("Starting WPF host.");
                var builder = Host.CreateApplicationBuilder(args);

                // console template
                builder.Configuration.AddAppSettingsSecretsJson();
                builder.Logging.ClearProviders().AddSerilog();
                builder.ConfigureContainer(builder.Services.AddAutofacServiceProviderFactory());
                builder.Services.AddHostedService<SampleHostedServcie<App, MainWindow>>();
                await builder.Services.AddApplicationAsync<SampleModule>();

                _host = builder.Build();
                await _host.InitializeAsync();
                await _host.RunAsync();

                return 0;

            }
            catch (Exception ex)
            {
                if (ex is HostAbortedException)
                {
                    throw;
                }

                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
