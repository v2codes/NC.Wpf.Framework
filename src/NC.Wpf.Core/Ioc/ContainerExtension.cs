using Microsoft.Extensions.DependencyInjection;

namespace NC.Wpf.Core.Ioc
{
    public static class ContainerExtension
    {
        public static bool IsRegistered(this IServiceProvider serviceProvider, Type type)
        {
            return serviceProvider.GetService(type) == null ? false : true;
        }
        public static bool IsRegistered(this IServiceProvider serviceProvider, Type type, string name)
        {
            return serviceProvider.GetService(type) == null ? false : true;
        }

        public static Type GetRegistrationType(this IServiceProvider serviceProvider, Type type)
        {
            return serviceProvider.GetService(type).GetType();
        }

        public static Type? GetRegistrationType(this IServiceProvider serviceProvider, string name)
        {
            var obj = serviceProvider.GetServices(typeof(object)).FirstOrDefault(p => p.GetType().Name == name);
            if (obj == null)
            {
                obj = serviceProvider.GetKeyedServices(typeof(object), name).FirstOrDefault();
            }
            return obj?.GetType();
            //return serviceProvider?.GetServices<object>()?.FirstOrDefault(p => p.GetType().Name == name)?.GetType();
        }

        public static Type GetRegistrationType(this IServiceProvider serviceProvider, Type type, string name)
        {
            return serviceProvider.GetService(type).GetType();
        }

        public static object? GetService<T>(this IServiceProvider serviceProvider, string name)
        {
            var obj = serviceProvider.GetServices(typeof(T)).FirstOrDefault(p => p.GetType().Name == name);
            if (obj == null)
            {
                obj = serviceProvider.GetKeyedServices(typeof(T), name).FirstOrDefault();
            }
            return obj;
            //return serviceProvider.GetServices(typeof(T)).FirstOrDefault(p => p.GetType().Name == name)?.GetType();
        }
    }
}
