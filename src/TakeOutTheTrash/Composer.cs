using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace TakeOutTheTrash
{
    public class Composer : IUserComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            // Composers are Umbraco specific magic
            // Useful for packages that use Umbraco ZIPs & want to auto register
            // If this was just a Nuget package then the user would add AddTakeOutTheTrashPackage() in Startup.cs
            // to the ConfigureServices() method

            // We are adding the package & setting it to read from the configuration providers
            // So that AppSettings.json or ENV Variables with 'TakeOutTheTrash' section key

            builder.Services.AddTakeOutTheTrashPackage(builder.Config);
        }
    }
}
