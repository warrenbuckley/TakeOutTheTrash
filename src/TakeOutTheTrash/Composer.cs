using Umbraco.Core;
using Umbraco.Core.Composing;

namespace TakeOutTheTrash
{
    [RuntimeLevel(MinLevel = RuntimeLevel.Run)]
    public class Composer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Components().Append<Component>();
        }
    }
}
