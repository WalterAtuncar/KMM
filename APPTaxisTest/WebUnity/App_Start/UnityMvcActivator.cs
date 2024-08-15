using System;
using System.Linq;
using System.Web.Mvc;
using Unity.AspNet.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebUnity.UnityMvcActivator), nameof(WebUnity.UnityMvcActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(WebUnity.UnityMvcActivator), nameof(WebUnity.UnityMvcActivator.Shutdown))]

namespace WebUnity
{
    public static class UnityMvcActivator
    {
        public static void Start() 
        {
            var filterProviderToRemove = FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().FirstOrDefault();
            
            if (filterProviderToRemove != null)
            {
                FilterProviders.Providers.Remove(filterProviderToRemove);
            }

            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(UnityConfig.Container));

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}
