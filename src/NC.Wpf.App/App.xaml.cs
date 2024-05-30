using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using NC.Wpf.App.Views;
using NC.Wpf.Core.Ioc;
using NC.Wpf.Core.Navigation.Regions;
using NC.Wpf.Framework.Navigation.Regions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using Serilog.Events;
using Serilog;
using Volo.Abp;
using NC.Wpf.Framework.Extensions;

namespace NC.Wpf.App
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application, ISingletonDependency
    {
        private IAbpApplicationWithInternalServiceProvider? _abpApplication;

        protected override async void OnStartup(StartupEventArgs e)
        {
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt", rollingInterval: RollingInterval.Day))
                .CreateLogger();

            try
            {
                

                Log.Information("Starting WPF host.");

                _abpApplication = await AbpApplicationFactory.CreateAsync<NCWpfAppModule>(options =>
                {
                    options.UseAutofac();
                    options.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
                    options.Services.AddRegion().AddDialog().AddMvvm();
                });

                // 设置默认语言
                var configuration = _abpApplication.Services.GetConfiguration();
                var culture = configuration["Culture"] ?? "zh-CN";
                var cultureInfo = new CultureInfo(culture);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;

                await _abpApplication.InitializeAsync();
                
                // 窗口化应用需要在 STA 模式下运行，设置当前
                //if (!Thread.CurrentThread.TrySetApartmentState(ApartmentState.STA))
                //{
                //    Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
                //    Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
                //}

                // 入口窗体
                var entryWindow = _abpApplication.Services.GetRequiredService<LoginWindow>();
                entryWindow.Show();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            if (_abpApplication != null)
            {
                await _abpApplication.ShutdownAsync();
            }
            Log.CloseAndFlush();
        }
    }

    #region Program.cs 启动方式
    ///// <summary>
    ///// Interaction logic for App.xaml
    ///// </summary>
    //public partial class App : System.Windows.Application, ISingletonDependency
    //{
    //    //IContainerExtension _containerExtension;
    //    //IModuleCatalog _moduleCatalog;

    //    private readonly IServiceProvider _serviceProvider;

    //    public App()
    //    {
    //        _serviceProvider = ContainerLocator.Current;

    //        // 加载 App.xaml 中的资源
    //        // 需要在Application实例化时载入，否则 StaticResources 无法解析
    //        LoadComponent(this, new Uri("App.xaml", UriKind.Relative));
    //    }

    //    protected override void OnStartup(StartupEventArgs e)
    //    {
    //        base.OnStartup(e);
    //        InitializeInternal();
    //    }

    //    /// <summary>
    //    /// Run the initialization process.
    //    /// </summary>
    //    void InitializeInternal()
    //    {
    //        //ConfigureViewModelLocator();
    //        Initialize();
    //        //OnInitialized();
    //    }

    //    ///// <summary>
    //    ///// Configures the <see cref="Prism.Mvvm.ViewModelLocator"/> used by Prism.
    //    ///// </summary>
    //    //protected virtual void ConfigureViewModelLocator()
    //    //{
    //    //    PrismInitializationExtensions.ConfigureViewModelLocator();
    //    //}

    //    /// <summary>
    //    /// Runs the initialization sequence to configure the Prism application.
    //    /// </summary>
    //    protected virtual void Initialize()
    //    {
    //        //ContainerLocator.SetContainerExtension(CreateContainerExtension);
    //        //_containerExtension = ContainerLocator.Current;
    //        //_moduleCatalog = CreateModuleCatalog();
    //        //RegisterRequiredTypes(_containerExtension);
    //        //RegisterTypes(_containerExtension);
    //        //_containerExtension.FinalizeExtension();

    //        //ConfigureModuleCatalog(_moduleCatalog);

    //        //var regionAdapterMappings = _containerExtension.Resolve<RegionAdapterMappings>();
    //        //var regionAdapterMappings = _serviceProvider.GetService<RegionAdapterMappings>();
    //        //ConfigureRegionAdapterMappings(regionAdapterMappings);

    //        //var defaultRegionBehaviors = _containerExtension.Resolve<IRegionBehaviorFactory>();
    //        //var defaultRegionBehaviors = _serviceProvider.GetService<IRegionBehaviorFactory>();
    //        //ConfigureDefaultRegionBehaviors(defaultRegionBehaviors);

    //        //RegisterFrameworkExceptionTypes();

    //        var shell = CreateShell();
    //        if (shell != null)
    //        {
    //            //MvvmHelpers.AutowireViewModel(shell);
    //            RegionManager.SetRegionManager(shell, _serviceProvider.GetService<IRegionManager>());
    //            //RegionManager.SetRegionManager(shell, _containerExtension.Resolve<IRegionManager>());
    //            RegionManager.UpdateRegions();
    //            //InitializeShell(shell);
    //        }

    //        //InitializeModules();
    //    }

    //    ///// <summary>
    //    ///// Creates the container used by Prism.
    //    ///// </summary>
    //    ///// <returns>The container</returns>
    //    //protected abstract IContainerExtension CreateContainerExtension();

    //    ///// <summary>
    //    ///// Creates the <see cref="IModuleCatalog"/> used by Prism.
    //    ///// </summary>
    //    /////  <remarks>
    //    ///// The base implementation returns a new ModuleCatalog.
    //    ///// </remarks>
    //    //protected virtual IModuleCatalog CreateModuleCatalog()
    //    //{
    //    //    return new ModuleCatalog();
    //    //}

    //    ///// <summary>
    //    ///// Registers all types that are required by Prism to function with the container.
    //    ///// </summary>
    //    ///// <param name="containerRegistry"></param>
    //    //protected virtual void RegisterRequiredTypes(IContainerRegistry containerRegistry)
    //    //{
    //    //    containerRegistry.RegisterRequiredTypes(_moduleCatalog);
    //    //}

    //    ///// <summary>
    //    ///// Used to register types with the container that will be used by your application.
    //    ///// </summary>
    //    //protected abstract void RegisterTypes(IContainerRegistry containerRegistry);

    //    ///// <summary>
    //    ///// Configures the <see cref="IRegionBehaviorFactory"/>. 
    //    ///// This will be the list of default behaviors that will be added to a region. 
    //    ///// </summary>
    //    //protected virtual void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
    //    //{
    //    //    regionBehaviors?.RegisterDefaultRegionBehaviors();
    //    //}

    //    ///// <summary>
    //    ///// Configures the default region adapter mappings to use in the application, in order
    //    ///// to adapt UI controls defined in XAML to use a region and register it automatically.
    //    ///// May be overwritten in a derived class to add specific mappings required by the application.
    //    ///// </summary>
    //    ///// <returns>The <see cref="RegionAdapterMappings"/> instance containing all the mappings.</returns>
    //    //protected virtual void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
    //    //{
    //    //    regionAdapterMappings?.RegisterDefaultRegionAdapterMappings();
    //    //}

    //    /// <summary>
    //    /// Registers the <see cref="Type"/>s of the Exceptions that are not considered 
    //    /// root exceptions by the <see cref="ExceptionExtensions"/>.
    //    /// </summary>
    //    protected virtual void RegisterFrameworkExceptionTypes()
    //    {
    //    }

    //    /// <summary>
    //    /// Creates the shell or main window of the application.
    //    /// </summary>
    //    /// <returns>The shell of the application.</returns>
    //    //protected abstract Window CreateShell();
    //    protected Window CreateShell()
    //    {
    //        return _serviceProvider.GetService<MainWindow>();
    //    }

    //    ///// <summary>
    //    ///// Initializes the shell.
    //    ///// </summary>
    //    //protected virtual void InitializeShell(Window shell)
    //    //{
    //    //    MainWindow = shell;
    //    //}

    //    ///// <summary>
    //    ///// Contains actions that should occur last.
    //    ///// </summary>
    //    //protected virtual void OnInitialized()
    //    //{
    //    //    MainWindow?.Show();
    //    //}

    //    ///// <summary>
    //    ///// Configures the <see cref="IModuleCatalog"/> used by Prism.
    //    ///// </summary>
    //    //protected virtual void ConfigureModuleCatalog(IModuleCatalog moduleCatalog) { }

    //    ///// <summary>
    //    ///// Initializes the modules.
    //    ///// </summary>
    //    //protected virtual void InitializeModules()
    //    //{
    //    //    PrismInitializationExtensions.RunModuleManager(Container);
    //    //}
    //}
    #endregion
}
