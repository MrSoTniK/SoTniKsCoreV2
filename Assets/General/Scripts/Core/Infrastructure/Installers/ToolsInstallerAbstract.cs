using VContainer;

namespace Core.Infrastructure.Installers
{
    public abstract class ToolsInstallerAbstract : MonoInstallerAbstract
    {
        public override void RegisterBindings(IContainerBuilder builder)
        {
            RegisterTools(builder);
        }

        protected abstract void RegisterTools(IContainerBuilder builder);
    }
}