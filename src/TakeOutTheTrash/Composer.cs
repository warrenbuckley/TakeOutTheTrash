using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace TakeOutTheTrash
{
    public class Composer : IUserComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            // OLD WAY
            // composition.Components().Append<Component>();

            // NEW WAY - COMPONENTS ARE BAD SUPPOSEDLY?!
            //builder.Components().Append<Component>();


            // This can be added explicitly to the startup class as well
            // Not sure I understand why you would do that ?!
            builder.AddTakeOutTheTrashPackage();
        }
    }
}
