using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NC.Wpf.Core.Navigation.Regions;
using NC.Wpf.Core.Ioc;
using NC.Wpf.Framework.Navigation.Regions;
using NC.Wpf.Framework.Navigation.Regions.Behaviors;
using NC.Wpf.Core.Mvvm;
using Volo.Abp;
using System.Windows;
using NC.Wpf.Core.Dialogs;
using NC.Wpf.Framework.Dialogs;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace NC.Wpf.Framework.Extensions
{
    public static class ServiceCollectionExtensions
    {

        /// <summary>
        /// 注册 Region 服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRegion(this IServiceCollection services)
        {
            services.AddSingleton<RegionAdapterMappings>();
            services.AddScoped<SelectorRegionAdapter>();
            services.AddScoped<ItemsControlRegionAdapter>();
            services.AddScoped<ContentControlRegionAdapter>();

            services.AddSingleton<IRegionManager, RegionManager>();
            services.AddSingleton<IRegionNavigationContentLoader, RegionNavigationContentLoader>();
            services.AddSingleton<IRegionViewRegistry, RegionViewRegistry>();
            services.AddSingleton<IRegionBehaviorFactory, RegionBehaviorFactory>();
            services.AddSingleton<IRegionNavigationJournalEntry, RegionNavigationJournalEntry>();
            services.AddSingleton<IRegionNavigationJournal, RegionNavigationJournal>();
            services.AddSingleton<IRegionNavigationService, RegionNavigationService>();

            services.AddTransient<DelayedRegionCreationBehavior>();

            services.AddTransient<BindRegionContextToDependencyObjectBehavior>();
            services.AddTransient<RegionActiveAwareBehavior>();
            services.AddTransient<SyncRegionContextWithHostBehavior>();
            services.AddTransient<RegionManagerRegistrationBehavior>();
            services.AddTransient<RegionMemberLifetimeBehavior>();
            services.AddTransient<ClearChildViewsRegionBehavior>();
            services.AddTransient<AutoPopulateRegionBehavior>();
            services.AddTransient<DestructibleRegionBehavior>();
            return services;
        }

        /// <summary>
        /// 注册 Dialog 服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDialog(this IServiceCollection services)
        {
            services.AddSingleton<IDialogService, DialogService>();
            services.AddTransient<IDialogWindow, DialogWindow>(); //default dialog host

            return services;
        }

        /// <summary>
        /// 配置 Region 服务
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost UseRegion(this IHost host)
        {
            ContainerLocator.SetContainerExtension(() => host.Services);

            var regionAdapterMappings = host.Services.GetService<RegionAdapterMappings>();
            regionAdapterMappings?.RegisterMapping<Selector, SelectorRegionAdapter>();
            regionAdapterMappings?.RegisterMapping<ItemsControl, ItemsControlRegionAdapter>();
            regionAdapterMappings?.RegisterMapping<ContentControl, ContentControlRegionAdapter>();

            var regionBehaviors = host.Services.GetService<IRegionBehaviorFactory>();
            regionBehaviors?.AddIfMissing<BindRegionContextToDependencyObjectBehavior>(BindRegionContextToDependencyObjectBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<RegionActiveAwareBehavior>(RegionActiveAwareBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<SyncRegionContextWithHostBehavior>(SyncRegionContextWithHostBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<RegionManagerRegistrationBehavior>(RegionManagerRegistrationBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<RegionMemberLifetimeBehavior>(RegionMemberLifetimeBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<ClearChildViewsRegionBehavior>(ClearChildViewsRegionBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<AutoPopulateRegionBehavior>(AutoPopulateRegionBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<DestructibleRegionBehavior>(DestructibleRegionBehavior.BehaviorKey);

            // App.xaml.cs 中执行Region注册
            //RegionManager.SetRegionManager(shell, host.Services.GetService<IRegionManager>());
            //RegionManager.UpdateRegions();

            return host;
        }

        /// <summary>
        /// 配置 Region 服务
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IServiceProvider UseRegion<TWindow>(this IServiceProvider serviceProvider) where TWindow : Window
        {
            ContainerLocator.SetContainerExtension(() => serviceProvider);

            var regionAdapterMappings = serviceProvider.GetService<RegionAdapterMappings>();
            regionAdapterMappings?.RegisterMapping<Selector, SelectorRegionAdapter>();
            regionAdapterMappings?.RegisterMapping<ItemsControl, ItemsControlRegionAdapter>();
            regionAdapterMappings?.RegisterMapping<ContentControl, ContentControlRegionAdapter>();

            var regionBehaviors = serviceProvider.GetService<IRegionBehaviorFactory>();
            regionBehaviors?.AddIfMissing<BindRegionContextToDependencyObjectBehavior>(BindRegionContextToDependencyObjectBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<RegionActiveAwareBehavior>(RegionActiveAwareBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<SyncRegionContextWithHostBehavior>(SyncRegionContextWithHostBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<RegionManagerRegistrationBehavior>(RegionManagerRegistrationBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<RegionMemberLifetimeBehavior>(RegionMemberLifetimeBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<ClearChildViewsRegionBehavior>(ClearChildViewsRegionBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<AutoPopulateRegionBehavior>(AutoPopulateRegionBehavior.BehaviorKey);
            regionBehaviors?.AddIfMissing<DestructibleRegionBehavior>(DestructibleRegionBehavior.BehaviorKey);

            #region Program.cs 启动入口,在 App.xaml.cs 中执行 以下 Region 注册
            var entryWindow = serviceProvider.GetRequiredService<TWindow>();
            RegionManager.SetRegionManager(entryWindow, serviceProvider.GetRequiredService<IRegionManager>());
            RegionManager.UpdateRegions();
            #endregion

            return serviceProvider;
        }

        /// <summary>
        /// 配置 MVVM 的视图模型提供程序
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMvvm(this IServiceCollection services)
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory((view, type) =>
            {
                return ContainerLocator.Container?.GetService(type);
            });
            return services;
        }
    }
}
