using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Volo.Abp;
using NC.Wpf.App.Views;
using NC.Wpf.Framework.Extensions;

namespace NC.Wpf.App
{
    static class Program
    {
        private static IHost? _host;
        private static IAbpApplicationWithExternalServiceProvider? _abpApplication;

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

                //builder.Configuration.AddAppSettingsSecretsJson();
                //builder.Logging.ClearProviders().AddSerilog();
                //builder.ConfigureContainer(builder.Services.AddAutofacServiceProviderFactory());
                //builder.Services.AddHostedService<NCWpfAppHostedServcie<App, MainWindow>>();

                _abpApplication = await builder.Services.AddApplicationAsync<NCWpfAppModule>(options =>
                {
                    options.UseAutofac();
                    options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
                    //options.Services.AddHostedService<NCWpfAppHostedServcie<App, MainWindow>>();
                    options.Services.AddHostedService<NCWpfAppHostedServcie<App, LoginWindow>>();
                    options.Services.AddRegion()
                                    .AddMvvm();
                });

                //var hashCode = _abpApplication.ServiceProvider?.GetHashCode();

                _host = builder.Build()
                               .UseRegion();

                await _host.InitializeAsync();

                await _host.RunAsync();
                //host.Run();

                return 0;

                //Abp
                //Log.Information("Starting WPF host.");
                //var builder = WebApplication.CreateBuilder(args);
                //builder.Host.AddAppSettingsSecretsJson()
                //    .UseAutofac()
                //    .UseSerilog();
                //await builder.AddApplicationAsync<MyProjectNameHttpApiHostModule>();
                //var app = builder.Build();
                //await app.InitializeApplicationAsync();
                //await app.RunAsync();
                //return 0;
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
