using VContainer;

namespace Core.Infrastructure.Installers
{
    public abstract class FactoriesInstallerAbstract : MonoInstallerAbstract
    {
        public override void RegisterBindings(IContainerBuilder builder)
        {
            RegisterFactories(builder);
        }

        protected abstract void RegisterFactories(IContainerBuilder builder);
    }
}