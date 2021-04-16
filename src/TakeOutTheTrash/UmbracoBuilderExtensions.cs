using Microsoft.Extensions.DependencyInjection;
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
            builder.Services.AddHostedService<CleanRoom>();

            return builder;
        } 
    }
}
