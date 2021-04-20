using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Umbraco.Cms.Core.DependencyInjection;

namespace TakeOutTheTrash
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTakeOutTheTrashPackage(this IServiceCollection services)
        {
            // BackgroundTasks
            // RecurringHostedServiceBase Background Task stuff is added as HostedService
            // Always check to ensure the collection/services etc do not already contain
            // what you want to add, in case the .AddTakeOutTheTrashPackage() called from Startup as well
            // as the Composer appproach in the package auto-plumbing it

            var cleanRoomHostedService = new UniqueServiceDescriptor(typeof(IHostedService), typeof(CleanRoom), ServiceLifetime.Singleton);
            if (!services.Contains(cleanRoomHostedService))
            {
                services.Add(cleanRoomHostedService);
            }

            return services;
        }

        /// <summary>
        /// Use configuration such as AppSettings or Environment Variables etc
        /// </summary>
        public static IServiceCollection AddTakeOutTheTrashPackage(this IServiceCollection services, IConfiguration configuration)
        {
            if(configuration is not null)
            {
                services.Configure<TakeOutTheTrashSettings>(configuration.GetSection("TakeOutTheTrash"));
            }

            services.AddTakeOutTheTrashPackage();
            return services;
        }

        /// <summary>
        /// Configure options in code
        /// </summary>
        public static IServiceCollection AddTakeOutTheTrashPackage(this IServiceCollection services, Action<TakeOutTheTrashSettings> configure)
        {           

            // Add Options POCO so that the BackgroundTask
            // Can use/read values if user has configured it when calling AddTakeOutTheTrashPackage() extension method
            if(configure is not null)
            {
                services.Configure<TakeOutTheTrashSettings>(configure);
            }

            services.AddTakeOutTheTrashPackage();
            return services;
        } 
    }
}
