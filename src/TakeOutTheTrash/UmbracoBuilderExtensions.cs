using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Umbraco.Cms.Core.DependencyInjection;

namespace TakeOutTheTrash
{
    public static class UmbracoBuilderExtensions
    {
        public static IUmbracoBuilder AddTakeOutTheTrashPackage(this IUmbracoBuilder builder)
        {
            // Register any services or any notification handlers (events) here

            // EXAMPLE - Add to Umbraco collection of things
            // builder.Sections().Append<MyAwesomeSection>()
            // builder.Dashboards().Add<MyFabDashboard>()

            // EXAMPLE - Custom config 
            // builder.AddConfiguration

            // BackgroundTasks
            // RecurringHostedServiceBase Background Task stuff is added as HostedService

            // Always check to ensure the collection/services etc do not already contain
            // what you want to add, in case the .AddTakeOutTheTrashPackage() called from Startup as well
            // as the Composer appproach in the package auto-plumbing it
            if (builder.Services.Contains(ServiceDescriptor.Singleton<IHostedService, CleanRoom>()) == false)
            {
                builder.Services.AddHostedService<CleanRoom>();
            }

            return builder;
        } 
    }
}
