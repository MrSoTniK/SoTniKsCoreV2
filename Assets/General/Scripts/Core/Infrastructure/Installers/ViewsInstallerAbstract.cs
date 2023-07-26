using VContainer;

namespace Core.Infrastructure.Installers 
{
    public abstract class ViewsInstallerAbstract : MonoInstallerAbstract
    {
        public override void RegisterBindings(IContainerBuilder builder)
        {
            RegisterViews(builder);
        }

        protected abstract void RegisterViews(IContainerBuilder builder);
    }
}