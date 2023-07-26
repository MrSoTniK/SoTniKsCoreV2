using VContainer;

namespace Core.Infrastructure.Installers 
{
    public abstract class DataBasesInstallerAbstract : MonoInstallerAbstract
    {
        public override void RegisterBindings(IContainerBuilder builder)
        {
            RegisterDataBases(builder);
        }

        protected abstract void RegisterDataBases(IContainerBuilder builder);
    }
}