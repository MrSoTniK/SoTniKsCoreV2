using Core.Infrastructure.LifetimeScopes;
using VContainer;

namespace Gameplay.LifetimeScopes.MainMenu 
{
    public class MainMenuLifetimeScope : ContextLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
        }
    }
}