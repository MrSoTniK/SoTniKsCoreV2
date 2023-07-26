using VContainer;
using Core.Infrastructure.LifetimeScopes;

namespace Gameplay.LifetimeScopes.Bootstrap
{
    public class ProjectLifetimeScope : ContextLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
        }
    }
}